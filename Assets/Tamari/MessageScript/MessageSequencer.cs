using UnityEngine;

public class MessageSequencer : MonoBehaviour
{
    private Story _storyJudge = Story.yashiki_Clear;

    [SerializeField] private MessagePrinter _printer = default;
    
    [Header("ストーリー入力欄")]
    [SerializeField] private string[] _yashiki_DayLight_ClearMessages = default;
    [SerializeField] private string[] _yashiki_Night_ClearMessages = default;
    [SerializeField] private string[] _yashikiFaildMessages = default;

    [SerializeField] private string[] _seaStageClearMessage = default;
    [SerializeField] private string[] _seaStageFailedMessage = default;

    [SerializeField] private GameObject _sceneChangeButtons = default;

    private string[] _storyMessages;
    private int _currentIndex = -1;

    public Story StoryJudge { get => _storyJudge; set => _storyJudge = value; }

    private void Start()
    {
        //ここが複数のステージに対応していないため、修正する
        if (GameManager.IsGameClear)
        {
            _storyJudge = Story.yashiki_Clear;

        }
        else
        {
            _storyJudge = Story.yashiki_Failed;
        }
        _sceneChangeButtons.SetActive(false);

        switch(_storyJudge)
        {
            case Story.yashiki_Clear:
                if (GameManager.StageLevelNum == 0)
                {
                    _storyMessages = _yashiki_DayLight_ClearMessages;
                }
                else
                {
                    _storyMessages = _yashiki_Night_ClearMessages;
                }
                break;

            case Story.yashiki_Failed:
                _storyMessages = _yashikiFaildMessages;
                break;

            case Story.SeaStage_Clear:
                _storyMessages = _seaStageClearMessage;
                break;

            case Story.SeaStage_Failed:
                _storyMessages = _seaStageFailedMessage;
                break;
        }
        MoveNext();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_printer.IsPrinting)
            {
                MoveNext();
            }
            else
            {
                _printer?.Skip();
            }
            Debug.Log(_printer.IsPrinting);
        }
    }

    /// <summary> 次のセリフがあるなら次に進む </summary>
    private void MoveNext()
    {
        if (_storyMessages is null or { Length: 0 }) return;

        if (_currentIndex + 1 < _storyMessages.Length)
        {
            _currentIndex++;
            _printer?.ShowMessage(
                _storyMessages[_currentIndex], _storyMessages[_currentIndex].Split(',')[0], _storyMessages[_currentIndex].Split(',')[1]);

            if (_currentIndex + 1 >= _storyMessages.Length)
            {
                FadeScript.StartFadeOut();
                _sceneChangeButtons.SetActive(true);
            }
        }
    }
}