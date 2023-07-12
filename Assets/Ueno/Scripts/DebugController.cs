using Common;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    enum GameStageType
    {
        Yashiki_DayLight,
        Yashiki_Night,
        Sea_DayLight,
        Sea_Night,
    }

    [SerializeField] private GameStageType _gameStageType = GameStageType.Yashiki_DayLight;

    
    private void OnValidate()
    {
#if UNITY_EDITOR
        switch (_gameStageType)
        {
            case GameStageType.Yashiki_DayLight:
                GameManager.StageType = StageType.YASHIKI_DAYTIME;
                break;
            case GameStageType.Yashiki_Night:
                GameManager.StageType = StageType.YASHIKI_NIGHT;
                break;
            case GameStageType.Sea_DayLight:
                GameManager.StageType = StageType.SEA_DAYTIME;
                break;
            case GameStageType.Sea_Night:
                GameManager.StageType = StageType.SEA_NIGHT;
                break;
        }
#endif
    }
}
