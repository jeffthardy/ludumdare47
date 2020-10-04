using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMover : MonoBehaviour
{
    public float meowRate = 10.0f;
    public float moveRate = 5;
    public float stallTime = 5;



    float timeSinceMeow;
    float timeSinceStall;
    float angle;
    float horizontal;
    float vertical;
    AudioSource audioSource;
    Rigidbody2D rigidbody2d;
    bool moveEnable = true;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        timeSinceMeow = Time.time;
        timeSinceStall = Time.time;
        angle = Random.Range(0, 360);
        horizontal = Mathf.Sin(Mathf.Deg2Rad * angle);
        vertical = Mathf.Cos(Mathf.Deg2Rad * angle);
    }

    private void FixedUpdate()
    {
        if (moveEnable)
        {
            Vector2 position = transform.position;
            position.x = position.x + moveRate * horizontal * Time.deltaTime;
            position.y = position.y + moveRate * vertical * Time.deltaTime;

            rigidbody2d.MovePosition(position);
        }
        else
            rigidbody2d.MovePosition(transform.position);


    }

    private void Update()
    {
        if (Time.time - timeSinceMeow > meowRate) {
            audioSource.Play();
            timeSinceMeow = Time.time;
        }

        if(Time.time - timeSinceStall > stallTime)
        {
            timeSinceStall = Time.time;
            moveEnable = !moveEnable;
            angle = Random.Range(0, 360);
            horizontal = Mathf.Sin(Mathf.Deg2Rad * angle);
            vertical = Mathf.Cos(Mathf.Deg2Rad * angle);
        }
    }
}
