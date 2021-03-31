using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SwipeGesture : MonoBehaviour
{
    private const float requiredSwipeDistance = 350f;

    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    public UnityEvent onPointerDown;
    public UnityEvent onPointerUp;

    public UnityEvent onSwipeDown;
    public UnityEvent onSwipeUp;
    public UnityEvent onSwipeRight;
    public UnityEvent onSwipeLeft;

    private bool pointerDown;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            pointerDown = true;
            onPointerDown.Invoke();
        }

        if (Input.GetMouseButtonUp(0))
        {
            pointerDown = false;
            onPointerUp.Invoke();
        }

        if (pointerDown)
        {
            Swipe();
        }
    }

    private void Swipe()
    {

        //save ended touch 2d point
        secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        //create vector from the two points
        currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
        
        //swipe upwards
        if (currentSwipe.y > requiredSwipeDistance && currentSwipe.x > -requiredSwipeDistance && currentSwipe.x < requiredSwipeDistance)
        {
            //Copy();
            onSwipeUp.Invoke();
            pointerDown = false;

            //active = true;
            Debug.Log("up swipe");
        }

        //swipe down
        if (currentSwipe.y < -requiredSwipeDistance && currentSwipe.x > -requiredSwipeDistance && currentSwipe.x < requiredSwipeDistance)
        {
            //Paste();
            onSwipeDown.Invoke();
            pointerDown = false;

            //active = true;
            Debug.Log("down swipe");
        }

        //swipe left
        if (currentSwipe.x < -requiredSwipeDistance && currentSwipe.y > -requiredSwipeDistance && currentSwipe.y < requiredSwipeDistance)
        {

            onSwipeLeft.Invoke();
            pointerDown = false;

            Debug.Log("left swipe");
        }

        //swipe right
        if (currentSwipe.x > requiredSwipeDistance && currentSwipe.y > -requiredSwipeDistance && currentSwipe.y < requiredSwipeDistance)
        {
            onSwipeRight.Invoke();
            pointerDown = false;

            Debug.Log("right swipe");
        }
        
    }
}
