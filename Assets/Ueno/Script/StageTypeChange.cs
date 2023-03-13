using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameStageBackGroundType
{
    yashiki_Daylight,
    yashik_Night,
    sea_Daylight,
    sea_Sunset,
}
public class StageTypeChange : MonoBehaviour
{
    [SerializeField] private GameStageBackGroundType _stageType = GameStageBackGroundType.yashiki_Daylight;
    [Header("アタッチするもの")]
    [SerializeField] private Material[] _targetMaterial;
    [HideInInspector] public Material CurrentMaterial;

    [SerializeField] public bool isSea;
    [SerializeField] private SeaStageMoveScript _seaScript;
    

    private void OnValidate()
    {
        StageChange();
    }

    private void Start()
    {
        if (GameManager.GameStageNum == 0)
        {
            _stageType = (GameManager.StageLevelNum == 0) ? GameStageBackGroundType.yashiki_Daylight : GameStageBackGroundType.yashik_Night;
            isSea = false;
        }
        else if (GameManager.GameStageNum == 1)
        {
            _stageType = (GameManager.StageLevelNum == 0) ? GameStageBackGroundType.sea_Daylight : GameStageBackGroundType.sea_Sunset;
            _seaScript = _seaScript.gameObject.GetComponent<SeaStageMoveScript>();
            _seaScript.enabled = true;
            isSea= true;
        }



        StageChange();
    }

    private void StageChange()
    {
        switch (_stageType)
        {
            case GameStageBackGroundType.yashiki_Daylight:
                GetComponent<MeshRenderer>().material = _targetMaterial[0];
                CurrentMaterial = _targetMaterial[0];
                SoundManager.Instance.CriAtomBGMPlay("BGM_mansion_middey");
                break;
            case GameStageBackGroundType.yashik_Night:
                GetComponent<MeshRenderer>().material = _targetMaterial[1];
                CurrentMaterial = _targetMaterial[1];
                SoundManager.Instance.CriAtomBGMPlay("BGM_mansion_night");
                break;
            case GameStageBackGroundType.sea_Daylight:
                
                break;
            case GameStageBackGroundType.sea_Sunset:

                break;
        }
    }

}
