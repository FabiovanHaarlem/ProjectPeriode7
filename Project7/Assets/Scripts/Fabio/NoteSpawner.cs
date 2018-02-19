using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    private List<Node> m_AllCurrentlyUsedMusicNotes;
    private List<Node> m_MiddleMusicNotes;
    private List<Node> m_DecoyMusicNotes;

    private List<List<Node>> m_AllSongs;
    private List<List<MiddleMusicNote>> m_MiddleCheckMusicNotes;
    private List<MiddleMusicNote> m_CurrnetlyUsedMiddleCheckMusicNotes;

    private List<int> m_MiddleMusicNotesID;

    void Start ()
    {
        m_AllSongs = new List<List<Node>>();
        m_AllCurrentlyUsedMusicNotes = new List<Node>();

        m_MiddleCheckMusicNotes = new List<List<MiddleMusicNote>>();
        m_MiddleMusicNotes = new List<Node>();
        m_DecoyMusicNotes = new List<Node>();

        m_CurrnetlyUsedMiddleCheckMusicNotes = new List<MiddleMusicNote>();

        m_MiddleMusicNotesID = new List<int>();

        SetupGame();
        StartLevel();
	}

    private void SetupGame()
    {
        LoadMiddleMusicNotes();
        LoadDecoyMusicNotes();
    }

    private void SetupLevel()
    {
        PrepareMiddleNotes();
        PrepareDecoyMusicNotes();
    }

    private void StartLevel()
    {
        SetupLevel();

        for (int i = 0; i < m_AllCurrentlyUsedMusicNotes.Count; i++)
        {
            m_AllCurrentlyUsedMusicNotes[i].Activate(new Vector2(Random.Range(0, 4), Random.Range(0, 4)));
        }

        StaticInstanceManager.m_Instance.GetNoteChecker.GetMusicNotes(m_AllCurrentlyUsedMusicNotes, m_MiddleMusicNotesID, m_CurrnetlyUsedMiddleCheckMusicNotes);
    }

    private void DeactivateLevel()
    {
        for (int i = 0; i < m_AllCurrentlyUsedMusicNotes.Count; i++)
        {
            m_AllCurrentlyUsedMusicNotes[i].Deactivate();
            m_AllCurrentlyUsedMusicNotes.Clear();
        }
    }

    private void LoadMiddleMusicNotes()
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

    private void PrepareMiddleNotes()
    {
        int randomIndex = Random.Range(0, m_AllSongs.Count);
        List<Node> musicNotes = m_AllSongs[randomIndex];
        List<MiddleMusicNote> middleMusicNotes = m_MiddleCheckMusicNotes[randomIndex];


        for (int i = 0; i < musicNotes.Count; i++)
        {
            int id = i + 1;

            m_MiddleMusicNotesID.Add(id);

            Node musicNote = musicNotes[i];
            musicNote.Setup(Vector2.zero, id);
            m_AllCurrentlyUsedMusicNotes.Add(musicNote);

            middleMusicNotes[i].name = "MiddleNote " + i;
            middleMusicNotes[i].Setup(new Vector2(-5f + 2f * (i + 1f), 0f), id);
            m_CurrnetlyUsedMiddleCheckMusicNotes.Add(middleMusicNotes[i]);
        }
    }

    private void PrepareDecoyMusicNotes()
    {
        for (int i = 0; i < 10; i++)
        {
            int id = 0;

            Node musicNote = m_DecoyMusicNotes[i];
            musicNote.Setup(Vector2.zero, id);
            m_AllCurrentlyUsedMusicNotes.Add(musicNote);
        }
    }

    private void LoadFromFolder(Object[] loadedMiddleNotes)
    {
        List<Node> middleNotes = new List<Node>();

        for (int i = 0; i < loadedMiddleNotes.Length; i++)
        {
            GameObject gameObjectNote = Instantiate((GameObject)loadedMiddleNotes[i]);

            middleNotes.Add(gameObjectNote.GetComponent<Node>());
            gameObjectNote.SetActive(false);
        }

        List<MiddleMusicNote> middleCheckNotes = new List<MiddleMusicNote>();

        for (int i = 0; i < loadedMiddleNotes.Length; i++)
        {
            GameObject gameObjectNote = Instantiate((GameObject)loadedMiddleNotes[i]);
            Destroy(gameObjectNote.GetComponent<Node>());
            gameObjectNote.AddComponent(typeof(MiddleMusicNote));
            gameObjectNote.SetActive(false);

            middleCheckNotes.Add(gameObjectNote.GetComponent<MiddleMusicNote>());
            gameObjectNote.SetActive(false);
        }
        m_MiddleCheckMusicNotes.Add(middleCheckNotes);
        m_AllSongs.Add(middleNotes);
    }
}
