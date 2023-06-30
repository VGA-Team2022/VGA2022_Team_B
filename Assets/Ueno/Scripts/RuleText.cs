using UnityEngine;
using UnityEngine.UI;

public class RuleText : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite[] _rulePicture;

    [SerializeField] private Button _right;
    [SerializeField] private Button _left;

    [SerializeField] private Text _text;

    [SerializeField] private string[] _ruleText;

    [SerializeField]
    private RuleTextPrinter _printer = default;

    private int _currentIndex = -1;

    private void Start()
    {
        _image = _image.gameObject.GetComponent<Image>();
        MoveNext();
    }

    private void Update()
    {
        if (_currentIndex == 2)
        {
            _image.sprite = _rulePicture[1]; 
        }
        else if (_currentIndex == 3)
        {
            _image.sprite = _rulePicture[2];
        }
        else 
        {
            _image.sprite = _rulePicture[0];
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
        Debug.Log(_printer.IsPrinting);
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
    /// 次のセリフがあるなら次に進む
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
    /// 前のセリフがあるなら前に進む
    /// </summary>
    void MoveBack()
    {
        if (_ruleText is null or { Length: 0 })
        {
            return;
        }

        if (_currentIndex - 1 >= 0)
        {
            _currentIndex--;
            _printer?.ShowMessage(_ruleText[_currentIndex]);
        }
    }
}
