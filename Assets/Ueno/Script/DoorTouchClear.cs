using UnityEngine;

public class DoorTouchClear : MonoBehaviour
{

    private Animator _anim;

    private void Start()
    {
        _anim= GetComponent<Animator>();    

    }

    private void FixedUpdate()
    {
        if (GameManager.isAppearDoorObj)
        {
            _anim.SetBool("isClear", true);
        }
        else
        {
            _anim.SetBool("isClear", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obon")
        {
            Debug.Log(11111111111);
            GameManager.isGameClear= true;
            Debug.Log($"aaaaaaaaaaaaaaaaaaaa:{GameManager.isGameClear}");
        }
        
    }
}
