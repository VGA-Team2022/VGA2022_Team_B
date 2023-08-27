using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialMessenger : MonoBehaviour
{
    #region UI一覧
    [Tooltip("セリフのテキストデータ")]
    [SerializeField]
    private TextAsset _textData = default;

    [Header("表示Text一覧")]
    [Tooltip("何秒かけてセリフを表示させるか")]
    [SerializeField]
    private float _indicateTime = 1f;
    [SerializeField]
    private Text _speakerNameText = default;
    [SerializeField]
    private Text _dialogueText = default;

    [Header("Image類のUI")]
    [SerializeField]
    private Image _princessImage = default;
    [SerializeField]
    private Image _maidImage = default;

    [Header("割り当て用のSprite")]
    [SerializeField]
    private Sprite[] _princessSprites = default;
    [SerializeField]
    private Sprite[] _maidSprites = default;
    #endregion

    private List<string[]> _showTexts = new();
    private bool _isViewText = false;

    private void Start()
    {
        var messages = _textData.text.Split(",,");
        for (int i = 0; i < messages.Length; i++) { _showTexts.Add(messages[i].Split("\n")); }

        _speakerNameText.text = "";
        _dialogueText.text = "";
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) { ViewText(); }
    }

    private void ViewText()
    {
        if (_isViewText)
        {
            Skip();
            return;
        }
    }

    private void Skip() { _dialogueText.DOComplete(); }
}
