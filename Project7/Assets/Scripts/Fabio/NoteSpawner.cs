using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    private List<Node> m_CurrentlyUsedMusicNotes;
    private List<Node> m_DecoyMusicNotes;

    private List<List<Node>> m_AllSongs;
    private List<MiddleMusicNote> m_MiddleMusicNotes;

    private List<int> m_MiddleMusicNotesID;

    void Start ()
    {
        m_AllSongs = new List<List<Node>>();
        m_CurrentlyUsedMusicNotes = new List<Node>();

        m_DecoyMusicNotes = new List<Node>();

        m_MiddleMusicNotes = new List<MiddleMusicNote>();

        m_MiddleMusicNotesID = new List<int>();

        SetupGame();
        StartLevel();
	}

    private void SetupGame()
    {
        LoadSongMusicNotes();
        LoadDecoyMusicNotes();
        LoadMiddleMusicNotes();
    }

    private void SetupLevel()
    {
        PrepareSongNotes();
        PrepareDecoyMusicNotes();
        PrepareMiddleMusicNote();
    }

    private void StartLevel()
    {
        SetupLevel();

        for (int i = 0; i < m_CurrentlyUsedMusicNotes.Count; i++)
        {
            m_CurrentlyUsedMusicNotes[i].Activate(new Vector2(Random.Range(-4, 4), Random.Range(-4, 4)));
        }

        StaticInstanceManager.m_Instance.GetNoteChecker.GetMusicNotes(m_CurrentlyUsedMusicNotes, m_MiddleMusicNotesID, m_MiddleMusicNotes);
    }

    private void DeactivateLevel()
    {
        for (int i = 0; i < m_CurrentlyUsedMusicNotes.Count; i++)
        {
            m_CurrentlyUsedMusicNotes[i].Deactivate();
            m_CurrentlyUsedMusicNotes.Clear();
        }
    }

    private void LoadMiddleMusicNotes()
    {
        List<MiddleMusicNote> middleCheckNotes = new List<MiddleMusicNote>();

        for (int i = 0; i < 4; i++)
        {
            GameObject gameObjectNote = Instantiate(Resources.Load<GameObject>("Prefabs/MiddleMusicNote"));
            gameObjectNote.SetActive(false);
            gameObjectNote.layer = 9;

            middleCheckNotes.Add(gameObjectNote.GetComponent<MiddleMusicNote>());
        }
        m_MiddleMusicNotes =middleCheckNotes;
    }

    private void LoadSongMusicNotes()
    {
        Object[] loadedMiddleNotes = new Object[4];


        loadedMiddleNotes = Resources.LoadAll("Prefabs/Songs/Song0");
        LoadFromFolder(loadedMiddleNotes);

        loadedMiddleNotes = Resources.LoadAll("Prefabs/Songs/Song1");
        LoadFromFolder(loadedMiddleNotes);

        loadedMiddleNotes = Resources.LoadAll("Prefabs/Songs/Song2");
        LoadFromFolder(loadedMiddleNotes);

        loadedMiddleNotes = Resources.LoadAll("Prefabs/Songs/Song3");
        LoadFromFolder(loadedMiddleNotes);
    }

    private void LoadDecoyMusicNotes()
    {
        Object[] loadedDecoyMusicNotes = Resources.LoadAll("Prefabs/DecoyMusicNotes");

        for (int i = 0; i < loadedDecoyMusicNotes.Length; i++)
        {
            GameObject gameObject = Instantiate((GameObject)loadedDecoyMusicNotes[i]);
            m_DecoyMusicNotes.Add(gameObject.GetComponent<Node>());
            gameObject.SetActive(false);
        }
    }

    private void PrepareMiddleMusicNote()
    {
        for (int i = 0; i < m_MiddleMusicNotes.Count; i++)
        {
            m_MiddleMusicNotes[i].Setup(new Vector2(-5f + 2f * (i + 1f), 0f), m_CurrentlyUsedMusicNotes[i].GetComponent<SpriteRenderer>().sprite, m_CurrentlyUsedMusicNotes[i].GetID());
            m_MiddleMusicNotes[i].name = "MiddleNote " + i;
        }
    }

    private void PrepareSongNotes()
    {
        int randomIndex = Random.Range(0, m_AllSongs.Count);
        List<Node> musicNotes = m_AllSongs[randomIndex];



        for (int i = 0; i < musicNotes.Count; i++)
        {
            int id = i + 1;

            m_MiddleMusicNotesID.Add(id);

            Node musicNote = musicNotes[i];
            musicNote.Setup(Vector2.zero, id);
            m_CurrentlyUsedMusicNotes.Add(musicNote);


        }
    }

    private void PrepareDecoyMusicNotes()
    {
        for (int i = 0; i < 10; i++)
        {
            int id = 0;

            Node musicNote = m_DecoyMusicNotes[i];
            musicNote.Setup(Vector2.zero, id);
            m_CurrentlyUsedMusicNotes.Add(musicNote);
        }
    }

    private void LoadFromFolder(Object[] loadedMiddleNotes)
    {
        List<Node> middleNotes = new List<Node>();

        for (int i = 0; i < loadedMiddleNotes.Length; i++)
        {
            GameObject gameObjectNote = Instantiate((GameObject)loadedMiddleNotes[i]);
            gameObjectNote.SetActive(false);
            gameObjectNote.layer = 10;
            middleNotes.Add(gameObjectNote.GetComponent<Node>());
        }
        m_AllSongs.Add(middleNotes);
    }
}
