using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class LineNavigation : MonoBehaviour
{
    public static LineNavigation instance;
    [SerializeField] private Transform Player;
    [SerializeField] private LineRenderer Path;
    [SerializeField] private float PathHeightOffset = 1.25f;
    [SerializeField] private float SpawnHeightOffset = 1.5f;
    [SerializeField] private float PathUpdateSpeed = 0.25f;
    private bool _navigationIsActive;
    public bool navigationIsActive { get { return _navigationIsActive; } }

    private Coroutine DrawPathCoroutine;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void startDrawPathToTarget(GameObject target)
    {
        _navigationIsActive = true;
        DrawPathCoroutine = StartCoroutine(drawPath(target));
    }

    public void stopDrawPathToTarget()
    {
        _navigationIsActive = false;

        if (DrawPathCoroutine != null)
            StopCoroutine(DrawPathCoroutine);
        Path.positionCount = 0;
    }

    private IEnumerator drawPath(GameObject target)
    {
        NavMeshPath path = new NavMeshPath();

        while (target != null)
        {
            if (NavMesh.CalculatePath(Player.position, target.transform.position, NavMesh.AllAreas, path))
            {
                Path.positionCount = path.corners.Length;

                for (int i = 0; i < path.corners.Length; i++)
                {
                    Path.SetPosition(i, path.corners[i] + Vector3.up * PathHeightOffset);
                }
            }
            else
            {
                Debug.LogError($"Unable to calculate a path on the NavMesh between {Player.position} and {target.transform.position}!");
            }
            yield return new WaitForSeconds(PathUpdateSpeed);
        }
    }
}