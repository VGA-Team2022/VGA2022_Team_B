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
		Up();
		Down();

		//GetGyro();
	}

	/// <summary>
	/// プレイヤーの移動
	/// </summary>
	private void Move()
	{ 
		//自動で右に進む?
		//_rb.velocity = new Vector2(_playerSpeed, _rb.velocity.y);
		//レーンを更新
		gameObject.transform.position = _raneNum[_nowPos].position;
	}

	/// <summary>
	/// 上のレーンに移動
	/// </summary>
	private void Up()
    {
		if (Input.GetKeyDown(KeyCode.W))
		{
			if(_nowPos >= _raneNum.Length - 1)
            {
				return;
            }
			else if(_nowPos < _raneNum.Length)
            {
				_nowPos++;
            }
		}
    }
	/// <summary>
	/// 下のレーンに移動
	/// </summary>
    private void Down()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
			if (_nowPos == 0)
			{
				return;
			}
			else if (_nowPos > 0)
			{
				_nowPos--;
			}
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
		}
	}

}