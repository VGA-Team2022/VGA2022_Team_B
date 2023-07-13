using UnityEngine;

public enum DogType
{
    [Tooltip("走り抜ける")]
    Outrun,
    [Tooltip("突然止まる")]
    Stop
}

public class EnemyDogScript : MonoBehaviour
{
    [Tooltip("犬の種類")]
    [SerializeField] private DogType _dogType = DogType.Outrun;
    [Range(0f, 10f)]
    [Tooltip("止まる犬がどこで止まるかの値調整")]
    [SerializeField] private float _stopRange = 1;

    /// <summary>エネミーのスピード</summary>
    private float _speed = 2.0f;

    /// <summary>犬生成Manager/// </summary>
    private EnemyInstanceManager _enemyInstanceManager;
    public EnemyInstanceManager EnemyInstanceManager { get => _enemyInstanceManager; set => _enemyInstanceManager = value; }

    private StageMove _stageMove;

    /// <summary>Animation</summary>
    private Animator _anim = default;

    /// <summary>生成位置のx軸が負の値かの判定</summary>
    private bool _isSpawnNegativeX = false;
    /// <summary>止まる犬の座る判定</summary>
    private bool _isStop = false;
    private bool _isForwardFast = false;

    private void Start()
    {
        _stageMove = GameObject.Find("StageManager").GetComponent<StageMove>();
        _anim = transform.GetChild(0).gameObject.GetComponentInChildren<Animator>();
        _speed = _stageMove.KeepSpeed;
        _isStop = false;

        if (transform.position.x <= 0)//生成位置が０より小さいのでTrue
        {
            _isSpawnNegativeX = true;

        }
        else
        {
            _isSpawnNegativeX = false;
            transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }

        if (transform.position.z <= -4 && transform.position.z >= -7)
        {
            transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 8;
        }
        else if (transform.position.z <= -6)
        {
            transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 15;
        }

        SoundManager.InstanceSound.PlayAudioClip(
            _dogType == DogType.Outrun ?
            SoundManager.SE_Type.Enemy_SmallDog_Cry : SoundManager.SE_Type.Enemy_BigDog_Cry);
    }

    private void Update()
    {
        if (_isStop)
        {
            _speed = _isSpawnNegativeX ? -_stageMove.MoveSpeed : _stageMove.MoveSpeed;
        }
    }

    /// <summary> エネミーの移動 </summary>
    void FixedUpdate()
    {
        switch (_dogType)
        {
            case DogType.Outrun:
                InvokeRepeating("SmallDogBreathPlayAudio", 0, 2f);
                //スタート位置によって進行方向とSpriteの向きを変える
                if (!_isSpawnNegativeX)//Playerの進行方向(画面右端から左端に向けて)からくる挙動
                {
                    transform.position -= new Vector3(Time.deltaTime * _speed, 0);
                }
                else
                {
                    transform.position += new Vector3(Time.deltaTime * _speed, 0);
                    if (!_isForwardFast && transform.position.x >= 25)
                    {
                        _enemyInstanceManager.Dog(gameObject);
                        _isForwardFast = true;
                    }
                }
                break;

            case DogType.Stop:
                InvokeRepeating("BigDogBreathPlayAudio", 0, 2f);
                //スタート位置によって進行方向とSpriteの向きを変える
                if (!_isSpawnNegativeX)//Playerの進行方向(画面右端から左端に向けて)からくる挙動
                {
                    transform.position -= new Vector3(Time.deltaTime * _speed, 0);

                    //ｘ軸の_stopRange地点に入ったら止まる(Playerの場所)
                    if (transform.position.x >= -_stopRange && transform.position.x <= _stopRange)
                    {
                        _isStop = true;//アニメーションの座る判定を正にする
                    }
                }
                else//Playerの後ろ方向からくる挙動
                {
                    transform.position += new Vector3(Time.deltaTime * _speed, 0);

                    //ｘ軸の_stopRange地点に入ったら止まる(Playerの場所)
                    if (transform.position.x >= -_stopRange && transform.position.x <= _stopRange)
                    {
                        _isStop = true;//アニメーションの座る判定を正にする
                    }
                }
                break;
        }

        //画面外に行ったら消える
        if (transform.position.x <= -30 || transform.position.x >= 30)
        {
            Destroy(gameObject);
        }

        //if (_anim)
        //{
        //    _anim.SetBool("isStop", _isStop);
        //}
    }

    private void BigDogBreathPlayAudio()
    {
        SoundManager.InstanceSound.PlayAudioClip(SoundManager.SE_Type.Enemy_BigDog_Breath);
    }

    private void SmallDogBreathPlayAudio()
    {
        SoundManager.InstanceSound.PlayAudioClip(SoundManager.SE_Type.Enemy_SmallDog_Breath);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obon obon))
        {
            _isStop = true;
            obon.Hit(transform.position.x);
        }
    }
}
