using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Diagnostics;


public class MessageSequencer : MonoBehaviour
{
    [SerializeField] public Story StoryJudge = Story.yashiki_Failed;

    [SerializeField] MessagePrinter _printer = default;

    private string[] _messages = default;

    [SerializeField] private string[] _yasikiClearMessages = default;
    [SerializeField] private string[] _yashikiFailedMessages = default;

    int _currentIndex = -1;

    [SerializeField] GameObject _sceneChangeButtons = default;

    void Start()
    {
        _sceneChangeButtons.SetActive(false);

        switch (StoryJudge)
        {
            case Story.yashiki_Clear:
                _messages = _yasikiClearMessages;
                break;

            case Story.yashiki_Failed:
                _messages = _yashikiFailedMessages;
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
    /// éüÇÃÉZÉäÉtÇ™Ç†ÇÈÇ»ÇÁéüÇ…êiÇﬁ
    /// </summary>
    void MoveNext()
    {
        if (_messages is null or { Length: 0 })
        {
            return;
        }

        if (_currentIndex + 1 < _messages.Length)
        {
            _currentIndex++;
            _printer?.ShowMessage(_messages[_currentIndex]);

            if (_currentIndex + 1 >= _messages.Length)
            {
                FadeScript.StartFadeOut();
                _sceneChangeButtons.SetActive(true);
            }
        }
    }
}