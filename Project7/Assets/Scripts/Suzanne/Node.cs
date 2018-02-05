using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    [SerializeField] private float m_Speed = 1f;
    [SerializeField] private int m_RandomDir;

    int[] s_Used = new int[5];
    Random rnd = new Random();

    void Start ()
    {
        m_RandomDir = Random.Range(1, 5);
	}
	
	void FixedUpdate ()
    {


        switch (m_RandomDir)
        {
            case 1:
                this.transform.Translate(-Vector2.up * m_Speed * Time.fixedDeltaTime);
                break;
            case 2:
                this.transform.Translate(Vector2.up * m_Speed * Time.fixedDeltaTime);
                break;
            case 3:
                this.transform.Translate(Vector2.right * m_Speed * Time.fixedDeltaTime);
                break;
            case 4:
                this.transform.Translate(-Vector2.right * m_Speed * Time.fixedDeltaTime);
                break;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collided with " + collision.collider.gameObject.name);
        if (collision.gameObject.tag == "Wall")
        {
            m_RandomDir = Random.Range(1, 5);

        }
    }
}
