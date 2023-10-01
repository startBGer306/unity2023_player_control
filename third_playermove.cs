using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class third_playermove : MonoBehaviour
{
    private float Horizontal;
    public third_camrea forangle;
    private float Vertical;
    private Vector3 walkto3;
    private 计时器 timer;
    private Rigidbody Rigidbody;
    public Transform bodytransform;
    public Slider HP;
    public bool IsJumping;
    public bool IsGround;
    public float JumpForce;
    public float deltatime;
    public float JumpTime;
    public float speed;
    public float run;
    public float walk;
    public float zoom;
   
    private void Move()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        if (Horizontal > 0 || Vertical > 0)
        {
            transform.Translate(bodytransform.forward * Time.deltaTime * speed);
        }
        if(Horizontal < 0 || Vertical < 0)
        {
            transform.Translate(-bodytransform.forward* Time.deltaTime * speed);
        }
        //if(Horizontal != 0 || Vertical != 0)
        //{
        //    bodytransform.forward = Mathf.LerpAngle(bodytransform,walkto3,0.1f);
        //}
        //跳跃控制代码
        if (Input.GetKeyDown(KeyCode.Space) && IsGround == true && deltatime - JumpTime >= 1.5f)
        {
            JumpTime = timer.TimeOfNew();
            Rigidbody.AddForce(Vector3.up * JumpForce);
        }
        //是否发生跑动
        if (IsGround == true && Input.GetKey(KeyCode.LeftShift))
        {
            speed = 12;
        }
        //是否在地面（方便以后出现缓降效果，判断是否处于空中
        if (IsGround == true && Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = Mathf.Lerp(run,walk, 0.5f);
        }

        if (IsGround == false)
        {
            IsJumping = true;
            speed =4;
        }

        if (IsGround == true)
        {
            IsJumping = false;
            speed = walk; 
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 12;
            }
        }
    }
    private void Findtheangleforbody()
    {
        walkto3 = forangle.player.position - forangle.CameraSPostion.position;
        walkto3 = new Vector3(walkto3.x, 0f, walkto3.z);
    }   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            HP.value -= 2;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            IsGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            IsGround = false;
        }
    }
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        bodytransform=GetComponent<Transform>();
        timer = GetComponent<计时器>();
        HP.value = 20;
    }
    private void Update()
    {
        Move();
        Findtheangleforbody();
        deltatime += Time.deltaTime;
    }
}