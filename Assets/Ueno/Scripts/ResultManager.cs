using UnityEngine;

public enum Story
{
    yashiki_Clear,
    yashiki_Failed,

    SeaStage_Clear,
    SeaStage_Failed,
}

public class ResultManager : MonoBehaviour
{
    [SerializeField] private Story _storyJudge = Story.yashiki_Failed;

    [SerializeField] private MessageSequencer _messageSequencer;

    private void Start()
    {
        //ここが複数のステージに対応していないため、修正する
        if (GameManager.IsGameClear)
        {
            _storyJudge = Story.yashiki_Clear;
        }
        else
        {
            _storyJudge = Story.yashiki_Failed;
        }

        //_messageSequencer = _messageSequencer.gameObject.GetComponent<MessageSequencer>();

        switch (_storyJudge)
        {
            case Story.yashiki_Clear:
                _messageSequencer.StoryJudge = Story.yashiki_Clear;
                break;

            case Story.yashiki_Failed:
                _messageSequencer.StoryJudge = Story.yashiki_Failed;
                break;

            case Story.SeaStage_Clear:
                _messageSequencer.StoryJudge = Story.SeaStage_Clear;
                break;

            case Story.SeaStage_Failed:
                _messageSequencer.StoryJudge = Story.SeaStage_Failed;
                break;

            default:
                return;
        }
    }
}
