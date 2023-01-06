using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] GameObject _LButton;
    [SerializeField] GameObject _RButton;
    [SerializeField] GameObject _Gyro;

    private void Awake()
    {
#if UNITY_EDITOR
        _LButton.SetActive(true);
        _RButton.SetActive(true);
        _Gyro.SetActive(true);
        Debug.Log("Unity_Editor");

#elif UNITY_ANDROID
        _LButton.SetActive(true);
        _RButton.SetActive(true);
        _Gyro.SetActive(true);
        Debug.Log("Unity_Android");

#elif UNITY_STANDALONE_WIN
        _LButton.SetActive(true);
        _RButton.SetActive(true);
        _Gyro.SetActive(false);
        Debug.Log("Unity_Windows");

#endif

    }
}
