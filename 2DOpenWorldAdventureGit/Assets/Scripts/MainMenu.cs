using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 100, 100, 30), "Play Game"))
        {
            SceneManager.LoadScene("Map");
        }

        if (GUI.Button(new Rect(10, 150, 100, 30), "Save Game"))
        {
            GameStatus.status.Save();
        }

        if (GUI.Button(new Rect(10, 200, 100, 30), "Load Game"))
        {
            GameStatus.status.Load();
        }
    }
}
