using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GyroController : MonoBehaviour
{
    [SerializeField] private float _speed = 100;
    [SerializeField] private Obon _obon;
    [SerializeField] private float _startKeepCakeTime = 5f;

    private Vector3 _acceleration;
    private bool _isStartGyroInvalid;

    void Start()
    {
        _obon = _obon.gameObject.GetComponent<Obon>();
        StartCoroutine(KeepCake());
    }

    void Update()
    {
        if (!_isStartGyroInvalid)
        {
            _acceleration = Input.acceleration;
            _obon.MisalignmentOfSweetsCausedByMovement(-_acceleration.x * _speed);
        }
    }

     IEnumerator KeepCake()
    {
        _isStartGyroInvalid = true;
        yield return new WaitForSeconds(_startKeepCakeTime);
        _isStartGyroInvalid = false;
    }

}