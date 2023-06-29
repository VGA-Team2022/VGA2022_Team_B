using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	[SerializeField, Tooltip("�㉺�ړ���Y�����̒l")] float _stickYNum;

	[Space(10)]

	[SerializeField, Tooltip("���[���̈ʒu")] Transform[] _raneNum;
	[Tooltip("�v���C���[�̌��ݒn")] private int _nowPos;

	bool _up;
	bool _down;

	private Soundmanager.SE_Type _moveSE = Soundmanager.SE_Type.FootStep_Yashiki;

	//�v���C���[�̌��ݒn���v���p�e�B��
	public int NowPos => _nowPos;

    void Start()
	{
		_nowPos = 1;
		gameObject.transform.position = _raneNum[_nowPos].position;

		_moveSE = GameManager.GameStageNum switch
		{
			0 => Soundmanager.SE_Type.FootStep_Yashiki,
			1 => Soundmanager.SE_Type.FootStep_Sea
        };
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
		Soundmanager.InstanceSound.PlayerMoveSE(_moveSE);

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
		if (GameManager.IsAppearClearObj)
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
    /// <summary> ��̃��[���Ɉړ� </summary>
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

			gameObject.transform.position
				= new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _raneNum[_nowPos].position.z);
			Soundmanager.InstanceSound.PlayAudioClip(Soundmanager.SE_Type.Player_LaneMove);
			Debug.Log("��Ɉړ�");
		}
    }
    /// <summary> ���̃��[���Ɉړ� </summary>
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
			gameObject.transform.position
				= new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _raneNum[_nowPos].position.z);
            Soundmanager.InstanceSound.PlayAudioClip(Soundmanager.SE_Type.Player_LaneMove);
            Debug.Log("���Ɉړ�");
		}
    }

    private void OnCollisionEnter(Collision collision)
    {
		if (SystemInfo.supportsVibration)
		{
			#if UNITY_ANDROID
			Handheld.Vibrate();
			#endif

		}
		else
		{
			Debug.Log("�U���ɑΉ����ĂȂ��您������");
		}
	}
}