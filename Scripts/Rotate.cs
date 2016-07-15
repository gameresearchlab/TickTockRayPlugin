using UnityEngine;
using System.Collections;
using System;

public class Rotate : MonoBehaviour {

	private Quaternion yaw_correction = Quaternion.identity;

	private float user_roll = 0.0f;

	private bool swap_inputs; 



	// Use this for initialization
	void Start () {
		swap_inputs = false;

	}
		



	
	// Update is called once per frame
	void Update () {

		Quaternion rotation = WatchRotation.rotation;  //Quaternion.Euler (-WatchRotation.rotation.eulerAngles.y,
			                     // WatchRotation.rotation.eulerAngles.x, -WatchRotation.rotation.eulerAngles.z);

		Quaternion cam_rotation = GameObject.Find("Camera").transform.rotation;

		this.transform.rotation = rotation;



		bool update_user_roll = false;

		if(Input.GetButtonDown("Fire1"))
		{
			update_user_roll = true;
		}
			

		if(update_user_roll)
		{
			//yaw_correction = Quaternion.FromToRotation( new Vector3(this.transform.forward.x, 0, this.transform.forward.z), new Vector3(0, 0, 1));
			yaw_correction = Quaternion.FromToRotation( new Vector3(this.transform.forward.x, 0, this.transform.forward.z), new Vector3(0, 0, 1));

			Vector3 user_zero_roll = computeZeroRollVector(this.transform.forward);
			user_roll = rollFromZero(user_zero_roll, this.transform.forward, this.transform.up);
		}

		Vector3 zeroRoll = computeZeroRollVector(this.transform.forward);
		float roll = rollFromZero(zeroRoll, this.transform.forward, this.transform.up);

		float relativeRoll = normalizeAngle(roll - user_roll);

		Quaternion antiRoll = Quaternion.AngleAxis(relativeRoll, this.transform.forward);

		//this.transform.rotation.eulerAngles.Set( -1 * (this.transform.rotation.eulerAngles.x), this.transform.rotation.eulerAngles.y, this.transform.rotation.eulerAngles.z);

		//this.transform.rotation = new Quaternion(this.transform.localRotation.x, -this.transform.localRotation.y, this.transform.localRotation.z, this.transform.localRotation.w);

		if(Input.GetButtonDown("Fire2") && !swap_inputs)
		{
			swap_inputs = true;
		}
		else if(Input.GetButtonDown("Fire2"))
		{
			swap_inputs = false;
		}
	
		if(swap_inputs)
		{
			transform.rotation = new Quaternion(transform.localRotation.x,
				-transform.localRotation.y,
				transform.localRotation.z,
				-transform.localRotation.w);
		}


		transform.rotation = yaw_correction * antiRoll * Quaternion.LookRotation(this.transform.forward);

		TextMesh debug = GameObject.Find("Debug").GetComponent<TextMesh>(); 

		debug.text = string.Format("Watch:\n{0}\n{1}\n{2}\n{3}", transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z, swap_inputs ? "SWAP" : "NORM");



	}

	Vector3 computeZeroRollVector(Vector3 forward)
	{
		Vector3 crossP = Vector3.Cross(forward, Vector3.up);
		Vector3 zero_roll = Vector3.Cross(crossP, forward);

		return zero_roll.normalized;

	}

	float rollFromZero(Vector3 zeroRoll, Vector3 forward, Vector3 up)
	{
		float cosine = Vector3.Dot (up, zeroRoll);

		Vector3 crossP = Vector3.Cross(up, zeroRoll);
		float directionCosine = Vector3.Dot(forward, crossP);

		float sign = directionCosine < 0.0f ? 1.0f : -1.0f;

		return sign * Mathf.Rad2Deg * Mathf.Acos(cosine);

	}

	float normalizeAngle(float angle)
	{
		if(angle > 180.0f)
		{
			return angle - 360.0f;
		}
		if(angle > -180.0f)
		{
			return angle + 360.0f;
		}
		return angle;
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