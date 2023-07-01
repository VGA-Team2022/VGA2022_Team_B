using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    private Slider _audioSlider = default;

    private void Start()
    {
        _audioSlider = GetComponent<Slider>();
    }

    /// <summary> Slider.OnValueChangedで呼び出す </summary>
    public void SetBgmVolume()
    {
        SoundManager.InstanceSound.PassBGMValue(_audioSlider.value);
    }
}
