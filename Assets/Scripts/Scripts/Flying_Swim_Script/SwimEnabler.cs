using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.SceneManagement;

public class SwimEnabler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] bool IsSwim;
   
   
    private void Update()
    {
		//if (SceneManager.GetActiveScene().name==StaticLibrary.SceneName.ArtCity_Portal)
		{
            if (this.transform.localPosition.y <= 23 && !IsSwim)
            {
                Swim(true);
                this.GetComponent<ThirdPersonController>().enabled = false;

            }
            if (this.transform.localPosition.y > 23 && IsSwim)
            {
                Swim(false);
                this.GetComponent<ThirdPersonController>().enabled = true;

            }
        }
    }

    void Swim(bool b)
    {
        IsSwim = b;
        this.GetComponent<JetPack>().enabled = false;
        this.GetComponent<SwimPack>().enabled = b;
        this.GetComponent<SwimPack>().FlyButton.interactable = !b;

        Debug.Log(b);
        if (b)
        {
           // this.transform.GetComponent<Animator>().SetTrigger("Fly");
            this.transform.GetComponent<Animator>().SetTrigger("Swim");
            this.transform.GetComponent<Animator>().ResetTrigger("Idle");
            this.transform.GetChild(1).transform.localEulerAngles = new Vector3(-20, 0, 0);
            this.transform.GetChild(1).transform.localPosition = new Vector3(0, 1, 0);
            this.GetComponent<CapsuleCollider>().enabled = true;

        }
        else
        {
            this.transform.GetComponent<Animator>().SetTrigger("Idle");
            this.transform.GetComponent<Animator>().ResetTrigger("Swim");
            this.transform.GetComponent<Animator>().ResetTrigger("Fly");
            this.transform.GetChild(1).transform.localEulerAngles = new Vector3(0, 0, 0);
            this.transform.GetChild(1).transform.localPosition = new Vector3(0, 0, 0);
            this.GetComponent<CapsuleCollider>().enabled = false;
            this.GetComponent<JetPack>()._mainCamera.GetComponent<CameraController>().enabled = false;

            StartCoroutine(EnableCameracontrol());

        }

    }
    IEnumerator EnableCameracontrol()
    {
        yield return new WaitForSeconds(1f);
        this.GetComponent<JetPack>()._mainCamera.GetComponent<CameraController>().enabled = true;

    }
}
