using System.Collections.Generic;
using UnityEngine;

public class Obon : MonoBehaviour
{
    [Tooltip("プレイヤーがゲーム開始時に持っているお菓子の配列")]
    [SerializeField]
    private GameObject[] _startOkasis;

    [Tooltip("プレイヤーのアニメーション管理クラス")]
    [SerializeField]
    private PlayerAnimControl _playerAnim;

    public PlayerAnimControl PlayerAnim => _playerAnim;

    [SerializeField]
    private float _zureSpeed;

    [HideInInspector]
    public bool _sweetsFall = false;

    /// <summary> プレイヤーがプレイ中に持っているお菓子の配列 </summary>
    private List<GameObject> _okasis = new();

    private float _zure;

    private float _movement;

    private float n = 1;

    //////////////仮置き/////////////////
    private static List<GameObject> _staticOkasis;
    public static bool _staticSweetsFall;

    public float Zure => _zure;
    public float Movement => _movement;

    private void Awake()
    {
        for (int i = 0; i < _startOkasis.Length; i++)
        {
            if (_okasis.Count == 0)
            {
                _okasis.Add(_startOkasis[0]);
                _okasis[0].transform.position = this.transform.position;
                _startOkasis[0].GetComponent<Sweets>().MisalignmentDifference = 0;//追加したお菓子の揺れの差を変更
            }
            else
            {
                _okasis.Add(_startOkasis[i]);
                _okasis[i].transform.position = _okasis[i - 1].GetComponent<Sweets>().NextPos.position;
                _startOkasis[i].GetComponent<Sweets>().MisalignmentDifference = 1 + (float)i / 10;//追加したお菓子の揺れの差を変更
            }
        }

        _staticOkasis = _okasis;
        _staticSweetsFall = _sweetsFall;
    }

    private void FixedUpdate()
    {
        var hol = Input.GetAxisRaw("Horizontal");

        //Zure += ((n += h * 0.00001f) * 100f);

        _zure += (hol * 0.00001f) * n;
        n += Mathf.Abs(_zure * 1000);

        if (hol == 0)
        {
            n = 1;
        }
    }

    public void SweetsAdd(GameObject gameObjects)
    {
        _okasis.Add(gameObjects);
        var secondSweet = _okasis[_okasis.Count - 2].GetComponent<Sweets>();
        _okasis[_okasis.Count - 1].transform.position = secondSweet.NextPos.position;
        gameObjects.GetComponent<Sweets>()._prevObj = _okasis[_okasis.Count - 2];
    }

    public GameObject SweetsAdd(GameObject gameObjects, bool boolean)
    {
        var obj = Instantiate(gameObjects);
        var secondSweet = _okasis[_okasis.Count - 1].GetComponent<Sweets>();
        obj.transform.position = secondSweet.NextPos.position;
        obj.GetComponent<Sweets>()._prevObj = secondSweet.gameObject;
        _okasis.Add(obj);
        return obj;
    }

    public void MisalignmentOfSweetsCausedByMovement(float stickX)
    {
        _movement += _zureSpeed * stickX;
    }

    public void Hit(float hitPos)
    {
        if (this.transform.position.x > hitPos)
        {
            _zure += 0.1f;
        }
        else if (this.transform.position.x < hitPos)
        {
            _zure -= 0.1f;
        }

        if (!_sweetsFall && !_staticSweetsFall)//まだゲームオーバーしてないとき
        {
            foreach (GameObject okasis in _okasis)//揺らす
            {
                if (okasis.TryGetComponent(out Sweets sweets))/////////////毎回ゲットコンポーネントするのだるいから最初からSweets型のListにする
                {
                    sweets.SwayAnim();
                }
            }
            SoundManager.InstanceSound.PlayAudioClip(SoundManager.SE_Type.Player_Collision);
        }
    }

    public void GameOver()
    {
        if (!_sweetsFall && !_staticSweetsFall)
        {
            foreach (GameObject okasis in _okasis)
            {
                if (okasis.TryGetComponent(out Sweets sweets))
                {
                    sweets.Boom(100);
                }
            }
            _sweetsFall = true;
            SoundManager.InstanceSound.PlayAudioClip(SoundManager.SE_Type.CakeStandFall);
        }
    }

    public static void OutSideGameOver()
    {
        if (!_staticSweetsFall)
        {
            foreach (GameObject okasis in _staticOkasis)
            {
                if (okasis.TryGetComponent(out Sweets sweets))
                {
                    sweets.Boom(50);
                }
            }
            _staticSweetsFall = true;
        }
    }
}
