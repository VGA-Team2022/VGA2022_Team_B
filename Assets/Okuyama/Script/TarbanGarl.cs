using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarbanGarl : MonoBehaviour
{
    [SerializeField] GameObject _tarban;
    StageMove _stageMove;
    private void Start()
    {
        _stageMove = GameObject.Find("StageManager").GetComponent<StageMove>();
    }
    private void FixedUpdate()
    {
        this.gameObject.transform.position -= new Vector3(Time.deltaTime * _stageMove.MoveSpeed, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Instantiate(_tarban, gameObject.transform);
        }
    }
}
