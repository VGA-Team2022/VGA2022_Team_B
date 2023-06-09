using UnityEngine;
using UnityEngine.UI;

public class DataLoad : MonoBehaviour
{
    [SerializeField] private Text _dataText = default;
    //　読む込むテキストが書き込まれている.txtファイル
    [SerializeField] private TextAsset _textAsset = default;
    [SerializeField] private Fade _fade = default;

    private string _loadText = default;
    private string[] _textArray = default;
    private int _textIndex = 0;


    private void Start()
    {
        _loadText = _textAsset.text;
        _textArray = _loadText.Split(char.Parse("\n"));
        _textIndex = 0;
        _dataText.text = "";
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ViewMessage();
        }
    }

    /// <summary> 読み込んだテキストファイルの内容を表示 </summary>
    private void ViewMessage()
    {
        if (_textIndex < _textArray.Length)
        {
            _dataText.text = _textArray[_textIndex];
            _textIndex++;
        }
        else
        {
            _fade.FadeOut();
        }
    }
}
