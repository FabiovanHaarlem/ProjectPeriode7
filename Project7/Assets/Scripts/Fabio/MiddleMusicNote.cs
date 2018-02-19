using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleMusicNote : MonoBehaviour
{
    public void Setup(Vector2 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
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
