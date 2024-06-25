using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    public AudioClip[] footstepClips;
    public AudioSource audioSource;

    public CharacterController controller;

    public float footstepThreshhold;
    public float footstepRate;
    public float lastFootstepTime;


    private void Update()
    {
        if(controller.velocity.magnitude > footstepThreshhold)
        {
            if(Time.time -lastFootstepTime > footstepRate)
            {
                lastFootstepTime = Time.time;
                audioSource.PlayOneShot(footstepClips[Random.Range(0,footstepClips.Length)]);
            }
        }
    }



}
