using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public void StartLevel()
    {
        StaticInstanceManager.m_Instance.GetNoteSpawner.StartLevel();
    }
}
