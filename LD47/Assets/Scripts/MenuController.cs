using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public Button startButton;
    public string gameScene;

    // Start is called before the first frame update
    void Start()
    {
        if (startButton)
            startButton.onClick.AddListener(StartButtonOnClick);

    }

    void StartButtonOnClick()
    {
        Debug.Log("You have clicked the start button.  Loading game...");
        SceneManager.LoadScene(gameScene);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
