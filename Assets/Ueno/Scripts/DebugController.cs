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
        switch (_gameStageType)
        {
            case GameStageType.Yashiki_DayLight:
                GameManager.GameStageNum = 0;
                GameManager.StageLevelNum = 0;
                break;
            case GameStageType.Yashiki_Night:
                GameManager.GameStageNum = 0;
                GameManager.StageLevelNum = 1;
                break;
            case GameStageType.Sea_DayLight:
                GameManager.GameStageNum = 1;
                GameManager.StageLevelNum = 0;
                break;
            case GameStageType.Sea_Night:
                GameManager.GameStageNum = 1;
                GameManager.StageLevelNum = 1;
                break;
        }
        
    }
}
