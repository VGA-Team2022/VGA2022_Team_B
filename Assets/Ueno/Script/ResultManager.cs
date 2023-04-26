using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Story
{
    yashiki_Clear,
    yashiki_Failed,
}

public class ResultManager : MonoBehaviour
{
    [SerializeField] public Story StoryJudge = Story.yashiki_Failed;

    [SerializeField] private MessageSequencer _messageSequencer;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.isGameClear)
        {
            StoryJudge = Story.yashiki_Clear;
        }
        else
        {
            StoryJudge = Story.yashiki_Failed;
        }

        _messageSequencer = _messageSequencer.gameObject.GetComponent<MessageSequencer>();

        switch (StoryJudge)
        {
            case Story.yashiki_Clear:
                _messageSequencer.StoryJudge = Story.yashiki_Clear;
                AudioManager.Instance.CriAtomBGMPlay("BGM_success");
                break;

            case Story.yashiki_Failed:
                _messageSequencer.StoryJudge = Story.yashiki_Failed;
                AudioManager.Instance.CriAtomBGMPlay("BGM _failure");
                break;
        }
    }

}
