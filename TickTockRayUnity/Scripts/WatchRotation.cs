using UnityEngine;
using System.Collections;
using System;

public class WatchRotation : MonoBehaviour {
	public static Quaternion rotation = Quaternion.identity;
	public static Quaternion initialRotation = Quaternion.identity;
	public static Int32 mouseState = 0;

	private bool firstMeasurement = true;
    
    AndroidJavaClass cls_WatchReaderActivity;

	// Use this for initialization
	void Start () {
        AndroidJNI.AttachCurrentThread();

        using (AndroidJavaClass cls_UnityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer")) {

            using (AndroidJavaObject obj_Activity = cls_UnityPlayer.GetStatic<AndroidJavaObject> ("currentActivity")) {

                cls_WatchReaderActivity = new AndroidJavaClass ("com.kpicture.watchcasting.ListenToWearableService");

            }
		}


    }

	void Update() {

		String data = cls_WatchReaderActivity.CallStatic<String>("getCurrentData");

		// make sure that you got some real data
		if (data != "nothing") {
			var values = data.Split (',');

			// make sure that the data is actually a rotation data (has 4 compotents of Quaternion)
			if (values.Length == 5) {
				rotation = new Quaternion (Single.Parse (values [1]), Single.Parse (values [2]), Single.Parse (values [3]), Single.Parse (values [0]));
				mouseState = Int32.Parse (values [4]);
				// save this rotation as an "offset" if it was the first time it was measured
				if (firstMeasurement) {
					initialRotation = rotation;
					firstMeasurement = false;
				}
			}
		}

	}


}