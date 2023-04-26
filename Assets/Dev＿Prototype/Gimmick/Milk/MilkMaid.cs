using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����𒍂����̃M�~�b�N
/// </summary>

public class MilkMaid : MonoBehaviour
{

    [SerializeField] private float _fadeDuration;//�����鑬�x�A�Ԋu
    
    private Image _milkPanel;
    private Color _fadeColor = default;
    private bool isGimmickAction;

    private StageMove _stageMove;

    private void Awake()
    {
        _stageMove = GameObject.Find("StageManager").GetComponent<StageMove>();
        _milkPanel = GameObject.Find("MilkPanel").gameObject.GetComponent<Image>();
        _fadeColor = _milkPanel.gameObject.GetComponent<Image>().color;
        isGimmickAction = false;
    }


    // Update is called once per frame
    void Update()
    {
        GimmickAction();
    }

    /// <summary>
    /// �A�N�V�����J�n�̃��\�b�h
    /// </summary>
    private void GimmickAction()
    {
        if (this.gameObject.transform.position.x <= 12 && !isGimmickAction)//x����12�ȉ��ɂȂ�����
        {
            AudioManager.Instance.CriAtomPlay(CueSheet.SE, "SE_enemy_kaiga_miruku");
            StartCoroutine(FadeIn());
            isGimmickAction = true;
        }
    }

    /// <summary>
    /// �~���N�p�l���̃t�F�[�h�C������
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeIn()
    {
        _milkPanel.enabled = true;//image��on

        float clearScale = 1f;//���l

        Color currentColor = _fadeColor;

        while (clearScale > 0f)//clearScale���O�ɂȂ�܂ŉ�
        {
            clearScale -= _fadeDuration * Time.deltaTime;//1�b���ƂɃ��l��������

            if (clearScale <= 0f)//�l��0�����ɂȂ邱�Ƃ�����Ă���
            {
                clearScale = 0f;
            }

            currentColor.a = clearScale;//���l��color�ɑ��

            _milkPanel.color = currentColor;//color�����ۂ�Image��color�ɑ��
            
            yield return null;
        }
        _milkPanel.enabled = false;//while�����I��������Image��false�ɂ���
    }

    /// <summary>
    /// �ړ�����
    /// </summary>
    private void FixedUpdate()
    {
        
         this.gameObject.transform.position -= new Vector3(Time.deltaTime * _stageMove.MoveSpeed, 0); 
    }
}
