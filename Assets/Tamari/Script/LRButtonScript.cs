using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LRButtonScript : MonoBehaviour
{
    [SerializeField, Tooltip("バランスとる関数の引数に渡す数値(右)")] float RightValue;
    [SerializeField, Tooltip("バランスとる関数の引数に渡す数値(左)")] float LeftValue;

    Obon _obon;
    void Start()
    {
        _obon = GetComponent<Obon>();
    }

    void Update()
    {
        GetBalance();
    }

    void GetBalance()
    {
        var current = Gamepad.current;

        if (current == null)
            return;

        var RightButton = current.buttonEast;
        var LeftButton = current.buttonWest;


        var isPressedRight = RightButton.IsPressed();
        var isPressedLeft = LeftButton.IsPressed();

        if (isPressedRight)
        {
            Debug.Log("右ボタン押してる");
            _obon.MisalignmentOfSweetsCausedByMovement(RightValue);
        }
        else if(isPressedLeft)
        {
            Debug.Log("左ボタン押してる");
            _obon.MisalignmentOfSweetsCausedByMovement(LeftValue);
        }
    }
}
