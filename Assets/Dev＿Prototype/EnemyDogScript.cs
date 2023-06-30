using UnityEngine;

public enum DogType
{
    Outrun,//走り抜ける
    Stop//突然止まる
}

public class EnemyDogScript : MonoBehaviour
{
    [Tooltip("犬の種類"), SerializeField] private DogType _dogType = DogType.Outrun;

    [Tooltip("止まる犬がどこで止まるかの値調整"), SerializeField, Range(0f, 10f)] private float _stopRange = 1;

    /// <summary>エネミーのスピード</summary>
    private float _speed = 2.0f;
    public float Speed
        { get { return _speed; } set { _speed = value; } }

    public EnemyInstansManager EnemyInstansManager { get => _enemyInstansManager; set => _enemyInstansManager = value; }

    private StageMove _stageMove;


    /// <summary>エネミーの軸移動方向</summary>
    private float _startPosX = 0;

    /// <summary>Animation</summary>
    private Animator _anim = default;

    /// <summary>生成位置のx軸が負の値かの判定</summary>
    private bool isSpawnNegativeX = false;
    /// <summary>止まる犬の座る判定</summary>
    private bool _isStop = false;
    /// <summary>犬生成Manager/// </summary>
    private EnemyInstansManager _enemyInstansManager;
    private bool _forwardFast = false;

    private void Start()
    {
        _stageMove = GameObject.Find("StageManager").GetComponent<StageMove>();
        _anim= this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<Animator>();
        Speed = _stageMove.KeepSpeed;
        _startPosX = transform.position.x;
        _isStop = false;

        if (this.gameObject.transform.position.x <= 0)//生成位置が０より小さいのでTrue
        {
            isSpawnNegativeX = true;

        }
        else 
        {
            isSpawnNegativeX = false;
            this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }

        if (this.gameObject.transform.position.z <= -4 && this.gameObject.transform.position.z >= -7)
        {
            this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder= 8;
        }
        else if (this.gameObject.transform.position.z <= -6)
        {
            this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 15;
        }

        if (_dogType == DogType.Outrun)
        {
            Soundmanager.InstanceSound.PlayAudioClip(Soundmanager.SE_Type.Enemy_SmallDog_Cry);
        }
        else 
        {
            Soundmanager.InstanceSound.PlayAudioClip(Soundmanager.SE_Type.Enemy_BigDog_Cry);
        }

    }

    private void Update()
    {
        if (_isStop && isSpawnNegativeX)//座る判定が正になったら且つ進行方向が右(正方向)の犬の場合
        {
            Speed = -_stageMove.MoveSpeed;//ステージと同じスピードにする
        }
        else if (_isStop && !isSpawnNegativeX)//座る判定が正になったら且つ進行方向が左(負方向)の犬の場合
        {
            Speed = _stageMove.MoveSpeed;//マイナスをかけステージと同じ方向とスピードにする
        }

    }


    /// <summary>
    /// エネミーの移動
    /// </summary>
    void FixedUpdate()
    {

        switch (_dogType)
        {
            case DogType.Outrun:
                InvokeRepeating("SmallDogBreathPlayAudio", 0, 2f);
                //スタート位置によって進行方向とSpriteの向きを変える
                if (!isSpawnNegativeX)//Playerの進行方向(画面右端から左端に向けて)からくる挙動
                {
                        //this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
                        this.gameObject.transform.position -= new Vector3(Time.deltaTime * Speed, 0);
                }
                else
                {
                  this.gameObject.transform.position += new Vector3(Time.deltaTime * Speed, 0);
                    if (!_forwardFast && this.gameObject.transform.position.x >= 25)
                    {
                        EnemyInstansManager.Dog(gameObject);
                        _forwardFast = true;
                    }
                    
                }

                break;

            case DogType.Stop:
                InvokeRepeating("BigDogBreathPlayAudio", 0, 2f);
                //スタート位置によって進行方向とSpriteの向きを変える
                if (!isSpawnNegativeX)//Playerの進行方向(画面右端から左端に向けて)からくる挙動
                {
                    //this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
                    this.gameObject.transform.position -= new Vector3(Time.deltaTime * Speed, 0);

                    //ｘ軸の_stopRngeＭ地点に入ったら止まる(Playerの場所)
                    if (transform.position.x >= -_stopRange && transform.position.x <= _stopRange)
                    {
                        _isStop = true;//アニメーションの座る判定を正にする
                    }
                    
                }
                else//Playerの後ろ方向からくる挙動
                {
                    this.gameObject.transform.position += new Vector3(Time.deltaTime * Speed, 0);

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
            Destroy(this.gameObject);
        }

        if (_anim)
        {
            _anim.SetBool("isStop",_isStop);
        }
    }

    private void BigDogBreathPlayAudio()
    {
        Soundmanager.InstanceSound.PlayAudioClip(Soundmanager.SE_Type.Enemy_BigDog_Breath);
    }
    private void SmallDogBreathPlayAudio()
    {
        Soundmanager.InstanceSound.PlayAudioClip(Soundmanager.SE_Type.Enemy_SmallDog_Breath);           
    }

    //あたり判定
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obon")
        {
            _isStop = true;
            if(collision.gameObject.TryGetComponent(out Obon obon))
            {
                obon.Hit(this.transform.position.x);
            }

        }
    }
}
