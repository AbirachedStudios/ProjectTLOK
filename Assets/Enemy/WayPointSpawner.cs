using UnityEngine;
using UnityEngine.AI;

public class WayPointSpawner : MonoBehaviour
{
    public int numberOfWaypoints = 10; // Adjust the desired number of waypoints
    public GameObject waypointPrefab; // Assign your waypoint prefab in the Inspector

    void Start()
    {
        SpawnWaypoints();
    }

    void SpawnWaypoints()
    {
        for (int i = 0; i < numberOfWaypoints; i++)
        {
            // 1. Find a Random Point on the NavMesh
            Vector3 randomPoint;
            if (RandomPoint(out randomPoint))
            {
                // 2. Instantiate Waypoint
                Instantiate(waypointPrefab, randomPoint, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Failed to find a valid NavMesh point for waypoint " + i);
            }
        }
    }

    // Helper function to get a random point on the NavMesh
    bool RandomPoint(out Vector3 point)
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        // Pick the first indice of a random triangle in the nav mesh
        int t = Random.Range(0, navMeshData.indices.Length - 3);

        // Select a random point on it
        Vector3 pointOnMesh = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], Random.value);
        Vector3.Lerp(pointOnMesh, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value);

        NavMeshHit hit;
        
        if (NavMesh.SamplePosition(pointOnMesh, out hit, 1f, NavMesh.GetNavMeshLayerFromName("Ground")))
        {
            point = hit.position;
            return true;
        }

        point = Vector3.zero;
        return false;
    }
}
