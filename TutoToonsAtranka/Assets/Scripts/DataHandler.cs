using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DataHandler : MonoBehaviour
{
    [SerializeField] private GameObject gemPref;
    public Camera mainCamera;

    private Vector2 convertedPoint;
    public Vector2[] pointsConverted;
    private Vector2[] points;
    private int[] intPoints;

    private string fileName = "level_data.json";
    
    private Levels levelData;
    private FileHandler dataHandler;

    private int targetWidth;
    private int targetHeight;
    public static DataHandler instance { get; private set; }
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);      
        }
        instance = this;
    }

    void Start()
    {
        targetWidth = Screen.width;
        targetHeight = Screen.height;

        this.dataHandler = new FileHandler(Application.persistentDataPath, fileName);
        LoadData();
        LoadSceneData();
    }

    //Method that loads given data.
    private void LoadData()
    {
        this.levelData = dataHandler.Load();
    }

    //This method loads given points to its coresponding level scene and parses data from string to int value.
    //If statement checks scene's index value and gets the level data with the same index.
    //In the end, a new method is called that instantiates those points on screen.
    private void LoadSceneData()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        for (int i = 0; i < levelData.levels.Length; i++)
        {           
            if(currentSceneIndex == i+1)
            {
                LevelData pointData = levelData.levels[i];
                intPoints = new int[pointData.level_data.Length];
                for (int j = 0; j < pointData.level_data.Length; j++)
                {
                    string data = pointData.level_data[j];
                    int dataInt = int.Parse(data);
                    intPoints[j] = dataInt;
                }
            }            
        }

        GetCoordinates(intPoints);
    }

    //This method takes point data's int array and parses it into vector2 array.
    //It also instantiates those points in their correct positions that are calculated
    //based on device's screen size as well as adds correct number text to each point prefab.
    private void GetCoordinates(int[] dataXY)
    {
        points = new Vector2[dataXY.Length / 2];
        for (int i = 0; i < dataXY.Length; i += 2)
        {
            float x = dataXY[i];
            float y = dataXY[i + 1];

            points[i / 2] = new Vector2(x,  y);
        }

        TextMeshPro indexText = gemPref.GetComponentInChildren<TextMeshPro>();

        int ind = 0;
        pointsConverted = new Vector2[dataXY.Length / 2];

        float centerX = targetWidth / 2f;

        foreach (Vector2 point in points)
        {
            //The coordinates are converted by dividing them by 1000 because that is the range within which their positions can vary.
            //The Y value is subtracted by 1000 to invert it, and the X value is subtracted by 0.5 for centering.
            //To keep their aspect ratio, both position values are multiplied by targetHeight.
            //Multiplying the X value by targetWidth would stretch point positions lengthwise, which would not keep the aspect ratio.
            float convertedX = ((point.x / 1000f) - 0.5f) * targetHeight + centerX;
            float convertedY = ((1000f - point.y) / 1000f) * targetHeight;

            convertedPoint = mainCamera.ScreenToWorldPoint(new Vector2(convertedX, convertedY));
            pointsConverted[ind] = convertedPoint;

            indexText.text = (ind + 1).ToString();
            Instantiate(gemPref, convertedPoint, Quaternion.identity);
            ind++;
        }
    }
      
   
}
