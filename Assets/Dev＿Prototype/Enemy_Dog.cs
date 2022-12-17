using UnityEngine;

public enum DogType
{
    Outrun,//走り抜ける
    Stop//突然止まる
}

public class Enemy_Dog : MonoBehaviour
{
    [Tooltip("犬の種類"), SerializeField] private DogType _dogType = DogType.Outrun;

    [Tooltip("止まる犬がどこで止まるかの値調整"), SerializeField, Range(0f, 10f)] private float _stopRange = 1;

    /// <summary>エネミーのスピード</summary>
    private float _speed = 2.0f;
    public float Speed
        { get { return _speed; } set { _speed = value; } }

    private StageMove _stageMove;


    /// <summary>エネミーの軸移動方向</summary>
    private float _startPosX = 0;

    /// <summary>Animation</summary>
    private Animator _anim = default;

    /// <summary>生成位置のx軸が負の値かの判定</summary>
    private bool isSpawnNegativeX = false;
    /// <summary>止まる犬の座る判定</summary>
    private bool isStop = false;

   
    private void Start()
    {
        _stageMove = GameObject.Find("StageManager").GetComponent<StageMove>();
        _anim= this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<Animator>();
        Speed = _stageMove._keepSpeed;
        _startPosX = transform.position.x;
        isStop = false;

        if (this.gameObject.transform.position.x <= 0)//生成位置が０より小さいのでTrue
        {
            isSpawnNegativeX = true;

        }
        else 
        {
            isSpawnNegativeX = false;
            this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }

        if (this.gameObject.transform.position.z <= -6)
        {
            this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder= 15;
        }

    }

    private void Update()
    {
        if (isStop && isSpawnNegativeX)//座る判定が正になったら且つ進行方向が右(正方向)の犬の場合
        {
            Speed = -_stageMove.MoveSpeed;//ステージと同じスピードにする
        }
        else if (isStop && !isSpawnNegativeX)//座る判定が正になったら且つ進行方向が左(負方向)の犬の場合
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

                //スタート位置によって進行方向とSpriteの向きを変える
                if (!isSpawnNegativeX)//Playerの進行方向(画面右端から左端に向けて)からくる挙動
                {
                        //this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
                        this.gameObject.transform.position -= new Vector3(Time.deltaTime * Speed, 0);
                    }
                    else
                    {
                        this.gameObject.transform.position += new Vector3(Time.deltaTime * Speed, 0);
                    }

                break;

            case DogType.Stop:

                //スタート位置によって進行方向とSpriteの向きを変える
                if (!isSpawnNegativeX)//Playerの進行方向(画面右端から左端に向けて)からくる挙動
                {
                    //this.gameObject.transform.GetChild(0).gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
                    this.gameObject.transform.position -= new Vector3(Time.deltaTime * Speed, 0);

                    //ｘ軸の_stopRngeＭ地点に入ったら止まる(Playerの場所)
                    if (transform.position.x >= -_stopRange && transform.position.x <= _stopRange)
                    {
                        isStop = true;//アニメーションの座る判定を正にする
                    }
                    
                }
                else//Playerの後ろ方向からくる挙動
                {
                    this.gameObject.transform.position += new Vector3(Time.deltaTime * Speed, 0);

                    //ｘ軸の_stopRange地点に入ったら止まる(Playerの場所)
                    if (transform.position.x >= -_stopRange && transform.position.x <= _stopRange)
                    {
                        isStop = true;//アニメーションの座る判定を正にする
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
            _anim.SetBool("isStop",isStop);
        }
    }


    //あたり判定
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("メイド「あああああああああああ」");
        }
    }
}
