using UnityEngine;
using System.Collections;
using System;

public class WatchRotationJNI : MonoBehaviour {
	public static Quaternion rotation = Quaternion.identity;
	public static Quaternion initialRotation = Quaternion.identity;
	public static Int32 mouseState = 0;

	private bool firstMeasurement = true;

	AndroidJavaClass watchReader;

	// Use this for initialization
	void Start () {

		AndroidJNI.AttachCurrentThread();

		using (AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer")) {

			using (AndroidJavaObject obj_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject> ("currentActivity")) {

				using(AndroidJavaObject constructor = new AndroidJavaObject("edu.csumb.hci.listenerplugin.ServiceConstructor", obj_Activity)){

					watchReader = new AndroidJavaClass ("edu.csumb.hci.listenerplugin.ListenToWearableService");

				}

			}
		}




	}

	void Update() {

		String data = watchReader.CallStatic<String>("getCurrentData");


		// make sure that you got some real data
		if (data != "nothing") {
			var values = data.Split (',');

			// make sure that the data is actually a rotation data (has 4 compotents of Quaternion)
			if (values.Length == 5) {
				rotation = new Quaternion (Single.Parse (values [1]), Single.Parse (values [2]), Single.Parse (values [3]), Single.Parse (values [0]));
				mouseState = Int32.Parse (values [4]);

				TextMesh debug = GameObject.Find("Debug").GetComponent<TextMesh>();
				debug.text = string.Format("Watch:\n{0}\n{1}\n{2}", rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z);

				// save this rotation as an "offset" if it was the first time it was measured
				if (firstMeasurement) {
					initialRotation = rotation;
					firstMeasurement = false;
				}
			}
		}

	}


	/*
	 *
	 * 	AndroidJavaClass androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		debug2.text = "Player";
		AndroidJavaObject jo = androidJC.GetStatic<AndroidJavaObject>("currentActivity");
		debug2.text = "Activity";

		watchReader = new AndroidJavaClass("edu.csumb.hci.listenerplugin.ListenToWearableService");

		debug2.text = "Serv Init";

		try{
			debug2.text = watchReader.CallStatic<string>("clarify");
			string me = "Daniel";
			debug2.text = watchReader.CallStatic<string>("clarifySomeMore", me);
			watchReader.CallStatic("beginService", jo);
		}catch(Exception e){
			debug2.text = e.StackTrace;
		}

		using (AndroidJavaClass androidJC = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
			debug2.text = "Player";
			using (AndroidJavaObject jo = androidJC.GetStatic<AndroidJavaObject>("currentActivity")) {
				debug2.text = "Activity";

				watchReader = new AndroidJavaClass("edu.csumb.hci.listenerplugin.ListenToWearableService");

				debug2.text = "Serv Init";

				try{
					watchReader.CallStatic("beginService", jo);
				}catch(Exception e){
					debug2.text = e.Message;
				}
				//debug2.text = "Started!";
			}
		}


	using(var javaUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
		{
			debug2.text = "Player";
			using(var currentActivity = javaUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
			{
				debug2.text = "Activity";
				using(var listenerLauncher = new AndroidJavaClass("edu.csumb.hci.listenerplugin.ListenerLauncher"))
				{
					debug2.text = "Launcher Init";

					try{
					listenerLauncher.CallStatic("Call", currentActivity);
					debug2.text = "Method Call";
					}catch(Exception e){
						debug2.text = e.StackTrace;
					}

					//watchReader = new AndroidJavaClass("edu.csumb.hci.listenerplugin.ListenToWearableService");
					//debug2.text = "Reader Bound";
				}

			}

		}




	*/

}
