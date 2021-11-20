using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmbientMusicManager : MonoBehaviour
{
    public Text buttonTxt;

    private AudioSource _audioSource;

    private bool isMuted;
    // Start is called before the first frame update
    void Start()
    {
        isMuted = false;
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetMuteState()
    {
        buttonTxt.text = isMuted ? "Mute": "Unmute" ;
        _audioSource.enabled = isMuted;
        isMuted = !isMuted;
    }


    public void Mute()
    {
        isMuted = false;
        buttonTxt.text = isMuted ? "Unmute" : "Mute";
        _audioSource.enabled = isMuted;
    }
    
}
