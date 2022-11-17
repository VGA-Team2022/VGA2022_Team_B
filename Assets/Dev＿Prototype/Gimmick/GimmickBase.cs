using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ƒMƒ~ƒbƒN‚ÌŠî’êscript
/// </summary>

public class GimmickBase : MonoBehaviour
{
    [SerializeField] private float _speed;
    public float Speed
    {get { return _speed; }set { _speed = value; }}

    //[SerializeField] private StageMove _stageMove;
    void Start()
    {
        //Speed = _stageMove.gameObject.GetComponent<StageMove>().MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
