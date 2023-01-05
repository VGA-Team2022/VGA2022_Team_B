using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    private Slider _slider;
    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = GameManager.GameTimeClearLength;
        _slider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value =  GameManager.GameTimeClearLength - GameManager.CurrentTime;
    }
}
