using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceScript : MonoBehaviour
{
    [Tooltip("こいつの下のお菓子"), SerializeField]
    Transform _underOkasi;

    [Tooltip("こいつの上のお菓子たち"), SerializeField]
    Rigidbody2D[] _uperObjs;

    [Tooltip("ジョイントが切れる距離"), SerializeField]
    float _width;

    void Update()
    {
        if(_underOkasi.position.x + _width <= this.transform.position.x || _underOkasi.position.x - _width >= this.transform.position.x)
        {
            this.GetComponent<SpringJoint2D>().enabled = false;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

            foreach ( Rigidbody2D rb2D in _uperObjs)
            {
                rb2D.constraints = RigidbodyConstraints2D.None;
                rb2D.GetComponent<SpringJoint2D>().enabled = false;
            }
        }
    }
}
