using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleText : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _ruluPicture;

    [SerializeField] private Button _right;
    [SerializeField] private Button _left;

    [SerializeField] private Text _text;

    [SerializeField] private string[] _ruleText;

    [SerializeField]
    private RuleTextPrinter _printer = default;

    private int _currentIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        _image = _image.gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        if (_currentIndex == 3)
        {
            _image.sprite = _ruluPicture[1]; 
        }
        else if (_currentIndex == 4)
        {
            _image.sprite = _ruluPicture[2];
        }
        else 
        {
            _image.sprite = _ruluPicture[0];
        }
    }

    public void OnButtonTapNext()
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

    public void OnButtonTapBack()
    {
        if (!_printer.IsPrinting)
        {
            MoveBack();
        }
        else
        {
            _printer?.Skip();
        }
        UnityEngine.Debug.Log(_printer.IsPrinting);
    }

    /// <summary>
    /// ���̃Z���t������Ȃ玟�ɐi��
    /// </summary>
    void MoveNext()
    {
        if (_ruleText is null or { Length: 0 })
        {
            return;
        }

        if (_currentIndex + 1 < _ruleText.Length)
        {
            _currentIndex++;
            _printer?.ShowMessage(_ruleText[_currentIndex]);
        }
    }

    /// <summary>
    /// �O�̃Z���t������Ȃ�O�ɐi��
    /// </summary>
    void MoveBack()
    {
        if (_ruleText is null or { Length: 0 })
        {
            return;
        }

        if (_currentIndex - 1 > 0)
        {
            _currentIndex--;
            _printer?.ShowMessage(_ruleText[_currentIndex]);
        }
    }
}