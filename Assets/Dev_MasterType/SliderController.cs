using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        //_slider.maxValue = GameManager.GameTimeClearLength;
        _slider.maxValue = 100;
        _slider.value = 0;
    }

    private void Update()
    {
        _slider.value = GameManager.CurrentTime / GameManager.GameTimeClearLength * 100;
    }
}
