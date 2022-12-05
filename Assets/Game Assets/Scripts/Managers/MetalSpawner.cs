using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalSpawner : MonoBehaviour
{
    private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    private PoolingManager poolingManager;

    [SerializeField] private int spawnCount = 100;
    [SerializeField] private Vector3 firstPosition = new Vector3(-50, 2, 15);
    [SerializeField] private float xSpacing = 10f;
    [SerializeField] private float zSpacing = 10f;
    [SerializeField] private int line = 10;
    [SerializeField] private float spawnTimer;

    private WaitForSeconds spawnTime;

    private void Start()
    {
        poolingManager = PoolingManager.Instance;
        spawnTime = new WaitForSeconds(spawnTimer);

        SetPositions();
    }

    private void SetPositions()
    {
        //Creates spawn points by given values in the editor.
        var xMultiplier = 0f; var zMultiplier = 0f;
        for (int i = 1; i <= spawnCount; i++)
        {
            var offset = new Vector3(xSpacing * xMultiplier, 0, zSpacing * zMultiplier);
            var spawnPos = firstPosition + offset;

            spawnPoints.Add(new SpawnPoint(spawnPos));

            xMultiplier++;

            //If created point count is equal to a multiple of line points changes the column.
            if (i % line == 0)
            {
                zMultiplier++;
                xMultiplier = 0f;
            }
        }

        SpawnMetals();
    }

    private void SpawnMetals()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            var spawnPoint = spawnPoints[i];

            var randomOffset = new Vector3(RandomValue(xSpacing), 0, RandomValue(zSpacing));

            var spawnPos = spawnPoint.Position + randomOffset;

            var metalGameObject = poolingManager.InstantiateFromPool("Metal", spawnPos, Quaternion.identity);
            var metal = metalGameObject.GetComponent<Metal>();
            spawnPoint.SetMetal(metal);
            metal.SetSpawnPoint(spawnPoint);
        }

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        SpawnPoint emptyPoint = null;
        while (true)
        {
            //Checks if spawn point has a metal. If not spawns metal at point after given time.
            if (emptyPoint == null)
            {
                foreach (var spawnPoint in spawnPoints)
                {
                    if (spawnPoint.GetMetal()) continue;

                    emptyPoint = spawnPoint;
                }
            }
            else
            {
                var spawnPos = emptyPoint.Position + new Vector3(RandomValue(xSpacing), 0, RandomValue(zSpacing));

                var metalGameObject = poolingManager.InstantiateFromPool("Metal", spawnPos, Quaternion.identity);
                var metal = metalGameObject.GetComponent<Metal>();
                emptyPoint.SetMetal(metal);
                metal.SetSpawnPoint(emptyPoint);
                emptyPoint = null;
            }

            yield return spawnTime;
        }
    }

    private float RandomValue(float randomBase)
    {
        return Random.Range(-(randomBase / 2), (randomBase / 2));
    }
}

public class SpawnPoint
{
    private Vector3 position;
    public Vector3 Position => position;

    private Metal metal;

    public SpawnPoint(Vector3 position)
    {
        this.position = position;
    }

    public void SetMetal(Metal metal)
    {
        this.metal = metal;
    }
    public Metal GetMetal()
    {
        return metal;
    }
}
