using UnityEngine;

/// <summary>
///�G�l�~�[�ړ�
/// </summary>
public class EnemyMove : MonoBehaviour
{
    /// <summary>
    /// �G�l�~�[�̃X�s�[�h
    /// </summary>
    [Tooltip("�G�l�~�[�ɃA�^�b�`����"), SerializeField] private float _speed = 2.0f;

    /// <summary>�G�l�~�[�̎��ړ�����</summary>
    private int x = 0;

    private void Start()
    {
        GetComponent<Transform>().position = this.gameObject.transform.position;
    }

    /// <summary>
    /// �G�l�~�[�̈ړ�
    /// </summary>
    void Update()
    {
        if(this.gameObject.name == "dog") x += 1;//�w�i�X�s�[�h*����
        else  x-= 1;
        transform.Translate(x, 0, 0);

        if (x <= 30 || x >= -30)
        {
            Destroy(this.gameObject);
        }
    }
}
