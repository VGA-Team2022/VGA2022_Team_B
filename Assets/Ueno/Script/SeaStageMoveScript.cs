using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Tooltip("背景-空"),SerializeField] private MeshRenderer _skyMesh;
    
    [Header("海の背景オブジェクトPrefab")]
    [SerializeField] private BackGroundObject[] _backGroundObject;


    [Header("material")]
    [Tooltip("上からDayLight→Sunset"), SerializeField] private Material[] _skyMaterial;
    [Tooltip("上からDayLight→Sunset"),SerializeField] private Material[] _waveMaterial;

   private void Awake()
    {
        _skyMesh.enabled = false;
        _waveScript = _wave.GetComponent<BackGroundScroll>();

        StageJudge();
        
        this.enabled = false;
    }

    private void OnValidate()
    {
        StageJudge();
    }

    private void StageJudge()
    {
        if (GameManager.GameStageNum == 1)
        {
            _waveScript.enabled = true;
            if (GameManager.StageLevelNum == 0)
            {
                foreach (var i in _backGroundObject[1]._objPrefabs)
                {
                    i.enabled = false;//_backGroundObject[0]のobject以外を非表示にする
                }
            }
            else if (GameManager.StageLevelNum == 1)
            {
                foreach (var i in _backGroundObject[0]._objPrefabs)
                {
                    i.enabled = false;//_backGroundObject[1]のobject以外を非表示にする
                }
            }
            else
            {
                Debug.LogError("範囲外の値が入力されました");
            }
        }
        else
        {
            for (int i = 0; i < _backGroundObject.Length; i++)
            {
                foreach (var obj in _backGroundObject[i]._objPrefabs)
                {
                    obj.enabled = false;//海objectを非表示にする
                }
            }
        }
    }
}
