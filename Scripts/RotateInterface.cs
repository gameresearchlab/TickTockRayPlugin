using UnityEngine;
using System.Collections;

public class RotateInterface : MonoBehaviour {

	private static int state;
	public const int IDLE = 0;
	public const int RIGHT_HALF = 1;
	public const int RIGHT_CLICK = 2;
	public const int LEFT_HALF = 3;
	public const int LEFT_CLICK = 4;

	Quaternion correction;

	private int framesToWait;
	private int framesWaited;

	//FOR DEBUG ONLY
	public static int click_count;


	// Use this for initialization
	void Start () {
		state = IDLE;
		framesToWait = 60;
		framesWaited = 0;
		click_count = 0;

		correction = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update () {


		//TextMesh debug = GameObject.Find("DebugText").GetComponent<TextMesh>();
		//TextMesh debug2 = GameObject.Find("DebugText2").GetComponent<TextMesh>();

		float zO = GameObject.Find("Watch").transform.localRotation.eulerAngles.z;
		//debug.text = string.Format("Watch:\n{0}\n{1}", zO, state);



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


		rotation = rotation * Quaternion.Inverse(correction);




		rotation = new Quaternion(rotation.x,
			-rotation.y,
			-rotation.z,
			-rotation.w);



		float z = rotation.eulerAngles.z;



		//debug2.text = string.Format("RIV:\n{0}", z);



		if(state == RIGHT_CLICK || state == LEFT_CLICK)
		{
			state = IDLE;
		}
		
		if(state != IDLE){
			framesWaited++;
			if((330 < z && z <= 360) || (0 <= z && z < 30)){
				if(state == RIGHT_HALF)
				{
					state = RIGHT_CLICK;
					click_count++;
					framesWaited = 0;
				}else if(state == LEFT_HALF)
				{
					state = LEFT_CLICK;
					click_count++;
					framesWaited = 0;
				}
			}


		}
			
		if(state == IDLE)
		{
			if(40 < z && z < 90){
				state = RIGHT_HALF;
			}else if( 180 < z && z < 290){
				state = LEFT_HALF;
			}
		}

		if(framesWaited == framesToWait){
			framesWaited = 0;
			state = IDLE;
		}

	}


	public static int getState(){
		return state;
	}
}
