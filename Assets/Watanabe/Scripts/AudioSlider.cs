using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    private Slider _audioSlider = default;

    private void Start()
    {
        _audioSlider = GetComponent<Slider>();
    }

    /// <summary> Slider.OnValueChanged‚ÅŒÄ‚Ño‚· </summary>
    public void SetBgmVolume()
    {
        SoundManager.InstanceSound.PassBGMValue(_audioSlider.value);
    }
}
