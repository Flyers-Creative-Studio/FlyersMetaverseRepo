using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
public class FlightEnabler : MonoBehaviour
{
    public static FlightEnabler instance = null;

    private void Awake()
	{
        instance = this;
	}

   [SerializeField] bool Isflight;
    public void ApplyFlight()
    {
        Isflight = !Isflight;
        Flight(Isflight);
    }
    
    void Flight(bool b)
    {
        this.GetComponent<ThirdPersonController>().enabled = !b;
        this.GetComponent<JetPack>().enabled = b;
        this.GetComponent<JetPack>()._mainCamera.GetComponent<CameraController>()._cinemachineTargetPitch = 0;
        this.GetComponent<JetPack>()._mainCamera.GetComponent<CameraController>().enabled = false;
        if (b)
        {
            this.transform.GetComponent<Animator>().SetTrigger("Fly");
           // this.transform.GetComponent<Animator>().SetTrigger("Swim");
            this.transform.GetComponent<Animator>().ResetTrigger("Idle");
            this.transform.GetChild(1).transform.localEulerAngles = new Vector3(-20, 0, 0);
            this.transform.GetChild(1).transform.localPosition = new Vector3(0, 1, 0);
            this.GetComponent<CapsuleCollider>().enabled = true;

        }
        else
        {
            this.transform.GetComponent<Animator>().SetTrigger("Idle");
           // this.transform.GetComponent<Animator>().ResetTrigger("Swim");
            this.transform.GetComponent<Animator>().ResetTrigger("Fly");
            this.transform.GetChild(1).transform.localEulerAngles = new Vector3(0, 0, 0);
            this.transform.GetChild(1).transform.localPosition = new Vector3(0, 0, 0);
            this.GetComponent<CapsuleCollider>().enabled = false;

        }
        StartCoroutine(EnableCameracontrol());
    }
   IEnumerator EnableCameracontrol()
    {
        yield return new WaitForSeconds(1f);
        this.GetComponent<JetPack>()._mainCamera.GetComponent<CameraController>().enabled = true;

    }
}
