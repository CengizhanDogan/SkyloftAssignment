using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Metal : MonoBehaviour, IInteractable, IPoolable
{
    private SpawnPoint spawnPoint;

    [SerializeField] private Collider coll;
    private Vector3 scale;

    private bool getOnce;
    public void Pooled()
    {
        if (!getOnce)
        {
            getOnce = true;
            coll = GetComponent<Collider>();
            scale = transform.localScale;
        }

        transform.localScale = Vector3.zero;

        transform.DOScale(scale, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => coll.enabled = true);
    }
    public void SetSpawnPoint(SpawnPoint spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }
    public SpawnPoint GetSpawnPoint()
    {
        return spawnPoint;
    }
    public void Interact(Interactor interactor)
    {
        var stackManager = interactor.GetComponent<StackManager>();
        if (stackManager.StackIsFull) return;
        stackManager.CollectMetal(this);
        spawnPoint.SetMetal(null);
        coll.enabled = false;
    }
    public IEnumerator MetalMovement(Transform lastTransform, List<Transform> splineTransforms,
        float stackDistance, int stackCount, Transform parent, float stackSpeed,bool isSpend)
    {
        transform.DORotate(lastTransform.eulerAngles, 0.25f);

        var lastPos = lastTransform.position + Vector3.up * stackCount * stackDistance;
        if (isSpend) lastPos = lastTransform.position;

        float interpolateAmount = 0;

        Transform splineTransform = splineTransforms[Random.Range(0, splineTransforms.Count)];

        Vector3 a = transform.position;

        while (interpolateAmount < 0.975)
        {
            Vector3 b = splineTransform.position;
            Vector3 c = lastPos;

            lastPos = lastTransform.position + Vector3.up * stackCount * stackDistance;
            if (isSpend) lastPos = lastTransform.position;

            interpolateAmount = (interpolateAmount + Time.deltaTime * stackSpeed) % 1f;
            Vector3 ab = Vector3.Lerp(a, b, interpolateAmount);
            Vector3 bc = Vector3.Lerp(b, c, interpolateAmount);
            transform.position = Vector3.Lerp(ab, bc, interpolateAmount);
            yield return null;
        }

        PoolingManager poolingManager = PoolingManager.Instance;

        transform.position = lastPos;
        if (!isSpend) transform.SetParent(parent);
        else transform.SetParent(poolingManager.transform);
        if (!isSpend) transform.DOLocalRotate(lastTransform.localEulerAngles, 0.25f);
        else transform.DOScale(0, 0.25f).OnComplete(() => poolingManager.DestroyPoolObject(gameObject));
    }
}
