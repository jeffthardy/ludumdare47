using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Sprite openHand;
    public Sprite closedHand;
    public GameObject child;

    SpriteRenderer spriteRenderer;

    bool objectIsBeingDragged;
    GameObject objectBeingDragged;
    Vector2 objectScreenSpace;
    Vector2 objectOffset;

    public bool isGrabbingChild()
    {
        if (objectIsBeingDragged && objectBeingDragged == child)
            return true;
        else
            return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = openHand;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = pos;
               

        // Handle mouse clicks similar to touches
        if (Input.GetMouseButtonDown(0))
        {
            spriteRenderer.sprite = closedHand;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);
            if (hit.collider != null)
            {
                if ((hit.collider.gameObject.GetComponent<Draggable>() != null) && hit.collider.gameObject.GetComponent<Draggable>().isDragable)
                {
                    objectIsBeingDragged = true;
                    objectBeingDragged = hit.collider.gameObject;
                    objectScreenSpace = Camera.main.WorldToScreenPoint(objectBeingDragged.transform.position);
                    objectOffset = objectBeingDragged.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
                    Debug.Log("Dragging " + objectBeingDragged);
                }
                else
                    spriteRenderer.sprite = openHand;
            }
            else
                spriteRenderer.sprite = openHand;
        }
        // Handle mouse clicks similar to touches
        if (Input.GetMouseButtonUp(0))
        {
            if (objectIsBeingDragged)
            {
                Debug.Log("Stopped Dragging " + objectBeingDragged);
                objectIsBeingDragged = false;
                objectBeingDragged = null;
            }
            spriteRenderer.sprite = openHand;
        }
        // Disable dragging if object was destroyed 
        if ((objectBeingDragged == null) && objectIsBeingDragged)
        {
            objectIsBeingDragged = false;
        }
        //If object is being dragged, update with mouse
        if (objectIsBeingDragged)
        {
            //convert the screen mouse position to world point and adjust with offset
            var curPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + objectOffset;

            //update the position of the object in the world
            objectBeingDragged.transform.position = curPosition;

        }
    }
}
