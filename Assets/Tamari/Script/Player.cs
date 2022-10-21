using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	//[SerializeField, Tooltip("�v���C���[�̈ړ����x")] float _playerSpeed = 2;
	[SerializeField, Tooltip("�W���C���̑��x")] float _playerGyroSpeed;
	[SerializeField, Tooltip("�W���C�����󂯕t����Ԋu")] float _gyroTime;

	[Space(10)]

	[SerializeField, Tooltip("���[���̈ʒu")] Transform[] _raneNum;
	[Tooltip("�v���C���[�̌��ݒn")] private int _nowPos;

	//�v���C���[�̌��ݒn���v���p�e�B��
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
	/// �v���C���[�̈ړ�
	/// </summary>
	private void Move()
	{ 
		//�����ŉE�ɐi��?
		//_rb.velocity = new Vector2(_playerSpeed, _rb.velocity.y);
		//���[�����X�V
		gameObject.transform.position = _raneNum[_nowPos].position;
	}

	/// <summary>
	/// ��̃��[���Ɉړ�
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
	/// ���̃��[���Ɉړ�
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