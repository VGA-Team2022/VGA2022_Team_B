using UnityEngine;

/// <summary>
/// クリア判定 
/// </summary>
public class GameClearObjectAttachedScript : MonoBehaviour
{
    [Tooltip("接触したらクリア判定にするobjects"),SerializeField] private Animator[] _objectPrefabsAnim;
    [SerializeField] private StageMove _stageMove;

    private int _gameStageNum;
    //AnimatorのClearアニメーションに遷移するためのParamの名前
    private string _animClearPram = "isClear";
    private BoxCollider _col;

    private void Start()
    {
        _gameStageNum = GameManager.GameStageNum; 
        _objectPrefabsAnim[_gameStageNum] = _objectPrefabsAnim[_gameStageNum].GetComponent<Animator>();
        _stageMove = _stageMove.gameObject.GetComponent<StageMove>();
        _col = GetComponent<BoxCollider>();
        _col.enabled = false;
    }

    private void Update()
    {
        if (GameManager.IsAppearClearObj)
        {
            _col.enabled = true;
            _objectPrefabsAnim[_gameStageNum].SetBool(_animClearPram,true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obon")
        {
            GameManager.IsGameClear = true;
            Debug.Log("GameClear");
        }
    }


}


