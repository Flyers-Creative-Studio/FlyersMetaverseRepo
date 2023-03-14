using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AvatarLoadingManager : MonoBehaviour
{
    [SerializeField] GameObject PlayerGrp;
    [SerializeField] public GameObject Avatar;
    [SerializeField] GameObject MaleDefaultAvatarObjectRef, FemaleDefaultAvatarObjectRef;
    [SerializeField] Avatar Male, Female;
     GameObject MainCamref;
    // Start is called before the first frame update
    void Awake() {
        PlayerGrp.SetActive(false);
        MainCamref= Camera.main.gameObject;
    }
    private void OnEnable() {
        MainCamref.GetComponent<CameraController>().enabled = false;
        if (AvatarHolderManager.instance.avatar == null) {

            //for Default avtar
            if (AvatarHolderManager.instance.MaleAvatar) {
                if (FemaleDefaultAvatarObjectRef != null) { 
                    
                    FemaleDefaultAvatarObjectRef.SetActive(false);
                    FemaleDefaultAvatarObjectRef.transform.parent = AvatarHolderManager.instance.transform;
                }
              

                MaleDefaultAvatarObjectRef = AvatarHolderManager.instance.MaleDefaultavatar;
                MaleDefaultAvatarObjectRef.SetActive(true);

                MaleDefaultAvatarObjectRef.transform.SetParent(PlayerGrp.transform);
                MaleDefaultAvatarObjectRef.transform.localPosition = Vector3.zero;
                MaleDefaultAvatarObjectRef.transform.localRotation = Quaternion.identity;
                if (MaleDefaultAvatarObjectRef.GetComponent<Animator>() != null) MaleDefaultAvatarObjectRef.GetComponent<Animator>().enabled = false;

            }
            else {
                if (MaleDefaultAvatarObjectRef != null) { 
                    MaleDefaultAvatarObjectRef.SetActive(false);
                    MaleDefaultAvatarObjectRef.transform.parent = AvatarHolderManager.instance.transform;
                }
               
                FemaleDefaultAvatarObjectRef = AvatarHolderManager.instance.FemaleDefaultavatar;
                FemaleDefaultAvatarObjectRef.SetActive(true);

                FemaleDefaultAvatarObjectRef.transform.SetParent(PlayerGrp.transform);
                FemaleDefaultAvatarObjectRef.transform.localPosition = Vector3.zero;
                FemaleDefaultAvatarObjectRef.transform.localRotation = Quaternion.identity;
                if (FemaleDefaultAvatarObjectRef.GetComponent<Animator>() != null) FemaleDefaultAvatarObjectRef.GetComponent<Animator>().enabled = false;
            }
            StartCoroutine(EnablePlayerObject());

        }
        else
        {
            //for Customized avtar
            if (AvatarHolderManager.instance.avatar != null)
            {
                if (AvatarHolderManager.instance != null && Avatar == null)
                {
                    //if (MaleDefaultAvatarObjectRef != null) Destroy(MaleDefaultAvatarObjectRef.gameObject);
                    //if (FemaleDefaultAvatarObjectRef != null) Destroy(FemaleDefaultAvatarObjectRef.gameObject);

                    Avatar = AvatarHolderManager.instance.avatar;
                    if (Avatar.GetComponent<Animator>() != null) Avatar.GetComponent<Animator>().enabled = false;
                    Avatar.transform.SetParent(PlayerGrp.transform);
                    Avatar.transform.localPosition = Vector3.zero;
                    Avatar.transform.rotation = Quaternion.identity;
                    Avatar.SetActive(true);


                }

                StartCoroutine(EnablePlayerObject());

            }
        }


    }
  
    IEnumerator EnablePlayerObject() {
        if (AvatarHolderManager.instance.avatar != null) AvatarHolderManager.instance.avatar.gameObject.SetActive(true);

        if (AvatarHolderManager.instance.MaleAvatar) {
            PlayerGrp.GetComponent<Animator>().avatar = Male;
        } else {
            PlayerGrp.GetComponent<Animator>().avatar = Female;
        }
        PlayerGrp.SetActive(false);
        MainCamref.GetComponent<CameraController>().enabled = false;

        yield return new WaitForSeconds(.5f);
        PlayerGrp.SetActive(true);
        MainCamref.GetComponent<CameraController>().enabled = true;

        this.gameObject.SetActive(false);
    }

   

}
