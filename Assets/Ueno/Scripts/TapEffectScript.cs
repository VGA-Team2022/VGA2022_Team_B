using UnityEngine;

/// <summary> 画面をタップした時にタップエフェクトを出したい </summary>
public class TapEffectScript : MonoBehaviour
{
    [SerializeField] private ParticleSystem _tapEffect;
    [SerializeField] private Camera _camera;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // マウスのワールド座標までパーティクルを移動し、パーティクルエフェクトを1つ生成する
            var pos = _camera.ScreenToWorldPoint(Input.mousePosition + _camera.transform.forward * 10);
            //AudioManager.Instance.CriAtomPlay(CueSheet.SE, "SE_touch_normal");
            _tapEffect.transform.position = pos;
            _tapEffect.Emit(10);
        }
    }
}
