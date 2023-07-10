using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Buttonにタッチ音を付与する
/// </summary>
public class ButtonSE : MonoBehaviour
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
        SoundManager.InstanceSound.PlayerMoveSE(SoundManager.SE_Type.Touch_Button);
    }
}
