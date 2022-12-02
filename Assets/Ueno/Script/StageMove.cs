using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;



public class StageMove : MonoBehaviour
{
    [Header("�A�^�b�`�������")]
    [SerializeField] private GameObject[] _wall;
    [SerializeField] public Transform StartPos;
    [SerializeField] private Transform _centerPos;
    [SerializeField] public Transform EndPos;

    [Header("Pram")]
    [Tooltip("�ړ����x"),SerializeField] private float _moveSpeed = 5f;
    public float MoveSpeed  
        { get { return _moveSpeed; } set { _moveSpeed = value; } }

    [SerializeField] Obon _obon;
    [SerializeField] int i;
    /// <summary>��~���鎞��Speed�̒l������Ă���</summary>
    private float _keepSpeed;
    /// <summary>�X�}�z�f�o�b�O�p�̃t���O</summary>
    [SerializeField] private bool isPhonDebug;

    void Start()
    {
        _keepSpeed = MoveSpeed;
        MoveSpeed = 0;
        _wall[0].transform.position = _centerPos.position;
        _obon = _obon.gameObject.GetComponent<Obon>();
    }

    void Update()
    {
        if (!isPhonDebug)
        {
            //if (Input.GetKey(KeyCode.Space))
            //{
            //    MoveSpeed = _keepSpeed;
                
            //}
            //else
            //{
            //    MoveSpeed = 0;
            //}
            StickMove();
        }
        for (int i = 0; i < _wall.Length; i++)
        {

            _wall[i].transform.position -= new Vector3(Time.deltaTime * MoveSpeed, 0);


            if (_wall[i].transform.position.x <= EndPos.position.x)
            {
                _wall[i].transform.position = StartPos.position;
            }
        }

    }
    private void StickMove()
    {
        // ���݂̃Q�[���p�b�h���
        var current = Gamepad.current;

        // �Q�[���p�b�h�ڑ��`�F�b�N
        if (current == null)
            return;

        // ���X�e�B�b�N���͎擾
        var leftStickValue = current.leftStick.ReadValue();

        //���ɂ͓����Ȃ�
        if (leftStickValue.x < 0)
        {
            return;
        }
        else if (leftStickValue.x > 0f)
        {
            //transform.Translate(-(leftStickValue.x * MoveSpeed * Time.deltaTime), 0, 0);
            MoveSpeed = leftStickValue.x * i;
        }
        else if (leftStickValue.x == 0)
        {
            MoveSpeed = 0;
        }
        _obon.MisalignmentOfSweetsCausedByMovement(leftStickValue.x);
    }

    public void OnTapAdvance()
    {
        MoveSpeed = _keepSpeed;
    }
    public void ExitTapAdvance()
    {
        MoveSpeed = 0;
    }

}
