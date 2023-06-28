using Common;
using UnityEngine;

public class LoadToNextStageButton : MonoBehaviour
{
    [Tooltip("シーン遷移先")]
    [SerializeField] private SceneNames _sceneNames = default;

    /// <summary> 次のステージに挑戦する </summary>
    public void LoadNextSatge()
    {
        SceneChangeScript.NoFadeLoadScene(Define.Scenes[_sceneNames]);
    }
}
