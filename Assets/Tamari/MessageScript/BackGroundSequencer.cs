using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundSequencer : MonoBehaviour
{
    [SerializeField] ColorTransitioner _colorTransitioner;

    [SerializeField] Color[] _colors;
    private int _currentIndex = -1;
    void Start()
    {
        MoveNext();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_colorTransitioner.IsCompleted)
            {
                MoveNext();
            }
            else
            {
                _colorTransitioner?.Skip();
            }
            Debug.Log(_colorTransitioner.IsCompleted);
        }
    }

    /// <summary>
    /// éüÇÃÉZÉäÉtÇ™Ç†ÇÈÇ»ÇÁéüÇ…êiÇﬁ
    /// </summary>
    void MoveNext()
    {
        if (_colors is null or { Length: 0 })
        {
            return;
        }

        if (_currentIndex + 1 < _colors.Length)
        {
            _currentIndex++;
            _colorTransitioner?.Play(_colors[_currentIndex]);
        }
    }
}
