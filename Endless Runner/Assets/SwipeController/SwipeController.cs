using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public static SwipeController instance;
    public bool swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;
    private bool isMobile = false;

    private void Awake()
    {
        instance = this;   
    }

    private void Start()
    {
        isMobile = Application.isMobilePlatform;
    }

    private void Update()
    {
        swipeDown = swipeUp = swipeLeft = swipeRight = false;
        #region PC
        if (!isMobile)
        {
            if (Input.GetMouseButtonDown(0))
            {
            
                isDraging = true;
                startTouch = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                CalculateSwipeDelta(startTouch, isMobile);//Просчитать дистанцию
            }
        }
        #endregion
        #region Mobile
        else
        {
            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {

                    isDraging = true;
                    startTouch = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    CalculateSwipeDelta(startTouch, isMobile);//Просчитать дистанцию
                }
            }
        }
        #endregion
    }

    void CalculateSwipeDelta(Vector2 start, bool isMobile)
    {
        if (isMobile)
            swipeDelta = Direction(startTouch, Input.touches[0].position);
        else
            swipeDelta = Direction(startTouch, (Vector2)Input.mousePosition);

        //Проверка на пройденность расстояния
        //Debug.Log(swipeDelta.magnitude);
        if (swipeDelta.magnitude > 200)
        {
            //Определение направления
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            //if (Mathf.Abs(x) > Mathf.Abs(y))
            {

                if (x < -80)
                    swipeLeft = true;
                else if (x > 80)
                    swipeRight = true;
            }
            //else
            {

                if (y < -80)
                    swipeDown = true;
                else if (y > 80)
                    swipeUp = true;
            }

            Reset();
        }
    }

    Vector2 Direction(Vector2 start, Vector2 end)
    {
        return end - start;
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
}
