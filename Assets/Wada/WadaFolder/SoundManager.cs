using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Soundmanager : MonoBehaviour
{
    public enum SE_Type
    {
        Touch_Button = 0,
        Touch_Result = 1,
        Player_Collision = 2,
        Player_LaneMove = 3,
        CakeStandFall = 4,
        FootStep_Yashiki = 5,
        FootStep_Sea = 6,
        FootStep_Garden = 7,
        Enemy_SmallDog_Cry = 8,
        Enemy_BigDog_Cry = 9,
        Enemy_SmallDog_Breath = 10,
        Enemy_BigDog_Breath = 11,
        Enemy_Employee = 12,
        Enemy_Armor = 13,
        Enemy_ArtPaint_Milk = 14,
        Enemy_ArtPaint_Tarban = 15,
        Enemy_Coconut = 16,
        Enemy_Rooling = 17,
        Enemy_Whale_Voice = 18,
        Enemy_Whale_WaterSplash = 19,
        Enemy_Whale_WaterPaddle = 20,

    }
    public enum BGM_Type
    {
        Jingle_Clear = 0,
        Jingle_Faild = 1,
        BGM_Result_GameClear = 2,
        BGM_Result_GameOver = 3,
        BGM_Title_Home = 4,
        BGM_Yshiki_DayLight = 5,
        BGM_Yashiki_Night = 6,
        BGM_Sea_DayLight = 7,
        BGM_Sea_Sunset = 8,
    }


    private static AudioSource _soundEffectSource;
    private static AudioSource _BGMSource;

    private static Soundmanager instanceSound;
    public static Soundmanager InstanceSound
    {
        get
        {
            if (instanceSound == null)
            {
                var soundObj = new GameObject("SoundManagerObj");
                instanceSound = soundObj.AddComponent<Soundmanager>();
                _soundEffectSource = soundObj.AddComponent<AudioSource>();

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
    
    /// <summary> Player移動時に呼び出す関数 </summary>
    public void PlayerMoveSE(SE_Type audioClip)
    {
        //移動中 && サウンド実行中だったら何もしない
        if (_soundEffectSource.isPlaying) return;

        _soundEffectSource.PlayOneShot(GetSoundEffectList((int)audioClip));
    }

    /// <summary>
    ///　SEを鳴らす関数
    /// </summary>
    public void PlayAudioClip(SE_Type audioClip)
    {

        _soundEffectSource.Stop();
        _soundEffectSource.PlayOneShot(GetSoundEffectList((int)audioClip));

    }

    /// <summary>
    ///　BGMを鳴らすオーバーロード
    /// </summary>
    public void PlayAudioClip(BGM_Type audioClip)
    {
        _BGMSource.Stop();
        _BGMSource.PlayOneShot(GetBGMList((int)audioClip));
    }

    /// <summary>
    /// SEを指定の発生源で鳴らすおーばーろーど
    /// </summary>
    /// <param name="audioClip">流したい効果音</param>
    /// <param name="audioSource">音の発生源</param>
    public void PlayAudioClip(SE_Type audioClip, AudioSource audioSource)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(GetSoundEffectList((int)audioClip));
    }

    /// <summary>
    /// BGMを指定の発生源で鳴らすおーばーろーど
    /// </summary>
    /// <param name="audioClip">流したい効果音</param>
    /// <param name="audioSource">音の発生源</param>
    public void PlayAudioClip(BGM_Type audioClip, AudioSource audioSource)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(GetBGMList((int)audioClip));
    }

    public void SEVolumeSeT(float vol)
    {
        if (_soundEffectSource == null)
        {
            Debug.LogError("SEのオーディオソースがないのだbyずんだもん");
            return;
        }
        _soundEffectSource.volume = vol;
    }
    public void BGMVolumeSeT(float vol)
    {
        if (_BGMSource == null)
        {
            Debug.LogError("BGMのオーディオソースがないのだbyずんだもん");
            return;
        }
        _BGMSource.volume = vol;
    }
}