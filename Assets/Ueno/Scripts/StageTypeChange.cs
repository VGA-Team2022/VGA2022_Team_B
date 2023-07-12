using Common;
using System;
using UnityEngine;

/// <summary> ステージ切替のクラス </summary>
public class StageTypeChange : MonoBehaviour
{
    [Header("アタッチするもの")]
    [SerializeField] private Material[] _targetMaterials;
    [SerializeField] private GameObject[] _groundsObjs;

    [HideInInspector] public Material CurrentMaterial;

    [Header("海ステージ用の設定")]
    public bool IsSea;
    [SerializeField] private SeaStageMoveScript _seaScript;

    private SoundManager _soundManager;


    private void Start()
    {
        _soundManager = SoundManager.InstanceSound;

        SettingStageType();

        StageChange();
    }

    /// <summary> enumを変更する為のメソッド </summary>
    private void SettingStageType()
    {
        if (GameManager.GameState.Stage == StageType.SEA)
        {
            _seaScript = _seaScript.gameObject.GetComponent<SeaStageMoveScript>();
            _seaScript.enabled = true;
            IsSea = true;
        }
        else
        {
            IsSea = false;
        }
    }

    /// <summary> 指定されているenumに沿ってステージ上の切替を行う </summary>
    private void StageChange()
    {
        switch (GameManager.GameState)
        {
            case GameState { Stage: StageType.YASHIKI, Time: StageTime.DAYTIME }:
                SetGroundObject(0);
                SetStageMaterial(_targetMaterials[0]);
                PlayBGM(SoundManager.BGM_Type.BGM_Yshiki_DayLight);
                break;

            case GameState { Stage: StageType.YASHIKI, Time: StageTime.NIGHT }:
                SetGroundObject(0);
                SetStageMaterial(_targetMaterials[1]);
                PlayBGM(SoundManager.BGM_Type.BGM_Yashiki_Night);
                break;

            case GameState { Stage: StageType.SEA, Time: StageTime.DAYTIME }:
                SetGroundObject(1);
                SetStageMaterial(_targetMaterials[2]);
                PlayBGM(SoundManager.BGM_Type.BGM_Sea_DayLight);
                break;

            case GameState { Stage: StageType.SEA, Time: StageTime.NIGHT }:
                SetGroundObject(1);
                SetStageMaterial(_targetMaterials[3]);
                PlayBGM(SoundManager.BGM_Type.BGM_Sea_Sunset);
                break;
        }
    }

    /// <summary> 背景のマテリアル設定 </summary>
    private void SetStageMaterial(Material material)
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = material;
        CurrentMaterial = material;
    }

    /// <summary> 地面の設定 </summary>
    private void SetGroundObject(int num)
    {
        Array.ForEach(_groundsObjs, obj => obj.SetActive(false));

        if (num >= 0 && num < _groundsObjs.Length)
        {
            _groundsObjs[num].SetActive(true);
        }
    }

    private void PlayBGM(SoundManager.BGM_Type bgmType)
    {
        _soundManager.PlayAudioClip(bgmType);
    }
}