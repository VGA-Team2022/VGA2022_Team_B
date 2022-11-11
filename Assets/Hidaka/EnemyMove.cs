using UnityEngine;
using System.Collections;

public enum Dogs
{
    Stop, //一時停止する
    Run //止まらず走る
}

/// <summary>
///エネミー移動
/// </summary>
public class EnemyMove : MonoBehaviour
{
    /*[Header("エネミー")]
    [Tooltip("エネミーのスピード"), SerializeField]*/

    ///<summary> エネミーのスピード <summary>
    private float _speed = 2.0f;

    private float _stopTime = 2f;

    private int _startPosX = 0;

    private StageMove _stageMoves;

    [Tooltip("犬の種類"), SerializeField] private Dogs _dogs = Dogs.Run;

    private EnemyInstansManager _enemyInstansManager;


    /// <summary>エネミーの軸移動方向</summary>
    private int x = 0;

    /// <summary>
    /// エネミーの移動
    /// </summary>
    void FixedUpdate()
    {
        switch(_dogs)
        {
            case Dogs.Run:
                if (_startPosX >= 0)
                {
                    this.gameObject.transform.position -= new Vector3(Time.deltaTime * _speed, 0);
                }
                else
                {
                    this.gameObject.transform.position += new Vector3(Time.deltaTime * _speed, 0);
                }
                break;
            case Dogs.Stop:
                if (_startPosX == 0)
                {
                    //走ってる犬
                    transform.position += new Vector3(Time.deltaTime * _speed, 0);
                }
                else//Playerの後ろ方向から
                {
                    this.gameObject.transform.position += new Vector3(Time.deltaTime * _speed, 0);

                    //プレイヤーの前後1ｍ圏内に入ったときの挙動
                    if (transform.position.x >= -1 && transform.position.x <= 1)
                    {
                        StartCoroutine(StopDogTime(_stopTime));
                    }
                }
                if(transform.position.x <= 25)
                {
                    //画面外に行った後プレイヤーのいるレーンから出現　
                    //プレイヤーのレーンはPlayerのNowPosプロパティ取得しているので、参照可
                    _enemyInstansManager.Dog(this.gameObject);
                }
                break;
        }
        if (transform.position.x <= 30 || transform.position.x >= -30) 
        {
            Destroy(this.gameObject);
        }
    }

    //止まる犬停止中
    private IEnumerator StopDogTime(float time)
    {
        _speed = 0;
        yield return new WaitForSeconds(time);
        _speed = _stageMoves.MoveSpeed;
    }
}
