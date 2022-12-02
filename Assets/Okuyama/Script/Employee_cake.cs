using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ケーキを積む使用人用ケーキ
/// </summary>
public class Employee_cake : MonoBehaviour
{
    SpringJoint _joint = null;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Cake"))
        {
            _joint.connectedBody = collision.rigidbody;
        }
    }
}
