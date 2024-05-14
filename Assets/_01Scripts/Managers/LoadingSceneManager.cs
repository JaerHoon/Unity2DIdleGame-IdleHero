using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public static LoadingSceneManager instance;
    public static int nextScene;

    [SerializeField]
    Image progressBar;

    public static bool IsDone;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
       
    }

    private void Start()
    {
        StartCoroutine(LoadScene());
       
    }

    public static void LoadScene(int sceneNum)
    {
        nextScene = sceneNum;
        SceneManager.LoadScene("LoadingScene");
    }

    IEnumerator LoadScene()
    {
        progressBar.fillAmount = 0;
        yield return null;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
       
        float timer = 0.0f;
        while (!op.isDone)
        {
            IsDone = op.isDone;
           
            yield return new WaitForSeconds(0.05f);

            timer += Time.deltaTime; 
            if (op.progress < 0.9f) 
            { 
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress) { timer = 0f; } 
            } 
            else 
            { 
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if (progressBar.fillAmount == 1.0f) 
                { 
                    op.allowSceneActivation = true; yield break; 
                } 
            
            
            }
        }
    }
    
}
