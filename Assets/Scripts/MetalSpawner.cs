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
        var xMultiplier = 0f; var zMultiplier = 0f;
        for (int i = 1; i <= spawnCount; i++)
        {
            var offset = new Vector3(xSpacing * xMultiplier, 0, zSpacing * zMultiplier);
            var spawnPos = firstPosition + offset;

            spawnPoints.Add(new SpawnPoint(spawnPos));

            xMultiplier++;

            if (i % 10 == 0)
            {
                zMultiplier++;
                xMultiplier = 0f;
            }
        }

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            while (spawnPoint.GetMetal())
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            }

            var spawnPos = spawnPoint.Position + new Vector3(RandomValue(xSpacing), 0, RandomValue(zSpacing));

            var metalGameObject = poolingManager.SpawnFromPool("Metal", spawnPos, Quaternion.identity);
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
            if (emptyPoint == null)
            {
                foreach (var spawnPoint in spawnPoints)
                {
                    if (spawnPoint.GetMetal()) continue;

                    emptyPoint = spawnPoint;
                    yield return spawnTime;
                }
            }
            else
            {
                var spawnPos = emptyPoint.Position + new Vector3(RandomValue(xSpacing), 0, RandomValue(zSpacing));

                var metalGameObject = poolingManager.SpawnFromPool("Metal", spawnPos, Quaternion.identity);
                var metal = metalGameObject.GetComponent<Metal>();
                emptyPoint.SetMetal(metal);
                metal.SetSpawnPoint(emptyPoint);
                emptyPoint = null;
            }

            yield return null;
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
