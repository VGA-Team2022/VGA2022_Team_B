using System;
using System.Linq;
using UnityEngine;


/// <summary>
/// �X�e�[�W�ؑ�
/// </summary>
public enum SeaType
{ 
    Daylight,
    Sunset,
}
/// <summary>
/// �w�iobjectPrefab�̌^
/// </summary>
[Serializable]
class BackGroundObject
{
    [Tooltip("�C�̔w�iPrefab"), SerializeField] public SpriteRenderer[] _objPrefabs;
}

/// <summary>
/// �C�X�e�[�W�̔w�i��ω�������N���X
/// </summary>
public class SeaStageMoveScript : MonoBehaviour
{

    [Tooltip("�X�e�[�W�^�C�v"), SerializeField] private SeaType _stageLevelType;
    [Tooltip("�gobj"), SerializeField] private GameObject _wave; 
    //�g�̐���pscript
    private BackGroundScroll _waveScript;
    private MeshRenderer _waveMeshRenderer;
    
    [Header("�C�̔w�i�I�u�W�F�N�gPrefab")]
    [SerializeField] private BackGroundObject[] _backGroundObject;


    [Header("material")]
    [Tooltip("�ォ��DayLight��Sunset"),SerializeField] private Material[] _waveMaterial;
    [SerializeField] private Material[] _beauchMaterial;

    [Header("BeauchObj")]
    [SerializeField] private MeshRenderer[] _beauchMeshRenderer;

    private void Awake()
    {
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
            _waveMeshRenderer = _wave.gameObject.GetComponent<MeshRenderer>();
            _waveScript = _wave.gameObject.GetComponent<BackGroundScroll>();
            _waveScript.enabled = true;
            if (GameManager.StageLevelNum == 0)
            {
                _waveMeshRenderer.material = _waveMaterial[GameManager.StageLevelNum];
                _waveScript.TargetMaterial = _waveMaterial[GameManager.StageLevelNum];
                Debug.Log($"�C�X�e�[�W��");
                DisableObjects(_backGroundObject[1]._objPrefabs);
                BeauchColorChange(GameManager.StageLevelNum);
            }
            else if (GameManager.StageLevelNum == 1)
            {
                _waveMeshRenderer.material = _waveMaterial[GameManager.StageLevelNum];
                _waveScript.TargetMaterial = _waveMaterial[GameManager.StageLevelNum];
                Debug.Log($"�C�X�e�[�W�[��");
                DisableObjects(_backGroundObject[0]._objPrefabs);
                BeauchColorChange(GameManager.StageLevelNum);
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
                DisableObjects(backGround._objPrefabs);
                Debug.Log($"�C�X�e�[�W�ł͂Ȃ��̂�{backGround._objPrefabs}���\���ɂ���");
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
