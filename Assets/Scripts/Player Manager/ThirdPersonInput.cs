using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ThirdPersonInput : MonoBehaviour
{
    public FixedButton jumpButton;
    public SprintButton sprintButton;
    protected ThirdPersonUserControl Control;
    public float maxYAngle = 80f;
    public PhysicMaterial playermaterial;
    public AudioClip[] footSteps;
    public AudioSource mainAudioSource;
    void Start()
    {
        Control = GetComponent<ThirdPersonUserControl>();
    }

    void Update()
    {
    }
    private void LeftStep()
    {
        if (Control.H_Input!=0 || Control.V_Input != 0)
        {
            mainAudioSource.PlayOneShot(footSteps[0]);
        }
    }
    private void RightStep()
    {
        if (Control.H_Input != 0 || Control.V_Input != 0)
        {
            mainAudioSource.PlayOneShot(footSteps[1]);
        }
    }
}
