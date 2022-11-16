using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarban : MonoBehaviour
{
    [SerializeField, Tooltip("スピード")] float _speed = 0.1f;
    [SerializeField, Tooltip("消す場所")] float _destroyPos = -13;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rb.AddForce(gameObject.transform.forward * -_speed, ForceMode.Impulse);
        if(gameObject.transform.position.z <= _destroyPos)
        {
            gameObject.SetActive(false);
        }
    }
}
