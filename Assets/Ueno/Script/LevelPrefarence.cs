using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StageType
{
    none = -1,
    yasiki = 0,
    umibe = 1,
    niwa = 2
}
public class LevelPrefarence : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("アタッチするもの")]
    [Tooltip("各imageを格納する"), SerializeField] private Sprite[] _stageSprite;
    [Tooltip("レベルオブジェクト"), SerializeField] private GameObject[] _levelButtons;

    [Header("各種変更値")]
    [SerializeField] private StageType _stageType = StageType.yasiki;

    private int num = 0;

    private void OnValidate()
    {
        ChangeStageType();
    }


    private void Update()
    {
        switch(GameManager.GameStageNum)
        {
            case 0:
                _stageType = StageType.yasiki;
                break;
            case 1:
                _stageType = StageType.umibe;
                break;
            case 2:
                _stageType = StageType.niwa;
                Debug.Log("aaaaaaaaaaaaaaaaaa");
                break;
        }

        ChangeStageType();
    }

    private void ChangeStageType()
    {
        switch (_stageType)
        {
            case 0:
                num = 0;
                foreach (var i in _levelButtons)
                {
                    i.GetComponent<Image>().sprite = _stageSprite[num];
                    num++;
                }
                break;
            case (StageType)1:
                num = 3;
                foreach (var i in _levelButtons)
                {
                    i.GetComponent<Image>().sprite = _stageSprite[num];
                    num++;
                }
                break;
            case (StageType)2:
                num = 6;
                foreach (var i in _levelButtons)
                {
                    i.GetComponent<Image>().sprite = _stageSprite[num];
                    num++;
                }
                break;
        }
    }
}