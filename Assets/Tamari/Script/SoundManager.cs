using UnityEngine;
using CriWare;
using System.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// CriAtom�ŉ����Đ�������SoundManager
/// </summary>
public class SoundManager// : Singleton<SoundManager>
{
    /// <summary>
    /// SE/ME�p��AtomSorce
    /// </summary>
    CriAtomSource _criAtomSource;

    /// <summary>
    /// BGM�p��AtomSorce
    /// </summary>
    CriAtomSource _criAtomBGMSource;

    /// <summary>
    /// BGM���؂�ւ��܂ł̎���
    /// </summary>
    float _changeSpeed = 1;

    const string BGMCueSheet = "BGM";

    static public SoundManager Instance = new SoundManager();

    SoundManager() { }

    /// <summary>
    /// SoundManeger�̏����ݒ�
    /// </summary>
    /// <param name="attachment"></param>
    public void Setup(SoundManagerAttachment attachment)
    {
        _criAtomSource = attachment.AtomSource;
        _criAtomBGMSource = attachment.AtomBGMSource;
        _changeSpeed = attachment.ChangeBGMSpeed;
    }

    /// <summary>
    /// BGM�̐؂�ւ����s��
    /// </summary>
    /// <param name="cueName"></param>
    void ChangeBGM(string cueName)
    {
        //Volume�̃t�F�[�h�A�E�g
        DOVirtual.Float(_criAtomBGMSource.volume, 0, _changeSpeed / 2, value => _criAtomBGMSource.volume = value)
            .OnComplete(() =>
            {
                //�ݒ肵�čĐ�
                _criAtomBGMSource.cueName = cueName;
                _criAtomBGMSource.Play();

                //Volume�̃t�F�[�h�C��
                DOVirtual.Float(_criAtomBGMSource.volume, 1, _changeSpeed / 2, value => _criAtomBGMSource.volume = value);
            });
    }

    /// <summary>
    /// SE/ME���Đ�����ׂ̊֐�
    /// </summary>
    /// <param name="cueSheet"></param>
    /// <param name="cueName"></param>
    public void CriAtomPlay(CueSheet cueSheet, string cueName)
    {
        if (!_criAtomSource)
        {
            Debug.Log("CriAtomSorce������܂���");
            return;
        }

        //�ݒ肵�čĐ�
        _criAtomSource.cueSheet = cueSheet.ToString();
        _criAtomSource.cueName = cueName;
        _criAtomSource.Play();
    }

    /// <summary>
    /// BGM���Đ�����
    /// </summary>
    /// <param name="cueName"></param>
    public void CriAtomBGMPlay(string cueName)
    {
        if (!_criAtomBGMSource)
        {
            Debug.Log("CriAtomBGMSorce������܂���");
            return;
        }

        //CueSheet��BGM�łȂ���ΐݒ�
        if (_criAtomBGMSource.cueSheet != BGMCueSheet)
            _criAtomBGMSource.cueSheet = BGMCueSheet;

        ChangeBGM(cueName);
    }

    //�ǂ����̃^�C�~���O�ŉ����~�߂�K�v���o�Ă��邩���Ȃ̂ŁA�K�v�ȂƂ��ɃR�����g�����E�R�[�h�̍X�V
    public void CriAtomStop()
    {
        if (!_criAtomSource)
        {
            Debug.Log("CriAtomSorce������܂���");
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



