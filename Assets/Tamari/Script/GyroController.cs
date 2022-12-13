using UnityEngine;
using System.Collections;

public class GyroController : MonoBehaviour
{
    [SerializeField] float _speed = 10;

    private Vector3 acceleration;

    Obon _obon;

    void Start()
    {

    }

    void Update()
    {
        this.acceleration = Input.acceleration;

        var dir = Vector3.zero;
        dir.z = Input.acceleration.x * -1;

        dir *= Time.deltaTime;
        _obon.MisalignmentOfSweetsCausedByMovement(dir.z * _speed);
    }
}