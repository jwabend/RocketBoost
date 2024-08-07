using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;
using System.Runtime.InteropServices;
using UnityEditor.Callbacks;


public class CollisionHandler : MonoBehaviour
{
   
    
    [SerializeField] float delay;

    void OnCollisionEnter(Collision other) 
    {
        

        switch (other.gameObject.tag)
        {
            case "Finish":
                Debug.Log("You win!");
                StartSucessSequence();
                
                break;
            case "Friendly":
                Debug.Log("On landing pad");
                break;
            default:
                Debug.Log("You died!");
                StartCrashSequence();
                
            
                break;
        }
    }

    void StartCrashSequence(){
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel" ,  delay);
        
        
    }

    void StartSucessSequence(){
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delay);
        
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings){
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);
    }
}
