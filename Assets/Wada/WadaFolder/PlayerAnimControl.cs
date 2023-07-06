using System.Collections;
using UnityEngine;

public class PlayerAnimControl : MonoBehaviour
{
    private Animator _animator;
    private bool _coroutine;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Abunaaaaaaai()
    {
        if(!_coroutine)
        {
            StartCoroutine(FaceReturn());
        }
    }

    private IEnumerator FaceReturn()
    {
        _coroutine = true;
        _animator.SetBool("Face", true);
        yield return new WaitForSecondsRealtime(1.5f);
        _animator.SetBool("Face", false);

        yield return new WaitForSecondsRealtime(0.5f);
        _coroutine = false;
    }
}
