using UnityEngine;
using System.Collections;

public class KeepUpright : MonoBehaviour {

	// Use this for initialization
	void Start () {

		this.GetComponent<Rigidbody>().AddForce(Vector3.down);
		this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
