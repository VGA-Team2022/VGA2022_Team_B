using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SeaType
{ 
    Daylight,
    Sunset,
}

[Serializable]
class BackGroundObject
{
    [Tooltip("�C�̔w�iPrefab"), SerializeField] public GameObject[] _objPrefabs;
}


public class SeaStageMoveScript : MonoBehaviour
{

    [Tooltip("�X�e�[�W�^�C�v"),SerializeField] private SeaType _stageLevelType;

    [Header("�C�̔w�i�I�u�W�F�N�gPrefab")]
    [SerializeField] private BackGroundObject[] _backGroundObject;

    // Start is called before the first frame update
    private void Awake()
    {
        this.enabled= false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
