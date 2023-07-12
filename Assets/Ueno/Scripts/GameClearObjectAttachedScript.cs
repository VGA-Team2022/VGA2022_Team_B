using Common;
using UnityEngine;

/// <summary> クリア判定 </summary>
public class GameClearObjectAttachedScript : MonoBehaviour
{
    [Tooltip("接触したらクリア判定にするobjects")]
    [SerializeField] private Animator[] _objectPrefabsAnim;

    private int _gameStageNum;
    //AnimatorのClearアニメーションに遷移するためのParamの名前
    private string _animClearPram = "isClear";
    private bool _isClear;


    private void Start()
    {
        switch (GameManager.GameState.Stage)
        {
            case StageType.YASHIKI:
                _gameStageNum = 0;
                break;
            case StageType.SEA:
                _gameStageNum = 1;
                break;
            case StageType.GARDEN:
                _gameStageNum = 2;
                break;
        }
        _objectPrefabsAnim[_gameStageNum] = _objectPrefabsAnim[_gameStageNum].GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.IsAppearClearObj&& !_isClear)
        {
            _objectPrefabsAnim[_gameStageNum].SetBool(_animClearPram, true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Obon obon))
        {
            GameManager.IsGameClear = true;
            GameManager.GameResult = Common.GameResult.CLEAR;
            _isClear = true;
            Debug.Log("GameClear");
        }
    }
}


