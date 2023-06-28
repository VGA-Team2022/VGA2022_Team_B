using UnityEngine.SceneManagement;

public class SceneChangeScript
{
    private static bool roadNow = false;

    /// <summary>
    /// �w��V�[���Ɉڍs����
    /// </summary>
    public static void LoadScene(string sceneName)
    {
        if (roadNow) return;

        roadNow = true;
        FadeScript.StartFadeOut(() => Load(sceneName));
        //  GameManager.isFindScenemng = false;
    }
    public static void NoFadeLoadScene(string sceneName)
    {
        if (roadNow) return;

        roadNow = true;
        Load(sceneName);
    }
    static void Load(string sceneName)
    {
        roadNow = false;
        SceneManager.LoadScene(sceneName);
    }

    /// <summary> ���̃X�e�[�W�ɐi�ނƂ��AGameManager�̒l���X�V���� </summary>
    /// <return> ���킷��X�e�[�W�������true, �Ȃ����false </return>
    public static bool StageUp()
    {
        GameManager.IsGameClear = false;

        //���~�X�e�[�W
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
        //�C�X�e�[�W
        else
        {
            if (GameManager.StageLevelNum == 0)
            {
                GameManager.StageLevelNum = 1;
            }
            else
            {
                //�Ō�܂ł������烊�Z�b�g
                GameManager.GameStageNum = 0;
                GameManager.StageLevelNum = 0;
                return false;
            }
        }
        return true;
    }
}
