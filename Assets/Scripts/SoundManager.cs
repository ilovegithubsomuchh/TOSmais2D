using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------- Audio Clip ----------")]
    public AudioClip musicInGame;
    public AudioClip musicSFX;
    
    
    void Start()
    {
        MusicSource.clip = musicInGame;
        MusicSource.Play();
    }
    
    
}
