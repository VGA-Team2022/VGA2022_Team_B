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
        _messageSequencer = _messageSequencer.gameObject.GetComponent<MessageSequencer>();

        

        switch (StoryJudge)
        {
            case Story.yashiki_Clear:
                _messageSequencer.StoryJudge = Story.yashiki_Clear;
                SoundManager.Instance.CriAtomBGMPlay("BGM_success");
                break;

            case Story.yashiki_Failed:
                _messageSequencer.StoryJudge = Story.yashiki_Failed;
                SoundManager.Instance.CriAtomBGMPlay("BGM _failure");
                break;
        }
    }


}
