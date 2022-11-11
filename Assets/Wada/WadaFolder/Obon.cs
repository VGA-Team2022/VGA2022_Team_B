using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obon : MonoBehaviour
{
    [Tooltip("�v���C���[���Q�[���J�n���Ɏ����Ă��邨�َq�̔z��"), SerializeField]
    private GameObject[] _startOkasis;

    [Tooltip("�v���C���[���v���C���Ɏ����Ă��邨�َq�̔z��")]
    private List<GameObject> _okasis = new List<GameObject>();

    [Tooltip("�v���C���[���Q�[���J�n���Ɏ����Ă��邨�َq�̔z��"), SerializeField]
    private int[] _int;

    private float _zure;

    float h;

    public float Zure
    {
        get
        {
            return _zure;
        }
        set
        {
            _zure = value;
        }
    }

    private void Awake()
    {
        for (int i = 0; i < _startOkasis.Length; i++)
        {
            if(_okasis.Count == 0)
            {
                _okasis.Add(_startOkasis[0]);
                _okasis[0].transform.position = this.transform.position;
            }
            else
            {
                _okasis.Add(_startOkasis[i]);
                _okasis[i].transform.position = _okasis[i - 1].GetComponent<Sweets>().NextPos.position;
            }
        }
    }

    private void FixedUpdate()
    {
        h = Input.GetAxisRaw("Horizontal");
        Zure += h * 0.005f;
    }

    public void SweetsAdd(GameObject[] gameObjects)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (_okasis.Count == 0)
            {
                _okasis.Add(gameObjects[0]);
                _okasis[0].transform.position = this.transform.position;
            }
            else
            {
                _okasis.Add(gameObjects[i]);
                _okasis[i].transform.position = _okasis[i - 1].GetComponent<Sweets>().NextPos.position;
            }
        }
    }
}
