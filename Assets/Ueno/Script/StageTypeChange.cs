using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameStageBackGroundType
{
    yashiki,
    umibe,
    niwa
}
public class StageTypeChange : MonoBehaviour
{
    [SerializeField] private GameStageBackGroundType _stageType = GameStageBackGroundType.yashiki;
    [Header("アタッチするもの")]
    [SerializeField] private GameObject[] _wall;
    [SerializeField] private Sprite[] _backgroundSprites;


    private void OnValidate()
    {
        StageChange();
    }

    private void StageChange()
    {
        switch (_stageType)
        {
            case GameStageBackGroundType.yashiki:
                foreach (var i in _wall)
                {
                    i.GetComponent<SpriteRenderer>().sprite = _backgroundSprites[0];
                }
                break;
            case GameStageBackGroundType.umibe:
                foreach (var i in _wall)
                {
                    i.GetComponent<SpriteRenderer>().sprite = _backgroundSprites[1];
                }
                break;
            case GameStageBackGroundType.niwa:
                foreach (var i in _wall)
                {
                    i.GetComponent<SpriteRenderer>().sprite = _backgroundSprites[2];
                }
                break;
        }
    }

}
