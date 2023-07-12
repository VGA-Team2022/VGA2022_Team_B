using UnityEngine;

/// <summary> クリア判定 </summary>
public class GameClearObjectAttachedScript : MonoBehaviour
{
    [Tooltip("接触したらクリア判定にするobjects"),SerializeField] private Animator[] _objectPrefabsAnim;

    private int _gameStageNum;
    //AnimatorのClearアニメーションに遷移するためのParamの名前
    private string _animClearPram = "isClear";
    private bool _isClear;


    private void Start()
    {
        switch (GameManager.StageType)
        {
            case Common.StageType.YASHIKI_DAYTIME:
            case Common.StageType.YASHIKI_NIGHT:
                _gameStageNum = 0;
                break;
            case Common.StageType.SEA_DAYTIME:
            case Common.StageType.SEA_NIGHT:
                _gameStageNum = 1;
                break;
            case Common.StageType.GARDEN_DAYTIME:
            case Common.StageType.GARDEN_NIGHT:
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
            _isClear = true;
            Debug.Log("GameClear");
        }
    }
}


