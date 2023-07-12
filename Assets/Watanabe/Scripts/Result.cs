using Common;
using UnityEngine;

public class Result : MonoBehaviour
{
    [SerializeField] private Sequencer _sequencer = default;

    private void Start()
    {
        //ResultのBGM再生
        SoundManager.InstanceSound.PlayAudioClip(
            GameManager.GameResult == GameResult.CLEAR ?
            SoundManager.BGM_Type.BGM_Result_GameClear : SoundManager.BGM_Type.BGM_Result_GameOver);

        _sequencer.SetDialogue();
    }

    /// <summary> プレイ結果を管理する構造体 </summary>
    public struct Patterns
    {
        public StageType Stage;
        public StageTime Time;
        public GameResult Result;

        public Patterns(StageType stage, StageTime time, GameResult result)
        {
            Stage = stage;
            Time = time;
            Result = result;
        }
    }
}
