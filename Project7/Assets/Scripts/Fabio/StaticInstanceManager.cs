using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInstanceManager : MonoBehaviour
{
    public static StaticInstanceManager m_Instance;

    private NoteSpawner m_NoteSpawner;
    public NoteSpawner GetNoteSpawner
    {
        get { return m_NoteSpawner; }
    }

    private NoteChecker m_NoteChecker;
    public NoteChecker GetNoteChecker
    {
        get { return m_NoteChecker; }
    }

    private SongManager m_SongManager;
    public SongManager GetSongManager
    {
        get { return m_SongManager; }
    }

    private GameManager m_GameManager;
    public GameManager GetGameManager
    {
        get { return m_GameManager; }
    }

    private void Awake()
    {
        m_Instance = this;

        m_NoteSpawner = GetComponent<NoteSpawner>();
        m_NoteChecker = GetComponent<NoteChecker>();
        m_SongManager = GetComponent<SongManager>();
        m_GameManager = GetComponent<GameManager>();
    }
	

	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}
