using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class PlayerManager : MonoBehaviour
{
    #region VARIABLES
    public AudioSource interactionAudioSource;
    public float rayDistance;
    public Transform eyeRayTransform;

    public Button eye_Button;
    public CameraController mainCameraRef;
    public LayerMask withoutCameraObjects;
    public LayerMask withCameraObjects;
    public Button setting_Button;
    public Button removeHouseFromLease;
    
    [Header("Main Controller")]
    [SerializeField] public Button bagButton_MainControllerScreen;

    string doorExit = "DoorExit";
    string doorEnterance = "DoorEnterance";
    #endregion

    #region UNITY_CALLBACKS


    private void Start()
    {
        if (eye_Button) eye_Button.onClick.AddListener(() => { OnSetPersonView(); });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(doorExit) )
        {
            OnClickBagButton(false, null);
        }

    }
    public void OnTriggerExit(Collider other)
    {
		if (removeHouseFromLease.gameObject.activeSelf)
            removeHouseFromLease.gameObject.SetActive(false);
    }



    #endregion

   

    public void OnSetPersonView(bool isFirstPerson = false)
    {
        eye_Button.interactable = false;
        HelperUtil.CallAfterDelay(() =>
        {
            eye_Button.interactable = true;
        }, 1f);
        if (isFirstPerson)
        {
            mainCameraRef.isFirstPerson = true;
        }
        else
        {
            if (mainCameraRef.isFirstPerson)
            {
                mainCameraRef.isFirstPerson = false;
            }
            else
            {
                mainCameraRef.isFirstPerson = true;
            }
        }
    }

    public void OnClickBagButton(bool isInteractable, UnityAction onClick)
    {
        bagButton_MainControllerScreen.interactable = isInteractable;
        bagButton_MainControllerScreen.onClick.RemoveAllListeners();
        bagButton_MainControllerScreen.onClick.AddListener(onClick);
    }
    #region INTERACTION_SYSTEM

    [SerializeField] private Button interactionButton;

    public void SetInteraction(UnityAction onClick, string interactionName = "Interact")
    {
        interactionButton.gameObject.SetActive(true);
        interactionButton.onClick.RemoveAllListeners();
        interactionButton.GetComponentInChildren<Text>().text = interactionName;
        if (onClick != null) interactionButton.onClick.AddListener(onClick);
        interactionButton.onClick.AddListener(() => interactionButton.gameObject.SetActive(false));
    }

    public void HideInteractionUI()
    {
        interactionButton.gameObject.SetActive(false);
    }

    #endregion
}
