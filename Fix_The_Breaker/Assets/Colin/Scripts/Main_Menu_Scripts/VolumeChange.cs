using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeChange : MonoBehaviour
{
    public AudioMixer masterMixer;
    public float masterLevel;
    public Slider masterSlider;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetMasterVolume();
    }

    public float GetSliderValue()
    {
        return masterSlider.value - 80;
    }
    public void SetMasterVolume()
    {
        masterLevel = GetSliderValue();
        masterMixer.SetFloat("MasterVolume", masterLevel);
    }
}
