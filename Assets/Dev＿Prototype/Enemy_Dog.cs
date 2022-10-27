using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DogType
{
    Outrun,//走り抜ける
    Stop//突然止まる
}

public class Enemy_Dog : MonoBehaviour
{
    [Tooltip("犬の種類"), SerializeField] private DogType _dogType = DogType.Outrun;

    /// <summary>エネミーのスピード</summary>
    private float _speed = 2.0f;
    public float Speed
        { get { return _speed; } set { _speed = value; } }

    private StageMove _stageMove;

    /// <summary>止まる犬の止まっている時間</summary>
    private int _stopTime = 2;

    /// <summary>エネミーの軸移動方向</summary>
    private float _startPosX = 0;

    private void Start()
    {
        _stageMove = GameObject.Find("StageManager_Yasiki").GetComponent<StageMove>();
        Speed = _stageMove.MoveSpeed;
        _startPosX = transform.position.x;
    }

    /// <summary>
    /// エネミーの移動
    /// </summary>
    void FixedUpdate()
    {
        switch (_dogType)
        {
            case DogType.Outrun:

                    //スタート位置によって進行方向を変える
                    if (_startPosX >= 0)
                    {
                        this.gameObject.transform.position -= new Vector3(Time.deltaTime * Speed, 0);
                    }
                    else
                    {
                        this.gameObject.transform.position += new Vector3(Time.deltaTime * Speed, 0);
                    }

                break;

            case DogType.Stop:

                //スタート位置によって進行方向を変える
                if (_startPosX >= 0)//Playerの進行方向からくる挙動
                {
                    this.gameObject.transform.position -= new Vector3(Time.deltaTime * Speed, 0);

                    //ｘ軸の-1～1Ｍ地点に入ったら止まる(Playerの場所)
                    if (transform.position.x >= -1 && transform.position.x <= 1)
                    {
                        StartCoroutine(StopDog(_stopTime));
                    }
                    
                }
                else//Playerの後ろ方向からくる挙動
                {
                    this.gameObject.transform.position += new Vector3(Time.deltaTime * Speed, 0);

                    //ｘ軸の-1～1Ｍ地点に入ったら止まる(Playerの場所)
                    if (transform.position.x >= -1 && transform.position.x <= 1)
                    {
                        StartCoroutine(StopDog(_stopTime));
                    }
                }


                break;
        }


        //画面外に行ったら消える
        if (transform.position.x <= -30 || transform.position.x >= 30)
        {
            Destroy(this.gameObject);
        }
    }


    //止まる犬
    private IEnumerator StopDog(float time)
    {
        Speed = 0;
        yield return new WaitForSeconds(time);
        Speed = _stageMove.MoveSpeed;
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
