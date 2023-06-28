using Common;
using UnityEngine;

public class LoadToNextStageButton : MonoBehaviour
{
    /// <summary> 次のステージに挑戦する </summary>
    public void LoadNextSatge(SceneNames scene)
    {
        SceneChangeScript.LoadScene(Define.Scenes[scene]);
    }

    /// <summary> 次のステージに進むとき、GameManagerの値を更新する </summary>
    private void StageUp()
    {
        //屋敷ステージ
        if (GameManager.GameStageNum == 0)
        {
            if (GameManager.StageLevelNum == 0)
            {
                GameManager.StageLevelNum = 1;
            }
            else
            {
                GameManager.GameStageNum = 1;
                GameManager.StageLevelNum = 0;
            }
        }
        //海ステージ
        else
        {
            if (GameManager.StageLevelNum == 0)
            {
                GameManager.StageLevelNum = 1;
            }
            else
            {
                //最後までいったらリセット
                GameManager.GameStageNum = 0;
                GameManager.StageLevelNum = 0;
            }
        }
    }
}
