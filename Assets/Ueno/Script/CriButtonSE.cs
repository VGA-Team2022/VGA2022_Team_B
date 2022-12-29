using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Buttonにタッチ音を付与する
/// </summary>
public class CriButtonSE : MonoBehaviour
{
    private Button[] _buttons;

    private void Awake()
    {
        _buttons = GameObject.FindObjectsOfType<Button>();

        foreach (var item in _buttons)
        {
            item.onClick.AddListener(OnButtonClick);
        }  
    }

    public void OnButtonClick()
    {
        SoundManager.Instance.CriAtomPlay(CueSheet.SE, "SE_touch_normal");
    }
}
