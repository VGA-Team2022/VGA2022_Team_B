using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    
    [Header("�C�̔w�i�I�u�W�F�N�gPrefab")]
    [SerializeField] private BackGroundObject[] _backGroundObject;


    [Header("material")]
    [Tooltip("�ォ��DayLight��Sunset"),SerializeField] private Material[] _waveMaterial;
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
                Debug.Log($"�C�X�e�[�W��");
                DisableObjects(_backGroundObject[1]._objPrefabs);
            }
            else if (GameManager.StageLevelNum == 1)
            {
                _waveScript.TargetMaterial = _waveMaterial[1];
                Debug.Log($"�C�X�e�[�W�[��");
                DisableObjects(_backGroundObject[0]._objPrefabs);
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

    //private void BeauchColorChange(int stageType)
    //{
    //    foreach (var item in _beauchMeshRenderer)
    //    {
    //        item.material = _beauchMaterial[stageType];
    //    }
    //}
}
