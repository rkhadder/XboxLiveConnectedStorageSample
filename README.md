# Connected Storage Sample
## A sample that demostrates how to save and load data from connected storage

* [tutorial](https://www.youtube.com/watch?v=CgzYb9yHThA)
* [xbox live plugin](https://github.com/Microsoft/xbox-live-unity-plugin) ([releases](https://github.com/Microsoft/xbox-live-unity-plugin/releases))
* [creators program](https://www.xbox.com/en-US/developers/creators-program)
* [connected storage docs](https://docs.microsoft.com/en-us/windows/uwp/xbox-live/storage-platform/connected-storage/connected-storage-overview)

### Initialize
```
XboxLiveUser user = SignInManager.Instance.GetPlayer(1);
GameSaveHelper gameSaveHelper = new GameSaveHelper();
StartCoroutine(gameSaveHelper.Initialize(user, result =>
{
    print(result == GameSaveStatus.Ok
            ? "Successfully initialized save system."
            : string.Format("InitializeSaveSystem failed: {0}", result));
}));
```

### Save
```
if (gameSaveHelper.IsInitialized())
{
    var data = new Dictionary<string, byte[]>();
    data[BLOB_NAME] = System.Text.Encoding.Unicode.GetBytes(/* some value */);

    StartCoroutine(gameSaveHelper.SubmitUpdates(CONTAINER_NAME, data, null, result =>
    {
        print(result == GameSaveStatus.Ok
                ? "Successfully stored data."
                : string.Format("Couldn't store data: {0}", result));
    }));
}
else
{
    print("GameSaveHelper is not initialized");
}
```

### Load
```
if (gameSaveHelper.IsInitialized())
{
    StartCoroutine(gameSaveHelper.GetAsBytes(CONTAINER_NAME, new string[] { BLOB_NAME }, result =>
    {
        if (result.ContainsKey(BLOB_NAME))
        {
            string value = System.Text.Encoding.Unicode.GetString(result[BLOB_NAME]);
        }
        else
        {
            print("Couldn't load data");
        }
    }));
}
else
{
    print("GameSaveHelper is not initialized");
}
```
