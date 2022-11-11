using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweets : MonoBehaviour
{
    [SerializeField]
    Transform _nextPos;

    [SerializeField]
    float _deadWidth;

    public GameObject _prevObj;

    Obon obon;

    Rigidbody2D _rb;


    //public GameObject PrevObj
    //{
    //    get
    //    {
    //        return _prevObj;
    //    }
    //    set
    //    {
    //        _prevObj = value;
    //        this.transform.position = _prevObj.GetComponent<Sweets>()._nextPos.position;
    //    }
    //}


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

    private void Start()
    {
        obon = GameObject.FindGameObjectWithTag("Obon").GetComponent<Obon>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.transform.position.x >= _prevObj.transform.position.x + _deadWidth || this.transform.position.x <= _prevObj.transform.position.x - _deadWidth)
        {
            this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            _rb.AddForce(Vector2.up * 1);
        }
        else
        {
            this.transform.position = new Vector3(_prevObj.transform.position.x + obon.Zure, this.transform.position.y, this.transform.position.z);
        }
    }

    public void PutOnSweets(GameObject gameObj)
    {
        gameObj.transform.position = _nextPos.position;
    }
}
