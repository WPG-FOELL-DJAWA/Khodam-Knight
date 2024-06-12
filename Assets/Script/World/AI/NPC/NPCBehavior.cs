using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCBehavior : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;
    void Start()
    {
        SetDestination();
    }

    void Update()
    {
        float speed = navMeshAgent.velocity.magnitude;
        animator.SetFloat("Speed", speed);


        // Cek apakah NPC sudah mencapai tujuan dengan toleransi jarak tertentu
        float dist = navMeshAgent.remainingDistance;
        if (dist != Mathf.Infinity && navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && dist == 0f)
        {
            StartCoroutine(IdleBeforeNextDestination());
        }
    }

    void SetDestination()
    {
        if (waypoints.Length > 0)
        {
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    IEnumerator IdleBeforeNextDestination()
    {
        // Hentikan agen dan beri waktu idle sejenak
        navMeshAgent.isStopped = true;

        // Tunggu selama beberapa detik sebelum melanjutkan ke tujuan berikutnya
        float idleDuration = 2f; // Ganti dengan durasi idle yang diinginkan
        yield return new WaitForSeconds(idleDuration);

        // Pilih tujuan berikutnya dan lanjutkan perjalanan
        UpdateWaypointIndex();
        navMeshAgent.isStopped = false;
        SetDestination();
    }

    void UpdateWaypointIndex()
    {
        // Jika NPC sudah mencapai waypoint terakhir, kurangi index
        // Jika NPC sudah mencapai waypoint pertama, tambahkan index
        if (currentWaypointIndex == waypoints.Length - 1)
        {
            currentWaypointIndex--;
        }
        else if (currentWaypointIndex == 0)
        {
            currentWaypointIndex++;
        }
    }
}
