using UnityEngine;
using UnityEngine.UI;

public class SoundConfigManager : MonoBehaviour
{
    private AudioSource audio;

    public AudioClip bgmClip;
    public AudioClip jumpClip;

    public Slider volumeSlider;
    public Toggle muteToggle;

    void Start()
    {
        audio = this.GetComponent<AudioSource>();

        audio.clip = bgmClip;
        audio.loop = true;
        audio.Play();

        volumeSlider.value = audio.volume;
        muteToggle.isOn = audio.mute;

        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        muteToggle.onValueChanged.AddListener(OnMuteToggled);
    }

    public void OnJumpSound()
    {
        audio.PlayOneShot(jumpClip);
    }

    private void OnVolumeChanged(float value)
    {
        if (!muteToggle.isOn)
            audio.volume = value;
    }

    private void OnMuteToggled(bool isMuted)
    {
        if (isMuted)
            audio.mute = true;
        else
            audio.mute = false;
    }
}
