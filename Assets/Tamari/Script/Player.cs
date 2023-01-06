using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{
	[SerializeField, Tooltip("上下移動のY方向の値")] float _stickYNum;
	//[SerializeField, Tooltip("ジャイロの速度")] float _playerGyroSpeed;
	[SerializeField, Tooltip("ジャイロを受け付ける間隔")] float _gyroTime;

	[Space(10)]

	[SerializeField, Tooltip("レーンの位置")] Transform[] _raneNum;
	[Tooltip("プレイヤーの現在地")] private int _nowPos;

	SpriteRenderer _sp;

	bool _up;
	bool _down;

	//プレイヤーの現在地をプロパティ化
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
	/// スティックによるプレイヤーの移動
	/// </summary>
	private void StickMove()
    {
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
	/// 上のレーンに移動
	/// </summary>
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

			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _raneNum[_nowPos].position.z);
			SoundManager.Instance.CriAtomPlay(CueSheet.SE, "レーン移動");
			Debug.Log("上に移動");
		}
    }
	/// <summary>
	/// 下のレーンに移動
	/// </summary>
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
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, _raneNum[_nowPos].position.z);
			SoundManager.Instance.CriAtomPlay(CueSheet.SE, "レーン移動");
			Debug.Log("下に移動");
		}

    }
}