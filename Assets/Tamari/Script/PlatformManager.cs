using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] GameObject _LRButton;
    [SerializeField] GameObject _Gyro;

    private void Awake()
    {
#if UNITY_EDITOR
        _LRButton.SetActive(true);
        _Gyro.SetActive(true);

#elif UNITY_ANDROID
        _LRButton.SetActive(true);
        _Gyro.SetActive(true);

#elif UNITY_STANDALONE_WIN
        _LRButton.SetActive(true);
        _Gyro.SetActive(false);

#endif

    }
}
