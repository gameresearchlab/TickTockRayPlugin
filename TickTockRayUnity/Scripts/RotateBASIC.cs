using UnityEngine;
using System.Collections;
using System;

public class RotateBASIC : MonoBehaviour {

	Quaternion correction;


	// Use this for initialization
	void Start () {
		correction = Quaternion.identity;
	}

	// Update is called once per frame
	void Update () {

		if(RotateInterface.getState() != RotateInterface.IDLE)
		{
			return;
		}
			

		Quaternion rotation = WatchRotation.rotation;

		bool update_correction = false;

		if(Input.GetButtonDown("Fire1"))
		{
			update_correction = true;
		}


		if(update_correction)
		{
			correction = WatchRotation.rotation;
		}


		transform.localRotation = rotation * Quaternion.Inverse(correction);




		transform.localRotation = new Quaternion(transform.localRotation.x,
			-transform.localRotation.y,
			-transform.localRotation.z,
			-transform.localRotation.w);


	}



		
	public void draw_axis_giz()
	{
		Debug.DrawRay(this.transform.position, this.transform.up, Color.green);
		Debug.DrawRay(this.transform.position, this.transform.right, Color.red);
		Debug.DrawRay(this.transform.position, this.transform.forward, Color.blue);

	}



}






/*


		TextMesh debug = GameObject.Find("Debug").GetComponent<TextMesh>();
		TextMesh debug2 = GameObject.Find("Debug2").GetComponent<TextMesh>();



		debug.text = string.Format("Watch:\n{0}\n{1}\n{2}", rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z);
		debug2.text = string.Format("Phone:\n{0}\n{1}\n{2}", cam_rotation.eulerAngles.x, cam_rotation.eulerAngles.y, cam_rotation.eulerAngles.z);
		*/


/*				// SWAPS X AND Y FOR WATCH
float x = rotation.eulerAngles.x;
float y = rotation.eulerAngles.y;
float z = rotation.eulerAngles.z;

rotation.eulerAngles.Set(y, x, z);
*/

/*
// SWAPS X AND Y FOR CAMERA FOR TRANSFORM
float x = cam_rotation.eulerAngles.x;
float y = cam_rotation.eulerAngles.y;
float z = cam_rotation.eulerAngles.z;

cam_rotation.eulerAngles.Set(y, x, 0);

rotation.eulerAngles.Set(rotation.eulerAngles.x, rotation.eulerAngles.y, 0);
*/