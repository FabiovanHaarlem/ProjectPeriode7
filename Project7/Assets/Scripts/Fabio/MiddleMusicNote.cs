using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleMusicNote : MonoBehaviour
{
    private SpriteRenderer m_SpriteRenderer;
    private int m_ID;

    private void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Setup(Vector2 position, Sprite sprite, int id)
    {
        transform.position = position;
        gameObject.SetActive(true);
        m_ID = id;
        m_SpriteRenderer.sprite = sprite;
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
