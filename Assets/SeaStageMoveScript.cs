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
    [Tooltip("海の背景Prefab"), SerializeField] public GameObject[] _objPrefabs;
}


public class SeaStageMoveScript : MonoBehaviour
{

    [Tooltip("ステージタイプ"),SerializeField] private SeaType _stageLevelType;

    [Header("海の背景オブジェクトPrefab")]
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
