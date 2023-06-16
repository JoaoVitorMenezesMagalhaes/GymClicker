using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class upgradeSound : MonoBehaviour
{
    public AudioSource audio1;
    //public AudioSource audio2;
    private bool isSoundPlaying = false;

    private void Awake()
    {
        audio1.Stop();
        //audio2.Stop();
        isSoundPlaying = false;
    }

    private void OnEnable()
    {
        audio1.Stop();
        //audio2.Stop();
        isSoundPlaying = false;
    }

    public void playUpgrade()
    {
        if (!isSoundPlaying)
        {
            audio1.Play();
            isSoundPlaying = true;
            StartCoroutine(WaitForSoundToEnd());
        }
    }

    IEnumerator WaitForSoundToEnd()
    {
        while (audio1.isPlaying)
        {
            yield return null;
        }
        
        isSoundPlaying = false;
    }
}
