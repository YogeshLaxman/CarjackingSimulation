using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{   
    public void ReloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
       
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
