using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float timeDelay = 2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem crashEffects;
    [SerializeField] ParticleSystem successEffects;

    AudioSource myAudioSource;

    bool isTransitioning = false;

    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if(isTransitioning) { return; }

        switch(other.gameObject.tag)
        {
            case "Finish":
                StartLevelSequence();
                break;
            case "Friendly":
                Debug.Log("Just tradin' paint!");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        myAudioSource.Stop();
        crashEffects.Play();
        myAudioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", timeDelay);
    }

    void StartLevelSequence()
    {
        isTransitioning = true;
        myAudioSource.Stop();
        successEffects.Play();
        myAudioSource.PlayOneShot(success);
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", timeDelay);
    }

    void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
