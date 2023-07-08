using System;
using UnityEngine;

public class Sweets : MonoBehaviour
{
    [SerializeField]
    private Transform _nextPos;

    [Tooltip("これ以上横にはみ出したら落ちる")]
    [SerializeField]
    private float _deadWidth;

    [SerializeField]
    private GameObject _prevObj;

    private float _misalignmentDifference;//ずれの差上に行けば行くほど大きく動くやつの変数

    private Obon _obon;
    private Rigidbody _rb;
    private Animator _anim;

    public Transform NextPos => _nextPos;
    public GameObject PrevObj { get => _prevObj; set => _prevObj = value; }
    public float MisalignmentDifference { get => _misalignmentDifference; set => _misalignmentDifference = value; }

    private void Start()
    {
        _obon = GameObject.FindGameObjectWithTag("Obon").GetComponent<Obon>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(!_obon._sweetsFall)
        {
            if (transform.position.x >= _prevObj.transform.position.x + _deadWidth ||
                transform.position.x <= _prevObj.transform.position.x - _deadWidth)
            {
                //_rb.AddForce(Vector2.up * 10);//AddForceせんと崩れないからAddForce。演出にも使えソう;
                _obon.GameOver();//Obonクラスのゲームオーバー関数の呼び出し
                _obon._sweetsFall = true;
                Obon.IsSweetsFall = true;
            }
            else
            {
                transform.position
                    = new Vector3(_prevObj.transform.position.x + (_obon.Zure * _misalignmentDifference) - _obon.Movement,
                                  transform.position.y,
                                  transform.position.z);
            }
        }

        try
        {
            if (transform.position.x >= _prevObj.transform.position.x + _deadWidth / 2
                || transform.position.x <= _prevObj.transform.position.x - _deadWidth / 2)
            {
                _obon.PlayerAnim.Abunaaaaaaai();
            }
        }
        catch (NullReferenceException nullException)
        {
            Debug.LogWarning(nullException);
            _obon.PlayerAnim.Abunaaaaaaai();
        }
    }

    public void PutOnSweets(GameObject gameObj)
    {
        gameObj.transform.position = _nextPos.position;
    }

    public void Boom(int power)
    {
        _prevObj = null;
        _rb.constraints = RigidbodyConstraints.None; //Rigidbodyのロックを解除
        _anim.enabled = false;
        transform.eulerAngles = new Vector3(0, 0,UnityEngine.Random.Range(-30,30));//お菓子の向きをランダムに変える

        _rb.AddForce(new Vector3(UnityEngine.Random.Range(-1, 1), 1,0) * power);//AddForceせんと崩れないからAddForce。演出にも使えソう;
    }

    public void SwayAnim()
    {
        _anim.Play("GuraGura");
    }
}
