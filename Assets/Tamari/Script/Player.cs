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
	void Start()
	{
		_nowPos = 1;
		gameObject.transform.position = _raneNum[_nowPos].position;
		_sp = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
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

		if (leftStickValue.x < 0)
		{
			return;
		}
		//���ɂ͓����Ȃ�
		if (GameManager.isAppearDoorObj)
		{
			if (leftStickValue.x > 0f)
			{
				transform.Translate(leftStickValue.x * /*_playerSpeed*/ 5 * Time.deltaTime, 0, 0);
			}
		}

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
}