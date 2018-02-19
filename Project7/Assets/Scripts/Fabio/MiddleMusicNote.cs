using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleMusicNote : MonoBehaviour
{
    int m_ID;

    public void Setup(Vector2 position, int id)
    {
        transform.position = position;
        gameObject.SetActive(true);
        m_ID = id;
    }

    public void ResetMusicNote()
    {
        gameObject.SetActive(false);
        //Change sprite back to normale or disable particle
    }

    public void Found()
    {
        //Change sprite to found or activate particle
    }
}
