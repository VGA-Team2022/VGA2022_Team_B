using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ShowGamePad : MonoBehaviour
{
    [SerializeField] bool _isShowed;

    [SerializeField] Image _gamePad;
    void Start()
    {
        _isShowed = false; 
        _gamePad.GetComponent<Image>();
        _gamePad.color = Color.clear;
    }
    void Update()
    {
        //var pos = this.transform.position;

        if(!_isShowed)
        {
            if(Input.GetMouseButton(0))
            {
                _isShowed = true;
                // 現在のゲームパッド情報
                var current = Gamepad.current;

                // ゲームパッド接続チェック
                if (current == null)
                    return;

                Vector3 mousePosition = Input.mousePosition;

                _gamePad.transform.position = mousePosition;

                _gamePad.color = Color.red;
                //_gamePad.gameObject.SetActive(true);
            }
        }

        if (_isShowed)
        {
            //if (_gamePad.transform.position.x < Screen.width /2)
            //{
            //    _gamePad.transform.position
            //}
            if (Input.GetMouseButtonUp(0))
            {
                _isShowed = false;
                _gamePad.color = Color.clear;
            }
        }
    }
}
