using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    //speed and the new and old direction
    [SerializeField] private float m_Speed = 1f;
    [SerializeField] private int m_RandomDir;
    [SerializeField] private int m_LastDir;
    public bool s_IfNotPressed = true;

    private SpriteRenderer m_SpriteRenderer;

    //id and song
    private int m_ID;
    [SerializeField]
    private int m_SongIndex;

    [SerializeField] private ParticleSystem s_Particle;
    [SerializeField] private GameObject s_Middle;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start ()
    {
        m_RandomDir = Random.Range(1, 5);

	}
	
	void FixedUpdate ()
    {
        if (s_IfNotPressed)
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

    public void Activate(Vector2 startPosition, Sprite sprite)
    {
        //set gameobject active
        m_SpriteRenderer.sprite = sprite;
        transform.position = startPosition;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        //set gameobject deactive
        gameObject.SetActive(false);
    }

    public Sprite GetSprite()
    {
        return m_SpriteRenderer.sprite;
    }

    public int GetID()
    {
        //gets id from node
        return m_ID;
    }

    public void IsMiddleMusicNote()
    {
        //checks if is same ad middle node
        this.transform.position = new Vector2(s_Middle.transform.position.x, s_Middle.transform.position.y);
        s_Particle.transform.position = this.transform.position;
        StartCoroutine(Particle());
        //StartCoroutine(DeactivateNode());
    }

    public void IsNotMiddleMusicNote()
    {
        //checks if is nor same as middle note
        StartCoroutine(DeactivateNode());
    }
    IEnumerator Particle()
    {
        yield return new WaitForSeconds(2);
        s_Particle.Play();
    }
    IEnumerator DeactivateNode()
    {
        yield return new WaitForSeconds(4);
        Deactivate();
    }
}
