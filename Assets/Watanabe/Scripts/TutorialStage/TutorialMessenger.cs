using System.Collections.Generic;
using UnityEngine;

public class TutorialMessenger : MonoBehaviour
{
    [SerializeField]
    private TextAsset _textData = default;

    private List<string[]> _showTexts = new();

    private void OnEnable()
    {
        var messages = _textData.text.Split(",,");

        for (int i = 0; i < messages.Length; i++) { _showTexts.Add(messages[i].Split("\n")); }
    }
}
