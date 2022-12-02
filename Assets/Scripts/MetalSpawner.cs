using System.Collections.Generic;
using UnityEngine;

public class MetalSpawner : MonoBehaviour
{
    private List<SpawnPosition> spawnPositions = new List<SpawnPosition>();

    private PoolingManager poolingManager;

    [SerializeField] private Vector3 firstPosition = new Vector3(-50, 2, 15);
    [SerializeField] private float xSpacing = 10f;
    [SerializeField] private float zSpacing = 10f;
    [SerializeField] private float spawnTimer;

    private void Start()
    {
        poolingManager = PoolingManager.Instance;

        SetPositions();
    }

    private void SetPositions()
    {
        var xMultiplier = 0f; var zMultiplier = 0f;
        for (int i = 1; i <= 100; i++)
        {
            var offset = new Vector3(xSpacing * xMultiplier, 0, zSpacing * zMultiplier);
            var spawnPos = firstPosition + offset;

            spawnPositions.Add(new SpawnPosition(spawnPos));

            xMultiplier++;

            if (i % 10 == 0)
            {
                zMultiplier++;
                xMultiplier = 0f;
            }
        }

        for (int i = 0; i < spawnPositions.Count / 4; i++)
        {
            var spawnPos = spawnPositions[Random.Range(0, spawnPositions.Count)];
            while (spawnPos.GetMetal())
            {
                spawnPos = spawnPositions[Random.Range(0, spawnPositions.Count)];
            }
            var metal = poolingManager.SpawnFromPool("Metal", spawnPos.Position, Quaternion.identity);
            spawnPos.SetMetal(metal.GetComponent<Metal>());
        }
    }

    private float RandomValue(float randomBase)
    {
        return Random.Range(-(randomBase / 2), (randomBase / 2));
    }
}

public class SpawnPosition
{
    private Vector3 position;
    public Vector3 Position => position;

    private Metal metal;

    public SpawnPosition(Vector3 position)
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
