using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    bool m_isPlayerInRange;
    public GameEnding gameEnding;

    void OnTriggerEnter (Collider other)
    {
        if (other.transform == player)
        {
            m_isPlayerInRange = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.transform == player)
        {
            m_isPlayerInRange = false;
        }
    }

    void Update ()
    {
        if (m_isPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up; //находим вектор направления
                                                                        //вычитаем из позиции игрока, позицию PointOfView 
                                                                        //и чтобы горгулья направляла на центр персонажа прибавляем одну единицу по y (+ Vector3.up)
            Ray ray = new Ray (transform.position, direction);//создаем луч, начинающийся с позиции горгульи с направлением direction
            RaycastHit rayCastHit;                            //необходимо для записи было ли соприкосновение луча с объектом
            if (Physics.Raycast(ray, out rayCastHit))
            {
                if (rayCastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer ();
                }
            }
        }
    }
}
