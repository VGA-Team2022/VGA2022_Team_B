using DG.Tweening;
using UnityEngine;

public class CoconutMove : MonoBehaviour
{
    [Tooltip("ココナッツが止まる場所")]
    [Range(0f, 10f)]
    [SerializeField]
    private float _stopRange = 1;
    [Tooltip("1周するのに何秒かかるか")]
    [SerializeField]
    private float _duration = 1f;
    [SerializeField]
    private float _speed = 2f;
    [Tooltip("オブジェクトが消える範囲")]
    [SerializeField]
    private float _destroyRange = 30;

    private StageMove _stageMove = default;
    /// <summary>生成位置のx軸が負の値かの判定</summary>
    private bool _isSpawnNegativeX = false;
    /// <summary>止まる犬の座る判定</summary>
    private bool _isStop = false;

    private void Start()
    {
        Rotate();

        _stageMove = GameObject.Find("StageManager").GetComponent<StageMove>();
        _speed = _stageMove.KeepSpeed;
        _isStop = false;

        if (gameObject.transform.position.x <= 0)//生成位置が0より小さいのでTrue
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
        Soundmanager.InstanceSound.PlayerMoveSE(Soundmanager.SE_Type.Enemy_Rooling);

        if (_isStop && _isSpawnNegativeX)
        {
            _speed = -_stageMove.MoveSpeed; //ステージと同じスピードにする
        }
        else if (_isStop && !_isSpawnNegativeX)
        {
            _speed = _stageMove.MoveSpeed; //マイナスをかけステージと同じ方向とスピードにする
        }
    }

    void FixedUpdate()
    {
        //スタート位置によって進行方向とSpriteの向きを変える
        if (!_isSpawnNegativeX) //Playerの進行方向(画面右端から左端に向けて)からくる挙動
        {
            gameObject.transform.position -= new Vector3(Time.deltaTime * _speed, 0);

            // Playerの周辺で止まる
            if (transform.position.x >= -_stopRange && transform.position.x <= _stopRange)
            {
                _isStop = true;
            }

        }
        else//Playerの後ろ方向からくる挙動
        {
            gameObject.transform.position += new Vector3(Time.deltaTime * _speed, 0);

            //Playerの周辺で止まる
            if (transform.position.x >= -_stopRange && transform.position.x <= _stopRange)
            {
                _isStop = true;
            }
        }
        //画面外に行ったら消える
        if (transform.position.x <= _destroyRange || transform.position.x >= _destroyRange)
        {
            Destroy(gameObject);
        }
    }

    private void Rotate()
    {
        transform
            .DORotate(Vector3.forward * 360f, _duration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obon obon))
        {
            Soundmanager.InstanceSound.PlayAudioClip(Soundmanager.SE_Type.Enemy_Coconut);
            _isStop = true;
            obon.Hit(transform.position.x);
        }
    }
}
