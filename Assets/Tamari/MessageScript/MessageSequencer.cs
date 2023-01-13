using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics;


public class MessageSequencer : MonoBehaviour
{
    public Story StoryJudge = Story.yashiki_Clear;

    [SerializeField]
        private MessagePrinter _printer = default;
    
    [Header("ストーリー入力欄")]
    [SerializeField] 
        private string[] _yashiki_DayLight_ClearMessages = default;

    [SerializeField]
        private string[] _yashiki_Night_ClearMessages = default;

    [SerializeField]
        private string[] _yashikiFaildMessages = default;

    [SerializeField]
        private GameObject _sceneChangeButtons = default;

    private string[] _storyMessages;
    private int _currentIndex = -1;

    void Start()
    {
        if (GameManager.isGameClear)
        {
            StoryJudge = Story.yashiki_Clear;

        }
        else
        {
            StoryJudge = Story.yashiki_Failed;
        }


        _sceneChangeButtons.SetActive(false);

        switch(StoryJudge)
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
        }

        MoveNext();
    }

    void Update()
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
            UnityEngine.Debug.Log(_printer.IsPrinting);
        }


    }

    /// <summary>
    /// 次のセリフがあるなら次に進む
    /// </summary>
    void MoveNext()
    {
        if (_storyMessages is null or { Length: 0 })
        {
            return;
        }

        if (_currentIndex + 1 < _storyMessages.Length)
        {
            _currentIndex++;
            _printer?.ShowMessage(_storyMessages[_currentIndex], _storyMessages[_currentIndex].Split(',')[0], _storyMessages[_currentIndex].Split(',')[1]);

            if (_currentIndex + 1 >= _storyMessages.Length)
            {
                FadeScript.StartFadeOut();
                _sceneChangeButtons.SetActive(true);
            }
        }
    }
}