using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationController : MonoBehaviour
{
    public bool isEnabled = true;

    GameObject hand;

    // Start is called before the first frame update
    void Start()
    {
        hand = GameObject.Find("Hand");
        if (hand == null)
            Debug.Log("Couldnt find master hand!");
    }

    // Update is called once per frame
    void Update()
    {
        // Highlight when mouse over and child grabbed

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);
        if (hit.collider == this.GetComponent<BoxCollider2D>())
        {
            if (hand.GetComponent<HandController>().isGrabbingChild())
                Debug.Log("Child is over " + this);

        }


    }
}
