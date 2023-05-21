using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    //#if UNITY_ANDROID
    //    Vector3 FirstPoint;
    //    Vector3 SecondPoint;
    //    float xAngle;
    //    float yAngle;
    //    float xAngleTemp;
    //    float yAngleTemp;

    //    void Start()
    //    {
    //        xAngle = 0;
    //        yAngle = 0;
    //        this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
    //    }

    //    void Update()
    //    {
    //        if (Input.touchCount > 0)
    //        {
    //            if (Input.GetTouch(0).phase == TouchPhase.Began)
    //            {
    //                FirstPoint = Input.GetTouch(0).position;
    //                xAngleTemp = xAngle;
    //                yAngleTemp = yAngle;
    //            }
    //            if (Input.GetTouch(0).phase == TouchPhase.Moved)
    //            {
    //                SecondPoint = Input.GetTouch(0).position;
    //                xAngle = xAngleTemp + (SecondPoint.x - FirstPoint.x) * 180 / Screen.width;
    //                yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;
    //                this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
    //            }
    //        }

    //    }
    //#endif
#if UNITY_EDITOR_WIN
    public Transform target;
    public Vector3 offset;
    public float sensitivity; // чувствительность мышки
    public float limit; // ограничение вращения по Y
    public float zoom; // чувствительность при увеличении, колесиком мышки
    public float zoomMax; // макс. увеличение
    public float zoomMin; // мин. увеличение
    public float zoomCurr; // мин. увеличение
    private float X, Y;

    void Start()
    {
        limit = Mathf.Abs(limit);
        if (limit > 90) limit = 90;

        offset = new Vector3(offset.x, offset.y, -zoomCurr);
        transform.position = target.position + offset;
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0) offset.z += zoom;
            else if (Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= zoom;
            offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));

            X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
            Y += Input.GetAxis("Mouse Y") * sensitivity;
            Y = Mathf.Clamp(Y, -limit, limit);
            transform.localEulerAngles = new Vector3(-Y, X, 0);
            transform.position = transform.localRotation * offset + target.position;
        }
    }
#endif
}
