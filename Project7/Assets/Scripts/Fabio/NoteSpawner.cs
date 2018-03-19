using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    private List<Sprite> m_LoadedSprites;
    private List<Sprite> m_UsedSprites;
    private List<Node> m_MusicNotes;
    private List<Node> m_DecoyMusicNotes;
    private List<MiddleMusicNote> m_MiddleMusicNotes;
    private List<Node> m_AllMusicNotes;

    void Start ()
    {
        m_MiddleMusicNotes = new List<MiddleMusicNote>();

        m_LoadedSprites = new List<Sprite>();
        m_MusicNotes = new List<Node>();
        m_DecoyMusicNotes = new List<Node>();
        m_UsedSprites = new List<Sprite>();
        m_AllMusicNotes = new List<Node>();

        SetupGame();
        StartLevel();
	}

    private void SetupGame()
    {
        LoadAllSprites();
        CreateDecoyMusicNotes();
        CreateMusicNotes();
        CreateMiddleMusicNotes();
    }

    private void SetupLevel()
    {
        m_UsedSprites = new List<Sprite>(m_LoadedSprites);
        PrepareDecoyMusicNotes();
        PrepareMusicNotes();
        PrepareMiddleMusicNotes();
        m_AllMusicNotes = m_DecoyMusicNotes;

        for (int i = 0; i < m_MusicNotes.Count; i++)
        {
            m_AllMusicNotes.Add(m_MusicNotes[i]);
        }

        StaticInstanceManager.m_Instance.GetNoteChecker.GetMusicNotes(m_AllMusicNotes, m_MiddleMusicNotes);
    }

    private void LoadAllSprites()
    {
        Object[] sprites = Resources.LoadAll("noten", typeof(Sprite));

        for (int i = 0; i < sprites.Length; i++)
        {
            m_LoadedSprites.Add((Sprite)sprites[i]);
        }

        m_UsedSprites = new List<Sprite>(m_LoadedSprites);
    }

    private void CreateDecoyMusicNotes()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject gameObjectNote = Instantiate(Resources.Load<GameObject>("Prefabs/MusicNote"));
            gameObjectNote.name = "DecoyMusicNote" + i;
            Node musicNote = gameObjectNote.GetComponent<Node>();
            musicNote.Setup(Vector2.zero, 0);
            m_DecoyMusicNotes.Add(musicNote);
        }
    }

    private void CreateMusicNotes()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject gameObjectNote = Instantiate(Resources.Load<GameObject>("Prefabs/MusicNote"));
            gameObjectNote.name = "MusicNote" + i;
            Node musicNote = gameObjectNote.GetComponent<Node>();
            musicNote.Setup(Vector2.zero, (i + 1));
            m_MusicNotes.Add(musicNote);
        }
    }

    private void CreateMiddleMusicNotes()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject gameObjectNote = Instantiate(Resources.Load<GameObject>("Prefabs/MiddleMusicNote"));
            gameObjectNote.name = "MiddleMusicNote" + i;
            MiddleMusicNote middleMusicNote = gameObjectNote.GetComponent<MiddleMusicNote>();
            m_MiddleMusicNotes.Add(middleMusicNote);
        }
    }

    private void PrepareMusicNotes()
    {
        for (int i = 0; i < 4; i++)
        {
            int randomIndex = Random.Range(0, m_UsedSprites.Count);
            Sprite sprite = m_UsedSprites[randomIndex];
            m_UsedSprites.RemoveAt(randomIndex);

            m_MusicNotes[i].Setup(Vector2.zero, (i + 1));
            m_MusicNotes[i].Activate(new Vector2(Random.Range(-4, 4), Random.Range(-4, 4)), sprite);
        }
    }

    private void PrepareDecoyMusicNotes()
    {
        for (int i = 0; i < 8; i++)
        {
            int randomIndex = Random.Range(0, m_UsedSprites.Count);
            Sprite sprite = m_UsedSprites[randomIndex];
            m_UsedSprites.RemoveAt(randomIndex);

            m_DecoyMusicNotes[i].Setup(Vector2.zero, 0);
            m_DecoyMusicNotes[i].Activate(new Vector2(Random.Range(-4, 4), Random.Range(-4, 4)), sprite);
        }
    }

    private void PrepareMiddleMusicNotes()
    {
        for (int i = 0; i < m_MiddleMusicNotes.Count; i++)
        {
            m_MiddleMusicNotes[i].Setup(new Vector2(-2.4f + 1.1f * (i + 1f), 0f), m_MusicNotes[i].GetSprite(), m_MusicNotes[i].GetID());
        }
    }

    public void StartLevel()
    {
        SetupLevel();
    }


    //public void StartLevel()
    //{
    //    SetupLevel();

    //    for (int i = 0; i < m_CurrentlyUsedMusicNotes.Count; i++)
    //    {
    //        m_CurrentlyUsedMusicNotes[i].Activate(new Vector2(Random.Range(-4, 4), Random.Range(-4, 4)));
    //    }

    //    StaticInstanceManager.m_Instance.GetNoteChecker.GetMusicNotes(m_CurrentlyUsedMusicNotes, m_MiddleMusicNotesID, m_MiddleMusicNotes);
    //}

    //private void DeactivateLevel()
    //{
    //    for (int i = 0; i < m_CurrentlyUsedMusicNotes.Count; i++)
    //    {
    //        m_CurrentlyUsedMusicNotes[i].Deactivate();
    //        m_CurrentlyUsedMusicNotes.Clear();
    //    }
    //}

    //private void LoadMiddleMusicNotes()
    //{
    //    List<MiddleMusicNote> middleCheckNotes = new List<MiddleMusicNote>();

    //    for (int i = 0; i < 4; i++)
    //    {
    //        GameObject gameObjectNote = Instantiate(Resources.Load<GameObject>("Prefabs/MiddleMusicNote"));
    //        gameObjectNote.SetActive(false);
    //        gameObjectNote.layer = 9;

    //        middleCheckNotes.Add(gameObjectNote.GetComponent<MiddleMusicNote>());
    //    }
    //    m_MiddleMusicNotes =middleCheckNotes;
    //}

    //private void LoadSongMusicNotes()
    //{
    //    Object[] loadedMiddleNotes = new Object[4];


    //    loadedMiddleNotes = Resources.LoadAll("Prefabs/Songs/Song0");
    //    LoadFromFolder(loadedMiddleNotes);

    //    loadedMiddleNotes = Resources.LoadAll("Prefabs/Songs/Song1");
    //    LoadFromFolder(loadedMiddleNotes);

    //    loadedMiddleNotes = Resources.LoadAll("Prefabs/Songs/Song2");
    //    LoadFromFolder(loadedMiddleNotes);

    //    loadedMiddleNotes = Resources.LoadAll("Prefabs/Songs/Song3");
    //    LoadFromFolder(loadedMiddleNotes);
    //}

    //private void LoadDecoyMusicNotes()
    //{
    //    Object[] loadedDecoyMusicNotes = Resources.LoadAll("Prefabs/DecoyMusicNotes");

    //    for (int i = 0; i < loadedDecoyMusicNotes.Length; i++)
    //    {
    //        GameObject gameObject = Instantiate((GameObject)loadedDecoyMusicNotes[i]);
    //        //m_DecoyMusicNotes.Add(gameObject.GetComponent<Node>());
    //        gameObject.SetActive(false);
    //    }
    //}

    //private void PrepareMiddleMusicNote()
    //{
    //    for (int i = 0; i < m_MiddleMusicNotes.Count; i++)
    //    {
    //        m_MiddleMusicNotes[i].Setup(new Vector2(-5f + 2f * (i + 1f), 0f), m_CurrentlyUsedMusicNotes[i].GetComponent<SpriteRenderer>().sprite, m_CurrentlyUsedMusicNotes[i].GetID());
    //        m_MiddleMusicNotes[i].name = "MiddleNote " + i;
    //    }
    //}

    //private void PrepareSongNotes()
    //{
    //    int randomIndex = Random.Range(0, m_AllSongs.Count);
    //    List<Node> musicNotes = m_AllSongs[randomIndex];



    //    for (int i = 0; i < musicNotes.Count; i++)
    //    {
    //        int id = i + 1;

    //        m_MiddleMusicNotesID.Add(id);

    //        Node musicNote = musicNotes[i];
    //        musicNote.Setup(Vector2.zero, id);
    //        m_CurrentlyUsedMusicNotes.Add(musicNote);


    //    }
    //}

    //private void PrepareDecoyMusicNotes()
    //{
    //    for (int i = 0; i < 10; i++)
    //    {
    //        int id = 0;

    //        //Node musicNote = m_DecoyMusicNotes[i];
    //        //musicNote.Setup(Vector2.zero, id);
    //        //m_CurrentlyUsedMusicNotes.Add(musicNote);
    //    }
    //}

    //private void LoadFromFolder(Object[] loadedMiddleNotes)
    //{
    //    List<Node> middleNotes = new List<Node>();

    //    for (int i = 0; i < loadedMiddleNotes.Length; i++)
    //    {
    //        GameObject gameObjectNote = Instantiate((GameObject)loadedMiddleNotes[i]);
    //        gameObjectNote.SetActive(false);
    //        gameObjectNote.layer = 10;
    //        middleNotes.Add(gameObjectNote.GetComponent<Node>());
    //    }
    //    m_AllSongs.Add(middleNotes);
    //}
}
