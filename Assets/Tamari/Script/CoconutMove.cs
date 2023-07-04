using DG.Tweening;
using UnityEngine;

public class CoconutMove : GimmickBase
{
    [Tooltip("ココナッツが止まる場所")]
    [Range(0f, 10f)]
    [SerializeField]
    private float _stopRange = 1;
    [Tooltip("1周するのに何秒かかるか")]
    [SerializeField]
    private float _duration = 1f;

    private float _speed = 2f;
    /// <summary>生成位置のx軸が負の値かの判定</summary>
    private bool _isSpawnNegativeX = false;
    /// <summary>止まる犬の座る判定</summary>
    private bool _isStop = false;

    private void Start()
    {
        Rotate();

        _speed = StageMovement.KeepSpeed;
        var sprite = transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>();

        if (transform.position.x <= 0)//生成位置が0より小さいのでTrue
        {
            _isSpawnNegativeX = true;
        }
        else
        {
            _isSpawnNegativeX = false;
            sprite.flipX = true;
        }

        if (transform.position.z <= -4 && transform.position.z >= -7)
        {
            sprite.sortingOrder = 8;
        }
        else if (transform.position.z <= -6)
        {
            sprite.sortingOrder = 15;
        }
        SoundManager.InstanceSound.PlayerMoveSE(SoundManager.SE_Type.Enemy_Rooling);
    }

    private void Update()
    {
        if (_isStop && _isSpawnNegativeX)
        {
            _speed = -StageMovement.MoveSpeed; //ステージと同じスピードにする
        }
        else if (_isStop && !_isSpawnNegativeX)
        {
            _speed = StageMovement.MoveSpeed; //マイナスをかけステージと同じ方向とスピードにする
        }
    }

    void FixedUpdate()
    {
        //スタート位置によって進行方向とSpriteの向きを変える
        if (!_isSpawnNegativeX) //Playerの進行方向(画面右端から左端に向けて)からくる挙動
        {
            transform.position -= new Vector3(Time.deltaTime * _speed, 0);

            // Playerの周辺で止まる
            if (transform.position.x >= -_stopRange && transform.position.x <= _stopRange)
            {
                _isStop = true;
            }

        }
        else//Playerの後ろ方向からくる挙動
        {
            transform.position += new Vector3(Time.deltaTime * _speed, 0);

            //Playerの周辺で止まる
            if (transform.position.x >= -_stopRange && transform.position.x <= _stopRange)
            {
                _isStop = true;
            }
        }
        //画面外に行ったら消える
        if (transform.position.x <= -30f || transform.position.x >= 30f)
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
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.TryGetComponent(out Obon obon))
        {
            SoundManager.InstanceSound.PlayAudioClip(SoundManager.SE_Type.Enemy_Coconut);
            _isStop = true;
            obon.Hit(transform.position.x);
        }
    }
}
