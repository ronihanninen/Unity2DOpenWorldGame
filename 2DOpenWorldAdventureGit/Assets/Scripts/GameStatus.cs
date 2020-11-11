using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class GameStatus : MonoBehaviour
{
    public static GameStatus status;

    public string currentLevel;

    public float health;
    public float maxHealth;
    public float previousHealth;

    public bool Level1;
    public bool Level2;
    public bool Level3;

    private void Awake()
    {
        // Singleton
        if (status == null)
        {
            DontDestroyOnLoad(gameObject);
            status = this;
        }
        else
        {
            // Jos tapahtuu jotain omituista, ja peliin päätyy toinenkin GameStatus, mitä tehdään?
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if (Input.GetKeyUp(KeyCode.M))
        {
            currentLevel = null;
            SceneManager.LoadScene("MainMenu");
        }   
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();

        data.health = health;
        data.maxHealth = maxHealth;
        data.currentLevel = currentLevel;
        data.Level1 = Level1;
        data.Level2 = Level2;
        data.Level3 = Level3;

        bf.Serialize(file, data);

        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            health = data.health;
            maxHealth = data.maxHealth;
            currentLevel = data.currentLevel;
            Level1 = data.Level1;
            Level2 = data.Level2;
            Level3 = data.Level3;
        }
    }
}

[Serializable]
class PlayerData
{
    public float health;
    public float maxHealth;
    public string currentLevel;
    public bool Level1;
    public bool Level2;
    public bool Level3;
}