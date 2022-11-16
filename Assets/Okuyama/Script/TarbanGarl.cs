using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarbanGarl : MonoBehaviour
{
    [SerializeField] GameObject _tarban;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Instantiate(_tarban, gameObject.transform);
        }
    }
}
