using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class third_camrea : MonoBehaviour
{
    private float MouseY;
    private float MouseX;
    private Transform pointofview;
    public Transform CameraSPostion;
    public Transform player;
    public float zoom;
    private float mousewheel;
    private Vector3 vector3;
    public bool isMoving=false;
    public float moveSpeed=5;

    private float disction;

    void Start()
    {
       pointofview=GetComponent<Transform>();
    }
    /// <summary>
    /// 用于控制镜头的旋转
    /// </summary>
    private void CameraMove()
    {
       
        mousewheel = Input.GetAxis("Mouse ScrollWheel");
        if (mousewheel != 0)
        {
            disction = Vector3.Distance(pointofview.position, CameraSPostion.position);
            vector3 = -(pointofview.position - CameraSPostion.position).normalized;
            if (mousewheel != 0)
            {
                disction -= mousewheel;
            }
            disction = Mathf.Clamp(disction, 2, 4.8f);
            CameraSPostion.position = player.position + disction * vector3;
        }
        MouseX += Input.GetAxis("Mouse X") * zoom;
        MouseY += Input.GetAxis("Mouse Y") * zoom;
        MouseY = Mathf.Clamp(MouseY, -70, 70);
        pointofview.rotation= Quaternion.Euler(-MouseY, MouseX, 0);
        CameraSPostion.LookAt(pointofview.position);
    }
    /// <summary>
    /// 用于控制使镜头一直跟随player
    /// </summary>
    /// <returns></returns>
    private IEnumerator SmoothMove()
    {
        isMoving = true;
        float t = 0f;

        while (t <= 1f)
        {
            t += Time.deltaTime*0.001f;
            transform.position = Vector3.Lerp(pointofview.position, player.position, t);
            yield return null;
        }
        isMoving = false;
    }
    // Update is called once per frame
    void Update()
    {
        CameraMove();
        if (Vector3.Distance(pointofview.position,player.position)>0.2f)
        {
            StartCoroutine(SmoothMove());   
        }
    }
}
