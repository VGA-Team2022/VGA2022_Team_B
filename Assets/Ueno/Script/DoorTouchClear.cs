using UnityEngine;

public class DoorTouchClear : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obon")
        {
            Debug.Log(11111111111);
            GameManager.isGameClear= true;

        }
        
    }
}
