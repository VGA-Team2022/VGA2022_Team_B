using System;
using UnityEngine;

public class Result : MonoBehaviour
{
    [SerializeField] private GameResult _storyJudge = GameResult.YashikiStage_Daytime_Clear;
    [SerializeField] private Sequencer _sequencer = default;

    private Patterns[] _resultPatterns = default;

    private void Start()
    {
        _resultPatterns =
            new Patterns[]
            { new Patterns(0, 0, true), new Patterns(0, 0, false),
              new Patterns(0, 1, true), new Patterns(0, 1, false),
              new Patterns(1, 0, true), new Patterns(1, 0, false),
              new Patterns(1, 1, true), new Patterns(1, 1, false) };

        _storyJudge =
            (GameResult)Array.IndexOf(
            _resultPatterns,
            new Patterns(GameManager.GameStageNum, GameManager.StageLevelNum, GameManager.IsGameClear));

        _sequencer.SetDialogue(_storyJudge);
    }

    public struct Patterns
    {
        public int Stage;
        public int Level;
        public bool IsClear;

        public Patterns(int stage, int level, bool isClear)
        {
            Stage = stage;
            Level = level;
            IsClear = isClear;
        }
    }
}

public enum GameResult
{
    YashikiStage_Daytime_Clear,
    YashikiStage_Daytime_Failed,
    YashikiStage_Night_Clear,
    YashikiStage_Night_Failed,

    SeaStage_Daytime_Clear,
    SeaStage_Daytime_Failed,
    SeaStage_Night_Clear,
    SeaStage_Night_Failed,
}
