using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	[SerializeField, Tooltip("プレイヤーの移動速度")] float _playerSpeed = 2;
	[SerializeField, Tooltip("ジャイロの速度")] float _playerGyroSpeed;
	[SerializeField, Tooltip("ジャイロを受け付ける感覚")] float _gyroTime;

	[Space(10)]

	[SerializeField, Tooltip("上のレーン")] float _upLane;
	[SerializeField, Tooltip("真ん中のレーン")] Vector2 _centerLane;
	[SerializeField, Tooltip("下のレーン")] float _downLane;

	Rigidbody _rb;
	
	float time;

	void Start()
	{
		_rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		Move();
		//GetGyro();
		Up();
		Down();
	}

	/// <summary>
	/// プレイヤーの移動
	/// </summary>
    private void Move()
	{ 
		//自動で右に進む
		_rb.velocity = new Vector2(_playerSpeed, _rb.velocity.y);
	}

	private void Up()
    {
		if (Input.GetKeyDown(KeyCode.W))
		{

		}
    }
    private void Down()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {

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