using UnityEngine;

//ÉXÉCÉJ
public class Siika : GimmickBase
{
    [SerializeField] Sprite _suikaImage = null;
    [SerializeField] Animator _animator= null;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obon")
        {
            _animator.enabled = false;
            transform.GetChild(0).rotation = Quaternion.Euler(0f, 0f, 0f);
            transform.GetChild(0).
                GetComponentInChildren<SpriteRenderer>().sprite = _suikaImage;
        }
    }
}
