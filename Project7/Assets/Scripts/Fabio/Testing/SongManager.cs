using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> m_Songs;
    private List<List<AudioClip>> m_SongFragments;

    //private AudioClip m_ActiveSong;
    private List<AudioClip> m_ActiveSongFragments;
    private List<AudioClip> m_SongsQue;

    private AudioSource m_AudioSource;
    [SerializeField] private NoteChecker s_Node;

    private int m_SongIndex;
    private int m_FragmentIndex;
    private int m_FragmentsPlayed;
    private int m_SongPart;
    private float m_RemoveDelay;

    private int m_WholeSongIndex;

    private bool m_SongsInQue;
    private bool m_SongPlaying;

    void Start()
    {
        m_ActiveSongFragments = new List<AudioClip>();
        m_Songs = new List<AudioClip>();
        m_SongFragments = new List<List<AudioClip>>();
        m_AudioSource = GetComponent<AudioSource>();
        m_SongsQue = new List<AudioClip>();
        m_SongPart = 0;
        m_WholeSongIndex = 0;
        m_RemoveDelay = 0.08f;
        m_SongsInQue = false;
        m_SongPlaying = false;
        m_FragmentIndex = 0;
        m_FragmentsPlayed = 0;
        //LoadSongs();
        LoadSongFragments();
        SetActiveFragments();
        //StartCoroutine(PlaySong());

    }

    public float GetSongLength()
    {
        return m_AudioSource.clip.length;
    }

    public void UpdateRing(int index, Vector3 position)
    {
        s_Node.s_Sprite[index].transform.position = position;
    }

    public void UpdateRing(int index1, Vector3 position1, int index2, Vector3 position2)
    {
        s_Node.s_Sprite[index1].transform.position = position1;
        s_Node.s_Sprite[index2].transform.position = position2;
    }

    private void LoadSongs()
    {
        Object[] songs = Resources.LoadAll("Songs");

        for (int i = 0; i < songs.Length; i++)
        {
            m_Songs.Add((AudioClip)songs[i]);
        }
    }


    private void LoadSongFragments()
    {
        int randomNumber = Random.Range(0, 1);
        Object[] songFragments = Resources.LoadAll("SongFragments/Song1");

        switch (randomNumber)
        {
            case 0:
                songFragments = Resources.LoadAll("SongFragments/Song1");
                break;
            case 1:
                songFragments = Resources.LoadAll("SongFragments/Song2");
                break;
            case 2:
                songFragments = Resources.LoadAll("SongFragments/Song3");
                break;
            case 3:
                songFragments = Resources.LoadAll("SongFragments/Song4");
                break;
        }

        int amountOfSongs = songFragments.Length / 4;
        int index = 0;

        for (int i = 0; i < amountOfSongs; i++)
        {
            List<AudioClip> audioClips = new List<AudioClip>();

            for (int j = 0; j < 4; j++)
            {
                audioClips.Add((AudioClip)songFragments[index]);
                index += 1;
            }
            m_SongFragments.Add(audioClips);
        }
    }

    private void SetActiveFragments()
    {
        m_ActiveSongFragments = m_SongFragments[m_WholeSongIndex];
    }

    private void NextSongPart()
    {
        for (int i = 0; i < s_Node.s_Sprite.Count; i++)
        {
            s_Node.s_Sprite[i].transform.position = new Vector3(-3.719069f, 6.268066f, 0);

        }

        if (m_SongIndex == m_SongFragments.Count)
        {
            LoadSongFragments();
            m_WholeSongIndex = 0;
        }
        else
        {
            m_WholeSongIndex++;
            m_FragmentIndex = 0;
            m_FragmentsPlayed = 0;
            m_ActiveSongFragments = m_SongFragments[m_WholeSongIndex];
        }
    }

    private void SelectNewSong()
    {
        LoadSongFragments();
        m_WholeSongIndex = 0;

        m_WholeSongIndex++;
        m_FragmentIndex = 0;
        m_FragmentsPlayed = 0;
        SetActiveFragments();

        for (int i = 0; i < s_Node.s_Sprite.Count; i++)
        {
            s_Node.s_Sprite[i].transform.position = new Vector3(-3.719069f, 6.268066f, 0);

        }
    }

    public void PutSongFragmentInQue()
    {
        m_SongsQue.Add(m_ActiveSongFragments[m_FragmentIndex]);
        m_SongsInQue = true;
        m_FragmentIndex++;
    }

    private void Update()
    {
        if (m_SongsInQue && !m_SongPlaying)
        {
            PlaySongQue();
        }
    }

    public void PlaySongQue()
    {
        if (m_SongsQue.Count >= 1)
        {
            StartCoroutine(PlaySongFragment());
            m_SongPlaying = true;
        }
        else
        {
            m_SongsInQue = false;
        }
    }
   
    public IEnumerator PlaySongFragment()
    {
        m_AudioSource.clip = m_SongsQue[0];
        m_AudioSource.Play();
        yield return new WaitForSeconds(m_AudioSource.clip.length);
        if (m_FragmentsPlayed == 3)
        {
            StartCoroutine(PlaySong());
        }
        m_SongsQue.RemoveAt(0);
        m_SongPlaying = false;
        m_FragmentsPlayed++;

        yield return new WaitForSeconds(2);
    }

    public IEnumerator PlaySong()
    {
        m_AudioSource.clip = m_ActiveSongFragments[0];
        m_AudioSource.Play();
        yield return new WaitForSeconds(m_AudioSource.clip.length - m_RemoveDelay);

        m_AudioSource.clip = m_ActiveSongFragments[1];
        m_AudioSource.Play();
        yield return new WaitForSeconds(m_AudioSource.clip.length - m_RemoveDelay);

        m_AudioSource.clip = m_ActiveSongFragments[2];
        m_AudioSource.Play();
        yield return new WaitForSeconds(m_AudioSource.clip.length - m_RemoveDelay);

        m_AudioSource.clip = m_ActiveSongFragments[3];
        m_AudioSource.Play();
        yield return new WaitForSeconds(m_AudioSource.clip.length - m_RemoveDelay);

        if (m_WholeSongIndex == m_SongFragments.Count - 1)
        {

            for (int songPart = 0; songPart < m_SongFragments.Count; songPart++)
            {
                for (int fragment = 0; fragment < m_SongFragments[songPart].Count; fragment++)
                {
                    m_AudioSource.clip = m_SongFragments[songPart][fragment];
                    m_AudioSource.Play();
                    yield return new WaitForSeconds(m_AudioSource.clip.length - m_RemoveDelay);
                }
            }

            StaticInstanceManager.m_Instance.GetGameManager.StartLevel();
            SelectNewSong();
        }
        else
        {
            StaticInstanceManager.m_Instance.GetGameManager.StartLevel();
            NextSongPart();
        }

        yield return new WaitForSeconds(2);
    }
}
