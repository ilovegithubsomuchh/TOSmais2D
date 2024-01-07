using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
