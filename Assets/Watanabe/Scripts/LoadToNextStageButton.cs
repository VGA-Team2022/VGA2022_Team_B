using Common;
using UnityEngine;

public class LoadToNextStageButton : MonoBehaviour
{
    [SerializeField] private SceneNames _sceneNames = default;

    /// <summary> 次のステージに挑戦する </summary>
    public void LoadNextSatge()
    {
        SceneChangeScript.NoFadeLoadScene(Define.Scenes[_sceneNames]);
    }
}
