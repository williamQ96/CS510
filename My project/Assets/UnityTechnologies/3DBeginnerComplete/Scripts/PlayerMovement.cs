using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    public InputAction MoveAction;


    public float turnSpeed = 20f;

    //SPRINT RELATED
    public InputAction SprintAction;
    public float normalSpeed = 1f;
    public float sprintMultiplier = 1.5f;
    public ParticleSystem sprintEffect;
    //

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    void Start ()
    {
        m_Animator = GetComponent<Animator> ();
        m_Rigidbody = GetComponent<Rigidbody> ();
        m_AudioSource = GetComponent<AudioSource> ();
        //clones
        MoveAction = MoveAction.Clone();
        SprintAction = SprintAction.Clone();
        
        MoveAction.Enable();
        //SPRINT RELATED
        SprintAction.Enable();


        if (sprintEffect != null)
            sprintEffect.Stop();
        //
    }

    void FixedUpdate ()
    {
        var pos = MoveAction.ReadValue<Vector2>();
        //SPRINT RELATED
        bool isSprinting = SprintAction.IsPressed();
            //debug
        Debug.Log("Sprinting: " + isSprinting);

        float speed = normalSpeed * (isSprinting ? sprintMultiplier : 1f);
        //
        float horizontal = pos.x;
        float vertical = pos.y;
        
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize ();
        m_Movement *= speed;
    
        bool hasHorizontalInput = !Mathf.Approximately (horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately (vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool ("IsWalking", isWalking);
        
        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop ();
        }

        Vector3 desiredForward = Vector3.RotateTowards (transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation (desiredForward);
        
        // particle control
        if (sprintEffect != null)
        {
            if (isSprinting && !sprintEffect.isPlaying)
                sprintEffect.Play();
            else if (!isSprinting && sprintEffect.isPlaying)
                sprintEffect.Stop();
        }
    }

    void OnAnimatorMove ()
    {
        m_Rigidbody.MovePosition (m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation (m_Rotation);
    }
    
    void Update()
{
    if (Keyboard.current.leftShiftKey.isPressed)
    {
        Debug.Log(">>> Left Shift is being held down!");
    }
}
}