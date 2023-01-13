using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class RuleTextPrinter : MonoBehaviour
{
    [SerializeField] Text _textUi = default;

    /// <summary>
    /// 名前、セリフのセット
    /// </summary>
    [SerializeField] string _message = "";

    [SerializeField] float _speed = 1.0F;


    float _elapsed = 0;
    float _interval;
    int _currentIndex = -1;


    public bool IsPrinting
    {
        get
        {
            if (_currentIndex == _message.Length - 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    void Start()
    {

        if (_textUi is null)
        {
            return;
        }

        ShowMessage(_message);
    }

    void Update()
    {
        if (_textUi is null || _message is null || _currentIndex + 1 >= _message.Length)
        {
            return;
        }

        _elapsed += Time.deltaTime;
        if (_elapsed > _interval)
        {
            _currentIndex++;
            _textUi.text += _message[_currentIndex];
            _elapsed = 0;
        }
    }

    public void ShowMessage(string message)
    {
        _textUi.text = "";

        _message = message;


        _currentIndex = -1;
        _interval = _speed / _message.Length;
        //Debug.Log(dialogue.Length);
    }

   
    /// <summary>
    /// 現在のセリフをスキップ
    /// </summary>
    public void Skip()
    {
        _textUi.text = _message;
        _currentIndex = _message.Length - 1;
    }

}