using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimControler : MonoBehaviour
{
    [SerializeField] private StageMove _stageMove;

    private Animator _anim;
    private float _speed = 0;


    private void Start()
    {
        _stageMove = _stageMove.gameObject.GetComponent<StageMove>();
        _anim = GetComponent<Animator>();
        
    }

    private void FixedUpdate()
    {
        _speed = _stageMove.MoveSpeed;

        if (_anim)
        {
            _anim.SetFloat("Speed", _speed);
        }
    }

}
