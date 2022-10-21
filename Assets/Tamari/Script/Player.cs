using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	[SerializeField, Tooltip("�v���C���[�̈ړ����x")] float _playerSpeed = 2;
	[SerializeField, Tooltip("�W���C���̑��x")] float _playerGyroSpeed;
	[SerializeField, Tooltip("�W���C�����󂯕t���銴�o")] float _gyroTime;

	[Space(10)]

	[SerializeField, Tooltip("��̃��[��")] float _upLane;
	[SerializeField, Tooltip("�^�񒆂̃��[��")] Vector2 _centerLane;
	[SerializeField, Tooltip("���̃��[��")] float _downLane;

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
	/// �v���C���[�̈ړ�
	/// </summary>
    private void Move()
	{ 
		//�����ŉE�ɐi��
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
    /// �Ƃ肠�����̃W���C������
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