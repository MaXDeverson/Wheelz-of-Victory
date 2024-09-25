using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveLogic : MonoBehaviour
{
    public List<Transform> waypoints;
    public float speed = 5f;
    private int currentWaypointIndex = 0;
    bool move = true;
    void Update()
    {
        if (move)
        {
            if (waypoints.Count == 0) return;

            Transform targetWaypoint = waypoints[currentWaypointIndex];
            Vector3 direction = targetWaypoint.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (direction.magnitude <= distanceThisFrame)
            {
                // Arrived at waypoint
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
            }
            else
            {
                // Move towards waypoint
                transform.Translate(direction.normalized * distanceThisFrame, Space.World);
            }
        }
       
    }
    public void Move(bool isMove)
    {
        move = isMove;
    }
    public void SetPath(List<Transform> path)
    {
        waypoints = path;
    }
}