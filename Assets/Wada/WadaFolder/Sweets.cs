using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweets : MonoBehaviour
{
    [SerializeField]
    Transform _nextPos;

    

    [Tooltip("これ以上横にはみ出したら落ちる"), SerializeField]
    float _deadWidth;

    public GameObject _prevObj;

    private float _misalignmentDifference;//ずれの差上に行けば行くほど大きく動くやつの変数

    Obon obon;

    Rigidbody _rb;

    Animator _anim;

    public Transform NextPos
    {
        get
        {
            return _nextPos;
        }
        set
        {

        }
    }

    public float MisalignmentDifference
    {
        get
        {
            return _misalignmentDifference;
        }
        set
        {
            _misalignmentDifference = value;
        }
    }

    private void Start()
    {
        obon = GameObject.FindGameObjectWithTag("Obon").GetComponent<Obon>();
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        if(!obon._sweetsFall)
        {
            if (this.transform.position.x >= _prevObj.transform.position.x + _deadWidth || this.transform.position.x <= _prevObj.transform.position.x - _deadWidth)
            {

                //_rb.AddForce(Vector2.up * 10);//AddForceせんと崩れないからAddForce。演出にも使えソう;
                obon.GameOver();//Obonクラスのゲームオーバー関数の呼び出し
                obon._sweetsFall = true;
            }
            else
            {
                this.transform.position = new Vector3(_prevObj.transform.position.x + (obon.Zure * _misalignmentDifference) - obon.Movement, this.transform.position.y, this.transform.position.z);
            }
        }
        

        //Debug.Log(_prevObj.transform.position.x - _deadWidth);

        if (this.transform.position.x >= _prevObj.transform.position.x + _deadWidth / 2 || this.transform.position.x <= _prevObj.transform.position.x - _deadWidth / 2)
        {
            obon._playerAnim.Abunaaaaaaai();
        }
    }

    public void PutOnSweets(GameObject gameObj)
    {
        gameObj.transform.position = _nextPos.position;
    }

    public void Boom(int power)
    {
        _prevObj = null;
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;//Rigidbodyのロックを解除
        this.transform.eulerAngles = new Vector3(0, 0,Random.Range(-30,30));//お菓子の向きをランダムに変える
        _rb.AddForce(new Vector3(Random.Range(-1, 1), 1,0) * power);//AddForceせんと崩れないからAddForce。演出にも使えソう;
    }

    public void SwayAnim()
    {
        _anim.Play("GuraGura");
    }
}
