using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMove : MonoBehaviour
{
    [SerializeField] private GameObject[] _wall;
    [SerializeField] private Vector3 _startPos;
    [SerializeField] private Vector3 _endPos;
    [SerializeField] private float _moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        for (int i = 0; i < _wall.Length; i++)
        {
            
            _wall[i].transform.position -= new Vector3(Time.deltaTime * _moveSpeed,0);

            
            if (_wall[i].transform.position.x <= _endPos.x)
            {
                _wall[i].transform.position = _startPos;
            }
        }
    }

    private void SetPos()
    {
        for (int i = 0; i < _wall.Length; i++)
        {
            if (_wall[i].transform.position == _endPos)
            {
                _wall[i].SetActive(false);
                _wall[i].transform.position = _startPos;
                _wall[i].SetActive(true);
            }
        }
    }
}
