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
        if (GameManager.StageType == StageType.YASHIKI_DAYTIME || GameManager.StageType == StageType.YASHIKI_NIGHT ||
            GameManager.StageType == StageType.GARDEN_DAYTIME || GameManager.StageType == StageType.GARDEN_NIGHT)
        {
            IsSea = false;
        }
        else if (GameManager.StageType == StageType.SEA_DAYTIME || GameManager.StageType == StageType.SEA_NIGHT)
        {
            _seaScript = _seaScript.gameObject.GetComponent<SeaStageMoveScript>();
            _seaScript.enabled = true;
            IsSea = true;
        }
    }

    /// <summary> 指定されているenumに沿ってステージ上の切替を行う </summary>
    private void StageChange()
    {
        switch (GameManager.StageType)
        {
            case StageType.YASHIKI_DAYTIME:
                SetStageMaterial(_targetMaterials[0]);
                SetGroundObject(0);
                PlayBGM(SoundManager.BGM_Type.BGM_Yshiki_DayLight);
                break;
            case StageType.YASHIKI_NIGHT:
                SetGroundObject(0);
                SetStageMaterial(_targetMaterials[1]);
                PlayBGM(SoundManager.BGM_Type.BGM_Yashiki_Night);
                break;
            case StageType.SEA_DAYTIME:
                SetGroundObject(1);
                SetStageMaterial(_targetMaterials[2]);
                PlayBGM(SoundManager.BGM_Type.BGM_Sea_DayLight);
                break;
            case StageType.SEA_NIGHT:
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