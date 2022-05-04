using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{

    public static Save Sinstance;
    private void Awake()
    {
        Sinstance = this;
        LoadPos();
    }
    public void LoadPos()
    {
        if (PlayerPrefs.HasKey("PlayerPos")) GameObject.FindGameObjectWithTag("Player").transform.position = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("PlayerPos"));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SavePos();
        }
         if (Input.GetKeyDown(KeyCode.D)) PlayerPrefs.DeleteAll();
    }

    void SavePos()
    {
        PlayerPrefs.SetString("PlayerPos", JsonUtility.ToJson(PlayerCont.instance.transform.position));
        
        PlayerPrefs.Save();
    }

    
}
