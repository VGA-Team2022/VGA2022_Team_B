using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;



public class StageMove : MonoBehaviour
{
    [Header("Pram")]
    [Tooltip("�ړ����x"),SerializeField] private float _moveSpeed = 5f;
    public float MoveSpeed  
        { get { return _moveSpeed; } set { _moveSpeed = value; } }

    [SerializeField] Obon _obon;

    /// <summary>��~���鎞��Speed�̒l������Ă���</summary>
    [HideInInspector] public float _keepSpeed;

    private Material _targetMaterial;

    private StageTypeChange _stageTypeChange;

    /// <summary>UV�X�N���[�����x�������̂Œ����̈�</summary>
    [Tooltip("���x����"), SerializeField] private float _speedRatio = 0.1f;

    public float SpeedRatio => _speedRatio;

    private Vector2 offset;

    private bool isSetMaterial;

    [SerializeField] float _gyroSpeed = 1.2f;
    void Start()
    {
        _stageTypeChange = this.gameObject.GetComponent<StageTypeChange>();
        _keepSpeed = MoveSpeed;
        MoveSpeed = 0;
        _obon = _obon.gameObject.GetComponent<Obon>();
        isSetMaterial = false;
    }

    void Update()
    {
        if (!isSetMaterial)
        {
            _targetMaterial = _stageTypeChange.CurrentMaterial;
            offset = _targetMaterial.mainTextureOffset;
            isSetMaterial = true;
        }


        if (!GameManager.IsAppearDoorObj)
        {
            StickMove();

            offset.x += MoveSpeed * SpeedRatio * Time.deltaTime;
            //Debug.Log($"�w��offset{offset.x}:Materialoffset{_targetMaterial.mainTextureOffset}");
            _targetMaterial.mainTextureOffset = offset;
        }

        else
        {
            MoveSpeed = 0;
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
            GameManager.IsStop = false;
            //transform.Translate(-(leftStickValue.x * MoveSpeed * Time.deltaTime), 0, 0);
            MoveSpeed = _keepSpeed;

        }
        else if (leftStickValue.x == 0)
        {
            GameManager.IsStop = true;
            //AudioManager.Instance.CriAtomPlay(CueSheet.SE, "SE_player_footsteps1");
            MoveSpeed = 0;
        }

        //��������X���Ă���ɌX���悤�ɂ���
        if(_obon.Movement >= 0)
        {
            _obon.MisalignmentOfSweetsCausedByMovement(leftStickValue.x * _gyroSpeed);
        }
        else if(_obon.Movement < 0)
        {
            _obon.MisalignmentOfSweetsCausedByMovement(-leftStickValue.x * _gyroSpeed);
        }
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