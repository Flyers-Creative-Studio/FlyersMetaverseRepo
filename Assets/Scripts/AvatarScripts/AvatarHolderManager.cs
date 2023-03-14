using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class AvatarHolderManager : MonoBehaviour {

    public static AvatarHolderManager instance {

        get; set;
    }
    public GameObject avatar,avatarForUI;
    public GameObject MaleDefaultavatar,FemaleDefaultavatar;
    public bool MaleAvatar;
    private void Awake() {
        if (instance == null) {
            instance = this;

            DontDestroyOnLoad(this.gameObject);

        } else {
            Destroy(this.gameObject);

        }
    }
  

    public void LoadAvatars() {
        StartCoroutine(_SceneLoading());
    }

    IEnumerator _SceneLoading() {
        SetParent_DefaultAvatars();
        avatar.transform.SetParent(this.transform);
        avatarForUI.transform.SetParent(Scene1Manager.Instance.DefaultAvatarsForUI);
        avatarForUI.transform.localPosition = Vector3.zero;
        avatarForUI.transform.localScale = Vector3.one;
        avatarForUI.transform.localRotation = Quaternion.identity;
        Scene1Manager.Instance.ResetAllDefaultAvatars();
        avatarForUI.SetActive(true);
        yield return new WaitForSeconds(2f);
        avatar.SetActive(false);

      

    }
    public void SetParent_DefaultAvatars()
    {
        MaleDefaultavatar.transform.SetParent(this.transform);
        FemaleDefaultavatar.transform.SetParent(this.transform);
        MaleDefaultavatar.SetActive(false);
        FemaleDefaultavatar.SetActive(false);


    }

}
