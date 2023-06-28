using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// ステージ切替
/// </summary>
public enum SeaType
{ 
    Daylight,
    Sunset,
}
/// <summary>
/// 背景objectPrefabの型
/// </summary>
[Serializable]
class BackGroundObject
{
    [Tooltip("海の背景Prefab"), SerializeField] public SpriteRenderer[] _objPrefabs;
}

/// <summary>
/// 海ステージの背景を変化させるクラス
/// </summary>
public class SeaStageMoveScript : MonoBehaviour
{

    [Tooltip("ステージタイプ"), SerializeField] private SeaType _stageLevelType;
    [Tooltip("波obj"), SerializeField] private GameObject _wave; 
    //波の制御用script
    private BackGroundScroll _waveScript;
    
    [Header("海の背景オブジェクトPrefab")]
    [SerializeField] private BackGroundObject[] _backGroundObject;


    [Header("material")]
    [Tooltip("上からDayLight→Sunset"),SerializeField] private Material[] _waveMaterial;
    [SerializeField] private Material[] _beauchMaterial;

    [Header("BeauchObj")]
    [SerializeField] private MeshRenderer[] _beauchMeshRenderer;

    private void Awake()
    {
        CacheReferences();
        StageJudge();
        this.enabled = false;
    }

    private void OnValidate()
    {
        StageJudge();
    }

    private void CacheReferences()
    {
        _waveScript = _wave.GetComponent<BackGroundScroll>();
    }

    private void StageJudge()
    {
        if (GameManager.GameStageNum == 1)
        {
            _waveScript.enabled = true;
            if (GameManager.StageLevelNum == 0)
            {
                _waveScript.TargetMaterial = _waveMaterial[0];
                Debug.Log($"海ステージ昼");
                DisableObjects(_backGroundObject[1]._objPrefabs);
            }
            else if (GameManager.StageLevelNum == 1)
            {
                _waveScript.TargetMaterial = _waveMaterial[1];
                Debug.Log($"海ステージ夕方");
                DisableObjects(_backGroundObject[0]._objPrefabs);
            }
            else
            {
                Debug.LogError("範囲外の値が入力されました");
            }
        }
        else
        {
            foreach (var backGround in _backGroundObject)
            {
                DisableObjects(backGround._objPrefabs);
                Debug.Log($"海ステージではないので{backGround._objPrefabs}を非表示にした");
            }
        }
    }

    private void DisableObjects(SpriteRenderer[] objects)
    {
        foreach (var obj in objects)
        {
            obj.enabled = false;
        }
    }

    //private void BeauchColorChange(int stageType)
    //{
    //    foreach (var item in _beauchMeshRenderer)
    //    {
    //        item.material = _beauchMaterial[stageType];
    //    }
    //}
}
