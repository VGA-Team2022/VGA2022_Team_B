using System;
using System.Linq;
using UnityEngine;

/// <summary> �X�e�[�W�ؑ� </summary>
public enum SeaType
{ 
    Daylight,
    Sunset,
}

/// <summary> �w�iobjectPrefab�̌^ </summary>
[Serializable]
class BackGroundObject
{
    [Tooltip("�C�̔w�iPrefab")]
    [SerializeField] private SpriteRenderer[] _objPrefabs;

    public SpriteRenderer[] ObjPrefabs => _objPrefabs;
}

/// <summary> �C�X�e�[�W�̔w�i��ω�������N���X </summary>
public class SeaStageMoveScript : MonoBehaviour
{

    [Tooltip("�X�e�[�W�^�C�v")]
    [SerializeField] private SeaType _stageLevelType;
    [Tooltip("�gobj")]
    [SerializeField] private GameObject _wave;

    //�g�̐���pscript
    private BackGroundScroll _waveScript;
    private MeshRenderer _waveMeshRenderer;
    
    [Header("�C�̔w�i�I�u�W�F�N�gPrefab")]
    [SerializeField] private BackGroundObject[] _backGroundObject;


    [Header("�eMaterial")]
    [Tooltip("�ォ��DayLight��Sunset")]
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
                Debug.Log($"�C�X�e�[�W��");
            }
            else if (GameManager.StageLevelNum == 1)
            {
                Debug.Log($"�C�X�e�[�W�[��");
            }
            else
            {
                Debug.LogError("�͈͊O�̒l�����͂���܂���");
            }
        }
        else
        {
            foreach (var backGround in _backGroundObject)
            {
                DisableObjects(backGround.ObjPrefabs);
                Debug.Log($"�C�X�e�[�W�ł͂Ȃ��̂� {backGround.ObjPrefabs} ���\���ɂ���");
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
            Debug.Log("�X�e�[�W�͒�");
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
