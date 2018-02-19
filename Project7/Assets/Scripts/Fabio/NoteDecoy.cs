using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteDecoy : MonoBehaviour
{
    private int m_ID;
    [SerializeField]
    private int m_SongIndex;

    public void Setup(int iD)
    {
        m_ID = iD;
    }

    public void Activate(Vector2 startPosition)
    {
        transform.position = startPosition;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public int GetID()
    {
        return m_ID;
    }
    
    public void IsMiddleMusicNote()
    {
        Deactivate();
    }

    public void IsNotMiddleMusicNote()
    {
        Deactivate();
    }
}
