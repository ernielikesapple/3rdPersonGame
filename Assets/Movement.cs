using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;


    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;
    Vector3 velocity;
    public float gravity = -9.81f;
    public float jumpHeight = 5f;

    public float speed = 3.0f;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Animator animator;

    public bool faceToTarget;

    void Start()
    {
        
    }
    Vector3 targetPoint;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -15f;
        }
        velocity.y += gravity * 1.8f * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //Debug.DrawLine(ray.origin, hit.point, Color.red);
                targetPoint = hit.point;
                
            }
        }
        if (targetPoint != null)
        {
            Vector3 direction = new Vector3(targetPoint.x - transform.position.x, 0, targetPoint.z - transform.position.z);
            

            if (direction.magnitude > 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);//转换面向更加缓慢平滑
                transform.rotation = Quaternion.Euler(0f, angle, 0f);//绕Y轴旋转
                var next = direction.normalized * speed * Time.deltaTime;
                controller.Move(next);
                animator.SetBool("moving", true);
            }
            else
            {
                animator.SetBool("moving", false);
            }
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                animator.SetBool("running", true);

                speed = 5f;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                animator.SetBool("running", false);

                speed = 3f;
            }
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
        }

    }
}
