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
        if(getOnce) coll.enabled = false;
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
        float stackDistance, int stackCount, Transform parent, float stackSpeed, bool isSpend)
    {
        //Metal movement from 3 points using spline.
        transform.DORotate(lastTransform.eulerAngles, 0.25f);

        float interpolateAmount = 0;

        Transform splineTransform = splineTransforms[Random.Range(0, splineTransforms.Count)];

        Vector3 a = transform.position;

        while (interpolateAmount < 0.9)
        {
            Vector3 b = splineTransform.position;
            Vector3 c = StackPosition(lastTransform, stackCount, stackDistance, isSpend);

            interpolateAmount = (interpolateAmount + Time.deltaTime * stackSpeed) % 1f;
            //Creates and lerps spline points.
            Vector3 ab = Vector3.Lerp(a, b, interpolateAmount);
            Vector3 bc = Vector3.Lerp(b, c, interpolateAmount);
            transform.position = Vector3.Lerp(ab, bc, interpolateAmount);
            yield return null;
        }

        PoolingManager poolingManager = PoolingManager.Instance;

        //If metal is spend it shrinks and destroys itself after that.
        transform.position = StackPosition(lastTransform, stackCount, stackDistance, isSpend);
        if (!isSpend) transform.SetParent(parent);
        else transform.SetParent(poolingManager.transform);
        if (!isSpend) transform.DOLocalRotate(lastTransform.localEulerAngles, 0.25f);
        else transform.DOScale(0, 0.25f).OnComplete(() => poolingManager.DestroyPoolObject("Metal", gameObject));
    }

    private Vector3 StackPosition(Transform lastTransform, int stackCount, float stackDistance, bool isSpend)
    {
        //Finds how to stack from how many metals is in the stack.
        var upTransform = lastTransform.GetChild(0);
        float zStack = 0f;

        var lastPos = lastTransform.position + upTransform.up * stackCount * stackDistance;
        if (isSpend) lastPos = lastTransform.position;
        else if (stackCount >= 5)
        {
            zStack = Mathf.Floor((stackCount - 1) / 5f);

            lastPos = lastTransform.position + upTransform.up * (stackCount - (zStack * 5)) * stackDistance - lastTransform.forward * zStack * stackDistance;
        }
        if (stackCount >= 50)
        {
            var xStack = Mathf.Floor((stackCount - 1) / 50f);

            lastPos = lastTransform.position + upTransform.up * (stackCount - (zStack * 5)) * stackDistance -
                lastTransform.forward * (zStack - (xStack * 10)) * stackDistance +
                upTransform.forward * xStack * stackDistance * 1.5f;
        }

        return lastPos;
    }
}
