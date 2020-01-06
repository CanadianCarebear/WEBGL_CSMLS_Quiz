using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO; //contains functions related to loading and saving files
using System;

public class DataController : MonoBehaviour
{
    private RoundData[] allRoundData;
    private PlayerProgress playerProgress;

    private string gameDataFileName = "data.json";

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        LoadGameData();
        LoadPlayerProgress();
        SceneManager.LoadScene(1);
    }

    public RoundData GetCurrentRoundData()
    {
        //if we want to return different rounds, we could do that here
        //we could store an int representing the current round index in PlayerProgress
        return allRoundData[0]; //return all data to the persistent scene
    }

    public void SubmitNewPlayerScore(int newScore)
    {
        if (newScore > playerProgress.highestScore && newScore != 120)
        {
            playerProgress.highestScore = newScore;
            SavePlayerProgress();
        }
        else if (newScore == 120)
        {
            newScore = 0;
            playerProgress.highestScore = newScore;
            SavePlayerProgress();

        }
    }

    public int GetHighestPlayerScore()
    {
        return playerProgress.highestScore;
    }

    private void LoadGameData()
    {
        var jsonTextFile = Resources.Load<TextAsset>("data");
        string content = jsonTextFile.ToString();

        if(content != null)
        {
            GameData loadedData = JsonUtility.FromJson<GameData>(content);
            allRoundData = loadedData.allRoundData;
        }
        else
        {
            Debug.LogError("Cannot load game data!");
        }
       


        //// Path.Combine combines strings into a file path
        //// Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        //string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

        //if (File.Exists(filePath))
        //{
        //    // Read the json from the file into a string
        //    string dataAsJson = File.ReadAllText(filePath);
        //    // Pass the json to JsonUtility, and tell it to create a GameData object from it
        //    GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

        //    // Retrieve the allRoundData property of loadedData
        //    allRoundData = loadedData.allRoundData;
        //}
        //else
        //{
        //    Debug.LogError("Cannot load game data!");
        //}
    }

    private void LoadPlayerProgress()
    {
        playerProgress = new PlayerProgress();

        //in PlayerPrefs variables are called key
        if (PlayerPrefs.HasKey("highestScore"))
        {
            playerProgress.highestScore = PlayerPrefs.GetInt("highestScore");
        }
    }

    private void SavePlayerProgress()
    {
        // Save the value playerProgress.highestScore to PlayerPrefs, with a key of "highestScore"
        PlayerPrefs.SetInt("highestScore", playerProgress.highestScore);
    }
}
