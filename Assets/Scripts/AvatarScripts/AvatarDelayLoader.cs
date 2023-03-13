using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarDelayLoader : MonoBehaviour
{
    [SerializeField] GameObject AvatarLoader;
    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(loadAvatar());
    }

    IEnumerator loadAvatar()
    {
        yield return new WaitForSeconds(.5f);
        AvatarLoader.SetActive(true);
        yield return new WaitForSeconds(.5f);
        this.gameObject.SetActive(false);
    }
}
