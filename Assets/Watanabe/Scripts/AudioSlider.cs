using UnityEngine;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private SliderType _sliderType = SliderType.None;

    private Slider _audioSlider = default;

    private void Start()
    {
        _audioSlider = GetComponent<Slider>();
    }

    /// <summary> Slider.OnValueChangedÇ≈åƒÇ—èoÇ∑ </summary>
    public void SetBgmVolume()
    {
        if (_sliderType == SliderType.BGM)
        {
            SoundManager.InstanceSound.PassBGMVolume(_audioSlider.value);
        }
        else if (_sliderType == SliderType.SE)
        {
            SoundManager.InstanceSound.PassSEVolume(_audioSlider.value);
        }
    }
}

public enum SliderType
{
    None,
    BGM,
    SE,
}
