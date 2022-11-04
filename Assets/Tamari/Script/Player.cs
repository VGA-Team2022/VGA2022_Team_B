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

    //�t���b�N�֘A�̏��
	private Vector3 _touchStartPos;
	private Vector3 _touchEndPos;
	//float _flickValueX;
	float _flickValueY;
	[SerializeField, Tooltip("�t���b�N�̊��x")] float _flickValue;

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
	/// �v���C���[�̈ړ�
	/// </summary>
	private void Move()
	{ 
		//���[�����X�V
		gameObject.transform.position = _raneNum[_nowPos].position;
	}

	/// <summary>
	/// �t���b�N�̗ʂ��v�Z
	/// </summary>
	void FlickDirection()
	{
		//flickValue_x = _touchEndPos.x - _touchStartPos.x;
		_flickValueY = _touchEndPos.y - _touchStartPos.y;
		Debug.Log("y �X���C�v�ʂ�" + _flickValueY);
	}

	/// <summary>
	/// �t���b�N�ʂɉ����ď�ɍs�������ɍs����
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
	/// ��̃��[���Ɉړ�
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
	/// ���̃��[���Ɉړ�
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
			Debug.Log(acceleration.x);
		}
	}

}