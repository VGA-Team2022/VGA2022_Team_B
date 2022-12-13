using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GyroController : MonoBehaviour
{
    [SerializeField] float _speed = 100;
    Vector3 _acceleration;

    [SerializeField] Obon _obon;

    void Start()
    {
        _obon = _obon.gameObject.GetComponent<Obon>();
    }

    void Update()
    {

        _acceleration = Input.acceleration;

        _obon.MisalignmentOfSweetsCausedByMovement(-_acceleration.x * _speed);
        
        Debug.Log(_acceleration.x);

    }
}