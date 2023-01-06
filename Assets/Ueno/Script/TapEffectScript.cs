using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ʂ��^�b�v�������Ƀ^�b�v�G�t�F�N�g���o������
/// </summary>
public class TapEffectScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem _tapEffect;
    [SerializeField] private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
       _tapEffect = _tapEffect.gameObject.GetComponent<ParticleSystem>();  
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // �}�E�X�̃��[���h���W�܂Ńp�[�e�B�N�����ړ����A�p�[�e�B�N���G�t�F�N�g��1��������
            var pos = _camera.ScreenToWorldPoint(Input.mousePosition + _camera.transform.forward * 10);
            SoundManager.Instance.CriAtomPlay(CueSheet.SE, "SE_touch_normal");
            _tapEffect.transform.position = pos;
            _tapEffect.Emit(10);
        }
    }
}
