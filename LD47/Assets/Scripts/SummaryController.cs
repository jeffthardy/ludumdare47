using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SummaryController : MonoBehaviour
{

    public Button EndButton;
    public string menuScene;

    public Text scoreText;

    int loveGiven=0;
    int healthGiven = 0;
    int inteligenceGiven = 0;
    int loveMissed = 0;
    int healthMissed = 0;
    int inteligenceMissed = 0;

    float healthPercent;
    float lovePercent;
    float inteligencePercent;

    string finalName = "Parent";

    string loveMod;
    string IntelMod;
    string healthMod;
    string finalMod;

    // Start is called before the first frame update
    void Start()
    {
        if (EndButton)
            EndButton.onClick.AddListener(EndButtonOnClick);



        if (PlayerPrefs.HasKey("statHealth"))
            healthGiven = PlayerPrefs.GetInt("statHealth");
        if (PlayerPrefs.HasKey("statLove"))
            loveGiven = PlayerPrefs.GetInt("statLove");
        if (PlayerPrefs.HasKey("statInteligence"))
            inteligenceGiven = PlayerPrefs.GetInt("statInteligence");

        if (PlayerPrefs.HasKey("statHealthFails"))
            healthMissed = PlayerPrefs.GetInt("statHealthFails");
        if (PlayerPrefs.HasKey("statLoveFails"))
            loveMissed = PlayerPrefs.GetInt("statLoveFails");
        if (PlayerPrefs.HasKey("statInteligenceFails"))
            inteligenceMissed = PlayerPrefs.GetInt("statInteligenceFails");

        if (healthMissed + healthGiven == 0)
            healthPercent = 1;
        else
            healthPercent = healthGiven / (float)(healthGiven + healthMissed);

        if (loveMissed + loveGiven == 0)
            lovePercent = 1;
        else
            lovePercent = loveGiven / (float)(loveGiven + loveMissed);

        if (inteligenceMissed + inteligenceGiven == 0)
            inteligencePercent = 1;
        else
            inteligencePercent = inteligenceGiven / (float)(inteligenceGiven + inteligenceMissed);

        Debug.Log(healthGiven + "," + loveGiven + "," + inteligenceGiven + ","+ healthMissed + ","+ loveMissed + ","+ inteligenceMissed);

        if (lovePercent > .5)
            loveMod = "Loving ";
        else
            loveMod = "Uncaring ";

        if (inteligencePercent > .5)
            IntelMod = "Genius ";
        else
            IntelMod = "Brainless ";

        if (healthPercent > .5)
            healthMod = "Nurturing ";
        else
            healthMod = "Neglecting ";


        if (healthPercent + lovePercent + inteligencePercent == 0)
            finalMod = "??????!!!!";
        else
            finalMod = ".";


        finalName = loveMod + IntelMod + healthMod + "Parent" + finalMod;


        scoreText.text = "Health Score: " + (int)(healthPercent*100 ) + "%\n" +
                         "Inteligence Score: " + (int)(inteligencePercent * 100 ) + "%\n" +
                         "Love Score : " + (int)(lovePercent * 100 ) + "%\n\n" +
                         "Summary: " + finalName;


    }

    void EndButtonOnClick()
    {
        Debug.Log("You have clicked the menu button.  Loading menu...");
        SceneManager.LoadScene(menuScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
