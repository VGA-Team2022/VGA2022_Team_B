using System;
using System.Collections;
using System.Collections.Generic;
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

    /// <summary>
    /// 
    /// </summary>
    private void StageJudge()
    {
        if (GameManager.GameStageNum == 1)
        {
            _waveScript = _wave.GetComponent<BackGroundScroll>();
            _waveScript.enabled = true;
            if (GameManager.StageLevelNum == 0)
            {
                _waveScript.TargetMaterial = _waveMaterial[0];
                foreach (var i in _backGroundObject[1]._objPrefabs)
                {
                    i.enabled = false;//_backGroundObject[0]��object�ȊO���\���ɂ���
                }
            }
            else if (GameManager.StageLevelNum == 1)
            {
                _waveScript.TargetMaterial = _waveMaterial[1];
                foreach (var i in _backGroundObject[0]._objPrefabs)
                {
                    i.enabled = false;//_backGroundObject[1]��object�ȊO���\���ɂ���
                }
            }
            else
            {
                Debug.LogError("�͈͊O�̒l�����͂���܂���");
            }
        }
        else
        {
            for (int i = 0; i < _backGroundObject.Length; i++)
            {
                foreach (var obj in _backGroundObject[i]._objPrefabs)
                {
                    obj.enabled = false;//�Cobject���\���ɂ���
                }
            }
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="stagetype"></param>
    private void BeauchColorChange(int stagetype) 
    {
        //foreach (var item in _beauchMeshRenderer)
        //{
        //    item.GetComponent<MeshRenderer>().material = _;
        //}
    }

}
