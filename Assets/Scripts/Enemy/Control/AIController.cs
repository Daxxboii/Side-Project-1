using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        NavMeshAgent Brain;
        private void Awake()
        {
            Brain = GetComponent<NavMeshAgent>();
        }
        private void Update()
        {
            var player = GameObject.FindWithTag("Player");
            if (DistanceToPlayer(player) < chaseDistance)
            {
                Brain.SetDestination(player.transform.position);
            }
            else if(DistanceToPlayer(player) > chaseDistance)
            {
                Brain.SetDestination(transform.position);
            }
        }

        private float DistanceToPlayer(GameObject player)
        {
            return Vector3.Distance(player.transform.position, transform.position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}

