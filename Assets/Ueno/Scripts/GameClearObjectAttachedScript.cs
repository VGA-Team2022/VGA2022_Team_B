using UnityEngine;

/// <summary>
/// コライダーに触れるとクリア判定になるobjectにアタッチする
/// 
/// </summary>
public class GameClearObjectAttachedScript : MonoBehaviour
{
    [SerializeField] GameObject[] _ObjectPrefab;
    private Animator _anim;

    private void Start()
    {
        _anim= GetComponent<Animator>();    

    }

    private void FixedUpdate()
    {
        if (GameManager.IsAppearClearObj)
        {
            _anim.SetBool("isClear", GameManager.IsAppearClearObj);
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
