using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class AvatarHolderManager : MonoBehaviour {

    public static AvatarHolderManager instance {

        get; set;
    }
    public GameObject avatar;
    public bool MaleAvatar;
    private void Awake() {
        if (instance == null) {
            instance = this;

            DontDestroyOnLoad(this.gameObject);

        } else {
            Destroy(this.gameObject);

        }
    }

    
    public void LoadMetaverseScene() {
        StartCoroutine(_SceneLoading());
    }

    IEnumerator _SceneLoading() {
        avatar.transform.SetParent(this.transform);
        yield return new WaitForSeconds(2f);
        avatar.SetActive(false);

        SceneManager.LoadScene(1);

    }
    
}
