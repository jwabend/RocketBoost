using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class Movement : MonoBehaviour
{
   
    [SerializeField]public float thrust;
    [SerializeField]public float rotateThrust;
    [SerializeField] AudioClip thrusters;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;
    [SerializeField] ParticleSystem mainBooster;
    

    Rigidbody rb;
    AudioSource audioSource;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust(){
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {   
            
            rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);

            if (!audioSource.isPlaying)
            {   
                audioSource.PlayOneShot(thrusters);
            }
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            mainBooster.Play();
        }
        if(Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W))
        {
            mainBooster.Stop();
            audioSource.Stop();
        }

        else return;
        
    }

    void ProcessRotation()
    {
        ProcessRotationLeft();
        ProcessRotationRight();
    }

    void ProcessRotationLeft()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("A is being pressed");
            ApplyRotation(rotateThrust);

            if (!audioSource.isPlaying)
            {   
                audioSource.PlayOneShot(thrusters);
            }
        }

        if(Input.GetKeyUp(KeyCode.A))
        {
            rightBooster.Stop();
            audioSource.Stop();
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            rightBooster.Play();
        }
        else return;
    }

    void ProcessRotationRight()
    {
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("A is being pressed");
            ApplyRotation(-rotateThrust);

            if (!audioSource.isPlaying)
            {   
                audioSource.PlayOneShot(thrusters);
            }
        }

        if(Input.GetKeyUp(KeyCode.D))
        {
            leftBooster.Stop();
            audioSource.Stop();
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            leftBooster.Play();
        }
        else return;
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
