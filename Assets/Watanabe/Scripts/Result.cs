using UnityEngine;

public class Result : MonoBehaviour
{
    [SerializeField] private StoryResult _storyJudge = StoryResult.YashikiStage_Daytime_Clear;
    [SerializeField] private Sequencer _sequencer = default;

    private void Start()
    {
        _sequencer.SetDialogue(_storyJudge);
    }
}

public enum StoryResult
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
