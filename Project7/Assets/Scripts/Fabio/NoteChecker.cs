using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteChecker : MonoBehaviour
{
    private List<Node> m_MusicNotes;

    private List<MiddleMusicNote> m_MiddleMusicNotes;
    public List<GameObject> s_Sprite;

    public void GetMusicNotes(List<Node> musicNotes, List<MiddleMusicNote> middleMusicNotes)
    {
        m_MusicNotes = musicNotes;
        m_MiddleMusicNotes = middleMusicNotes;
    }

    public void CheckIfMiddleNote(GameObject selectedMusicNote)
    {
        Node musicNote = null;

        for (int i = 0; i < m_MusicNotes.Count; i++)
        {
            if (selectedMusicNote.name == m_MusicNotes[i].name)
            {
                musicNote = m_MusicNotes[i];
                break;
            }
        }

        for (int i = 0; i < m_MiddleMusicNotes.Count; i++)
        {
            if (m_MiddleMusicNotes[i].GetID() == musicNote.GetID())
            {
                musicNote.IsMiddleMusicNote();
                s_Sprite[i].transform.position = new Vector3(m_MiddleMusicNotes[i].transform.position.x, m_MiddleMusicNotes[i].transform.position.y, -1);
                
                musicNote = null;
                StartCoroutine(StaticInstanceManager.m_Instance.GetSongManager.PlaySongFragment());

                break;
            }
            

        }

        if (musicNote != null)
        {
            musicNote.IsNotMiddleMusicNote();
            musicNote = null;
        }

    }
	

	void Update ()
    {
#if UNITY_ANDROID
        for (var i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);

                if (hitInfo)
                {
                    CheckIfMiddleNote(hitInfo.collider.gameObject);
                }
            }
        }
#endif
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0))
        {
            Raycast();
        }
#endif
    }
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
    private void Raycast()
    {
        RaycastHit hit;

        Ray ray = (Camera.main.ScreenPointToRay(Input.mousePosition));

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 50, Color.red);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            CheckIfMiddleNote(hit.collider.gameObject);
        }
    }
#endif
}
