using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutMove : MonoBehaviour
{
    [Tooltip("ココナッツが止まる場所"), SerializeField, Range(0f, 10f)]
    private float _stopRange = 1;

    [SerializeField]
    private float _speed = 2.0f;
    public float Speed { get { return _speed; } set { _speed = value; } }

    [SerializeField, Tooltip("オブジェクトが消える範囲")]
    private float _destroyRange = 30;

    private EnemyInstansManager _enemyInstansManager = default;
    public EnemyInstansManager EnemyInstansManager { get => _enemyInstansManager; set => _enemyInstansManager = value; }

    private StageMove _stageMove = default;

    /// <summary>エネミーの軸移動方向</summary>
    private float _startPosX = 0;

    /// <summary>生成位置のx軸が負の値かの判定</summary>
    private bool _isSpawnNegativeX = false;

    /// <summary>止まる犬の座る判定</summary>
    private bool _isStop = false;

    /// <summary>Animation</summary>
    private Animator _anim = default;

    private void Start()
    {
        _stageMove = GameObject.Find("StageManager").GetComponent<StageMove>();
        // _anim = gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<Animator>();
        Speed = _stageMove._keepSpeed;
        _startPosX = transform.position.x;
        _isStop = false;

        if (gameObject.transform.position.x <= 0)//生成位置が０より小さいのでTrue
        {
            _isSpawnNegativeX = true;
        }
        else
        {
            _isSpawnNegativeX = false;
            gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }

        if (gameObject.transform.position.z <= -4 && gameObject.transform.position.z >= -7)
        {
            gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 8;
        }
        else if (gameObject.transform.position.z <= -6)
        {
            gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 15;
        }
    }
    private void Update()
    {
        if (_isStop && _isSpawnNegativeX)
        {
            Speed = -_stageMove.MoveSpeed;//ステージと同じスピードにする
        }
        else if (_isStop && !_isSpawnNegativeX)
        {
            Speed = _stageMove.MoveSpeed;//マイナスをかけステージと同じ方向とスピードにする
        }

    }

    void FixedUpdate()
    {
        //スタート位置によって進行方向とSpriteの向きを変える
        if (!_isSpawnNegativeX)//Playerの進行方向(画面右端から左端に向けて)からくる挙動
        {
            gameObject.transform.position -= new Vector3(Time.deltaTime * Speed, 0);

            // Playerの周辺で止まる
            if (transform.position.x >= -_stopRange && transform.position.x <= _stopRange)
            {
                _isStop = true;
            }

        }
        else//Playerの後ろ方向からくる挙動
        {
            gameObject.transform.position += new Vector3(Time.deltaTime * Speed, 0);

            //Playerの周辺で止まる
            if (transform.position.x >= -_stopRange && transform.position.x <= _stopRange)
            {
                _isStop = true;
            }
        }
        //画面外に行ったら消える
        if (transform.position.x <= _destroyRange || transform.position.x >= _destroyRange)
        {
            Destroy(this.gameObject);
        }

        if (_anim)
        {
            // ココナッツのアニメーション次第
            // _anim.SetBool("isStop", isStop);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obon")
        {
            _isStop = true;
            if (collision.gameObject.TryGetComponent(out Obon obon))
            {
                obon.Hit(this.transform.position.x);
            }
        }
    }

}
