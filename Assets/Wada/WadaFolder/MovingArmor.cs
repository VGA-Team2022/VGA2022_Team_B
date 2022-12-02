using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingArmor : MonoBehaviour
{
    [Tooltip("流れるスピード"), SerializeField]
    float _scrollSpeed;

    [Tooltip("動くのにかかる時間"), SerializeField]
    float _moveSpeed;

    [Tooltip("オブジェクトプールを使うか否か"), SerializeField]
    bool _objectpPool = false;

    private Transform _startPos;
    private Transform _finishPos;

    private int _startTime = 0;

    private float _moveTime = 0;//動いてる時間

    private bool _move = false;

    //////////////////TestZone↓/////////////////////


    [SerializeField] Transform a;
    [SerializeField] Transform b;


    //////////////////TestZone↑/////////////////////


    public Transform StartPos
    {
        get
        {
            return _startPos;
        }
        set
        {
            _startPos = value;
        }
    }
    public Transform FinishPos
    {
        get
        {
            return _finishPos;
        }
        set
        {
            _finishPos = value;
        }
    }

    private void OnEnable()
    {
        if (_objectpPool)
        {

        }
    }

    void Start()
    {
        StartCoroutine("AAAAAAA");
        if (!_objectpPool)
        {

        }
        _startPos = a;//Test
        _finishPos = b;//Test

        this.transform.position = new Vector3(this.transform.position.x, _startPos.position.y, _startPos.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ArmorMove();
        if(_moveTime < _moveSpeed && _move)
        {
            _moveTime += Time.deltaTime;
        }
    }

    void ArmorMove()
    {
        if(_move)
        {
            this.transform.position = Vector3.Lerp(new Vector3(this.transform.position.x, StartPos.position.y, StartPos.position.z), new Vector3(this.transform.position.x, FinishPos.position.y, FinishPos.position.z), (_moveTime / _moveSpeed));
        }
    }

    IEnumerator AAAAAAA()
    {
        yield return new WaitForSecondsRealtime(3);
        _move = true;
    }

}
