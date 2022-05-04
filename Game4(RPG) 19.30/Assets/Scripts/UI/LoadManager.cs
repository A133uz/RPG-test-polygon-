using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public Text progress, Tip;
    AsyncOperation opera;
    [TextArea(1,5)]
    public string[] tips;

    private void Start()
    {
        if (PlayerPrefs.HasKey("PlayerPos")) Save.Sinstance.LoadPos();
        opera = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
    }

    private void Update()
    {
        if (opera.isDone == false)
        {
            float progress = opera.progress;
            this.progress.text = $"{(progress * 100).ToString("0")}%";
        }
        else this.progress.text = "Нажми любой кей, чтобы поиграть";
        if (opera.isDone)
        {
            if (Input.anyKey)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
                SceneManager.UnloadSceneAsync(1);
            }
        }
    }
}
