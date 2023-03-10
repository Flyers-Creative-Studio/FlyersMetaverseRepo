using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HelperUtil : MonoBehaviour
{
    #region Variables 
    public Material defaultObject;

    [SerializeField] private Gamein.GameLibrary _gameLibraryRef;
    public static Gamein.GameLibrary GameLibrary => instance?._gameLibraryRef;

    public static HelperUtil instance { get; private set; }
    [SerializeField] GameObject messagePopupPrefab;
    [SerializeField] GameObject detailedMessagePopupPrefab;
    [SerializeField] GameObject messagePopupPrefablandScap;
    private GameObject messagePopup;
    private GameObject detailedMessagePopup;
    [SerializeField] GameObject loadingScreenPrefab;
    [SerializeField] GameObject downloadLoadingScreenPrefab;
    public GameObject loadingScreen;
    public GameObject downloadLoadingScreen;
    public Text fpscounter;

    [Header("WorldData")]
    public string miniMapUrl;
    public string converImageURL;
    public string worldId;

    #region Interface Objects

    public IUniversalResponseHandler iUniversalResponseHandler;
    public IUniversalInteractionHandler iInteractionHandler;
    public IUniversalPlayerInteraction iPlayerInteractionHandler;
    public IUniversalUIHandler iUniversalUIHandler;
    public IUniversalLandHandler iuniversalLandHandler;

    #endregion

    #endregion

    #region UnityCallBacks

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
    private void Update()
    {
        //fpscounter.text = (1 / Time.unscaledDeltaTime).ToString("0");
    }
    #endregion

    #region Timer 
    ///set Timer for Two text
    public void CreateTimerTwoText(Text minutetext, Text secondtext, int TotalTimer, Action OnTimerEnd)
    {
        StartCoroutine(startTimer(minutetext, secondtext, TotalTimer, OnTimerEnd));
    }
    IEnumerator startTimer(Text minutetext, Text secondtext, float Totalseconds, Action OnTimerEnd)
    {
        do
        {
            float minutes = Mathf.FloorToInt(Totalseconds / 60);
            float seconds = Totalseconds % 60;
            string currentTime = string.Format("{00:00}{1:00}", minutes, seconds);
            minutetext.text = currentTime[0].ToString() + " : " + currentTime[1].ToString();
            secondtext.text = currentTime[2].ToString() + " : " + currentTime[3].ToString();
            Totalseconds--;
            yield return new WaitForSecondsRealtime(1f);
            if (Totalseconds == 0)
            {
                StopCoroutine(startTimer(minutetext, secondtext, Totalseconds, OnTimerEnd));
                OnTimerEnd();
            }
        } while (Totalseconds >= 0);

    }

    ///set Timer for single text
    public void CreateTimerSingleText(Text timerText, int TotalTimer, Action OnTimerEnd)
    {
        StartCoroutine(startTimersingleText(timerText, TotalTimer, OnTimerEnd));
    }
    IEnumerator startTimersingleText(Text timerText, int TotalTimer, Action OnTimerEnd)
    {
        do
        {
            float minutes = Mathf.FloorToInt(TotalTimer / 60);
            float seconds = TotalTimer % 60;
            string currentTime = string.Format("{00:00}{1:00}", minutes, seconds);
            timerText.text = currentTime[0].ToString() + " " + currentTime[1].ToString() + " : " + currentTime[2].ToString() + "  " + currentTime[3].ToString();
            TotalTimer--;
            yield return new WaitForSecondsRealtime(1f);
            if (TotalTimer == 0)
            {
                StopCoroutine(startTimersingleText(timerText, TotalTimer, OnTimerEnd));
                OnTimerEnd();
            }
        } while (TotalTimer >= 0);
    }

    public void StopTimer()
    {
        StopAllCoroutines();
    }

    #endregion

    #region CallBacksOfHelperUtil
    /// <summary>
    /// Helper method to start an Coroutine call.(action will be called after delay).
    /// </summary>
    public static void CallAfterDelay(Action action, float delay, Func<bool> cancelCondition = null)
    {
        float initialTime = Time.time;

        instance.StartCoroutine(enumerator());
        IEnumerator enumerator()
        {
            while (true)
            {
                //If cancel condition gets true, return control from this line.
                if (cancelCondition != null && cancelCondition()) yield break;
                //If delay is reached, break this loop.
                else if (Time.time > initialTime + delay) break;

                //Hold control for set amount of time to decrease CPU pressure.
                yield return new WaitForSeconds(0.02f);
            }

            //Execute the action if delay reached and cancel condition is still false.
            action();
        }
    }

    /// <summary>
    /// Helper method to start an Coroutine call.(action will be called after the condition gets true).
    /// </summary>
    public static void CallAfterCondition(Action action, Func<bool> condition, Func<bool> cancelCondition = null)
    {
        instance.StartCoroutine(enumerator());
        IEnumerator enumerator()
        {
            while (!condition())
            {
                //If cancel condition gets true, return control from this line.
                if (cancelCondition != null && cancelCondition()) yield break;

                yield return new WaitForSeconds(0.5f);
            }
            action();
        }
    }

    public static void ShowMessage(string message, MessageActionData firstAction = null, MessageActionData secondAction = null, bool withBackground = false, bool enableConvertor = false, bool isLandscape = false)
    {
        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            if (enableConvertor) message = GameMessage.MessageConvertor(message);

            if (!instance.messagePopup)
            {
                if (!isLandscape)
                {
                    instance.messagePopup = Instantiate(instance.messagePopupPrefab);
                    DontDestroyOnLoad(instance.messagePopup);
                }
                else
                {
                    instance.messagePopup = Instantiate(instance.messagePopupPrefablandScap);
                    DontDestroyOnLoad(instance.messagePopupPrefablandScap);
                }
            }
            Transform messageTextParent = instance.messagePopup.transform.GetChild(0).GetChild(0);
            Transform buttonHolder = messageTextParent.transform.Find("ButtonHolder");

            //Set popup background visibility.
            Image messageBackground = instance.messagePopup.transform.GetChild(0).GetComponent<Image>();
            Color32 oldColor = messageBackground.color;
            messageBackground.color = new Color32(oldColor.r, oldColor.g, oldColor.b, withBackground ? (byte)255 : (byte)0);

            //Fetching the buttons and remove existing actions if any..
            Button actionButton = buttonHolder.transform.Find("Button_1").GetComponent<Button>();
            Button secondaryActionButton = buttonHolder.transform.Find("Button_2").GetComponent<Button>();
            actionButton.onClick.RemoveAllListeners();
            secondaryActionButton.onClick.RemoveAllListeners();

            if (secondAction != null)
            {
                secondaryActionButton.gameObject.SetActive(true);

                //Adding close action to both buttons.
                actionButton.onClick.AddListener(() => { instance.messagePopup.SetActive(false); });
                actionButton.onClick.AddListener(() =>
                {
                    //if (AvatarHolderManager.instance.avatar != null) {
                    //    AvatarHolderManager.instance.avatar.transform.SetParent(AvatarHolderManager.instance.transform);
                    //    AvatarHolderManager.instance.avatar.transform.localPosition = new Vector3(0,-.11f,5.36f);
                    //    AvatarHolderManager.instance.avatar.transform.localEulerAngles = new Vector3(0,180,0);
                    //    AvatarHolderManager.instance.avatar.transform.localScale = new Vector3(1,1,1);                     

                    //}
                });
                secondaryActionButton.onClick.AddListener(() => { instance.messagePopup.SetActive(false); });

                //Adding additional actions, if any.
                if (firstAction != null && firstAction.action != null) actionButton.onClick.AddListener(firstAction.action);
                if (secondAction != null && secondAction.action != null) secondaryActionButton.onClick.AddListener(secondAction.action);

                //Setting button names, if any.
                if (firstAction != null && !string.IsNullOrEmpty(firstAction.buttonName)) actionButton.GetComponentInChildren<Text>().text = firstAction.buttonName;
                if (secondAction != null && !string.IsNullOrEmpty(secondAction.buttonName)) secondaryActionButton.GetComponentInChildren<Text>().text = secondAction.buttonName;
            }
            else
            {
                secondaryActionButton.gameObject.SetActive(false);
                if (firstAction != null && firstAction.action != null) actionButton.onClick.AddListener(firstAction.action);
                actionButton.onClick.AddListener(
                () =>
                {
                    instance.messagePopup.SetActive(false);
                });
                if (firstAction != null) actionButton.GetComponentInChildren<Text>().text = firstAction.buttonName;
                else actionButton.GetComponentInChildren<Text>().text = "Ok";
            }

            //Setting the message.
            messageTextParent.Find("MessageText").GetComponent<Text>().text = message;
            instance.messagePopup.SetActive(true);
            Debug.Log(instance.messagePopup.activeInHierarchy);
        }
        else
        {
            if (enableConvertor) message = GameMessage.MessageConvertor(message);

            if (!instance.messagePopup)
            {
                instance.messagePopup = Instantiate(instance.messagePopupPrefablandScap);
                DontDestroyOnLoad(instance.messagePopupPrefablandScap);
            }
            Transform messageTextParent = instance.messagePopup.transform.GetChild(0).GetChild(0);
            Transform buttonHolder = messageTextParent.transform.Find("ButtonHolder");

            //Set popup background visibility.
            Image messageBackground = instance.messagePopup.transform.GetChild(0).GetComponent<Image>();
            Color32 oldColor = messageBackground.color;
            messageBackground.color = new Color32(oldColor.r, oldColor.g, oldColor.b, withBackground ? (byte)255 : (byte)0);

            //Fetching the buttons and remove existing actions if any..
            Button actionButton = buttonHolder.transform.Find("Button_1").GetComponent<Button>();
            Button secondaryActionButton = buttonHolder.transform.Find("Button_2").GetComponent<Button>();
            actionButton.onClick.RemoveAllListeners();
            secondaryActionButton.onClick.RemoveAllListeners();

            if (secondAction != null)
            {
                secondaryActionButton.gameObject.SetActive(true);

                //Adding close action to both buttons.
                actionButton.onClick.AddListener(() => { instance.messagePopup.SetActive(false); });
                secondaryActionButton.onClick.AddListener(() => { instance.messagePopup.SetActive(false); });

                //Adding additional actions, if any.
                if (firstAction != null && firstAction.action != null) actionButton.onClick.AddListener(firstAction.action);
                if (secondAction != null && secondAction.action != null) secondaryActionButton.onClick.AddListener(secondAction.action);

                //Setting button names, if any.
                if (firstAction != null && !string.IsNullOrEmpty(firstAction.buttonName)) actionButton.GetComponentInChildren<Text>().text = firstAction.buttonName;
                if (secondAction != null && !string.IsNullOrEmpty(secondAction.buttonName)) secondaryActionButton.GetComponentInChildren<Text>().text = secondAction.buttonName;
            }
            else
            {
                secondaryActionButton.gameObject.SetActive(false);
                if (firstAction != null && firstAction.action != null) actionButton.onClick.AddListener(firstAction.action);
                actionButton.onClick.AddListener(() => { instance.messagePopup.SetActive(false); });
                if (firstAction != null) actionButton.GetComponentInChildren<Text>().text = firstAction.buttonName;
                else actionButton.GetComponentInChildren<Text>().text = "Ok";
            }

            //Setting the message.
            messageTextParent.Find("MessageText").GetComponent<Text>().text = message;
            instance.messagePopup.SetActive(true);
        }
    }

    public static void ShowLoading(string textToShow = "Loading...", float hideTime = 0)
    {
        if (!instance.loadingScreen)
        {
            instance.loadingScreen = Instantiate(instance.loadingScreenPrefab);
            DontDestroyOnLoad(instance.loadingScreen);
        }
        //instance.loadingScreen.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = textToShow;

        instance.loadingScreen.SetActive(true);

        if (hideTime != 0) HelperUtil.CallAfterDelay(HideLoading, hideTime);
    }
    public static void ShowDetailMessage(string message, MessageActionData firstAction = null, MessageActionData secondAction = null, bool withBackground = false, bool enableConvertor = false, bool isLandscape = false)
    {
        if (enableConvertor) message = GameMessage.MessageConvertor(message);
        if (!instance.detailedMessagePopup)
        {
            if (!isLandscape)
            {
                instance.detailedMessagePopup = Instantiate(instance.detailedMessagePopupPrefab);
                DontDestroyOnLoad(instance.detailedMessagePopup);
            }
            else
            {
                instance.detailedMessagePopup = Instantiate(instance.detailedMessagePopupPrefab);
                DontDestroyOnLoad(instance.detailedMessagePopupPrefab);
            }
        }
        Transform messageTextParent = instance.detailedMessagePopup.transform.GetChild(0).GetChild(0);
        Transform buttonHolder = messageTextParent.transform.Find("ButtonHolder");

        //Set popup background visibility.
        Image messageBackground = instance.detailedMessagePopup.transform.GetChild(0).GetComponent<Image>();
        Color32 oldColor = messageBackground.color;
        messageBackground.color = new Color32(oldColor.r, oldColor.g, oldColor.b, withBackground ? (byte)255 : (byte)0);

        //Fetching the buttons and remove existing actions if any..
        Button actionButton = buttonHolder.transform.Find("Button_1").GetComponent<Button>();
        Button secondaryActionButton = buttonHolder.transform.Find("Button_2").GetComponent<Button>();
        actionButton.onClick.RemoveAllListeners();
        secondaryActionButton.onClick.RemoveAllListeners();

        if (secondAction != null)
        {
            secondaryActionButton.gameObject.SetActive(true);

            //Adding close action to both buttons.
            actionButton.onClick.AddListener(() => { instance.detailedMessagePopup.SetActive(false); });
            actionButton.onClick.AddListener(() =>
            {

            });
            secondaryActionButton.onClick.AddListener(() => { instance.detailedMessagePopup.SetActive(false); });

            //Adding additional actions, if any.
            if (firstAction != null && firstAction.action != null) actionButton.onClick.AddListener(firstAction.action);
            if (secondAction != null && secondAction.action != null) secondaryActionButton.onClick.AddListener(secondAction.action);

            //Setting button names, if any.
            if (firstAction != null && !string.IsNullOrEmpty(firstAction.buttonName)) actionButton.GetComponentInChildren<Text>().text = firstAction.buttonName;
            if (secondAction != null && !string.IsNullOrEmpty(secondAction.buttonName)) secondaryActionButton.GetComponentInChildren<Text>().text = secondAction.buttonName;
        }
        else
        {
            secondaryActionButton.gameObject.SetActive(false);
            if (firstAction != null && firstAction.action != null) actionButton.onClick.AddListener(firstAction.action);
            actionButton.onClick.AddListener(() => { instance.detailedMessagePopup.SetActive(false); });
            if (firstAction != null) actionButton.GetComponentInChildren<Text>().text = firstAction.buttonName;
            else actionButton.GetComponentInChildren<Text>().text = "Ok";
        }

        //Setting the message.
        messageTextParent.Find("MessageText").GetComponent<Text>().text = message;
        instance.detailedMessagePopup.SetActive(true);
    }
    public static void HideDetailMessage()
    {
        if (instance.detailedMessagePopup != null && instance.detailedMessagePopup.activeInHierarchy)
        {
            instance.detailedMessagePopup.SetActive(false);
        }
    }
    public static void HideLoading()
    {
        if (instance.loadingScreen && instance.loadingScreen.activeSelf) instance.loadingScreen.SetActive(false);
    }

    public static void HideMessage()
    {
        if (instance.messagePopup != null && instance.messagePopup.activeInHierarchy)
        {
            instance.messagePopup.SetActive(false);
        }
    }


    public static GameObject Instantiate(string path)
    {
        return GameObject.Instantiate(Resources.Load(path) as GameObject);
    }
    #endregion

    #region Download Image

    public static Dictionary<string, Texture> userProfilePicDictionary = new Dictionary<string, Texture>();
    public static void GetTextureFromURL(RawImage image, string url, Action onSuccess = null)
    {
        //if (userProfilePicDictionary.ContainsKey(url))
        //{
        //    image.texture = userProfilePicDictionary[url];
        //}
        //else
        //{
        instance.StartCoroutine(enumerator());
        IEnumerator enumerator()
        {
            UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url);
            yield return webRequest.SendWebRequest();

            if (webRequest.isHttpError || webRequest.isNetworkError)
            {
                Debug.Log("Network Error" + webRequest.error);
            }
            else
            {
                Texture imageTexture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
                //if (!userProfilePicDictionary.ContainsKey(url))
                //{
                //    userProfilePicDictionary.Add(url, imageTexture);
                //}
                if (image) image.texture = imageTexture;

                //Call OnSuccess callback if any.
                onSuccess?.Invoke();
            }
        }
        //}
    }

    public Dictionary<string, Texture> universalTextureData = new Dictionary<string, Texture>();

    public static void SetTexture(string url, Action<Texture> textureSetCallback, Texture defaultTexture = null)
    {
        if (url != null)
        {
            if (instance.universalTextureData.ContainsKey(url))
            {
                textureSetCallback(instance.universalTextureData[url]);
            }
            else if (url.Contains(".png") || url.Contains(".jpeg") || url.Contains(".jpg"))
            {
                //If the tecture already exist in the database.
                if (PlayerPrefs.HasKey(url))
                {
                    try
                    {
                        byte[] textureData = File.ReadAllBytes(PlayerPrefs.GetString(url));
                        Texture2D tex = new Texture2D(2, 2);
                        tex.LoadImage(textureData);
                        instance.universalTextureData.AddIfNotAvailable(url, tex);
                        textureSetCallback(tex);
                    }
                    catch
                    {
                        DownloadTexture();
                    }
                }
                else DownloadTexture();

                void DownloadTexture()
                {
                    //if (url.ToUpper().Contains("PDF") || url.ToUpper().Contains("txt") || url.ToUpper().Contains("MP3") || url.ToUpper().Contains("WAV"))
                    //{
                    //    if (defaultTexture != null) textureSetCallback(defaultTexture);
                    //}
                    //else
                    instance.StartCoroutine(enumerator());
                    IEnumerator enumerator()
                    {
                        UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url);
                        yield return webRequest.SendWebRequest();

                        if (!webRequest.isHttpError && !webRequest.isNetworkError)
                        {
                            Texture2D downloadedTexture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;

                            //Saving the file into the local storage.
                            string filePath = Path.Combine(Application.persistentDataPath, "Image_" + UnityEngine.Random.Range(0, 999999999) + ".png");
                            File.WriteAllBytes(filePath, downloadedTexture.EncodeToPNG());
                            PlayerPrefs.SetString(url, filePath);
                            if (downloadedTexture != null)
                            {
                                instance.universalTextureData.AddIfNotAvailable(url, downloadedTexture);
                                textureSetCallback(downloadedTexture);
                            }
                        }
                        else if (defaultTexture != null) textureSetCallback(defaultTexture);
                    }

                }
            }
            else if (url.Contains(".mp4"))
            {
                if (defaultTexture != null) textureSetCallback(GameLibrary.GetSprite("video_background").texture);
            }
            else if (url.Contains(".txt") || url.Contains(".json"))
            {
                if (defaultTexture != null) textureSetCallback(GameLibrary.GetSprite("text_background").texture);
            }
            else if (url.Contains(".pdf"))
            {
                if (defaultTexture != null) textureSetCallback(GameLibrary.GetSprite("pdf_background").texture);
            }
            else if (url.Contains(".zip"))
            {
                if (defaultTexture != null) textureSetCallback(GameLibrary.GetSprite("3d_model_background").texture);
            }
            else if (url.Contains(".mp3"))
            {
                if (defaultTexture != null) textureSetCallback(GameLibrary.GetSprite("music_background").texture);
            }
            else
            {
                if (defaultTexture != null) textureSetCallback(GameLibrary.defaultImage.texture);
            }
        }
    }

    #endregion


    #region Manage Text Length
    public string ManageLenghtOfText(string Text, int maxlimit)
    {
        string retundata = "";
        if (Text.Length > maxlimit)
        {
            for (int i = 0; i < maxlimit; i++)
            {
                retundata += Text[i].ToString();
            }
            retundata += "...";
            return retundata;

        }
        else
        {
            return Text;
        }
    }
    #endregion

  

    #region Addressable Methods

    public AssetReference GetNextScene(SceneType sceneType)
    {
        if (sceneType == SceneType.Lobby)
        {
            return GameLibrary.LobbySceneref;
        }
        else if (sceneType == SceneType.Healthee)
        {
            return GameLibrary.HealtheeRef;
        }
        else if (sceneType == SceneType.Mechano)
        {
            return GameLibrary.MechanoRef;
        }
        else if (sceneType == SceneType.Questy)
        {
            return GameLibrary.QuestyRef;
        }
        else if (sceneType == SceneType.Extroverse)
        {
            return GameLibrary.ExtroverseRef;
        }
        return GameLibrary.LobbySceneref;
    }

    public void LoadDownloadingScene(string sceneName)
    {
        Screen.orientation = ScreenOrientation.Landscape;

        CallAfterDelay(() =>
        {
            var asyncSceneRef = SceneManager.LoadSceneAsync(StaticLibrary.SceneName.DownloadingScene, LoadSceneMode.Single);
            asyncSceneRef.completed += (data) =>
            {
                ShowLoading();
                CallAfterDelay(() =>
                {
                    LoadScenesWithAdressable(sceneName, "Scenes");
                }, 2f);
            };

        }, 2f);


        //CallAfterCondition(() =>
        //{
        //    CallAfterDelay(() =>
        //    {
        //        var asyncSceneRef = SceneManager.LoadSceneAsync(StaticLibrary.SceneName.DownloadingScene, LoadSceneMode.Single);
        //        asyncSceneRef.completed += (data) =>
        //        {
        //            ShowLoading();
        //            CallAfterDelay(() =>
        //            {
        //                LoadScenesWithAdressable(sceneName, "Scenes");
        //            }, 2f);
        //        };

        //    }, 2f);

        //}, () => Screen.orientation == ScreenOrientation.Landscape, null);
    }
    public void LoadDownloadingSceneStandAlone(string sceneName)
    {
        var asyncSceneRef = SceneManager.LoadSceneAsync(StaticLibrary.SceneName.DownloadingScene, LoadSceneMode.Single);
        asyncSceneRef.completed += (data) =>
        {
            ShowLoading();
            CallAfterDelay(() =>
            {
                LoadScenesWithAdressable(sceneName, "Scenes");
            }, 2f);
        };
    }
    public void LoadDownloadingRequiredScene(string sceneName, Action onLoadScene)
    {
        ShowLoading();
        CallAfterDelay(() =>
        {
            LoadSceneWithAdressable((asynceRef) =>
            {
                HideLoading();
                HideDownloadLoading();
                if (asynceRef.Status == AsyncOperationStatus.Succeeded)
                {

                }
                if (asynceRef.Status == AsyncOperationStatus.Failed)
                {
                    ShowMessage(GameMessage.InternetNotWorking, new MessageActionData("Ok", () =>
                     {
                         Screen.orientation = ScreenOrientation.Portrait;
                         SceneManager.LoadScene(0);
                     }));
                }
            }, sceneName);
        }, 2);
    }

    public static void HideDownloadLoading()
    {
        if (instance.downloadLoadingScreen && instance.downloadLoadingScreen.activeSelf) instance.downloadLoadingScreen.SetActive(false);
    }

    public static void LoadSceneWithAdressable(Action<AsyncOperationHandle<SceneInstance>> onComplete, string Scene)
    {
        AsyncOperationHandle<SceneInstance> asyncoperation = Addressables.LoadSceneAsync(Scene, LoadSceneMode.Single);
        asyncoperation.Completed += onComplete;

        instance.StartCoroutine(ShowDownloadLoading(asyncoperation));
        IEnumerator ShowDownloadLoading(AsyncOperationHandle<SceneInstance> downloadingFileReference)
        {
            ShowLoading();
            if (!instance.downloadLoadingScreen)
            {
                instance.downloadLoadingScreen = Instantiate(instance.downloadLoadingScreenPrefab);
                DontDestroyOnLoad(instance.downloadLoadingScreen);
                instance.downloadLoadingScreen.SetActive(false);
            }
            Slider downloadSlider = instance.downloadLoadingScreen.transform.GetChild(0).GetChild(0).GetComponent<Slider>();
            downloadSlider.value = 0;
            while (!downloadingFileReference.GetDownloadStatus().IsDone)
            {
                if (downloadingFileReference.GetDownloadStatus().TotalBytes > 0)
                {
                    if (instance.loadingScreen.activeSelf)
                    {
                        HideLoading();
                        instance.downloadLoadingScreen.SetActive(true);
                    }
                    downloadSlider.value = downloadingFileReference.GetDownloadStatus().Percent;
                }
                yield return null;
            }
        }
    }

    public static void LoadScenesWithAdressable(string sceneName, string refData)
    {
        AsyncOperationHandle asyncoperation = Addressables.DownloadDependenciesAsync(refData);
        instance.StartCoroutine(ShowDownloadLoading(asyncoperation));
        IEnumerator ShowDownloadLoading(AsyncOperationHandle downloadingFileReference)
        {
            ShowLoading();
            if (!instance.downloadLoadingScreen)
            {
                instance.downloadLoadingScreen = Instantiate(instance.downloadLoadingScreenPrefab);
                DontDestroyOnLoad(instance.downloadLoadingScreen);
                instance.downloadLoadingScreen.SetActive(false);
            }
            Slider downloadSlider = instance.downloadLoadingScreen.transform.GetChild(0).GetChild(0).GetComponent<Slider>();
            downloadSlider.value = 0;
            while (!downloadingFileReference.GetDownloadStatus().IsDone)
            {
                if (downloadingFileReference.GetDownloadStatus().TotalBytes > 0)
                {
                    if (instance.loadingScreen.activeSelf)
                    {
                        HideLoading();
                        instance.downloadLoadingScreen.SetActive(true);
                    }
                    downloadSlider.value = downloadingFileReference.GetDownloadStatus().Percent;
                }
                yield return null;
            }
        }
        asyncoperation.Completed += (data) =>
        {
            instance.LoadDownloadingRequiredScene(sceneName, null);
        };
    }

    public void JsonDownload(string url, Action<string> onSuccess)
    {
        instance.StartCoroutine(enumerator());
        IEnumerator enumerator()
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(url);
            yield return webRequest.SendWebRequest();

            if (webRequest.isHttpError || webRequest.isNetworkError)
            {
                Debug.Log("Network Error" + webRequest.error);
            }
            else
            {
                var jsonData = ((DownloadHandler)webRequest.downloadHandler).text;
                Debug.Log(jsonData);
                onSuccess(jsonData);
            }
        }
    }

    internal static void SetDefinedTexture(RawImage profileImage, object thumbURL, Action p)
    {
        throw new NotImplementedException();
    }
    #endregion

}

#region Interfaces

//This is For All Scenes API Responce Handle
public interface IUniversalResponseHandler
{
    void OnGetHouseDataResponse(string response, string responseCode);

    void OnGetLeaseRoomDataResponse(string response, string responseCode);

    void OnRemoveLeaseRoomDataResponse(string response, string responseCode);

    void OnGetHouseItemDataResponse(string response, string responseCode);

    void OnItemPlaceDataResponse(string response, string responceCode);

    void OnGetViewItemResponse(string response, string responseCode);

    void OnGetDetilForPlaceResponse(string response, string responseCode);
}

//This is for Handle All Interaction Action Handle
public interface IUniversalInteractionHandler
{
    public PlayerManager LocalPlayer { get; set; }
    //public House currentHouse { get; set; }
    //public string currentHouseId { get; set; }
    public void EnterIntoProperty(Transform doorTransform);
}

//This is for Player and Door interaction 
public interface IUniversalPlayerInteraction
{
    //House currentHouse { get; set; }
    void OnGetAllOwnedItemInRoomDataResponse(string response, string responseCode);
    //void OnGetMarketplaceOwnedItemsResponse(string response, string responseCode);
}

//This is For the event of UI when Player interaction with Door and Other objects
public interface IUniversalUIHandler
{
    string currentMetaObject_Id { get; set; }
    void ActivatePanel(string panelName);
    Text description_ItemPlaceScreenProp { get; set; }
    void OnClickplaceNewObject_ItemPlaceScreen(string itemId, string currentHouseId);
}

//For handling land APis
public interface IUniversalLandHandler
{
    public void OnGetBasicLandInfoResponse(string response, string responseCode);

    public void OnGetAllLandResponse(string response, string responseCode);

    public void OnGetAllLandObjectResponse(string response, string responseCode);

    public void OnGetLeasableLandResponse(string response, string responseCode);

    public void OnGetNonLeasableLandResponse(string response, string responseCode);

    public void OnGetUnRentedLandResponse(string response, string responseCode);

    public void OnGetPaginationLandResponse(string response, string responseCode);

    public void OnGetRentedLandResponse(string response, string responseCode);

    public void OnGetLandbyIDResponse(string response, string responseCode);

    public void OnGetObjectOnLandResponse(string response, string responseCode);

    public void OnSaveLandInfoResponse(string response, string responceCode);

    public void OnRentPaymentConfirmResponse(string response, string responseCode);
}

#endregion

#region Data Classes

public class MessageActionData
{
    public string buttonName;
    public UnityEngine.Events.UnityAction action;

    public bool InUse
    {
        get
        {
            return !string.IsNullOrEmpty(buttonName) || action != null;
        }
    }

    public MessageActionData(string newStringName, UnityEngine.Events.UnityAction newAction)
    {
        buttonName = newStringName;
        action = newAction;
    }
}


#endregion



public static class HelperExtension
{
    public static void SetSprite(this Image theImage, string srcUrl)
    {
      //  HelperUtil.SetTexture(srcUrl, (texture) => theImage.sprite = Sprite.Create((Texture2D)texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f)));
    }
}

