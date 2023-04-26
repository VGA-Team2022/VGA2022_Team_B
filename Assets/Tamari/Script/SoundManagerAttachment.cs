using CriWare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerAttachment : MonoBehaviour
{
    [Header("AtomSorce")]
    [SerializeField, Tooltip("SE、ME")] CriAtomSource _atomSource;
    [SerializeField, Tooltip("BGM")] CriAtomSource _atomBGMSource;

    [Header("BGMが切り替わるに掛かる時間")]
    [SerializeField] float changeBGMSpeed = 1f;

    [Header("BGMのCueSheet")]
    [SerializeField] string _bgmCueName = "";

    public CriAtomSource AtomSource { get => _atomSource; }
    public CriAtomSource AtomBGMSource { get => _atomBGMSource; }
    public float ChangeBGMSpeed { get => changeBGMSpeed; }

    private void Awake()
    {
        //SoundManagerの初期設定を行う
        if (_atomSource && _atomBGMSource)
        {
            AudioManager.Instance.Setup(this);
        }
    }
    private void Start()
    {
        if (_bgmCueName == "") return;

        //BGMを再生
        AudioManager.Instance.CriAtomBGMPlay(_bgmCueName);
    }
    //ポーズがあるなら今後更新

    void Pause()
    {
    //    if (_atomSource)
    //    {
    //        _atomSource.Pause(true);
    //    }
    //    if (_atomBGMSource)
    //    {
    //        //BGMのポーズ処理は未定
    //        _atomBGMSource.Pause(true);
    //    }
    }

    //Resumeも上に同じく今後更新
    void Resume()
    {
    //    if (_atomSource)
    //    {
    //        _atomSource.Pause(false);
    //    }
    //    if (_atomBGMSource)
    //    {
    //        _atomBGMSource.Pause(false);
    //    }
    }
}
