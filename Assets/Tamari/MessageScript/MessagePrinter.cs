using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MessagePrinter : MonoBehaviour
{
    [SerializeField] Text _textUi = default;

    /// <summary>
    /// ���O�A�Z���t�̃Z�b�g
    /// </summary>
    [SerializeField] string _message = "";

    /// <summary>
    /// �Z���t
    /// </summary>
    [SerializeField] string _dialogue = "";

    /// <summary>
    /// ���O
    /// </summary>
    [SerializeField] string _name = "";

    [SerializeField] float _speed = 1.0F;

    [SerializeField] Text _nameText = default;

    float _elapsed = 0;
    float _interval;
    int _currentIndex = -1;

    string[] _texts;

    public bool IsPrinting
    {
        get
        {
            if (_currentIndex == _dialogue.Length - 1)
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

        ShowMessage(_message, _name, _dialogue);
    }

    void Update()
    {
        _texts = _message.Split(',');

        _name = _texts[0];

        _dialogue = _texts[1];

        if (_textUi is null || _message is null || _currentIndex + 1 >= _dialogue.Length)
        {
            return;
        }

        _elapsed += Time.deltaTime;
        if (_elapsed > _interval)
        {
            _currentIndex++;
            _nameText.text = _name;
            _textUi.text += _dialogue[_currentIndex];
            _elapsed = 0;
        }
    }

    public void ShowMessage(string message, string name, string dialogue)
    {
        _textUi.text = "";
        _nameText.text = "";

        _message = message;
        _name = name;
        _dialogue = dialogue;

        _currentIndex = -1;
        //_interval = _speed / _message.Split(',')[1].Length;
        _interval = _speed / dialogue.Length;
        Debug.Log(dialogue.Length);
    }

    /// <summary>
    /// ���݂̃Z���t���X�L�b�v
    /// </summary>
    public void Skip()
    {
        // TODO: �����ɃR�[�h������
        //_textUi.text = _message;
        _textUi.text = _dialogue;
        _currentIndex = _dialogue.Length - 1;
        //_currentIndex = _dialogue.Length - 1;
    }
}