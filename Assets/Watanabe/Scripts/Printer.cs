using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> UIデータの出力を行うクラス </summary>
[System.Serializable]
public class Printer
{
    [SerializeField] private Fade _fade = default;
    [Header("Text一覧")]
    [SerializeField] private Text _speakerText = default;
    [SerializeField] private Text _dialogueText = default;
    [Header("Image類のUI")]
    [SerializeField] private Image _princessImage = default;
    [SerializeField] private Image _maidImage = default;
    [SerializeField] private List<Sprite> _princessSprites = new();
    [SerializeField] private List<Sprite> _maidSprites = new();
    [Header("Debug")]
    [Tooltip("Textの表示をどうするか")]
    [SerializeField] private bool _isTextMove = false;

    /// <summary> 表示するセリフのインデックス </summary>
    private int _dialogueIndex = 2;
    /// <summary> セリフ全体 </summary>
    private string[] _dialogue = default;

    private const string _princess = "お嬢様";
    private const string _maid = "メイド";

    public void Init(string[] dialogue)
    {
        _dialogue = dialogue;
    }

    /// <summary> セリフ毎にキャラクターのイラストを切り替える </summary>
    private void SwitchSprite(string speaker, int index)
    {
        if (speaker == _princess)
        {
            _princessImage.sprite = _princessSprites[index];
        }
        else if (speaker == _maid)
        {
            _maidImage.sprite = _maidSprites[index];
        }
    }

    public void ShowText()
    {
        if (_isTextMove)
        {
            //1文字ずつ(DOTween使う)
        }
        else
        {
            //通常表示(ただ切り替えるだけ)
            if (_dialogueIndex < _dialogue.Length - 1)
            {
                var show = _dialogue[_dialogueIndex].Split(',');

                _speakerText.text = show[0];
                _dialogueText.text = show[1];
                _dialogueIndex++;

                SwitchSprite(show[0], int.Parse(show[2]));
            }
            else
            {
                _fade.FadeOut();
            }
        }
    }
}
