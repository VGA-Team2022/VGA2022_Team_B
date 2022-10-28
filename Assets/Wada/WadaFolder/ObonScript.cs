using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObonScript : MonoBehaviour
{
    [SerializeField] Transform _obonPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = _obonPos.position;
    }
}
