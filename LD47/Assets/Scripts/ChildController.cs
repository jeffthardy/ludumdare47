using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildController : MonoBehaviour
{
    // Public Controls
    public float speed = 4;
    public float AngleChangeTime = 1.0f;
    public float needLength = 3.0f;
    public Text myUINeedText;
    public Text myUIStatHealthText;
    public Text myUIStatLoveText;
    public Text myUIStatInteligenceText;
    public UIController myUI;
    public LevelController myLevelController;

    public AudioClip[] baby_pickup;
    public AudioClip[] baby_whine;
    public AudioClip[] baby_sleep;
    public AudioClip[] baby_eat;
    public AudioClip[] baby_clean;


    // Basic Child State Keeping
    public enum STAGE { BABY, TODDLER, CHILD, TEEN, ADULT};
    public enum NEEDS { NONE=0, FOOD, CLEAN, LOVE, SLEEP, EDUCATE};
    public enum STATE { IDLE = 0, MOVING, SLEEPING, EATING, HAPPY, SAD, THINKING };

    STAGE myStage;
    NEEDS myNeed;
    STATE myState;


    // Playthrough Stats
    int statHealth;
    int statLove;
    int statInteligence;


    Vector2 spawnPoint;
    Quaternion spawnRotation;

    float timeSinceAngleChange;
    float timeOfNeedStart;
    float angle;
    float horizontal;
    float vertical;
    Rigidbody2D rigidbody2d;
    Animator animator;
    Random random;

    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        myStage = STAGE.BABY;
        myNeed = NEEDS.NONE;
        myState = STATE.MOVING;
        statHealth=0;
        statLove=0;
        statInteligence=0;
        angle = 0;
        timeSinceAngleChange = Time.time;

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        spawnPoint = transform.position;
        spawnRotation = transform.rotation;
        ForceNeedClear();


        myUIStatHealthText.text = "H: " + statHealth;
        myUIStatLoveText.text = "L: " + statLove;
        myUIStatInteligenceText.text = "I: " + statInteligence;
    }



    private void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);

    }

    public void MoveChild(Vector2 pos)
    {
        transform.position = new Vector2(pos.x, pos.y);
    }

    public void ForceNeedClear()
    {
        myNeed = NEEDS.NONE;
        myUI.SetUINeed(0);
    }


    public void Respawn()
    {
        transform.position = spawnPoint;
        transform.rotation = spawnRotation;
    }

    public void AgeChild(STAGE childStage)
    {
        myStage = childStage;
    }

    public void playCleanAudio()
    {
        int index = Random.Range(0, baby_clean.Length);
        AudioSource.PlayClipAtPoint(baby_clean[index], transform.position);
    }

    public void playFoodAudio()
    {
        int index = Random.Range(0, baby_eat.Length);
        AudioSource.PlayClipAtPoint(baby_eat[index], transform.position);
    }

    public void playLearnAudio()
    {
        int index = Random.Range(0, baby_clean.Length);
        AudioSource.PlayClipAtPoint(baby_clean[index], transform.position);
    }

    public void playLoveAudio()
    {
        int index = Random.Range(0, baby_clean.Length);
        AudioSource.PlayClipAtPoint(baby_clean[index], transform.position);
    }

    public void playSleepAudio()
    {
        int index = Random.Range(0, baby_sleep.Length);
        AudioSource.PlayClipAtPoint(baby_sleep[index], transform.position);
    }

    public void playPickupAudio()
    {
        int index = Random.Range(0, baby_pickup.Length);
        AudioSource.PlayClipAtPoint(baby_pickup[index], transform.position);

    }
    public void playWhineAudio()
    {
        int index = Random.Range(0, baby_whine.Length);
        AudioSource.PlayClipAtPoint(baby_whine[index], transform.position);

    }

    public void HelpChild(NEEDS givenNeed)
    {
        if (myNeed == givenNeed)
        {
            Debug.Log("You met the childs need!");
            // Failed to meet need!
            switch (myNeed)
            {
                case NEEDS.CLEAN:
                    statHealth++;
                    playCleanAudio();
                    break;
                case NEEDS.FOOD:
                    statHealth++;
                    playFoodAudio();
                    break;
                case NEEDS.EDUCATE:
                    statInteligence++;
                    playLearnAudio();
                    break;
                case NEEDS.LOVE:
                    statLove++;
                    playLoveAudio();
                    break;
                case NEEDS.SLEEP:
                    statHealth++;
                    playSleepAudio();
                    break;
            }
            myNeed = NEEDS.NONE;
            myUINeedText.text = "NEED: None ";
            myUI.SetUINeed(0);
            myLevelController.NeedMet();


            myUIStatHealthText.text = "H: " + statHealth;
            myUIStatLoveText.text = "L: " + statLove;
            myUIStatInteligenceText.text = "I: " + statInteligence;
        }
        else
        {
            Debug.Log("You gave the child the wrong need!");
        }

    }

    // Trigger a need at time of game day
    public void TriggerNeed(float percentDay)
    {
        if(myNeed == NEEDS.NONE)
        {
            Debug.Log("Triggering" + myStage + " Event at " + percentDay);
            switch (myStage)
            {
                case STAGE.BABY:
                    // Handle BABY Times
                    playWhineAudio();
                    
                    if (percentDay < .2 || percentDay >= .8)
                        myNeed = NEEDS.SLEEP;
                    if(percentDay >= 0.2 && percentDay < 0.5)
                        myNeed = NEEDS.FOOD;
                    if (percentDay >= 0.5 && percentDay < 0.8)
                        myNeed = NEEDS.CLEAN;

                    timeOfNeedStart = Time.time;

                    break;
                case STAGE.TODDLER:
                    // Handle TODDLER Times
                    if (percentDay < .2 || percentDay >= .8)
                        myNeed = NEEDS.SLEEP;
                    if (percentDay >= 0.2 && percentDay < 0.5)
                        myNeed = NEEDS.FOOD;
                    if (percentDay >= 0.5 && percentDay < 0.8)
                        myNeed = NEEDS.CLEAN;

                    timeOfNeedStart = Time.time;



                    break;
                case STAGE.CHILD:
                    // Handle CHILD Times
                    if (percentDay < .2 || percentDay >= .8)
                        myNeed = NEEDS.SLEEP;
                    if (percentDay >= 0.2 && percentDay < 0.5)
                        myNeed = NEEDS.FOOD;
                    if (percentDay >= 0.5 && percentDay < 0.8)
                        myNeed = NEEDS.CLEAN;

                    timeOfNeedStart = Time.time;




                    break;
                case STAGE.TEEN:
                    // Handle TEEN Times
                    if (percentDay < .2 || percentDay >= .8)
                        myNeed = NEEDS.SLEEP;
                    if (percentDay >= 0.2 && percentDay < 0.5)
                        myNeed = NEEDS.FOOD;
                    if (percentDay >= 0.5 && percentDay < 0.8)
                        myNeed = NEEDS.CLEAN;

                    timeOfNeedStart = Time.time;




                    break;
                default:
                    break;
            }
        }

        string currentNeedText = "";
        switch (myNeed)
        {
            case NEEDS.NONE:
                currentNeedText = "None";
                break;
            case NEEDS.CLEAN:
                currentNeedText = "Clean";
                break;
            case NEEDS.FOOD:
                currentNeedText = "Food";
                break;
            case NEEDS.EDUCATE:
                currentNeedText = "Educate";
                break;
            case NEEDS.LOVE:
                currentNeedText = "Love";
                break;
            case NEEDS.SLEEP:
                currentNeedText = "Sleep";
                break;
        }
        myUINeedText.text = "NEED: " + currentNeedText;
    }

    // Update is called once per frame
    void Update()
    {
        if (myNeed != NEEDS.NONE)
        {
            myUI.SetUINeed((Time.time - timeOfNeedStart)/needLength);
            if (Time.time - timeOfNeedStart > needLength)
            {
                // Failed to meet need!
                switch (myNeed)
                {
                    case NEEDS.CLEAN:
                        statHealth--;
                        break;
                    case NEEDS.FOOD:
                        statHealth--;
                        break;
                    case NEEDS.EDUCATE:
                        statInteligence--;
                        break;
                    case NEEDS.LOVE:
                        statLove--;
                        break;
                    case NEEDS.SLEEP:
                        statHealth--;
                        break;
                }
                myNeed = NEEDS.NONE;
                myUINeedText.text = "NEED: None ";
                myUI.SetUINeed(0);


                myUIStatHealthText.text = "H: " + statHealth;
                myUIStatLoveText.text = "L: " + statLove;
                myUIStatInteligenceText.text = "I: " + statInteligence;
            }
        }

        // Handle random child movement
        if (Time.time - timeSinceAngleChange > AngleChangeTime)
        {
            timeSinceAngleChange = Time.time;
            angle = Random.Range(0, 360);
        }

        if(myState == STATE.MOVING)
        {
            horizontal = Mathf.Sin(Mathf.Deg2Rad * angle);
            vertical = Mathf.Cos(Mathf.Deg2Rad * angle);
        }
        
    }
}
