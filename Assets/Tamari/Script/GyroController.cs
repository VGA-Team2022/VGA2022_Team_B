using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroController : MonoBehaviour
{
    [SerializeField] float _adjustNum = 100;
    private Quaternion _gyro;
    [SerializeField] Obon _obon;
    void Start()
    {
        Input.gyro.enabled = true;
        _gyro.z = 0;
    }

    void Update()
    {
        this._gyro = Input.gyro.attitude;
        _gyro = Quaternion.Euler(90, 0, 0) * (new Quaternion(-_gyro.x, -_gyro.y, _gyro.z, _gyro.w));
        _obon.MisalignmentOfSweetsCausedByMovement(_gyro.z * _adjustNum);
    }
}
