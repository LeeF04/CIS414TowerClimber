//Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{

    [SerializeField]
    private float pitchVariance = 0.15f; // Adjust this value for more or less variance 


    public void PlaySoundWithVariance(AudioSource audioSource, AudioClip audioClip)
    {
        if (audioSource != null && audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.pitch = 1.0f + Random.Range(-pitchVariance, pitchVariance);
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is not assigned.");
        }
    }
}
