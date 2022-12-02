using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Metal : MonoBehaviour, IInteractable, IPoolable
{
    private SpawnPoint spawnPoint;

    public void Pooled()
    {
        var scale = transform.localScale;
        transform.localScale = Vector3.zero;

        transform.DOScale(scale, 0.5f).SetEase(Ease.OutBounce);
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
        GetComponent<Collider>().enabled = false;
        var stackManager = interactor.GetComponent<StackManager>();
        stackManager.CollectMetal(this);
        spawnPoint.SetMetal(null);
    }
    public IEnumerator CollectMovement(Transform stackTransform, List<Transform> splineTransforms, 
        float stackDistance, int stackCount, Transform parent)
    {
        transform.DORotate(stackTransform.eulerAngles, 0.25f);

        var stackPos = stackTransform.position + Vector3.up * stackCount * stackDistance;

        float interpolateAmount = 0;

        Transform splineTransform = splineTransforms[Random.Range(0, splineTransforms.Count)];

        Vector3 a = transform.position;
        Vector3 ab = new Vector3();
        Vector3 bc = new Vector3();

        while (Vector3.Distance(transform.position, stackPos) >= .1f)
        {
            Vector3 b = splineTransform.position;
            Vector3 c = stackPos;

            stackPos = stackTransform.position + Vector3.up * stackCount * stackDistance;

            interpolateAmount = (interpolateAmount + Time.deltaTime) % 1f;
            ab = Vector3.Lerp(a, b, interpolateAmount);
            bc = Vector3.Lerp(b, c, interpolateAmount);
            transform.position = Vector3.Lerp(ab, bc, interpolateAmount);
            yield return null;
        }

        transform.position = stackPos;
        transform.SetParent(parent);
        transform.DOLocalRotate(stackTransform.localEulerAngles, 0.25f);
    }

}
