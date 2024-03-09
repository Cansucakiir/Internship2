using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip coin,jump,death,crush,bend,gameOver;

    private void Awake()
    {
        instance = this;
    }
    public void PlayCoinSound()
    {
        audioSource.clip = coin;
        audioSource.Play();
    }
    public void PlayJumpingSound()
    {
        audioSource.clip = jump;
        audioSource.Play();
    }
    public void PlayDeathSound()
    {
        audioSource.clip = death;
        audioSource.Play();
    }
    public void PlayCrushSound()
    {
        audioSource.clip = crush;
        audioSource.Play();
    }
    public void PlayBendingSound()
    {
        audioSource.clip = bend;
        audioSource.Play();
    }
    public void PlayGameOverSound()
    {
        audioSource.clip = gameOver;
        audioSource.Play();
    }
}
