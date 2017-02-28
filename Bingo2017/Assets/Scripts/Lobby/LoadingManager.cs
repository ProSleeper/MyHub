using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public Slider loadingBar;
    //public Text percent;
    public AsyncOperation loading; // 비동기 작업 전용 객체 생성

    void Start() // 로딩은 처음에 그릴 때 안그려지면 안되니 무조건 그려야 해서 start?
    {
        loadingBar.value = 0;
        //loadingBar.fillAmount = 0;
        StartCoroutine("Loading");
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator Loading()
    {
        loading = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        // if (SceneManager.GetActiveScene().buildIndex == 3)
        // {
        //     loading = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
        // }
         // 다음 신 나오기 전까지 계속 로딩해라..
        // 이건 로딩 전용으로만 쓰는지?
        // 일반 신에다가 쓰면 어떻게 되는지

        // 신메니져가 로딩을 관련하는 // async 비동기 // 조금식 로딩을 한다..
        // 일을 하면 다 끝내고 알려주는, 동기는 무조건

        // 호출은 호출대로 끝나는건 내가 다시 물어봐야 함..

        // next 이름 쓴 이유는 로딩 때문에 다음 씬 이름을 까먹기 때문에 datamanager.instance.nextscene 에 넣어둠..

        /// 이 다음줄 바로 실행됨(여기서 뭘 기다리고 어쩌고 아님)
        
        while (!loading.isDone) // 로딩에 관련된 정보가 모두 들어있음 // 안 끝났으면 ! 꼭 넣어야 함
        {
            yield return loading; // null 일수도 있고 시간도 되고 
        } // 사실 while문 빼도 됨 더욱 명확하게 보이기 위해서

        // 로딩은 특수한 경우로 넘기는 경우
        // 한 프레임 단위가 아닌 loading은 로딩대로 진행되고 프레임 단위로 하는건 update
        // 엔진단에서 정하기 때문에 언제 다시 되는지는 알 수 없다.

        DestroyObject(gameObject);
    }

    void Update()
    {
        loadingBar.value = loading.progress;
        //percent.text = ((int)(loading.progress * 100)).ToString() + "%";
    }
}