using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

/*
 * Author: [Dorey, Dylan]
 * Last Updated: [10/29/2024]
 * [Saves players values from ItemsToSave to a JSON file for reloading on launch]
 */

public class Save : Singleton<Save>
{
    private void OnEnable()
    {
        GameEventBus.Subscribe(GameState.loadGame, LoadSave);
        GameEventBus.Subscribe(GameState.returnToMenu, SaveGame);
    }

    private void OnDisable()
    {
        GameEventBus.Unsubscribe(GameState.loadGame, LoadSave);
        GameEventBus.Unsubscribe(GameState.returnToMenu, SaveGame);
    }

    /// <summary>
    /// saves the players high score to a JSON file
    /// </summary>
    public void SaveGame()
    {
        //Create a save instance with all the data for the current session saved into it
        ItemsToSave save = CreateSaveGameObject();

        //create a binary formatter and filestream object by passing a path for the save instance to be saved into
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");

        //serialize the data into bytes and write it to the disk and close the filestream
        bf.Serialize(file, save);
        file.Close();

        Debug.Log("Game Saved");
    }

    /// <summary>
    /// loads and sets the players highest score from the saved JSON file
    /// </summary>
    public void LoadSave()
    {
        // check to see if a save file exists. If not log saying no current save file
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            //create a binary formatter to deserialize the byte file into a ItemToSave(object name) file
            //open a file and store the File Stream, then use that file as a the file to deserialize
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            ItemsToSave save = (ItemsToSave)bf.Deserialize(file);
            file.Close();

            //Convert values into game state
            PlayerData.Instance.highScore = save.highScore;
            GameManager.Instance.audioSetting = save.audioSetting;
            GameManager.Instance.tutorialSetting = save.tutorialSetting;
            UIManager.Instance.audioToggle.isOn = save.audioSetting;
            UIManager.Instance.tutorialToggle.isOn = save.tutorialSetting;


            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No Game Saved");
        }

        GameEventBus.Publish(GameState.gameLaunch);
    }


    /// <summary>
    /// creates a gameobject of type ItemsToSave and sets the highScore int to the players high score
    /// </summary>
    /// <returns> the players highscore </returns>
    private ItemsToSave CreateSaveGameObject()
    {
        //reference to a new save object
        ItemsToSave save = new ItemsToSave();

        //set the high score object to the players high score
        save.highScore = PlayerData.Instance.highScore;
        save.audioSetting = GameManager.Instance.audioSetting;
        save.tutorialSetting = GameManager.Instance.tutorialSetting;

        //save.highScore = 0;
        //save.audioSetting = true;
        //save.tutorialSetting = false;

        //return the save object
        return save;
    }
}
