using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimControl : MonoBehaviour
{
    Animator _animator;

    bool _coroutine;
    void Start()
    {
        _animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Abunaaaaaaai()
    {
        if(!_coroutine)
        {
            StartCoroutine(FaceReturn());
        }
    }

    IEnumerator FaceReturn()
    {
        _coroutine = true;
        _animator.SetBool("Face", true);
        yield return new WaitForSecondsRealtime(1.5f);
        _animator.SetBool("Face", false);

        yield return new WaitForSecondsRealtime(0.5f);
        _coroutine = false;
    }
}
