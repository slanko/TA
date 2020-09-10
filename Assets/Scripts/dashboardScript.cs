using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dashboardScript : MonoBehaviour
{
    [SerializeField] AudioSource aud;
    [SerializeField] Slider timeSlider;
    void Start()
    {

    }

    public void playSound(AudioClip soundToPlay)
    {
        aud.PlayOneShot(soundToPlay);
    }

    public void changeTimeScale()
    {
        Time.timeScale = timeSlider.value;
    }

    public void setTimeScale(int timeScaleValue)
    {
        Time.timeScale = timeScaleValue;
        timeSlider.value = timeScaleValue;
    }
}
