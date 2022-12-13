using UnityEngine;
using System.Collections;

public class GyroController : MonoBehaviour
{
    [SerializeField] float _speed = 100;


    Obon _obon;

    void Start()
    {

    }

    void Update()
    {
        var dire = Vector3.zero;

        dire.z = Input.acceleration.x * -1;
        dire.x = Input.acceleration.y;

        dire *= Time.deltaTime;
        transform.Rotate(dire * _speed);
    }
}