using Common;
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary> リザルトの出力関連全般を管理するクラス </summary>
[Serializable]
public class Printer
{
    [SerializeField] private GameObject _titleButton = default;
    [SerializeField] private GameObject _nextStageButton = default;
    [SerializeField] private GameObject _retryButton = default;

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

    [Tooltip("各キャラクターのステージ毎のSprite")]
    [SerializeField] private CharacterSprites _characters = new();
    [Tooltip("背景Sprite")]
    [SerializeField] private BackGrounds _backGrounds = new();
    #endregion

    /// <summary> 表示するセリフのインデックス </summary>
    private int _dialogueIndex = 2;
    /// <summary> セリフ全体 </summary>
    private string[] _dialogue = default;
    /// <summary> Text表示を実行中かどうか </summary>
    private bool _isShowText = false;
    /// <summary> 全てのセリフが出たか </summary>
    private bool _isFinishShowTexts = false;

    private Sprite[] _princessSprites = default;
    private Sprite[] _maidSprites = default;

    //話者
    private const string _princess = "お嬢様";
    private const string _maid = "メイド";

    /// <summary> データの初期化（表示するセリフの取得） </summary>
    public void Init(string[] dialogue, Stage stage)
    {
        _dialogue = dialogue;

        //キャラクターのSprite割り当て
        _princessSprites =
            stage == Stage.Yashiki ?
            _characters.PrincessYashiki : _characters.PrincessSea;

        _maidSprites = _characters.Maid;

        _isShowText = false;
        _isFinishShowTexts = false;

        _titleButton.SetActive(false);
        _nextStageButton.SetActive(false);
        _retryButton.SetActive(false);
        ShowText();
    }

    /// <summary> 背景設定 </summary>
    public void SetBackGround()
    {
        switch (GameManager.StageType)
        {
            case StageType.YASHIKI_DAYTIME:
                _resultBackGround.sprite = _backGrounds.YashikiDaytime;
                break;

            case StageType.YASHIKI_NIGHT:
                _resultBackGround.sprite = _backGrounds.YashikiNight;
                break;

            case StageType.SEA_DAYTIME:
                _resultBackGround.sprite= _backGrounds.SeaStageDaytime;
                break;

            case StageType.SEA_NIGHT:
                _resultBackGround.sprite = _backGrounds.SeaStageNight;
                break;

            case StageType.GARDEN_DAYTIME:
                _resultBackGround.sprite = _backGrounds.GardenDaytime;
                break;

            case StageType.GARDEN_NIGHT:
                _resultBackGround.sprite = _backGrounds.GardenNight;
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

    /// <summary> セリフ表示 </summary>
    public void ShowText()
    {
        if (_isShowText || _isFinishShowTexts)
        {
            Skip();
            return;
        }

        if (_dialogueIndex < _dialogue.Length - 1)
        {
            _isShowText = true;

            var show = _dialogue[_dialogueIndex].Split(',');

            _speakerText.text = show[0];
            _dialogueText.text = "";

            SwitchSprite(show[0], int.Parse(show[2]));

            _dialogueText.DOText(show[1], _indicateTime).SetEase(Ease.Linear)
                         .OnComplete(() =>
                         {
                             _isShowText = false;
                             _dialogueIndex++;
                         });
        }
        else
        {
            _isFinishShowTexts = true;

            Action onCompleteFadeOut
                = SceneChangeScript.StageUp() ?
                () =>
                {
                    if (GameManager.StageType == StageType.YASHIKI_DAYTIME)
                    {
                        SceneChangeScript.NoFadeLoadScene(Define.Scenes[SceneNames.TITLE_SCENE]);
                    }
                    else
                    {
                        _titleButton.SetActive(true);
                        _nextStageButton.SetActive(true);
                    }
                    GameManager.IsGameClear = false;
                } : 
                () =>
                {
                    _titleButton.SetActive(true);
                    _retryButton.SetActive(true);
                    GameManager.IsGameClear = false;
                };
            FadeScript.StartFadeOut(() => onCompleteFadeOut());
        }
    }

    private void Skip()
    {
        _dialogueText.DOComplete();
    }

    /// <summary> 背景のSpriteをまとめるクラス </summary>
    [Serializable]
    private class BackGrounds
    {
        [SerializeField] private Sprite _yashikiDaytime = default;
        [SerializeField] private Sprite _yashikiNight = default;
        [SerializeField] private Sprite _seaStageDaytime = default;
        [SerializeField] private Sprite _seaStageNight = default;
        [SerializeField] private Sprite _gardenDaytime = default;
        [SerializeField] private Sprite _gardenNight = default;

        public Sprite YashikiDaytime => _yashikiDaytime;
        public Sprite YashikiNight => _yashikiNight;
        public Sprite SeaStageDaytime => _seaStageDaytime;
        public Sprite SeaStageNight => _seaStageNight;
        public Sprite GardenDaytime => _gardenDaytime;
        public Sprite GardenNight => _gardenNight;
    }

    /// <summary> 各キャラクターのステージ毎のSpriteをまとめるクラス </summary>
    [Serializable]
    private class CharacterSprites
    {
        [SerializeField] private Sprite[] _princessYashiki = default;
        [SerializeField] private Sprite[] _princessSea = default;
        [SerializeField] private Sprite[] _maid = default;

        public Sprite[] PrincessYashiki => _princessYashiki;
        public Sprite[] PrincessSea => _princessSea;
        public Sprite[] Maid => _maid;
    }
}
