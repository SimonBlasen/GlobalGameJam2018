using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
	//	[SerializeField]
	private float m_idleJumpPower = 10000f;
	//	[SerializeField]
	private int m_idleJumpIntervall = 3;
	private bool m_jumpReady = true;

	[SerializeField]
	public Transform m_target;
	// Use this for initialization
	void Start ()
	{
		
	}

	public void SetTarget (Transform target)
	{
		m_target = target;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_jumpReady) {
			m_jumpReady = false;
			Invoke ("Jump", Random.Range (m_idleJumpIntervall - 1, m_idleJumpIntervall + 1));
		}	
	}

	void Jump ()
	{
		
//		GetComponent<Rigidbody> ().AddTorque (Vector3.up * m_idleJumpPower);
//		if (m_target)
//		if (m_target) {
//			Vector3 move = Vector3.MoveTowards (transform.position, m_target.position, 2.5f);
////			Vector3.RotateTowards
//		}
		GetComponent<Rigidbody> ().AddForce (Vector3.up * m_idleJumpPower * (Random.value * 0.3f + 0.1f));

//		else 
//			GetComponent<Rigidbody> ().AddForce (Vector3.up * m_idleJumpPower * (Random.value * 0.3f + 0.1f));

//		GetComponent<Rigidbody> ().
//		GetComponent<Rigidbody> ().
		Debug.Log ("cube jump");
		m_jumpReady = true;
	}
}
