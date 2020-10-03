using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildController : MonoBehaviour
{
    // Public Controls
    public float speed = 4;
    public float AngleChangeTime = 1.0f;


    // Basic Child State Keeping
    enum STAGE { BABY, TODDLER, CHILD, TEEN, ADULT};
    enum NEEDS { NONE=0, FOOD, CLEAN, LOVE, SLEEP, EDUCATE};
    enum STATE { IDLE = 0, MOVING, SLEEPING, EATING, HAPPY, SAD, THINKING };

    STAGE myStage;
    NEEDS myNeed;
    STATE myState;


    // Playthrough Stats
    int health;
    int love;
    int inteligence;


    float timeSinceAngleChange;
    float angle;
    float horizontal;
    float vertical;
    Rigidbody2D rigidbody2d;
    Animator animator;
    Random random;



    // Start is called before the first frame update
    void Start()
    {
        myStage = STAGE.BABY;
        myNeed = NEEDS.NONE;
        myState = STATE.MOVING;
        health = 0;
        love = 0;
        inteligence = 0;
        angle = 0;
        timeSinceAngleChange = Time.time;

        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();        
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

    // Trigger a need at time of game day
    public void TriggerNeed(float dayTime)
    {
        if(myNeed == NEEDS.NONE)
        {
            Debug.Log("Triggering" + myStage + " Event at " + dayTime);
            switch (myStage)
            {
                case STAGE.BABY:
                    // Handle BABY Times


                    break;
                case STAGE.TODDLER:
                    // Handle TODDLER Times



                    break;
                case STAGE.CHILD:
                    // Handle CHILD Times



                    break;
                case STAGE.TEEN:
                    // Handle TEEN Times



                    break;
                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {


        if(Time.time - timeSinceAngleChange > AngleChangeTime)
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
