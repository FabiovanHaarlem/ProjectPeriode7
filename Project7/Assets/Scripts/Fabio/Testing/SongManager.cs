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

    private AudioSource m_AudioSource;

    private int m_SongIndex;
    private int m_FragmentIndex;
    private int m_SongPart;

    private int m_WholeSongIndex;
	
	void Start ()
    {
        m_ActiveSongFragments = new List<AudioClip>();
        m_Songs = new List<AudioClip>();
        m_SongFragments = new List<List<AudioClip>>();
        m_SongPart = 0;
        m_WholeSongIndex = 0;
        LoadSongs();
        LoadSongFragments();
        SelectNextSongFragment();
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
            audioClips.Clear();
        }
    }

    public void SelectNextSongFragment()
    {
        m_SongPart = 0;
        m_ActiveSongFragments = m_SongFragments[m_SongPart];
        m_SongPart++;
    }

    public void NextSongPart()
    {
        m_WholeSongIndex++;
        m_SongPart = 0;
        m_ActiveSongFragments = m_SongFragments[m_WholeSongIndex];
    }

    public IEnumerator PlaySongFragment()
    {
        m_AudioSource.clip = m_ActiveSongFragments[m_FragmentIndex];
        m_AudioSource.Play();
        m_FragmentIndex++;
        new WaitForSeconds(m_AudioSource.clip.length);
        m_SongPart++;
        if (m_SongPart >= 4)
        {
            StartCoroutine(PlaySong());
        }
        SelectNextSongFragment();
        yield return new WaitForSeconds(2);
    }

    public IEnumerator PlaySong()
    {
        m_AudioSource.clip = m_ActiveSongFragments[0];
        m_AudioSource.Play();
        new WaitForSeconds(m_AudioSource.clip.length);

        m_AudioSource.clip = m_ActiveSongFragments[1];
        m_AudioSource.Play();
        new WaitForSeconds(m_AudioSource.clip.length);

        m_AudioSource.clip = m_ActiveSongFragments[3];
        m_AudioSource.Play();
        new WaitForSeconds(m_AudioSource.clip.length);

        m_AudioSource.clip = m_ActiveSongFragments[4];
        m_AudioSource.Play();
        new WaitForSeconds(m_AudioSource.clip.length);

        StaticInstanceManager.m_Instance.GetGameManager.StartLevel();
        yield return new WaitForSeconds(2);
    }
}
