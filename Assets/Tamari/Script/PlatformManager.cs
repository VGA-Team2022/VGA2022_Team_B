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
        _LButton.gameObject.SetActive(true);
        _RButton.gameObject.SetActive(true);
        _Gyro.gameObject.SetActive(false);
        Debug.Log("Unity_Editor");
#endif

#if UNITY_ANDROID
        _LButton.gameObject.SetActive(true);
        _RButton.gameObject.SetActive(true);
        _Gyro.gameObject.SetActive(true);
        Debug.Log("Unity_Android");
#endif

#if UNITY_STANDALONE_WIN
        _LButton.gameObject.SetActive(true);
        _RButton.gameObject.SetActive(true);
        _Gyro.gameObject.SetActive(false);
        Debug.Log("Unity_Win");
#endif
    }
}
