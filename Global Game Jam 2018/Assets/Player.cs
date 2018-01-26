using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField]
    private float m_hoverRadius = 5f;
    [SerializeField]
    private float m_constForce = 5f;
    [SerializeField]
    private float m_kickVecYComp = 0.3f;


    private Transform m_grabbedCubem = null;
    private Vector3 m_grab_relative = Vector3.zero;


	// Use this for initialization
	void Start () {
        Cursor.visible = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;

                if (objectHit.tag == "Cube" && Vector3.Distance(objectHit.position, transform.position) <= m_hoverRadius)
                {
                    m_grabbedCubem = objectHit;
                    m_grab_relative = objectHit.position - transform.position;
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && m_grabbedCubem != null)
        {
            m_grabbedCubem = null;
        }


        if (m_grabbedCubem)
        {
            m_grabbedCubem.position = transform.position + Camera.main.transform.forward * m_hoverRadius;
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;

                if (objectHit.tag == "Cube" && Vector3.Distance(objectHit.position, transform.position) <= m_hoverRadius)
                {
                    Vector3 camFor = Camera.main.transform.forward;
                    camFor.y = 0f;
                    camFor.Normalize();
                    camFor.y = m_kickVecYComp;
                    camFor.Normalize();


                    objectHit.GetComponent<Rigidbody>().AddForce(camFor * m_constForce);
                }
            }
        }
    }
}
