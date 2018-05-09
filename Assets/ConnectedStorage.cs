using Microsoft.Xbox.Services;
using Microsoft.Xbox.Services.Client;
using Microsoft.Xbox.Services.ConnectedStorage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectedStorage : MonoBehaviour
{
    public Text StoredValueText;
    public Text RandomValueText;

    XboxLiveUser user;
    GameSaveHelper gameSaveHelper;
    int randomValue;

    const string CONTAINER_NAME = "stats";
    const string BLOB_NAME = "score";
    
    public void Init()
    {
        user = SignInManager.Instance.GetPlayer(1);
        gameSaveHelper = new GameSaveHelper();
        StartCoroutine(gameSaveHelper.Initialize(user, result =>
        {
            print(result == GameSaveStatus.Ok
                    ? "Successfully initialized save system."
                    : string.Format("InitializeSaveSystem failed: {0}", result));
        }));
    }

    public void GenerateRandomValue()
    {
        randomValue = Random.Range(0, 100);
        RandomValueText.text = randomValue.ToString();
    }

    public void Save()
    {
        if (gameSaveHelper.IsInitialized())
        {
            var data = new Dictionary<string, byte[]>();
            data[BLOB_NAME] = System.Text.Encoding.Unicode.GetBytes(randomValue.ToString());

            StartCoroutine(gameSaveHelper.SubmitUpdates(CONTAINER_NAME, data, null, result =>
            {
                print(result == GameSaveStatus.Ok
                        ? "Successfully stored score."
                        : string.Format("Couldn't store score: {0}", result));
            }));
        }
        else
        {
            print("[Save] GameSaveHelper is not initialized");
        }
    }

    public void Load()
    {
        if (gameSaveHelper.IsInitialized())
        {
            StartCoroutine(gameSaveHelper.GetAsBytes(CONTAINER_NAME, new string[] { BLOB_NAME }, result =>
            {
                if (result.ContainsKey(BLOB_NAME))
                {
                    StoredValueText.text = System.Text.Encoding.Unicode.GetString(result[BLOB_NAME]);
                }
                else
                {
                    print("Couldn't load score");
                }
            }));
        }
        else
        {
            print("[Load] GameSaveHelper is not initialized");
        }
    }
}
