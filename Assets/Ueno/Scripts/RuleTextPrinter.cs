using UnityEngine;
using UnityEngine.UI;


public class RuleTextPrinter : MonoBehaviour
{
    [SerializeField] private Text _textUi = default;
    [SerializeField] private float _speed = 1f;

    /// <summary> 名前、セリフのセット </summary>
    private string _message = "";
    private float _elapsed = 0;
    private float _interval;
    private int _currentIndex = -1;

    public bool IsPrinting => !(_currentIndex == _message.Length - 1);

    private void Start()
    {
        if (_textUi == null) return;

        ShowMessage(_message);
    }

    private void Update()
    {
        if (_textUi == null || _message is null || _currentIndex + 1 >= _message.Length)
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