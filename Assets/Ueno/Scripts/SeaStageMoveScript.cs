using Common;
using System;
using System.Linq;
using UnityEngine;

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

    private void StageJudge()
    {
        if (GameManager.GameState.Stage == StageType.SEA)
        {
            _waveMeshRenderer = _wave.GetComponent<MeshRenderer>();
            _waveScript = _wave.GetComponent<BackGroundScroll>();
            _waveScript.enabled = true;

            _waveMeshRenderer.material = _waveMaterial[(int)GameManager.GameState.Stage % 2];
            _waveScript.TargetMaterial = _waveMaterial[(int)GameManager.GameState.Stage % 2];
            DisableObjects(_backGroundObject[1].ObjPrefabs);
            BeauchColorChange((int)GameManager.GameState.Stage % 2);

            if (GameManager.GameState.Time == StageTime.DAYTIME)
            {
                Debug.Log("海ステージ昼");
            }
            else if (GameManager.GameState.Time == StageTime.NIGHT)
            {
                Debug.Log("海ステージ夕方");
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
        if (stageType == 1)
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
        else if (stageType == 0)
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
