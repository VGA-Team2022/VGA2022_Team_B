using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroController : MonoBehaviour
{
    private Quaternion _gyro;
    [SerializeField] Obon _obon;
    void Start()
    {
        Input.gyro.enabled = true;
    }

    void Update()
    {
        this._gyro = Input.gyro.attitude;
        //this.transform.localRotation = Quaternion.Euler(90, 0, 0) * (new Quaternion(-_gyro.x, -_gyro.y, _gyro.z, _gyro.w));
        _obon.MisalignmentOfSweetsCausedByMovement(_gyro.z);
    }
}
