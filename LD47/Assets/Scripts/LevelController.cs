using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{

    public int currentLevel = 0;
    public int levelStages = 3;
    public float lengthOfDay = 5.0f;
    public float lengthOfIntermission = 1.0f;

    public float childNeedMinDelay = 1.0f;
    public float childNeedMaxDelay = 3.0f;
    public float childNeedTimeScale = 3.0f;


    public GameObject[] levelObjects;
    public ChildController child;
    public HandController hand;
    public GameObject overlayFog;
    public UIController myUI;
    public Text myUILevelText;
    public Text myUIStageText;

    public int [] cameraSizePerLevel;

    public Vector2[] cameraPositionPerLevel;


    public Vector2[] childSpawnPoint;


    bool isDay;
    int lastLevel;
    int currentStage;
    float startTime;
    float currentTime;

    // Handle need generation
    float timeSinceNeed;
    float timeToNeed;


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
        myUILevelText.text = "Level : " + (currentLevel + 1);
        myUIStageText.text = "Stage : " + (currentStage + 1);


        child.MoveChild(childSpawnPoint[currentLevel]);

        Camera.main.orthographicSize = cameraSizePerLevel[currentLevel];
        Camera.main.transform.position = new Vector3(cameraPositionPerLevel[currentLevel].x, cameraPositionPerLevel[currentLevel].y, -10);
        overlayFog.GetComponent<SpriteRenderer>().enabled = false;
        Debug.Log("Current Level is " + currentLevel + " and current stage is " + currentStage);


        // Get a random need delay
        timeToNeed = Random.Range(childNeedMinDelay, childNeedMaxDelay);
        timeSinceNeed = Time.time;
        Debug.Log("Expecting a need in " + timeToNeed + " seconds");
    }

    public void NeedMet()
    {
        timeToNeed = Random.Range(childNeedMinDelay, childNeedMaxDelay);
        timeSinceNeed = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDay)
        {
            currentTime = Time.time - startTime;

            // Handle triggering child needs
            // Handle need processing
            if (Time.time - timeSinceNeed > timeToNeed)
            {
                child.TriggerNeed(currentTime / lengthOfDay);
            }

            // Update UI Timer Display
            myUI.SetUITime(currentTime / lengthOfDay);


            // Detect end of day
            if (currentTime >= lengthOfDay)
            {
                startTime = Time.time;
                currentStage++;
                isDay = false;
                Debug.Log("Starting Intermission");
                
                levelObjects[currentLevel].SetActive(false);
                overlayFog.GetComponent<SpriteRenderer>().enabled = true;
                child.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                hand.ForceDrop();
                child.ForceNeedClear();
                child.Respawn();
            }
        }
        else
        {
            currentTime = Time.time - startTime;
            myUI.SetUITime(0);


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
                    switch (currentLevel)
                    {
                        case 0: child.AgeChild(ChildController.STAGE.BABY);break;
                        case 1: child.AgeChild(ChildController.STAGE.TODDLER); break;
                        case 2: child.AgeChild(ChildController.STAGE.CHILD); break;
                        case 3: child.AgeChild(ChildController.STAGE.TEEN); break;
                    }
                    if(currentLevel > lastLevel)
                    {
                        // We have finished the game... go to status screen
                        currentLevel = 0;
                    }
                }
                child.MoveChild(childSpawnPoint[currentLevel]);
                Camera.main.orthographicSize = cameraSizePerLevel[currentLevel];
                Camera.main.transform.position = new Vector3(cameraPositionPerLevel[currentLevel].x, cameraPositionPerLevel[currentLevel].y, -10);
                levelObjects[currentLevel].SetActive(true);
                child.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                overlayFog.GetComponent<SpriteRenderer>().enabled = false;
                Debug.Log("Current Level is " + currentLevel + " and current stage is " + currentStage);
                myUILevelText.text = "Level : " + (currentLevel + 1);
                myUIStageText.text = "Stage : " + (currentStage + 1);
            }

        }
    }
}
