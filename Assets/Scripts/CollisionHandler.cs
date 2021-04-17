using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch(other.transform.tag)
        {
            case "Finish":
                Debug.Log("You made it!");
                NextLevel();
                break;
            case "Friendly":
                Debug.Log("Just tradin' paint!");
                break;
            case "Ground":
                Debug.Log("Luckily that's a landing we CAN walk away from...");
                ReloadLevel();
                break;
            case "Untagged":
                Debug.Log("You've hit an unknown!");
                break;
        }
    }

    private static void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private static void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
