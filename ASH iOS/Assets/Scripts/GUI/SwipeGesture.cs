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
            onSwipeUp.Invoke();
            pointerDown = false;
        }

        //swipe down
        if (currentSwipe.y < -requiredSwipeDistance && currentSwipe.x > -requiredSwipeDistance && currentSwipe.x < requiredSwipeDistance)
        {
            onSwipeDown.Invoke();
            pointerDown = false;
        }
    }
}
