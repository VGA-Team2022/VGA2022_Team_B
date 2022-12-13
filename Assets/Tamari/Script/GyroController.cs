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

        dire.z = Input.acceleration.x;
        //dire.x = Input.acceleration.y;

        dire.z *= Time.deltaTime;
        _obon.MisalignmentOfSweetsCausedByMovement(dire.z * _speed);
    }
}