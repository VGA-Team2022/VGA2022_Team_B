using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> UIデータの出力を行うクラス </summary>
[Serializable]
public class Printer
{
    [SerializeField] private GameObject _sceneLoadButtons = default;

    [Tooltip("話していない人は暗くする")]
    [SerializeField] private Color _unSpeak = default;

    #region Inspectorで設定するUI
    [Header("Text一覧")]
    [Tooltip("何秒かけてセリフを表示させるか")]
    [SerializeField] private float _indicateTime = 1f;
    [SerializeField] private Text _speakerText = default;
    [SerializeField] private Text _dialogueText = default;

    [Header("Image類のUI")]
    [SerializeField] private Image _princessImage = default;
    [SerializeField] private Image _maidImage = default;
    [SerializeField] private Image _resultBackGround = default;

    [SerializeField] private List<Sprite> _princessSprites = new();
    [SerializeField] private List<Sprite> _maidSprites = new();
    [SerializeField] private BackGrounds _backGrounds = new();
    #endregion

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

        _sceneLoadButtons.SetActive(false);
        ShowText();
    }

    /// <summary> 背景設定 </summary>
    public void SetBackGround(GameResult result)
    {
        switch (result)
        {
            case GameResult.YashikiStage_Daytime_Clear:
            case GameResult.YashikiStage_Daytime_Failed:
                _resultBackGround.sprite = _backGrounds.YashikiDaytime;
                break;

            case GameResult.YashikiStage_Night_Clear:
            case GameResult.YashikiStage_Night_Failed:
                _resultBackGround.sprite = _backGrounds.YashikiNight;
                break;

            case GameResult.SeaStage_Daytime_Clear:
            case GameResult.SeaStage_Daytime_Failed:
                _resultBackGround.sprite= _backGrounds.SeaStageDaytime;
                break;

            case GameResult.SeaStage_Night_Clear:
            case GameResult.SeaStage_Night_Failed:
                _resultBackGround.sprite = _backGrounds.SeaStageNight;
                break;
        }
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

            _speakerText.text = show[0];
            _dialogueText.text = "";

            SwitchSprite(show[0], int.Parse(show[2]));

            //DOTween.Sequence() ... 複数のDOTweenの処理を1つにまとめることができる
            var sequence = DOTween.Sequence();
            //Text.DOText ... 指定した文字列を指定した時間で1文字ずつ表示する
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
            FadeScript.StartFadeOut(() => _sceneLoadButtons.SetActive(true));
        }
    }

    [Serializable]
    private class BackGrounds
    {
        [SerializeField] private Sprite _yashikiDaytime = default;
        [SerializeField] private Sprite _yashikiNight = default;
        [SerializeField] private Sprite _seaStageDaytime = default;
        [SerializeField] private Sprite _seaStageNight = default;

        public Sprite YashikiDaytime => _yashikiDaytime;
        public Sprite YashikiNight => _yashikiNight;
        public Sprite SeaStageDaytime => _seaStageDaytime;
        public Sprite SeaStageNight => _seaStageNight;
    }
}
