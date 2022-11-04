using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	//[SerializeField, Tooltip("プレイヤーの移動速度")] float _playerSpeed = 2;
	[SerializeField, Tooltip("ジャイロの速度")] float _playerGyroSpeed;
	[SerializeField, Tooltip("ジャイロを受け付ける間隔")] float _gyroTime;

	[Space(10)]

	[SerializeField, Tooltip("レーンの位置")] Transform[] _raneNum;
	[Tooltip("プレイヤーの現在地")] private int _nowPos;

    //フリック関連の情報
	private Vector3 _touchStartPos;
	private Vector3 _touchEndPos;
	//float _flickValueX;
	float _flickValueY;
	[SerializeField, Tooltip("フリックの感度")] float _flickValue;

	//プレイヤーの現在地をプロパティ化
	public int NowPos
    {
		get { return _nowPos; }
    }

	Rigidbody _rb;
	float time;

	void Start()
	{
		_nowPos = 1;
		gameObject.transform.position = _raneNum[_nowPos].position;
		_rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		Move();
		if (Input.GetMouseButtonDown(0))
		{
			_touchStartPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
		}
		if (Input.GetMouseButtonUp(0))
		{
			_touchEndPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
			FlickDirection();
			GetDirection();
		}

		GetGyro();
	}

	/// <summary>
	/// プレイヤーの移動
	/// </summary>
	private void Move()
	{ 
		//レーンを更新
		gameObject.transform.position = _raneNum[_nowPos].position;
	}

	/// <summary>
	/// フリックの量を計算
	/// </summary>
	void FlickDirection()
	{
		//flickValue_x = _touchEndPos.x - _touchStartPos.x;
		_flickValueY = _touchEndPos.y - _touchStartPos.y;
		Debug.Log("y スワイプ量は" + _flickValueY);
	}

	/// <summary>
	/// フリック量に応じて上に行くか下に行くか
	/// </summary>
	void GetDirection()
	{
		if (_flickValueY > _flickValue)
		{
			Up();
		}
		if (_flickValueY < -_flickValue)
        {
			Down();
        }
	}
	/// <summary>
	/// 上のレーンに移動
	/// </summary>
	private void Up()
    {
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //	if(_nowPos >= _raneNum.Length - 1)
        //          {
        //		return;
        //          }
        //	else if(_nowPos < _raneNum.Length)
        //          {
        //		_nowPos++;
        //          }
        //}
        if (_nowPos >= _raneNum.Length - 1)
        {
            return;
        }
        else if (_nowPos < _raneNum.Length)
        {
            _nowPos++;
        }
    }
	/// <summary>
	/// 下のレーンに移動
	/// </summary>
    private void Down()
    {
		//if(Input.GetKeyDown(KeyCode.S))
  //      {
		//	if (_nowPos == 0)
		//	{
		//		return;
		//	}
		//	else if (_nowPos > 0)
		//	{
		//		_nowPos--;
		//	}
		//}
        if (_nowPos == 0)
        {
            return;
        }
        else if (_nowPos > 0)
        {
            _nowPos--;
        }

    }
    /// <summary>
    /// とりあえずのジャイロ操作
    /// </summary>
    private void GetGyro()
    {
		time += Time.deltaTime;
		if (time % _gyroTime == 0)
		{
			Vector3 acceleration = Input.acceleration;
			acceleration.x *= 20;
			acceleration.y *= 0;
			acceleration.z = 0;
			Physics2D.gravity = acceleration;
			Debug.Log(acceleration.x);
		}
	}

}