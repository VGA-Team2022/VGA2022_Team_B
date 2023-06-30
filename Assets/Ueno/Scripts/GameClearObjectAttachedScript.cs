using UnityEngine;

/// <summary>
/// クリア判定 
/// </summary>
public class GameClearObjectAttachedScript : MonoBehaviour
{
    [Tooltip("接触したらクリア判定にするobjects"),SerializeField] private Animator[] _objectPrefabsAnim;

    private int _gameStageNum;
    //AnimatorのClearアニメーションに遷移するためのParamの名前
    private string _animClearPram = "isClear";
    private bool _isClear;


    private void Start()
    {
        _gameStageNum = GameManager.GameStageNum; 
        _objectPrefabsAnim[_gameStageNum] = _objectPrefabsAnim[_gameStageNum].GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.IsAppearClearObj&& !_isClear)
        {
            _objectPrefabsAnim[_gameStageNum].SetBool(_animClearPram,true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obon")
        {
            GameManager.IsGameClear = true;
            _isClear = true;
            Debug.Log("GameClear");
        }
    }


}


