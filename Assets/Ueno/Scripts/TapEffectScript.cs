using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Soundmanager;
/// <summary>
/// 画面をタップした時にタップエフェクトを出したい
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
            // マウスのワールド座標までパーティクルを移動し、パーティクルエフェクトを1つ生成する
            var pos = _camera.ScreenToWorldPoint(Input.mousePosition + _camera.transform.forward * 10);
            Soundmanager.InstanceSound.PlayAudioClip(SE_Type.Touch_Button);
            _tapEffect.transform.position = pos;
            _tapEffect.Emit(10);
        }
    }
}
