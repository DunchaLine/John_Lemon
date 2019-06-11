using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f; //скорость поворота персонажа

    Vector3 m_Movement; 
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Quaternion m_Rotation = Quaternion.identity;
    AudioSource m_AudioSource;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);//проверяем двигается ли персонаж по оси х
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f); //проверяем двигается ли персонаж по оси z
        bool isWalking = hasHorizontalInput || hasVerticalInput; //сравниваем два bool

        m_Animator.SetBool("isWalking", isWalking); //записываем значение полученного bool  в параметры аниматора

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else 
        {
            m_AudioSource.Stop();
        }

         Vector3 desiredForward = Vector3.RotateTowards(
             transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f); //transform.forward - изначальное направление куда смотрит персонад
                                                                             //m_Movement - куда он должен посмотреть
                                                                             //это записывается в Vector3, после чего
         m_Rotation = Quaternion.LookRotation(desiredForward);               //персонаж поворачивается относительно того какие клавиши нажал пользователь
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(
            m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
