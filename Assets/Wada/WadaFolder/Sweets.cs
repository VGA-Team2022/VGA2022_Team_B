using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweets : MonoBehaviour
{
    [SerializeField] Transform _nextPos;



    public Transform NextPos
    {
        get
        {
            return _nextPos;
        }
        set
        {

        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PutOnSweets(GameObject gameObj)
    {
        gameObj.transform.position = _nextPos.position;
    }
}
