using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    
    public List<Button> btnList;

    private void Awake()
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            btnList.Add(transform.GetChild(i).GetComponent<Button>());
        }
        btnList[0].onClick.AddListener(NewGame);
        btnList[1].interactable = false;
        btnList[2].onClick.AddListener(Quit);  
    }

    private void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
