using UnityEngine;

public class LookCameraBar : MonoBehaviour
{

    void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(Vector3.up*180.0f);
    }

}