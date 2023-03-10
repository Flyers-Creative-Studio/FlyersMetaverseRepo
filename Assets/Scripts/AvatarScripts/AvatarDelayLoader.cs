using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarDelayLoader : MonoBehaviour
{
    [SerializeField] GameObject AvatarLoader;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loadAvatar());
    }

    IEnumerator loadAvatar()
    {
        yield return new WaitForSeconds(1.5f);
        AvatarLoader.SetActive(true);
    }
}
