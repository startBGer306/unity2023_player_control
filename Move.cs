using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
public class Move : MonoBehaviour
{
    private Transform m_tran;
    private Transform m_body;
    public float zoom;
    void Start()
    {
        m_tran=GetComponent<Transform>();
        m_body =GetComponentInParent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {

        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        Vector3 vir = new Vector3(Horizontal, 0, Vertical);
        if(Horizontal != 0 || Vertical != 0)
        {
            if (vir != Vector3.zero)
            {
                m_body.Translate(vir * Time.deltaTime * zoom);
            }
        }
        ///summary
        ///此处的代码用于开启角色的旁观者模式
        ///summary
        if (Input.GetMouseButton(1))
        {
            if (Input.GetKey(KeyCode.E))
            {
                m_body.Translate(Vector3.up * Time.deltaTime * zoom);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                m_body.Translate(Vector3.down * Time.deltaTime * zoom);
            }
        }
        float mousex = Input.GetAxis("Mouse X");
        if (mousex != 0)
        {
            m_body.Rotate(Vector3.up, mousex * 200 * Time.deltaTime);
        }
        float mousey = Input.GetAxis("Mouse Y");
        if (mousey != 0)
        {
            m_tran.Rotate(Vector3.left, mousey * 200 * Time.deltaTime);
        }
        if (Vector3.Angle(m_body.forward, m_tran.forward) > 80)
        {
            m_tran.Rotate(Vector3.left, -mousey * 200 * Time.deltaTime);
        }
    }
}
