using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public enum SE_Type
    {
        //�@�o�^�����@�������o�^
        �N���b�N = 0,
        �G���[ = 1,
        �����グ = 2,
        ���Ƃ� = 3,
        �~���` = 4
    }
    public enum BGM_Type
    {
        //�@�o�^�����@�������o�^
        ���� = 0,
        ���� = 99,  // ����
        �g = 1

    }


    private static AudioSource _SoundEfectSource;
    private static AudioSource _BGMSource;

    private static SoundManager instanceSound;
    public static SoundManager InstanceSound
    {
        get
        {
            if (instanceSound == null)
            {
                var soundObj = new GameObject("SoundManagerObj");
                instanceSound = soundObj.AddComponent<SoundManager>();
                _SoundEfectSource = soundObj.AddComponent<AudioSource>();

                var bgmObj = new GameObject("BGMObj");
                _BGMSource = bgmObj.AddComponent<AudioSource>();
                bgmObj.transform.parent = soundObj.transform;

                DontDestroyOnLoad(soundObj);
            }
            return instanceSound;
        }
    }



    public AudioClip GetSoundEffectList(int num)
    {
        var list = Resources.Load<SoundID>("SoundList");
        return list.SE_Clips[num];
    }

    public AudioClip GetBGMList(int num)
    {
        var list = Resources.Load<SoundID>("SoundList");
        return list.BGM_Clips[num];
    }


    //////////////////////////////////////////////////////////////////////////////////////////

    /// <summary>
    ///�@SE��炷�֐�
    /// </summary>
    public void PlayAudioClip(SE_Type audioClip)
    {

        _SoundEfectSource.Stop();
        _SoundEfectSource.PlayOneShot(GetSoundEffectList((int)audioClip));

    }

    /// <summary>
    ///�@BGM��炷�I�[�o�[���[�h
    /// </summary>
    public void PlayAudioClip(BGM_Type audioClip)
    {
        _BGMSource.Stop();
        _BGMSource.PlayOneShot(GetBGMList((int)audioClip));
    }

    /// <summary>
    /// SE���w��̔������Ŗ炷���[�΁[��[��
    /// </summary>
    /// <param name="audioClip">�����������ʉ�</param>
    /// <param name="audioSource">���̔�����</param>
    public void PlayAudioClip(SE_Type audioClip, AudioSource audioSource)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(GetSoundEffectList((int)audioClip));
    }

    /// <summary>
    /// BGM���w��̔������Ŗ炷���[�΁[��[��
    /// </summary>
    /// <param name="audioClip">�����������ʉ�</param>
    /// <param name="audioSource">���̔�����</param>
    public void PlayAudioClip(BGM_Type audioClip, AudioSource audioSource)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(GetBGMList((int)audioClip));
    }

    public void SEVolumeSeT(float vol)
    {
        if (_SoundEfectSource == null)
        {
            Debug.LogError("SE�̃I�[�f�B�I�\�[�X���Ȃ��̂�by���񂾂���");
            return;
        }
        _SoundEfectSource.volume = vol;
    }
    public void BGMVolumeSeT(float vol)
    {
        if (_BGMSource == null)
        {
            Debug.LogError("BGM�̃I�[�f�B�I�\�[�X���Ȃ��̂�by���񂾂���");
            return;
        }
        _BGMSource.volume = vol;
    }
}