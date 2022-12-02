using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;



public class StageMove : MonoBehaviour
{
    [Header("アタッチするもの")]
    [SerializeField] private GameObject[] _wall;
    [SerializeField] public Transform StartPos;
    [SerializeField] private Transform _centerPos;
    [SerializeField] public Transform EndPos;

    [Header("Pram")]
    [Tooltip("移動速度"),SerializeField] private float _moveSpeed = 5f;
    public float MoveSpeed  
        { get { return _moveSpeed; } set { _moveSpeed = value; } }

    [SerializeField] Obon _obon;
    [SerializeField] int i;
    /// <summary>停止する時にSpeedの値を取っておく</summary>
    private float _keepSpeed;
    /// <summary>スマホデバッグ用のフラグ</summary>
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
        // 現在のゲームパッド情報
        var current = Gamepad.current;

        // ゲームパッド接続チェック
        if (current == null)
            return;

        // 左スティック入力取得
        var leftStickValue = current.leftStick.ReadValue();

        //左には動かない
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
