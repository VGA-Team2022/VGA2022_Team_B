using UnityEngine;
using CriWare;
using System.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// CriAtomで音を再生させるSoundManager
/// </summary>
public class AudioManager// : Singleton<SoundManager>
{
    /// <summary>
    /// SE/ME用のAtomSorce
    /// </summary>
    CriAtomSource _criAtomSource;

    /// <summary>
    /// BGM用のAtomSorce
    /// </summary>
    CriAtomSource _criAtomBGMSource;

    /// <summary>
    /// BGMが切り替わるまでの時間
    /// </summary>
    float _changeSpeed = 1;

    const string BGMCueSheet = "BGM";

    static public AudioManager Instance = new AudioManager();

    AudioManager() { }

    /// <summary>
    /// SoundManegerの初期設定
    /// </summary>
    /// <param name="attachment"></param>
    public void Setup(SoundManagerAttachment attachment)
    {
        _criAtomSource = attachment.AtomSource;
        _criAtomBGMSource = attachment.AtomBGMSource;
        _changeSpeed = attachment.ChangeBGMSpeed;
    }

    /// <summary>
    /// BGMの切り替えを行う
    /// </summary>
    /// <param name="cueName"></param>
    void ChangeBGM(string cueName)
    {
        //Volumeのフェードアウト
        DOVirtual.Float(_criAtomBGMSource.volume, 0, 1, value => _criAtomBGMSource.volume = value)
            .OnComplete(() =>
            {
                //設定して再生
                _criAtomBGMSource.cueName = cueName;
                _criAtomBGMSource.Play();

                //Volumeのフェードイン
                DOVirtual.Float(_criAtomBGMSource.volume, 0.25f, _changeSpeed / 2, value => _criAtomBGMSource.volume = value);
            });
    }

    /// <summary>
    /// SE/MEを再生する為の関数
    /// </summary>
    /// <param name="cueSheet"></param>
    /// <param name="cueName"></param>
    public void CriAtomPlay(CueSheet cueSheet, string cueName)
    {
        if (!_criAtomSource)
        {
            Debug.Log("CriAtomSorceがありません");
            return;
        }

        //設定して再生
        _criAtomSource.cueSheet = cueSheet.ToString();
        _criAtomSource.cueName = cueName;
        _criAtomSource.Play();
    }

    /// <summary>
    /// BGMを再生する
    /// </summary>
    /// <param name="cueName"></param>
    public void CriAtomBGMPlay(string cueName)
    {
        if (!_criAtomBGMSource)
        {
            Debug.Log("CriAtomBGMSorceがありません");
            return;
        }

        //CueSheetがBGMでなければ設定
        if (_criAtomBGMSource.cueSheet != BGMCueSheet)
            _criAtomBGMSource.cueSheet = BGMCueSheet;

        ChangeBGM(cueName);
    }

    //音停止。今後コードの更新
    public void CriAtomStop()
    {
        if (!_criAtomSource)
        {
            Debug.Log("CriAtomSorceがありません");
            return;
        }
        _criAtomSource.Stop();
    }
}

public enum CueSheet
{
    SE,
    ME
}



