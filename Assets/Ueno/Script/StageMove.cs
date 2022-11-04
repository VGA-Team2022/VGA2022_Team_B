using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMove : MonoBehaviour
{
    [Header("アタッチするもの")]
    [SerializeField] private GameObject[] _wall;
    [SerializeField] private Transform _startPos;
    [SerializeField] private Transform _endPos;

    [Header("Pram")]
    [Tooltip("移動速度"),SerializeField] private float _moveSpeed = 5f;
    public float MoveSpeed  
        { get { return _moveSpeed; } set { _moveSpeed = value; } }

    private float _keepSpeed;
    private bool isPhonDebug;
    // Start is called before the first frame update
    void Start()
    {
        _keepSpeed = MoveSpeed;
        MoveSpeed = 0;

    }

    void Update()
    {
        if (!isPhonDebug)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                MoveSpeed = _keepSpeed;
            }
            else
            {
                MoveSpeed = 0;
            }
        }

        for (int i = 0; i < _wall.Length; i++)
        {

            _wall[i].transform.position -= new Vector3(Time.deltaTime * MoveSpeed, 0);


            if (_wall[i].transform.position.x <= _endPos.position.x)
            {
                _wall[i].transform.position = _startPos.position;
            }
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
