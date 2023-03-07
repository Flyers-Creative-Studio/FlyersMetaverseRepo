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


    public static HelperUtil instance { get; private set; }
    [SerializeField] GameObject messagePopupPrefab;
    [SerializeField] GameObject messagePopupPrefablandScap;
    private GameObject messagePopup;
    [SerializeField] GameObject loadingScreenPrefab;
    [SerializeField] GameObject downloadLoadingScreenPrefab;
    public GameObject loadingScreen;
    public GameObject downloadLoadingScreen;


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
    /// Helper method to start an async call.(action will be called after delay).
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

  
    #endregion

    public static GameObject Instantiate(string path)
    {
        return GameObject.Instantiate(Resources.Load(path) as GameObject);
    }

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

    #region TextureRotation

    public static Texture2D RotateTexture(Texture2D targetTexture)
    {
        Texture2D newTexture = new Texture2D(targetTexture.height, targetTexture.width, targetTexture.format, false);

        for (int i = 0; i < targetTexture.width; i++)
        {
            for (int j = 0; j < targetTexture.height; j++)
            {
                newTexture.SetPixel(j, i, targetTexture.GetPixel(targetTexture.width - i, j));
            }
        }
        newTexture.Apply();
        return newTexture;
    }

    #endregion
}
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