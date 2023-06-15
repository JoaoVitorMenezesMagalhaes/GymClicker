using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSound : MonoBehaviour
{
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;
    public AudioSource audio4;
    public AudioSource audio5;
    

    public void playClicker1() {
        audio1.Play();
    }

    public void playClicker2() {
        audio2.Play();
    }

    public void playClicker3() {
        audio3.Play();
    }

    public void playClicker4() {
        audio4.Play();
    }

    public void playClicker5() {
        audio5.Play();
    }
}
