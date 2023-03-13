using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AvatarLoadingManager : MonoBehaviour
{
    [SerializeField] GameObject PlayerGrp;
    [SerializeField] public GameObject Avatar, MaleDefaultAvatar, FemaleDefaultAvatar;
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
          //  Debug.Log("in1");

            //for Default avtar
            if (AvatarHolderManager.instance.MaleAvatar) {
                if (FemaleDefaultAvatarObjectRef != null) { 
                    
                    FemaleDefaultAvatarObjectRef.SetActive(false);
                    FemaleDefaultAvatarObjectRef.transform.parent = null;
                }
                if (MaleDefaultAvatarObjectRef==null)
                {
                    MaleDefaultAvatarObjectRef = Instantiate(MaleDefaultAvatar, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                   
                }
                else
                {
                    MaleDefaultAvatarObjectRef.SetActive(true);

                }
                MaleDefaultAvatarObjectRef.transform.SetParent(PlayerGrp.transform);
                MaleDefaultAvatarObjectRef.transform.localPosition = Vector3.zero;
                MaleDefaultAvatarObjectRef.transform.localRotation = Quaternion.identity;
                if (MaleDefaultAvatarObjectRef.GetComponent<Animator>() != null) MaleDefaultAvatarObjectRef.GetComponent<Animator>().enabled = false;

            } else {
                if (MaleDefaultAvatarObjectRef != null) { 
                    MaleDefaultAvatarObjectRef.SetActive(false);
                    MaleDefaultAvatarObjectRef.transform.parent = null;
                }
                if (FemaleDefaultAvatarObjectRef == null)
                {
                    FemaleDefaultAvatarObjectRef = Instantiate(FemaleDefaultAvatar, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                   
                }
                else
                {
                    FemaleDefaultAvatarObjectRef.SetActive(true);

                }

                FemaleDefaultAvatarObjectRef.transform.SetParent(PlayerGrp.transform);
                FemaleDefaultAvatarObjectRef.transform.localPosition = Vector3.zero;
                FemaleDefaultAvatarObjectRef.transform.localRotation = Quaternion.identity;
                if (FemaleDefaultAvatarObjectRef.GetComponent<Animator>() != null) FemaleDefaultAvatarObjectRef.GetComponent<Animator>().enabled = false;
            }
            StartCoroutine(EnablePlayerObject());
        }
       

    }
    // Update is called once per frame
    void Update() {
        if (!PlayerGrp.activeInHierarchy) {
            //for Customized avtar
            if (AvatarHolderManager.instance.avatar != null) {
                if (AvatarHolderManager.instance != null && Avatar == null) {
                    if (MaleDefaultAvatarObjectRef != null) Destroy(MaleDefaultAvatarObjectRef.gameObject);
                    if (FemaleDefaultAvatarObjectRef != null) Destroy(FemaleDefaultAvatarObjectRef.gameObject);

                    Avatar = AvatarHolderManager.instance.avatar;
                    if (Avatar.GetComponent<Animator>() != null) Avatar.GetComponent<Animator>().enabled = false;
                    Avatar.transform.SetParent(PlayerGrp.transform);
                    Avatar.transform.localPosition = Vector3.zero;
                    Avatar.transform.rotation = Quaternion.identity;


                }


            }

            StartCoroutine(EnablePlayerObject());

        }



    }
    IEnumerator EnablePlayerObject() {
        if (AvatarHolderManager.instance.avatar != null) AvatarHolderManager.instance.avatar.gameObject.SetActive(true);

        if (AvatarHolderManager.instance.MaleAvatar) {
            PlayerGrp.GetComponent<Animator>().avatar = Male;
        } else {
            PlayerGrp.GetComponent<Animator>().avatar = Female;
        }

        yield return new WaitForSeconds(.1f);
        PlayerGrp.SetActive(true);
        MainCamref.GetComponent<CameraController>().enabled = true;

        this.gameObject.SetActive(false);
    }

   
   

}
