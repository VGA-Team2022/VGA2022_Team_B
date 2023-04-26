using CriWare;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerAttachment : MonoBehaviour
{
    [Header("AtomSorce")]
    [SerializeField, Tooltip("SE�AME")] CriAtomSource _atomSource;
    [SerializeField, Tooltip("BGM")] CriAtomSource _atomBGMSource;

    [Header("BGM���؂�ւ��Ɋ|���鎞��")]
    [SerializeField] float changeBGMSpeed = 1f;

    [Header("BGM��CueSheet")]
    [SerializeField] string _bgmCueName = "";

    public CriAtomSource AtomSource { get => _atomSource; }
    public CriAtomSource AtomBGMSource { get => _atomBGMSource; }
    public float ChangeBGMSpeed { get => changeBGMSpeed; }

    private void Awake()
    {
        //SoundManager�̏����ݒ���s��
        if (_atomSource && _atomBGMSource)
        {
            AudioManager.Instance.Setup(this);
        }
    }
    private void Start()
    {
        if (_bgmCueName == "") return;

        //BGM���Đ�
        AudioManager.Instance.CriAtomBGMPlay(_bgmCueName);
    }
    //�|�[�Y������Ȃ獡��X�V

    void Pause()
    {
    //    if (_atomSource)
    //    {
    //        _atomSource.Pause(true);
    //    }
    //    if (_atomBGMSource)
    //    {
    //        //BGM�̃|�[�Y�����͖���
    //        _atomBGMSource.Pause(true);
    //    }
    }

    //Resume����ɓ���������X�V
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
