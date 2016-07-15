using UnityEngine;
using System.Collections;

public class Reticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {



		Transform watch = GameObject.Find("Watch").transform;

		Ray ray = new Ray(watch.position, watch.forward);
		RaycastHit hit;

		Physics.Raycast(ray, out hit);

		this.transform.position = hit.point + (hit.normal * 0.01f);

		this.transform.up = hit.normal;

	
	
	}
}
