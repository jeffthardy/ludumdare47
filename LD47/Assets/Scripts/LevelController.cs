using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    public int currentLevel = 0;
    public int levelStages = 3;
    public float lengthOfDay = 5.0f;
    public float lengthOfIntermission = 1.0f;


    public GameObject[] levelObjects;
    public GameObject child;
    public GameObject overlayFog;

    public int [] cameraSizePerLevel;

    public Vector2[] cameraPositionPerLevel;


    public Vector2[] childSpawnPoint;


    bool isDay;
    int lastLevel;
    int currentStage;
    float startTime;
    float currentTime;


    // Start is called before the first frame update
    void Start()
    {
        if (cameraSizePerLevel.Length != levelObjects.Length)
            Debug.Log("ERROR! cameraSettingsPerLevel.Length != levelObjects.Length");
        if (cameraPositionPerLevel.Length != levelObjects.Length)
            Debug.Log("ERROR! cameraSettingsPerLevel.Length != levelObjects.Length");
        if (childSpawnPoint.Length != levelObjects.Length)
            Debug.Log("ERROR! cameraSettingsPerLevel.Length != levelObjects.Length");

        lastLevel = levelObjects.Length-1;
        for(int i = 0; i <= lastLevel; i++)
        {
            if(currentLevel == i)
            {
                levelObjects[i].SetActive(true);
            }
            else
            {
                levelObjects[i].SetActive(false);
            }
        }
        startTime = Time.time;
        isDay = true;
        currentStage = 0;


        child.GetComponent<ChildController>().MoveChild(childSpawnPoint[currentLevel]);

        Camera.main.orthographicSize = cameraSizePerLevel[currentLevel];
        Camera.main.transform.position = new Vector3(cameraPositionPerLevel[currentLevel].x, cameraPositionPerLevel[currentLevel].y, -10);
        overlayFog.GetComponent<SpriteRenderer>().enabled = false;
        Debug.Log("Current Level is " + currentLevel + " and current stage is " + currentStage);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDay)
        {
            currentTime = Time.time - startTime;


            // Detect end of day
            if (currentTime >= lengthOfDay)
            {
                startTime = Time.time;
                currentStage++;
                isDay = false;
                Debug.Log("Starting Intermission");
                
                levelObjects[currentLevel].SetActive(false);
                overlayFog.GetComponent<SpriteRenderer>().enabled = true;
                child.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        else
        {
            currentTime = Time.time - startTime;


            // Detect end of day
            if (currentTime >= lengthOfIntermission)
            {
                isDay = true;
                startTime = Time.time;

                // Check if it is time to go to the next age
                if(currentStage == levelStages)
                {
                    currentStage = 0;
                    currentLevel++;
                    if(currentLevel > lastLevel)
                    {
                        // We have finished the game... go to status screen
                        currentLevel = 0;
                    }
                }
                child.GetComponent<ChildController>().MoveChild(childSpawnPoint[currentLevel]);
                Camera.main.orthographicSize = cameraSizePerLevel[currentLevel];
                Camera.main.transform.position = new Vector3(cameraPositionPerLevel[currentLevel].x, cameraPositionPerLevel[currentLevel].y, -10);
                levelObjects[currentLevel].SetActive(true);
                child.GetComponent<SpriteRenderer>().enabled = true;
                overlayFog.GetComponent<SpriteRenderer>().enabled = false;
                Debug.Log("Current Level is " + currentLevel + " and current stage is " + currentStage);
            }

        }
    }
}
