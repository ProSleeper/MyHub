using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppReset : MonoBehaviour {

	bool isPaused;
	bool isReset;
	public GameObject[] dondes;

	// Use this for initialization
	void Start () {
		isPaused = false;
		isReset = false;
	}
	
	void OnApplicationFocus(bool hasFocus)
    {
       // Debug.Log("얻음");
        isPaused = !hasFocus;

        if (!isPaused && isReset)
        {
            SceneManager.LoadScene(0);
			Destroy(this);
        }

        if (isPaused)
        {
            isReset = true;
            //Debug.Log("잃음");
        }
    }

    //일단 리셋코드이긴 함... 근데 이걸 써먹기가 힘듬...
    public void RestartAppForAOS()
    {
        AndroidJavaObject AOSUnityActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaClass PendingIntentClass = new AndroidJavaClass("android.app.PendingIntent");
        AndroidJavaObject baseContext = AOSUnityActivity.Call<AndroidJavaObject>("getBaseContext");
        AndroidJavaObject intentObj
            = baseContext.Call<AndroidJavaObject>("getPackageManager").Call<AndroidJavaObject>("getLaunchIntentForPackage", baseContext.Call<string>("getPackageName"));

        AndroidJavaObject context = AOSUnityActivity.Call<AndroidJavaObject>("getApplicationContext");
        AndroidJavaObject pendingIntentObj
        = PendingIntentClass.CallStatic<AndroidJavaObject>("getActivity", context, 123456, intentObj, PendingIntentClass.GetStatic<int>("FLAG_CANCEL_CURRENT"));

        AndroidJavaClass AlarmManagerClass = new AndroidJavaClass("android.app.AlarmManager");
        AndroidJavaClass JavaSystemClass = new AndroidJavaClass("java.lang.System");

        AndroidJavaObject mAlarmManager = AOSUnityActivity.Call<AndroidJavaObject>("getSystemService", "alarm");
        long restartMillis = JavaSystemClass.CallStatic<long>("currentTimeMillis") + 100;
        mAlarmManager.Call("set", AlarmManagerClass.GetStatic<int>("RTC"), restartMillis, pendingIntentObj);

        JavaSystemClass.CallStatic("exit", 0);
    }
}
