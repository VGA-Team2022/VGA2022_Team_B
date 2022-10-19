using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	Rigidbody _rb;
	float time;
	[SerializeField] float _playerSpeed = 2;
	[SerializeField] float _playerGyroSpeed;

	void Start()
	{
		_rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		Move();
		GetGyro();
	}

	/// <summary>
	/// �����Ői��
	/// </summary>
    private void Move()
    {
		_rb.velocity = new Vector2(_playerSpeed, _rb.velocity.y);
	}

	/// <summary>
	/// �Ƃ肠�����̃W���C������(1�Ė�)
	/// </summary>
	private void GetGyro()
    {
		time += Time.deltaTime;
		if (time % 15 == 0)
		{
			Vector3 acceleration = Input.acceleration;
			acceleration.x *= 20;
			acceleration.y *= 40;
			acceleration.z = 0;
			Physics2D.gravity = acceleration;
		}
	}

	/// <summary>
	/// �W���C������(2�Ė�)
	/// </summary>
	private void GetGyro2()
    {
		var dire = Vector3.zero;

		dire.z = Input.acceleration.x * -1;
		dire.x = Input.acceleration.y;

		dire *= Time.deltaTime;
		transform.Rotate(dire * _playerGyroSpeed);
	}


}