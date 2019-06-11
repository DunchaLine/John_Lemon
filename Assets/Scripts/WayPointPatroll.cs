using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WayPointPatroll : MonoBehaviour
{

    public NavMeshAgent navMeshAgent;
    public Transform[] wayPoints;
    int m_CurrentWayPointIndex;

    void Start()
    {
        navMeshAgent.SetDestination(wayPoints[0].position);
    }

    void Update()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance) //если оставшееся расстояние до точки < stopping distance (
                                                                            //указано в инспекторе)
            m_CurrentWayPointIndex = (m_CurrentWayPointIndex + 1) % wayPoints.Length; //прибавляем индекс и остаток от деления - 
                                                                                     //следующая точка к которой призрак будет идти
            navMeshAgent.SetDestination(wayPoints[m_CurrentWayPointIndex].position);
        {

        }
    }
}
