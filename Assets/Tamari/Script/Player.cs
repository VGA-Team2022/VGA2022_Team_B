using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{
	[SerializeField, Tooltip("�v���C���[�̈ړ����x")] float _playerSpeed = 2;
	//[SerializeField, Tooltip("�W���C���̑��x")] float _playerGyroSpeed;
	[SerializeField, Tooltip("�W���C�����󂯕t����Ԋu")] float _gyroTime;

	[Space(10)]

	[SerializeField, Tooltip("���[���̈ʒu")] Transform[] _raneNum;
	[Tooltip("�v���C���[�̌��ݒn")] private int _nowPos;

    //�t���b�N�֘A�̏��
	private Vector3 _touchStartPos;
	private Vector3 _touchEndPos;
	float _flickValueY;
	[SerializeField, Tooltip("�t���b�N�̊��x")] float _flickValue;

	SpriteRenderer _sp;

	bool _up;
	bool _down;

	//�v���C���[�̌��ݒn���v���p�e�B��
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
        if (Input.GetMouseButtonDown(0))
        {
            _touchStartPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        }
        if (Input.GetMouseButtonUp(0))
        {
            _touchEndPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            //FlickDirection();
            //GetDirection();
        }

        //GetGyro();

		StickMove();
	}

	/// <summary>
	/// �X�e�B�b�N�ɂ��v���C���[�̈ړ�
	/// </summary>
	private void StickMove()
    {
		// ���݂̃Q�[���p�b�h���
		var current = Gamepad.current;

		// �Q�[���p�b�h�ڑ��`�F�b�N
		if (current == null)
			return;

		// ���X�e�B�b�N���͎擾
		var leftStickValue = current.leftStick.ReadValue();

		//���ɂ͓����Ȃ�
		if (leftStickValue.x < 0)
        {
			return;
        }
        else if (leftStickValue.x > 0f)
        {

            transform.Translate(leftStickValue.x * _playerSpeed * Time.deltaTime, 0, 0);
        }

		if(leftStickValue.y > 0.8f && !_up)
        {
			Up();
			_up = true;
        }
		else if(leftStickValue.y < -0.8f && !_down)
        {
			Down();
			_down = true;
        }
		else if(leftStickValue.y == 0)
        {
			_up = false;
			_down = false;
        }
    }

	/// <summary>
	/// �t���b�N�̗ʂ��v�Z
	/// </summary>
	void FlickDirection()
	{
		//flickValue_x = _touchEndPos.x - _touchStartPos.x;
		_flickValueY = _touchEndPos.y - _touchStartPos.y;
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
        if (_nowPos >= _raneNum.Length - 1)
        {
			Debug.Log("����ȏ㉺�ɂ����܂���");
			return;
        }
        else if (_nowPos < _raneNum.Length)
        {
			_nowPos++;

			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _raneNum[_nowPos].position.z);
			SoundManager.Instance.CriAtomPlay(CueSheet.SE, "");
			Debug.Log("��Ɉړ�");
		}
    }
	/// <summary>
	/// ���̃��[���Ɉړ�
	/// </summary>
    private void Down()
    {
        if (_nowPos == 0)
        {
			Debug.Log("����ȏ㉺�ɂ����܂���");
            return;
        }
        else if (_nowPos > 0)
        {
			_nowPos--;
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _raneNum[_nowPos].position.z);
			SoundManager.Instance.CriAtomPlay(CueSheet.SE, "���[���ړ�");
			Debug.Log("���Ɉړ�");
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