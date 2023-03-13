using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene1Manager : MonoBehaviour
{
    public static Scene1Manager Instance;
    public PlayerManager player;
    [SerializeField] Button CloseButton, ProfileButton,GenderButton;
    [SerializeField] GameObject CanvasObject, DefaultMale, DefaultFemale,Maincamera;
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
        ProfileButton.onClick.AddListener(() => SettingButtonListener());
        GenderButton.onClick.AddListener(() => GenderToggleListener());
    }
    void CloseButtonListener()
    {
        ResetAllDefaultAvatars();
        CanvasObject.SetActive(false);
        player.gameObject.SetActive(true);
        Maincamera.gameObject.SetActive(true);
        ProfileButton.gameObject.SetActive(true);
        AvatarCamera.SetActive(false);
    }
    void SettingButtonListener()
    {
        ProfileButton.gameObject.SetActive(false);
        CanvasObject.SetActive(true);
        player.gameObject.SetActive(false);
        Maincamera.gameObject.SetActive(false);
        AvatarCamera.SetActive(true);

        DefaultMale.SetActive(AvatarHolderManager.instance.MaleAvatar);
        DefaultFemale.SetActive(!AvatarHolderManager.instance.MaleAvatar);
    }
    public void GenderToggleListener()
    {
        AvatarHolderManager.instance.MaleAvatar = !AvatarHolderManager.instance.MaleAvatar;
        DefaultMale.SetActive(AvatarHolderManager.instance.MaleAvatar);
        DefaultFemale.SetActive(!AvatarHolderManager.instance.MaleAvatar);
    }
    void ResetAllDefaultAvatars()
    {
        DefaultMale.SetActive(false);
        DefaultFemale.SetActive(false);
    }
    private void OnDisable()
    {
        CloseButton.onClick.RemoveAllListeners();
        ProfileButton.onClick.RemoveAllListeners();
        GenderButton.onClick.RemoveAllListeners();
    }


   
}
