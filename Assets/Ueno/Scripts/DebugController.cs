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
                GameManager.GameState.Stage = StageType.YASHIKI;
                GameManager.GameState.Time = StageTime.DAYTIME;
                break;
            case GameStageType.Yashiki_Night:
                GameManager.GameState.Stage = StageType.YASHIKI;
                GameManager.GameState.Time = StageTime.NIGHT;
                break;
            case GameStageType.Sea_DayLight:
                GameManager.GameState.Stage = StageType.SEA;
                GameManager.GameState.Time = StageTime.DAYTIME;
                break;
            case GameStageType.Sea_Night:
                GameManager.GameState.Stage = StageType.SEA;
                GameManager.GameState.Time = StageTime.NIGHT;
                break;
        }
#endif
    }
}
