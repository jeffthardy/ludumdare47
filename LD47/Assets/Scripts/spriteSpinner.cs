using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteSpinner : MonoBehaviour
{

    public float rotationSpeed;
    public float verticalOscHeight;
    public float verticalOscSpeed;

    private Vector3 initialRotation;
    private Vector3 initialPosition;


    private Vector3 currentRotation;
    private Vector3 currentPosition;

    // Start is called before the first frame update
    void Start()
    {

        initialRotation = gameObject.transform.rotation.eulerAngles;
        initialPosition = gameObject.transform.position;
        currentRotation = initialRotation;
        currentPosition = initialPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentRotation += new Vector3(0.0f, 0.0f, rotationSpeed);
        gameObject.transform.eulerAngles = currentRotation;

        float vert = verticalOscHeight * Mathf.Cos(verticalOscSpeed * Time.time);
        float horz = verticalOscHeight * Mathf.Sin(verticalOscSpeed * Time.time);
        currentPosition = initialPosition + new Vector3(horz, vert, 0.0f);

        gameObject.transform.position = currentPosition;

    }
}
