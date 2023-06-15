using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradeSound : MonoBehaviour
{
    public AudioSource audio1;
    public AudioSource audio2;
    private bool isSoundPlaying = false;

    public void playUpgrade()
    {
        if (!isSoundPlaying)
        {
            int rand = Random.Range(0, 100);
            if (rand < 50){
                audio1.Play();
                isSoundPlaying = true;
                StartCoroutine(WaitForSoundToEnd());
            }
            else{
                audio2.Play();
                isSoundPlaying = true;
                StartCoroutine(WaitForSoundToEnd());
            }
                
        }
    }

    IEnumerator WaitForSoundToEnd()
    {
        while (audio1.isPlaying || audio2.isPlaying)
        {
            yield return null;
        }
        
        isSoundPlaying = false;
    }
}
