using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;
using System.Runtime.InteropServices;
using UnityEditor.Callbacks;


public class CollisionHandler : MonoBehaviour
{
    
    [SerializeField] float delay;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;


    AudioSource audioSource;
    

    bool isTransitioning = false;
    bool enableCollision = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        
    }

    void Update()
    {
        RespondToDebug();
    }

    void OnCollisionEnter(Collision other) 
    {
        
        if (isTransitioning || !enableCollision) { return; }
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



    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        Debug.Log("Playing Crash Particle");
        crashParticle.Play();
        audioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel" , delay);
    }

    void StartSucessSequence()
    {
        isTransitioning = true;
        successParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(success);
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

    void RespondToDebug()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            // Debug.Log("Pressing L");
            LoadNextLevel();
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            // Debug.Log("Pressing C");
            enableCollision = !enableCollision;
        }
    }

}
