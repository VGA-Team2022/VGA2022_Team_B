using Common;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
	[SerializeField, Tooltip("上下移動のY方向の値")] float _stickYNum;

	[Space(10)]

	[SerializeField, Tooltip("レーンの位置")] Transform[] _raneNum;
    /// <summary> プレイヤーの現在地 </summary>
    private int _nowPos;

	private bool _up;
	private bool _down;

	private SoundManager.SE_Type _moveSE = SoundManager.SE_Type.FootStep_Yashiki;

    /// <summary> プレイヤーの現在地 </summary>
    public int NowPos => _nowPos;

    private void Start()
	{
		_nowPos = 1;
		gameObject.transform.position = _raneNum[_nowPos].position;

		_moveSE = GameManager.GameState.Stage switch
		{
			StageType.YASHIKI => SoundManager.SE_Type.FootStep_Yashiki,
            StageType.SEA => SoundManager.SE_Type.FootStep_Sea,
        };
	}

	private void Update()
	{
		StickMove();
	}

    /// <summary> スティックによるプレイヤーの移動 </summary>
    private void StickMove()
    {
		SoundManager.InstanceSound.PlayerMoveSE(_moveSE);

		var current = Gamepad.current;

		// ゲームパッドの接続確認
		if (current == null)
			return;

		// 左スティック入力を取得
		var leftStickValue = current.leftStick.ReadValue();

		if (leftStickValue.x < 0)
		{
			return;
		}
		//左には動かない
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

    /// <summary> 上のレーンに移動 </summary>
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

			gameObject.transform.position
				= new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _raneNum[_nowPos].position.z);
			SoundManager.InstanceSound.PlayAudioClip(SoundManager.SE_Type.Player_LaneMove);
			Debug.Log("上に移動");
		}
    }

    /// <summary> 下のレーンに移動 </summary>
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
			gameObject.transform.position
				= new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _raneNum[_nowPos].position.z);
            SoundManager.InstanceSound.PlayAudioClip(SoundManager.SE_Type.Player_LaneMove);
            Debug.Log("下に移動");
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
			Debug.Log("振動に対応してないよおおおお");
		}
	}
}