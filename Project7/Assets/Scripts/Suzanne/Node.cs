using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    [SerializeField] private float m_Speed = 1f;
    [SerializeField] private int m_RandomDir;
    [SerializeField] private int m_LastDir;

    void Start ()
    {
        m_RandomDir = Random.Range(1, 5);
	}
	
	void FixedUpdate ()
    {
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
        if (collision.gameObject.tag == "Wall")
        {         
            m_LastDir = m_RandomDir;
            while(m_RandomDir == m_LastDir)
            {
                m_RandomDir = Random.Range(1, 5);
            }

        }
    }
}
