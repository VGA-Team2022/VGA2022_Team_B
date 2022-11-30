using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LRButtonScript : MonoBehaviour
{
    [SerializeField, Tooltip("�o�����X�Ƃ�֐��̈����ɓn�����l(�E)")] float RightValue;
    [SerializeField, Tooltip("�o�����X�Ƃ�֐��̈����ɓn�����l(��)")] float LeftValue;

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
            Debug.Log("�E�{�^�������Ă�");
            _obon.MisalignmentOfSweetsCausedByMovement(RightValue);
        }
        else if(isPressedLeft)
        {
            Debug.Log("���{�^�������Ă�");
            _obon.MisalignmentOfSweetsCausedByMovement(LeftValue);
        }
    }
}
