using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{
	[SerializeField, Tooltip("プレイヤーの移動速度")] float _playerSpeed = 2;
	//[SerializeField, Tooltip("ジャイロの速度")] float _playerGyroSpeed;
	[SerializeField, Tooltip("ジャイロを受け付ける間隔")] float _gyroTime;

	[Space(10)]

	[SerializeField, Tooltip("レーンの位置")] Transform[] _raneNum;
	[Tooltip("プレイヤーの現在地")] private int _nowPos;

    //フリック関連の情報
	private Vector3 _touchStartPos;
	private Vector3 _touchEndPos;
	float _flickValueY;
	[SerializeField, Tooltip("フリックの感度")] float _flickValue;

	SpriteRenderer _sp;

	//プレイヤーの現在地をプロパティ化
	public int NowPos
    {
		get { return _nowPos; }
    }

	float time;

	void Start()
	{
		_nowPos = 1;
		gameObject.transform.position = _raneNum[_nowPos].position;
		_sp = GetComponent<SpriteRenderer>();
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

		StickMove();

		
	}

	/// <summary>
	/// プレイヤーの移動
	/// </summary>
	private void Move()
	{
		//gameObject.transform.position = _raneNum[_nowPos].position;
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, _raneNum[_nowPos].position.y, gameObject.transform.position.z);
		//Debug.Log(_nowPos);
	}

	/// <summary>
	/// スティックによるプレイヤーの移動
	/// </summary>
	private void StickMove()
    {
		// 現在のゲームパッド情報
		var current = Gamepad.current;

		// ゲームパッド接続チェック
		if (current == null)
			return;

		// 左スティック入力取得
		var leftStickValue = current.leftStick.ReadValue();

		if (leftStickValue.x < 0)
        {
			return;
        }
		transform.Translate(leftStickValue.x * _playerSpeed * Time.deltaTime, 0, 0);
	}

	/// <summary>
	/// フリックの量を計算
	/// </summary>
	void FlickDirection()
	{
		//flickValue_x = _touchEndPos.x - _touchStartPos.x;
		_flickValueY = _touchEndPos.y - _touchStartPos.y;
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
        if (_nowPos >= _raneNum.Length - 1)
        {
			Debug.Log("これ以上下にいけません");
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
        if (_nowPos == 0)
        {
			Debug.Log("これ以上下にいけません");
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
			if(acceleration.x > 40)
            {
				_sp.color = Color.red;
            }

			if(acceleration.x == 0)
            {
				_sp.color = Color.white;
            }

			if(acceleration.x < -40)
            {
				_sp.color = Color.green;
            }

		}
	}

}