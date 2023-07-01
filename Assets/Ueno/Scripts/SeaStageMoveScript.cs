using System;
using System.Linq;
using UnityEngine;

/// <summary> ステージ切替 </summary>
public enum SeaType
{ 
    Daylight,
    Sunset,
}

/// <summary> 背景objectPrefabの型 </summary>
[Serializable]
class BackGroundObject
{
    [Tooltip("海の背景Prefab")]
    [SerializeField] private SpriteRenderer[] _objPrefabs;

    public SpriteRenderer[] ObjPrefabs => _objPrefabs;
}

/// <summary> 海ステージの背景を変化させるクラス </summary>
public class SeaStageMoveScript : MonoBehaviour
{

    [Tooltip("ステージタイプ")]
    [SerializeField] private SeaType _stageLevelType;
    [Tooltip("波obj")]
    [SerializeField] private GameObject _wave;

    //波の制御用script
    private BackGroundScroll _waveScript;
    private MeshRenderer _waveMeshRenderer;
    
    [Header("海の背景オブジェクトPrefab")]
    [SerializeField] private BackGroundObject[] _backGroundObject;


    [Header("各Material")]
    [Tooltip("上からDayLight→Sunset")]
    [SerializeField] private Material[] _waveMaterial;
    [SerializeField] private Material[] _beauchMaterial;

    [Header("BeauchObj")]
    [SerializeField] private MeshRenderer[] _beauchMeshRenderer;

    private void Awake()
    {
        StageJudge();
        enabled = false;
    }

    private void CacheReferences()                              
    {
        _waveScript = _wave.GetComponent<BackGroundScroll>();
    }

    private void StageJudge()
    {
        if (GameManager.GameStageNum == 1)
        {
            _waveMeshRenderer = _wave.GetComponent<MeshRenderer>();
            _waveScript = _wave.GetComponent<BackGroundScroll>();
            _waveScript.enabled = true;

            _waveMeshRenderer.material = _waveMaterial[GameManager.StageLevelNum];
            _waveScript.TargetMaterial = _waveMaterial[GameManager.StageLevelNum];
            DisableObjects(_backGroundObject[1].ObjPrefabs);
            BeauchColorChange(GameManager.StageLevelNum);

            if (GameManager.StageLevelNum == 0)
            {
                Debug.Log($"海ステージ昼");
            }
            else if (GameManager.StageLevelNum == 1)
            {
                Debug.Log($"海ステージ夕方");
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
                DisableObjects(backGround.ObjPrefabs);
                Debug.Log($"海ステージではないので {backGround.ObjPrefabs} を非表示にした");
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

    private void BeauchColorChange(int stageType)
    {
        if (stageType == 0)
        {
            Debug.Log("ステージは昼");
            _beauchMeshRenderer
                .Where((_, index) => index % 2 == 0)
                .ToList()
                .ForEach(meshRenderer => meshRenderer.gameObject.GetComponent<MeshRenderer>().material = _beauchMaterial[0]);

            _beauchMeshRenderer
                .Where((_, index) => index % 2 != 0)
                .ToList()
                .ForEach(meshRenderer => meshRenderer.gameObject.GetComponent<MeshRenderer>().material = _beauchMaterial[1]);
        }
        else if (stageType == 1)
        {
            _beauchMeshRenderer
                .Where((_, index) => index % 2 == 0)
                .ToList()
                .ForEach(meshRenderer => meshRenderer.gameObject.GetComponent<MeshRenderer>().material = _beauchMaterial[2]);

            _beauchMeshRenderer
                .Where((_, index) => index % 2 != 0)
                .ToList()
                .ForEach(meshRenderer => meshRenderer.gameObject.GetComponent<MeshRenderer>().material = _beauchMaterial[3]);
        }
    }
}
