using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System.IO;

public class DataSaveManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    [Header("File Data Saved Properties")]
    public GameDataSave gameDataSave;

    private List<InterfaceDataSave> dataSaveObjects;

    private FileDataHandler dataHandler;

    public static DataSaveManager instance { get; private set; }


    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one Data Persistent Manager in the scene.");
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }


    public void NewGame()
    {
        this.gameDataSave = new GameDataSave();
    }

    public void CreateNewSaveData()
    {
        this.gameDataSave = new GameDataSave();
    }

    public void LoadGame()
    {

        this.gameDataSave = dataHandler.Load();

        if(this.gameDataSave == null)
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded");
        }

        foreach (InterfaceDataSave dataSaveObj in dataSaveObjects)
        {
            dataSaveObj.LoadData(gameDataSave);
        }

        Debug.Log("Game Loaded");


    }

    public void SaveGame()
    {

        if (this.gameDataSave == null)
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded");
            NewGame();
        }

        foreach (InterfaceDataSave dataSaveObj in dataSaveObjects)
        {
            dataSaveObj.SaveData(ref gameDataSave);
            
        }

        dataHandler.Save(gameDataSave);

        Debug.Log("Game Saved");

    }

    public void DeleteSaveGame()
    {
        dataHandler.Delete();
        Debug.Log("Save Data Has Removed");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnLoaded;

    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnLoaded;

    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnScene Loaded Called");
        this.dataSaveObjects = FindAllDataSaveObjects();
        LoadGame();
    }

    public void OnSceneUnLoaded(Scene scene)
    {
        Debug.Log("OnScene UnLoaded Called");
        //SaveGame();
    }

    private void OnApplicationQuit()
    {
        //SaveGame(); 
    }

    private  List<InterfaceDataSave> FindAllDataSaveObjects()
    {
        IEnumerable<InterfaceDataSave> dataSaveObjects = FindObjectsOfType<MonoBehaviour>().OfType<InterfaceDataSave>();
        return new List<InterfaceDataSave>(dataSaveObjects);
    }

    public bool HasGameData()
    {
        return gameDataSave != null;
    }
}