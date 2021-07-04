using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public static AudioController Instance;
    public AudioClip[] boom = new AudioClip[5];
    public AudioClip fire;
    private AudioSource audioSource;
    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayBoom()
    {
        int index = Random.Range(0, 5);
        audioSource.clip = boom[index];
        audioSource.Play();
    }
    public void PlayFire()
    {
        audioSource.clip = fire;
        audioSource.Play();
    }
}
