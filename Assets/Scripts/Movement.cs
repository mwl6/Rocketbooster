using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] float rocketThrust = 1f;
    [SerializeField] float rotation = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem leftSideThrustEffects;
    [SerializeField] ParticleSystem rightSideThrustEffects;
    [SerializeField] ParticleSystem mainThrustEffects;

    Rigidbody myRigidbody;
    AudioSource myAudioSource;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }


    private void StartThrusting()
    {
        myRigidbody.AddRelativeForce(0, rocketThrust * Time.deltaTime, 0);
        if (!myAudioSource.isPlaying)
        {
            myAudioSource.PlayOneShot(mainEngine);
        }
        if (!mainThrustEffects.isPlaying)
        {
            mainThrustEffects.Play();
        }
    }

    private void StopThrusting()
    {
        myAudioSource.Stop();
        mainThrustEffects.Stop();
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            TurnLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            TurnRight();
        }
        else
        {
            rightSideThrustEffects.Stop();
            leftSideThrustEffects.Stop();
        }
    }

    private void TurnLeft()
    {
        leftSideThrustEffects.Stop();
        ApplyRotation(rotation);
        if (!rightSideThrustEffects.isPlaying)
        {
            rightSideThrustEffects.Play();
        }
    }
    private void TurnRight()
    {
        rightSideThrustEffects.Stop();
        ApplyRotation(-rotation);
        if (!leftSideThrustEffects.isPlaying)
        {
            leftSideThrustEffects.Play();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        myRigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        myRigidbody.freezeRotation = false;
    }
}

