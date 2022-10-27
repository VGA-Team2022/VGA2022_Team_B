using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Dog : MonoBehaviour
{
    /// <summary>
    /// エネミーのスピード
    /// </summary>
    [Tooltip("エネミーにアタッチする"), SerializeField] private float _speed = 2.0f;
    public float Speed
        { get { return _speed; } set { _speed = value; } }

    private StageMove _stageMove;

    /// <summary>エネミーの軸移動方向</summary>
    private float _startPosX = 0;

    private void Start()
    {
        _stageMove = GameObject.Find("StageManager_Yasiki").GetComponent<StageMove>();
        Speed = _stageMove.MoveSpeed;
        _startPosX = transform.position.x;
        //GetComponent<Transform>().position = this.gameObject.transform.position;
    }

    /// <summary>
    /// エネミーの移動
    /// </summary>
    void FixedUpdate()
    {
        //スタート位置によって進行方向を変える
        if (_startPosX >= 0)
        {
            //x += 1;//背景スピード*方向
           this.gameObject.transform.position -= new Vector3(Time.deltaTime * Speed, 0);
        }
        else 
        {
            this.gameObject.transform.position += new Vector3(Time.deltaTime * Speed, 0);
        }


        //if (x <= 30 || x >= -30)
        //{
        //    Destroy(this.gameObject);
        //}

        if (transform.position.x <= -30 || transform.position.x >= 30)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("メイド「あああああああああああ」");
        }
    }
}
