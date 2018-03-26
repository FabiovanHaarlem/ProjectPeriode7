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
    private AudioClip m_ActiveSongFragment;

    private AudioSource m_AudioSource;
    [SerializeField] private NoteChecker s_Node;

    private int m_SongIndex;
    private int m_FragmentIndex;
    private int m_SongPart;
    private float m_RemoveDelay;

    private int m_WholeSongIndex;

    void Start ()
    {
        m_ActiveSongFragments = new List<AudioClip>();
        m_Songs = new List<AudioClip>();
        m_SongFragments = new List<List<AudioClip>>();
        m_AudioSource = GetComponent<AudioSource>();
        m_SongPart = 0;
        m_WholeSongIndex = 0;
        m_RemoveDelay = 0.08f;
        //LoadSongs();
        LoadSongFragments();
        SetActiveFragments();
        //StartCoroutine(PlaySong());

    }

    private void Update()
    {

    }

    public float GetSongLength()
    {
        return m_AudioSource.clip.length;
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
        Object[] songFragments = Resources.LoadAll("SongFragments/Song1");

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
        m_WholeSongIndex++;
        m_FragmentIndex = 0;
        m_ActiveSongFragments = m_SongFragments[m_WholeSongIndex];
    }

    public IEnumerator PlaySongFragment()
    {
        m_AudioSource.clip = m_ActiveSongFragments[m_FragmentIndex];
        m_AudioSource.Play();
        yield return new WaitForSeconds(m_AudioSource.clip.length);
        if (m_FragmentIndex == 3)
        {
            StartCoroutine(PlaySong());
        }
        else
        {
            m_FragmentIndex++;
        }

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
        }
        else
        {
            StaticInstanceManager.m_Instance.GetGameManager.StartLevel();
            NextSongPart();
        }

        yield return new WaitForSeconds(2);
    }
}
