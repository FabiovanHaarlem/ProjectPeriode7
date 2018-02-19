using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    //speed and the new and old direction
    [SerializeField] private float m_Speed = 1f;
    [SerializeField] private int m_RandomDir;
    [SerializeField] private int m_LastDir;

    //id and song
    private int m_ID;
    [SerializeField]
    private int m_SongIndex;

    void Start ()
    {
        m_RandomDir = Random.Range(1, 5);
	}
	
	void FixedUpdate ()
    {
        //random direction
        switch (m_RandomDir)
        {
            case 1:
                //down
                this.transform.Translate(new Vector3(1 * m_Speed * Time.deltaTime, -1 * m_Speed * Time.deltaTime));
                break;
            case 2:
                //up
                this.transform.Translate(new Vector3(-1 * m_Speed * Time.deltaTime, 1 * m_Speed * Time.deltaTime));
                break;
            case 3:
                //right
                this.transform.Translate(new Vector3(1 * m_Speed * Time.deltaTime, 1 * m_Speed * Time.deltaTime));
                break;
            case 4:
                //left
                this.transform.Translate(new Vector3(-1 * m_Speed * Time.deltaTime, -1 * m_Speed * Time.deltaTime));
                break;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //collision with wall
        if (collision.gameObject.tag == "Wall")
        {         
            m_LastDir = m_RandomDir;
            while(m_RandomDir == m_LastDir)
            {
                m_RandomDir = Random.Range(1, 5);
            }

        }
    }
    public void Setup(Vector2 startPosition, int iD)
    {
        //random id from node
        m_ID = iD;
    }

    public void Activate(Vector2 startPosition)
    {
        //set gameobject active
        transform.position = startPosition;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        //set gameobject deactive
        gameObject.SetActive(false);
    }

    public int GetID()
    {
        //gets id from node
        return m_ID;
    }

    public void IsMiddleMusicNote()
    {
        //checks if is same ad middle node
        Deactivate();
    }

    public void IsNotMiddleMusicNote()
    {
        //checks if is nor same as middle note
        Deactivate();
    }
}
