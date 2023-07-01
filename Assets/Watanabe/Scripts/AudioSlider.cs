using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    private Slider _audioSlider = default;

    private void Start()
    {
        _audioSlider = GetComponent<Slider>();
    }

    /// <summary> Slider.OnValueChanged�ŌĂяo�� </summary>
    public void SetBgmVolume()
    {
        SoundManager.InstanceSound.PassBGMValue(_audioSlider.value);
    }
}
