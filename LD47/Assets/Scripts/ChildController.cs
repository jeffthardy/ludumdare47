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

    SpriteRenderer spriteRenderer;
    public Sprite baby;
    public Sprite toddler;
    public Sprite child;
    public Sprite teen;

    public GameObject splat;
    public GameObject splatHolder;

    public AudioClip[] baby_pickup;
    public AudioClip[] baby_whine;
    public AudioClip[] baby_sleep;
    public AudioClip[] baby_eat;
    public AudioClip[] baby_clean;

    public AudioClip[] toddler_pickup;
    public AudioClip[] toddler_whine;
    public AudioClip[] toddler_sleep;
    public AudioClip[] toddler_eat;
    public AudioClip[] toddler_clean;
    public AudioClip[] toddler_love;

    public AudioClip[] child_pickup;
    public AudioClip[] child_whine;
    public AudioClip[] child_sleep;
    public AudioClip[] child_eat;
    public AudioClip[] child_clean;
    public AudioClip[] child_love;
    public AudioClip[] child_learn;

    public AudioClip[] teen_pickup;
    public AudioClip[] teen_whine;
    public AudioClip[] teen_sleep;
    public AudioClip[] teen_eat;
    public AudioClip[] teen_clean;
    public AudioClip[] teen_love;
    public AudioClip[] teen_learn;


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
    int statHealthFails;
    int statLoveFails;
    int statInteligenceFails;


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

    private void Awake()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

    }

    // Start is called before the first frame update
    void Start()
    {
        myNeed = NEEDS.NONE;
        myState = STATE.MOVING;
        statHealth=0;
        statLove=0;
        statInteligence=0;
        statHealthFails = 0;
        statLoveFails = 0;
        statInteligenceFails = 0;
        angle = 0;
        timeSinceAngleChange = Time.time;

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = baby;

        spawnPoint = transform.position;
        spawnRotation = transform.rotation;
        ForceNeedClear();


        myUIStatHealthText.text = "H: " + statHealth + " / " + (statHealth + statHealthFails);
        myUIStatLoveText.text = "L: " + statLove + " / " + (statLove + statLoveFails);
        myUIStatInteligenceText.text = "I: " + statInteligence + " / " + (statInteligence + statInteligenceFails);
    }


    public void SaveStats()
    {
        PlayerPrefs.SetInt("statHealth", statHealth);
        PlayerPrefs.SetInt("statLove", statLove);
        PlayerPrefs.SetInt("statInteligence", statInteligence);
        PlayerPrefs.SetInt("statHealthFails", statHealthFails);
        PlayerPrefs.SetInt("statLoveFails", statLoveFails);
        PlayerPrefs.SetInt("statInteligenceFails", statInteligenceFails);
        PlayerPrefs.Save();


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
        switch (childStage)
        {
            case STAGE.BABY:  spriteRenderer.sprite = baby; break;
            case STAGE.TODDLER: spriteRenderer.sprite = toddler; break;
            case STAGE.CHILD: spriteRenderer.sprite = child; break;
            case STAGE.TEEN: spriteRenderer.sprite = teen; break;
        }
        
    }

    public void playCleanAudio()
    {
        int index;
        switch (myStage)
        {
            case STAGE.BABY:
                index = Random.Range(0, baby_clean.Length);
                AudioSource.PlayClipAtPoint(baby_clean[index], transform.position);
                break;
            case STAGE.TODDLER:
                index = Random.Range(0, toddler_clean.Length);
                AudioSource.PlayClipAtPoint(toddler_clean[index], transform.position);
                break;
            case STAGE.CHILD:
                index = Random.Range(0, child_clean.Length);
                AudioSource.PlayClipAtPoint(child_clean[index], transform.position);
                break;
            case STAGE.TEEN:
                index = Random.Range(0, teen_clean.Length);
                AudioSource.PlayClipAtPoint(teen_clean[index], transform.position);
                break;
        }
    }

    public void playFoodAudio()
    {
        int index;
        switch (myStage)
        {
            case STAGE.BABY:
                index = Random.Range(0, baby_eat.Length);
                AudioSource.PlayClipAtPoint(baby_eat[index], transform.position);
                break;
            case STAGE.TODDLER:
                index = Random.Range(0, toddler_eat.Length);
                AudioSource.PlayClipAtPoint(toddler_eat[index], transform.position);
                break;
            case STAGE.CHILD:
                index = Random.Range(0, child_eat.Length);
                AudioSource.PlayClipAtPoint(child_eat[index], transform.position);
                break;
            case STAGE.TEEN:
                index = Random.Range(0, teen_eat.Length);
                AudioSource.PlayClipAtPoint(teen_eat[index], transform.position);
                break;
        }
    }

    public void playLearnAudio()
    {
        int index;
        switch (myStage)
        {
            case STAGE.BABY:
                //index = Random.Range(0, baby_learn.Length);
                //AudioSource.PlayClipAtPoint(baby_learn[index], transform.position);
                break;
            case STAGE.TODDLER:
                //index = Random.Range(0, toddler_learn.Length);
                //AudioSource.PlayClipAtPoint(toddler_learn[index], transform.position);
                break;
            case STAGE.CHILD:
                index = Random.Range(0, child_learn.Length);
                AudioSource.PlayClipAtPoint(child_learn[index], transform.position);
                break;
            case STAGE.TEEN:
                index = Random.Range(0, teen_learn.Length);
                AudioSource.PlayClipAtPoint(teen_learn[index], transform.position);
                break;
        }
    }

    public void playLoveAudio()
    {
        int index;
        switch (myStage)
        {
            case STAGE.BABY:
                //index = Random.Range(0, baby_love.Length);
                //AudioSource.PlayClipAtPoint(baby_love[index], transform.position);
                break;
            case STAGE.TODDLER:
                index = Random.Range(0, toddler_love.Length);
                AudioSource.PlayClipAtPoint(toddler_love[index], transform.position);
                break;
            case STAGE.CHILD:
                index = Random.Range(0, child_love.Length);
                AudioSource.PlayClipAtPoint(child_love[index], transform.position);
                break;
            case STAGE.TEEN:
                index = Random.Range(0, teen_love.Length);
                AudioSource.PlayClipAtPoint(teen_love[index], transform.position);
                break;
        }
    }

    public void playSleepAudio()
    {
        int index;
        switch (myStage)
        {
            case STAGE.BABY:
                index = Random.Range(0, baby_sleep.Length);
                AudioSource.PlayClipAtPoint(baby_sleep[index], transform.position);
                break;
            case STAGE.TODDLER:
                index = Random.Range(0, toddler_sleep.Length);
                AudioSource.PlayClipAtPoint(toddler_sleep[index], transform.position);
                break;
            case STAGE.CHILD:
                index = Random.Range(0, child_sleep.Length);
                AudioSource.PlayClipAtPoint(child_sleep[index], transform.position);
                break;
            case STAGE.TEEN:
                index = Random.Range(0, teen_sleep.Length);
                AudioSource.PlayClipAtPoint(teen_sleep[index], transform.position);
                break;
        }
    }

    public void playPickupAudio()
    {
        int index;
        switch (myStage)
        {
            case STAGE.BABY:
                index = Random.Range(0, baby_pickup.Length);
                AudioSource.PlayClipAtPoint(baby_pickup[index], transform.position);
                break;
            case STAGE.TODDLER:
                index = Random.Range(0, toddler_pickup.Length);
                AudioSource.PlayClipAtPoint(toddler_pickup[index], transform.position);
                break;
            case STAGE.CHILD:
                index = Random.Range(0, child_pickup.Length);
                AudioSource.PlayClipAtPoint(child_pickup[index], transform.position);
                break;
            case STAGE.TEEN:
                index = Random.Range(0, teen_pickup.Length);
                AudioSource.PlayClipAtPoint(teen_pickup[index], transform.position);
                break;
        }

    }
    public void playWhineAudio()
    {
        int index;
        switch (myStage)
        {
            case STAGE.BABY:
                index = Random.Range(0, baby_whine.Length);
                AudioSource.PlayClipAtPoint(baby_whine[index], transform.position);
                break;
            case STAGE.TODDLER:
                index = Random.Range(0, toddler_whine.Length);
                AudioSource.PlayClipAtPoint(toddler_whine[index], transform.position);
                break;
            case STAGE.CHILD:
                index = Random.Range(0, child_whine.Length);
                AudioSource.PlayClipAtPoint(child_whine[index], transform.position);
                break;
            case STAGE.TEEN:
                index = Random.Range(0, teen_whine.Length);
                AudioSource.PlayClipAtPoint(teen_whine[index], transform.position);
                break;
        }

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


            myUIStatHealthText.text = "H: " + statHealth + " / " + (statHealth + statHealthFails);
            myUIStatLoveText.text = "L: " + statLove + " / " + (statLove + statLoveFails);
            myUIStatInteligenceText.text = "I: " + statInteligence + " / " + (statInteligence + statInteligenceFails);
        }
        else
        {
            Debug.Log("You gave the child the wrong need!");
        }

    }

    // Trigger a need at time of game day
    public void TriggerNeed(float percentDay)
    {
        int sel;
        if (myNeed == NEEDS.NONE)
        {
            Debug.Log("Triggering" + myStage + " Event at " + percentDay);
            switch (myStage)
            {
                case STAGE.BABY:
                    // Handle BABY Times
                    playWhineAudio();
                    
                    if (percentDay < .25 || percentDay >= .75)
                        myNeed = NEEDS.SLEEP;
                    if(percentDay >= 0.25 && percentDay < 0.75)
                    {
                        sel = Random.Range(0, 3);
                        if (sel == 0)
                            myNeed = NEEDS.FOOD;
                        else if (sel == 1)
                            myNeed = NEEDS.CLEAN;
                        else
                            myNeed = NEEDS.SLEEP;

                    }
                    timeOfNeedStart = Time.time;

                    break;
                case STAGE.TODDLER:
                    // Handle TODDLER Times
                    playWhineAudio();

                    if (percentDay < .3 || percentDay >= .7)
                        myNeed = NEEDS.SLEEP;
                    if (percentDay >= 0.3 && percentDay < 0.7)
                    {
                        sel = Random.Range(0, 3);
                        if (sel == 0)
                            myNeed = NEEDS.FOOD;
                        else if (sel == 1)
                            myNeed = NEEDS.CLEAN;
                        else
                            myNeed = NEEDS.LOVE;

                    }
                    timeOfNeedStart = Time.time;




                    break;
                case STAGE.CHILD:
                    // Handle CHILD Times
                    playWhineAudio();

                    if (percentDay < .3 || percentDay >= .7)
                        myNeed = NEEDS.SLEEP;
                    if (percentDay >= 0.3 && percentDay < 0.4)
                    {
                        sel = Random.Range(0, 4);
                        if (sel == 0)
                            myNeed = NEEDS.FOOD;
                        else if (sel == 1)
                            myNeed = NEEDS.CLEAN;
                        else if (sel == 2)
                            myNeed = NEEDS.EDUCATE;
                        else
                            myNeed = NEEDS.LOVE;

                    }
                    if (percentDay >= 0.4 && percentDay < 0.6)
                    {
                        sel = Random.Range(0, 3);
                        if (sel == 0)
                            myNeed = NEEDS.FOOD;
                        else if (sel == 1)
                            myNeed = NEEDS.LOVE;
                        else if (sel == 2)
                            myNeed = NEEDS.EDUCATE;

                    }
                    if (percentDay >= 0.6 && percentDay < 0.7)
                    {
                        sel = Random.Range(0, 4);
                        if (sel == 0)
                            myNeed = NEEDS.FOOD;
                        else if (sel == 1)
                            myNeed = NEEDS.CLEAN;
                        else if (sel == 2)
                            myNeed = NEEDS.EDUCATE;
                        else
                            myNeed = NEEDS.LOVE;

                    }
                    timeOfNeedStart = Time.time;

                    break;
                case STAGE.TEEN:
                    // Handle TEEN Times
                    playWhineAudio();
                    sel = Random.Range(0, 5);
                    if (sel == 0)
                        myNeed = NEEDS.FOOD;
                    else if (sel == 1)
                        myNeed = NEEDS.CLEAN;
                    else if (sel == 2)
                        myNeed = NEEDS.EDUCATE;
                    else if (sel == 3)
                        myNeed = NEEDS.SLEEP;
                    else
                        myNeed = NEEDS.LOVE;

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

    void MakeMess(ChildController.NEEDS failedNeed)
    {
        GameObject newSplat = Instantiate(splat);
        newSplat.transform.position = this.transform.position;
        newSplat.transform.parent = splatHolder.transform;

        Color col;
        // Failed to meet need!
        switch (myNeed)
        {
            case NEEDS.CLEAN:
                if (ColorUtility.TryParseHtmlString("#894C38", out col))
                    newSplat.GetComponent<SpriteRenderer>().color = col;
                break;
            case NEEDS.FOOD:
                if (ColorUtility.TryParseHtmlString("#93F56E", out col))
                    newSplat.GetComponent<SpriteRenderer>().color = col;
                break;
            case NEEDS.EDUCATE:
                if (ColorUtility.TryParseHtmlString("#93F56E", out col))
                    newSplat.GetComponent<SpriteRenderer>().color = col;
                break;
            case NEEDS.LOVE:
                if (ColorUtility.TryParseHtmlString("#F5746E", out col))
                    newSplat.GetComponent<SpriteRenderer>().color = col;
                break;
            case NEEDS.SLEEP:
                if (ColorUtility.TryParseHtmlString("#F6ED6E", out col))
                    newSplat.GetComponent<SpriteRenderer>().color = col;
                break;
        }

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
                        statHealthFails++;
                        break;
                    case NEEDS.FOOD:
                        statHealthFails++;
                        break;
                    case NEEDS.EDUCATE:
                        statInteligenceFails++;
                        break;
                    case NEEDS.LOVE:
                        statLoveFails++;
                        break;
                    case NEEDS.SLEEP:
                        statHealthFails++;
                        break;
                }
                MakeMess(myNeed);
                myNeed = NEEDS.NONE;
                myUINeedText.text = "NEED: None ";
                myUI.SetUINeed(0);


                myUIStatHealthText.text = "H: " + statHealth + " / " + (statHealth + statHealthFails);
                myUIStatLoveText.text = "L: " + statLove + " / " + (statLove + statLoveFails);
                myUIStatInteligenceText.text = "I: " + statInteligence + " / " + (statInteligence + statInteligenceFails);
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
