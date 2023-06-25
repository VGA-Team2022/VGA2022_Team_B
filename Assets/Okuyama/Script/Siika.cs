using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//ÉXÉCÉJ
public class Siika : MonoBehaviour
{
    [SerializeField] Sprite _suikaImage = null;
    [SerializeField] Animator _animator= null;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obon")
        {
            _animator.enabled = false;
            transform.GetChild(0).rotation = Quaternion.Euler(0f, 0f, 0f);
            this.gameObject.transform.GetChild(0).
                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _suikaImage; 
        }
    }
}
