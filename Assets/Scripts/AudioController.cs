using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public static AudioController Instance;
    public AudioClip boom;
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
        audioSource.clip = boom;
        audioSource.Play();
    }
    public void PlayFire()
    {
        audioSource.clip = fire;
        audioSource.Play();
    }
}
