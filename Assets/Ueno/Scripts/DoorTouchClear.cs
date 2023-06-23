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
        if (GameManager.IsAppearDoorObj)
        {
            _anim.SetBool("isClear", GameManager.IsAppearDoorObj);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obon")
        {
            Debug.Log(11111111111);
            GameManager.IsGameClear= true;
            Debug.Log($"aaaaaaaaaaaaaaaaaaaa:{GameManager.IsGameClear}");
        }
        
    }
}
