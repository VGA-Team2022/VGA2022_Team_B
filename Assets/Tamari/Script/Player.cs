using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{
	[SerializeField, Tooltip("�㉺�ړ���Y�����̒l")] float _stickYNum;
	//[SerializeField, Tooltip("�W���C���̑��x")] float _playerGyroSpeed;
	[SerializeField, Tooltip("�W���C�����󂯕t����Ԋu")] float _gyroTime;

	[Space(10)]

	[SerializeField, Tooltip("���[���̈ʒu")] Transform[] _raneNum;
	[Tooltip("�v���C���[�̌��ݒn")] private int _nowPos;

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
        //GetGyro();

		StickMove();
	}

	/// <summary>
	/// �X�e�B�b�N�ɂ��v���C���[�̈ړ�
	/// </summary>
	private void StickMove()
    {
		var current = Gamepad.current;

		// �Q�[���p�b�h�̐ڑ��m�F
		if (current == null)
			return;

		// ���X�e�B�b�N���͂��擾
		var leftStickValue = current.leftStick.ReadValue();

        //���ɂ͓����Ȃ�

        //if (leftStickValue.x < 0)
        //{
        //    return;
        //}
        //else if (leftStickValue.x > 0f)
        //{
        //    transform.Translate(leftStickValue.x * _playerSpeed * Time.deltaTime, 0, 0);
        //}

        if (leftStickValue.y > _stickYNum && !_up)
        {
			Up();
			_up = true;
        }
		else if(leftStickValue.y <  -_stickYNum && !_down)
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
			SoundManager.Instance.CriAtomPlay(CueSheet.SE, "���[���ړ�");
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