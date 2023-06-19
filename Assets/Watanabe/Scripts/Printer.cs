using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> UIデータの出力を行うクラス </summary>
[Serializable]
public class Printer
{
    [SerializeField] private Color _unSpeak = default;
    [SerializeField] private Fade _fade = default;

    [Header("Text一覧")]
    [Tooltip("何秒かけてセリフを表示させるか")]
    [SerializeField] private float _indicateTime = 1f;
    [SerializeField] private Text _speakerText = default;
    [SerializeField] private Text _dialogueText = default;

    [Header("Image類のUI")]
    [SerializeField] private Image _princessImage = default;
    [SerializeField] private Image _maidImage = default;
    [SerializeField] private List<Sprite> _princessSprites = new();
    [SerializeField] private List<Sprite> _maidSprites = new();

    /// <summary> 表示するセリフのインデックス </summary>
    private int _dialogueIndex = 2;
    /// <summary> セリフ全体 </summary>
    private string[] _dialogue = default;
    /// <summary> Text表示を実行中かどうか </summary>
    private bool _isShowText = false;

    //話者
    private const string _princess = "お嬢様";
    private const string _maid = "メイド";

    /// <summary> 表示するセリフの取得 </summary>
    public void Init(string[] dialogue)
    {
        _dialogue = dialogue;
    }

    /// <summary> セリフ毎にキャラクターのイラストを切り替える </summary>
    private void SwitchSprite(string speaker, int index)
    {
        if (speaker == _princess)
        {
            _princessImage.color = Color.white;
            _maidImage.color = _unSpeak;

            _princessImage.sprite = _princessSprites[index];
        }
        else if (speaker == _maid)
        {
            _princessImage.color = _unSpeak;
            _maidImage.color = Color.white;

            _maidImage.sprite = _maidSprites[index];
        }
    }

    public void ShowText()
    {
        if (_isShowText) return;

        if (_dialogueIndex < _dialogue.Length - 1)
        {
            _isShowText = true;

            var show = _dialogue[_dialogueIndex].Split(',');
            var sequence = DOTween.Sequence();

            _speakerText.text = show[0];
            _dialogueText.text = "";

            SwitchSprite(show[0], int.Parse(show[2]));
            //Text.DOText...指定した文字列を指定した時間で1文字ずつ表示する
            sequence.Append(_dialogueText.DOText(show[1], _indicateTime))
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        _isShowText = false;
                        _dialogueIndex++;
                    });
        }
        else
        {
            _fade.FadeOut();
        }
    }
}
