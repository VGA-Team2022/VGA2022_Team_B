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
    [HideInInspector] public Material CurrentMaterial;

    private void OnValidate()
    {
        StageChange();
    }

    private void Start()
    {
       _stageType = (GameManager.GameStageNum == 0 && GameManager.StageLevelNum == 0) ? GameStageBackGroundType.yashiki_DayLight : GameStageBackGroundType.yashik_Night;

        StageChange();
    }

    private void StageChange()
    {
        switch (_stageType)
        {
            case GameStageBackGroundType.yashiki_DayLight:
                GetComponent<MeshRenderer>().material = _targetMaterial[0];
                CurrentMaterial = _targetMaterial[0];
                AudioManager.Instance.CriAtomBGMPlay("BGM_mansion_middey");
                break;
            case GameStageBackGroundType.yashik_Night:
                GetComponent<MeshRenderer>().material = _targetMaterial[1];
                CurrentMaterial = _targetMaterial[1];
                AudioManager.Instance.CriAtomBGMPlay("BGM_mansion_night");
                break;
        }
    }

}
