using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> m_Songs;
    private List<List<AudioClip>> m_SongFragments;

    private AudioClip m_ActiveSong;
    private List<AudioClip> m_ActiveSongFragments;

    private AudioSource m_AudioSource;

    private int m_SongIndex;
    private int m_FragmentIndex;
	
	void Start ()
    {
        LoadSongs();
        LoadSongFragments();
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
        Object[] songFragments = Resources.LoadAll("SongFragments");

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

    public void SelectSong()
    {
        int randomSongIndex = Random.Range(0, m_Songs.Count);
        m_SongIndex = randomSongIndex;

        m_ActiveSong = m_Songs[randomSongIndex];
        m_ActiveSongFragments = m_SongFragments[randomSongIndex];
    }

    public void PlaySongFragment()
    {
        m_AudioSource.clip = m_ActiveSongFragments[m_FragmentIndex];
        m_AudioSource.Play();
        m_FragmentIndex++;
    }

    public IEnumerator PlaySong()
    {
        m_AudioSource.clip = m_ActiveSong;
        m_AudioSource.Play();
        new WaitForSeconds(m_AudioSource.clip.length);
        //Start Next Level
        yield return new WaitForSeconds(2);
    }
}
