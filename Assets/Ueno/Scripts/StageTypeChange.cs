using System;
using Unity.VisualScripting;
using UnityEngine;
public enum GameStageBackGroundType
{
    yashiki_Daylight,
    yashik_Night,
    sea_Daylight,
    sea_Sunset,
}
/// <summary>
/// ステージ切替のクラス
/// </summary>
public class StageTypeChange : MonoBehaviour
{
    [SerializeField] private GameStageBackGroundType _stageType = GameStageBackGroundType.yashiki_Daylight;
    [Header("アタッチするもの")]
    [SerializeField] private Material[] _targetMaterial;
    [SerializeField] private GameObject[] _groundsObj;

    [HideInInspector] public Material CurrentMaterial;

    [Header("海ステージ用の設定")]
    [SerializeField] public bool isSea;
    [SerializeField] private SeaStageMoveScript _seaScript;


    private Soundmanager _soundManager;

    private void Start()
    {
        _soundManager = Soundmanager.InstanceSound;

        SettingStageType();

        StageChange();
    }

    /// <summary>
    /// enumを変更する為のメソッド
    /// </summary>
    private void SettingStageType()
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
            isSea = true;
        }
    }

    /// <summary>
    ///指定されているenumに沿ってステージ上の切替を行う
    /// </summary>
    private void StageChange()
    {
        switch (_stageType)
        {
            case GameStageBackGroundType.yashiki_Daylight:
                SetStageMaterial(_targetMaterial[0]);
                SetGroundObject(0);
                PlayBGM(Soundmanager.BGM_Type.BGM_Yshiki_DayLight);
                break;
            case GameStageBackGroundType.yashik_Night:
                SetGroundObject(0);
                SetStageMaterial(_targetMaterial[1]);
                PlayBGM(Soundmanager.BGM_Type.BGM_Yashiki_Night);
                break;
            case GameStageBackGroundType.sea_Daylight:
                SetGroundObject(1);
                SetStageMaterial(_targetMaterial[2]);
                PlayBGM(Soundmanager.BGM_Type.BGM_Sea_DayLight);
                break;
            case GameStageBackGroundType.sea_Sunset:
                SetGroundObject(1);
                SetStageMaterial(_targetMaterial[3]);
                PlayBGM(Soundmanager.BGM_Type.BGM_Sea_Sunset);
                break;

        }
    }

    /// <summary>
    /// 背景のマテリアル設定
    /// </summary>
    /// <param name="material"></param>
    private void SetStageMaterial(Material material)
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = material;
        CurrentMaterial = material;
    }

    /// <summary>
    ///地面の設定 
    /// </summary>
    private void SetGroundObject(int num)
    {
        Array.ForEach(_groundsObj, obj => obj.SetActive(false));

        if (num >= 0 && num < _groundsObj.Length)
        {
            _groundsObj[num].SetActive(true);
        }
    }


    private void PlayBGM(Soundmanager.BGM_Type bgmType)
    {
        _soundManager.PlayAudioClip(bgmType);
    }
}