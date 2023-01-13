using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class MessagePrinter : MonoBehaviour
{
    [SerializeField] Text _textUi = default;

    [SerializeField] string _ojyouName;

    [SerializeField] string _meidoName;

    [SerializeField] string _spNum;

    /// <summary>
    /// 名前、セリフのセット
    /// </summary>
    [SerializeField] string _message = "";

    /// <summary>
    /// セリフ
    /// </summary>
    [SerializeField] string _dialogue = "";

    /// <summary>
    /// 名前
    /// </summary>
    [SerializeField] string _name = "";

    /// <summary>
    /// お嬢様立ち絵
    /// </summary>
    [SerializeField] Image _ojyoImage;

    /// <summary>
    /// メイド立ち絵
    /// </summary>
    [SerializeField] Image _meidoImage;

    [SerializeField] float _speed = 1.0F;

    [SerializeField] Text _nameText = default;

    [Header("お嬢様SpriteList")]
    [SerializeField] List<Sprite> _ojyouSpList;

    [Header("メイドSpriteList")]
    [SerializeField] List<Sprite> _meidoSpList;

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
        _ojyoImage.GetComponent<Image>();
        _meidoImage.GetComponent<Image>();
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

        _spNum = _texts[2];

        if (_textUi is null || _message is null || _currentIndex + 1 >= _dialogue.Length)
        {
            return;
        }

        _elapsed += Time.deltaTime;
        if (_elapsed > _interval)
        {
            ChangeSprite();
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
        _interval = _speed / dialogue.Length;
    }

    void ChangeSprite()
    {
        if (_name == _ojyouName)
        {
            if (_spNum == "0")
            {
                _ojyoImage.sprite = _ojyouSpList[0];
            }
            else if (_spNum == "1")
            {
                _ojyoImage.sprite = _ojyouSpList[1];
            }
            else if (_spNum == "2")
            {
                _ojyoImage.sprite = _ojyouSpList[2];
            }
            else if (_spNum == "3")
            {
                _ojyoImage.sprite = _ojyouSpList[3];
            }
            else if (_spNum == "4")
            {
                _ojyoImage.sprite = _ojyouSpList[4];
            }
            else if (_spNum == "5")
            {
                _ojyoImage.sprite = _ojyouSpList[5];
            }
            else if (_spNum == "6")
            {
                _ojyoImage.sprite = _ojyouSpList[6];
            }
            else if (_spNum == "7")
            {
                _ojyoImage.sprite = _ojyouSpList[7];
            }
        }
        else if (_name == _meidoName)
        {
            if (_spNum == "0")
            {
                _meidoImage.sprite = _meidoSpList[0];
            }
            else if (_spNum == "1")
            {
                _meidoImage.sprite = _meidoSpList[1];
            }
            else if (_spNum == "2")
            {
                _meidoImage.sprite = _meidoSpList[2];
            }
            else if (_spNum == "3")
            {
                _meidoImage.sprite = _meidoSpList[3];
            }
        }
    }

    /// <summary>
    /// 現在のセリフをスキップ
    /// </summary>
    public void Skip()
    {
        _textUi.text = _dialogue;
        _currentIndex = _dialogue.Length - 1;
    }

}