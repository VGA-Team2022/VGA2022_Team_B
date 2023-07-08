using Common;
using System;
using UnityEngine;

public class Result : MonoBehaviour
{
    [SerializeField] private Sequencer _sequencer = default;

    private static Patterns[] _resultPatterns = default;

    private void Awake()
    {
        //挑戦結果の該当し得るパターンを列挙（ゲーム一周目のみ初期化を行う）
        //※リザルトに入る度にインスタンスがつくられるのを防ぐため
        _resultPatterns ??= new Patterns[]
            { new Patterns(StageType.YASHIKI_DAYTIME, GameResult.CLEAR), new Patterns(StageType.YASHIKI_DAYTIME, GameResult.FAILED),
              new Patterns(StageType.YASHIKI_NIGHT, GameResult.CLEAR), new Patterns(StageType.YASHIKI_NIGHT, GameResult.FAILED),
              new Patterns(StageType.SEA_DAYTIME, GameResult.CLEAR), new Patterns(StageType.SEA_DAYTIME, GameResult.FAILED),
              new Patterns(StageType.SEA_NIGHT, GameResult.CLEAR), new Patterns(StageType.SEA_NIGHT, GameResult.FAILED), };
    }

    private void Start()
    {
        //挑戦結果がリザルトパターンのどれに該当するか
        var index = Array.IndexOf(
                    _resultPatterns,
                    new Patterns(GameManager.StageType, GameManager.GameResult));

        //ResultのBGM再生
        SoundManager.InstanceSound.PlayAudioClip(
            index % 2 == 0 ?
            SoundManager.BGM_Type.BGM_Result_GameClear : SoundManager.BGM_Type.BGM_Result_GameOver);

        _sequencer.SetDialogue();
    }

    /// <summary> ステージ種、ステージレベル、プレイ結果　を管理する構造体 </summary>
    public struct Patterns
    {
        public StageType Stage;
        public GameResult Result;

        public Patterns(StageType stage, GameResult result)
        {
            Stage = stage;
            Result = result;
        }
    }
}
