using UnityEngine;
using System.Collections;

public enum Dogs
{
    Stop, //一時停止する
    Run //止まらず走る
}

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Dogs _dogs = Dogs.Run;

    ///<summary> エネミーのスピード <summary>
    private float _speed = 2.0f;

    private float _stopTime = 2f;

    private int _startPosX = 0;

    void FixedUpdate()
    {
        switch(_dogs) //エネミーの移動
        {
            case Dogs.Run:
                if (_startPosX >= 0)
                {
                    SpriteRenderer children = GetComponentInChildren<SpriteRenderer>();
                    children.flipX = true;
                    transform.position -= new Vector3(Time.deltaTime * _speed, 0);
                }
                else
                {
                    transform.position += new Vector3(Time.deltaTime * _speed, 0);
                }
                break;

            case Dogs.Stop:
                if (_startPosX >= 0)
                {
                    //走ってる犬
                    transform.position += new Vector3(Time.deltaTime * _speed, 0);
                }
                else//Playerの後ろ方向から
                {
                    transform.position += new Vector3(Time.deltaTime * _speed, 0);

                    //プレイヤーの前後1ｍ圏内に入ったときの挙動
                    if (transform.position.x >= -1 && transform.position.x <= 1)
                    {
                        StartCoroutine(StopDogTime(_stopTime));
                    }
                }
                break;
        }

        if (transform.position.x <= 30 || transform.position.x >= -30) 
        {
            Destroy(gameObject);
        }
    }

    /// <summary> 止まる犬停止中 </summary>
    private IEnumerator StopDogTime(float time)
    {
        _speed = 0;
        yield return new WaitForSeconds(time);
    }
}
