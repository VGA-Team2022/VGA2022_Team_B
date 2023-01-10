using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameStageBackGroundType
{
    yashiki_DayLight,
    yashik_Night,
    umibe,
}
public class StageTypeChange : MonoBehaviour
{
    [SerializeField] private GameStageBackGroundType _stageType = GameStageBackGroundType.yashiki_DayLight;
    [Header("アタッチするもの")]
    [SerializeField] private Material[] _targetMaterial;


    private void OnValidate()
    {
        StageChange();
    }

    private void StageChange()
    {
        switch (_stageType)
        {
            case GameStageBackGroundType.yashiki_DayLight:
                GetComponent<MeshRenderer>().material = _targetMaterial[0];
                break;
            case GameStageBackGroundType.yashik_Night:
                GetComponent<MeshRenderer>().material = _targetMaterial[1];
                break;
        }
    }

}
