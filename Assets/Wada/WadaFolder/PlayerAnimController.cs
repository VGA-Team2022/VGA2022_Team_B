using System.Collections;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private Animator _animator;
    private bool isAnim;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SweetsFallPlayerAnimationChange()
    {
        if (!isAnim)
        {
            StartCoroutine(PlayerPanicAnim());
        }
    }

    public void GameClearPlayerAnimationChange()
    {
        if (!isAnim)
        {
            StartCoroutine(PlayerSmileAnim());
        }
    }

    private IEnumerator PlayerPanicAnim()
    {
        isAnim = true;
        _animator.SetBool("isPanic", true);
        yield return new WaitForSeconds(1.5f);
        _animator.SetBool("isPanic", false);

        yield return new WaitForSecondsRealtime(0.5f);
        isAnim = false;
    }
    private IEnumerator PlayerSmileAnim()
    {
        isAnim = true;
        _animator.SetBool("isSmile", true);
        yield return new WaitForSeconds(1.5f);
        _animator.SetBool("isSmile", false);

        yield return new WaitForSecondsRealtime(0.5f);
        isAnim = false;
    }
}
