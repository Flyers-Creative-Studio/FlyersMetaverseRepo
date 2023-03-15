using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene1Manager : MonoBehaviour
{
    public static Scene1Manager Instance;
    public PlayerManager player;
    [SerializeField] Button CloseButton, ProfileButton;
    public Button GenderButton;
    [SerializeField] GameObject CanvasObject, Maincamera;
    [SerializeField] GameObject AvatarCamera;
    public static event Action OnGenderToggle;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        Maincamera.GetComponent<CameraController>().enabled = false;

    }

    private void Start()
    {
        Maincamera.GetComponent<CameraController>().enabled = true;

        CloseButton.onClick.AddListener(() => CloseButtonListener());
        ProfileButton.onClick.AddListener(() => ProfileButtonListener());
        GenderButton.onClick.AddListener(() => GenderToggleListener());
    }
    void CloseButtonListener()
    {
        ResetAllDefaultAvatars();
        CanvasObject.SetActive(false);
        player.gameObject.SetActive(true);
        Maincamera.gameObject.SetActive(true);
        AvatarCamera.SetActive(false);
        HelperUtil.CallAfterDelay(() => { 
        
        ProfileButton.gameObject.SetActive(true);

        }, 1f);
    }
    void ProfileButtonListener()
    {
        if (AvatarHolderManager.instance.avatar!=null) {
            GenderButton.gameObject.SetActive(false); 
        }
        ProfileButton.gameObject.SetActive(false);
        CanvasObject.SetActive(true);
        player.gameObject.SetActive(false);
        Maincamera.gameObject.SetActive(false);
        AvatarCamera.SetActive(true);
       
        AvatarHolderManager.instance.SetAvatarForUI();


    }
    public void GenderToggleListener()
    {
        AvatarHolderManager.instance.MaleAvatar = !AvatarHolderManager.instance.MaleAvatar;
        AvatarHolderManager.instance.DefaultMale.SetActive(AvatarHolderManager.instance.MaleAvatar);
        AvatarHolderManager.instance.DefaultFemale.SetActive(!AvatarHolderManager.instance.MaleAvatar);
    }
    public void ResetAllDefaultAvatars()
    {
        

        foreach (Transform T in AvatarHolderManager.instance.DefaultAvatarsForUI)
        {
            T.gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        CloseButton.onClick.RemoveAllListeners();
        ProfileButton.onClick.RemoveAllListeners();
        GenderButton.onClick.RemoveAllListeners();
    }


   
}
