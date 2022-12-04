using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrainRoad : MonoBehaviour, IPurchasable
{
    [SerializeField] private List<Transform> railPoints = new List<Transform>();
    public List<Transform> RailPoints => railPoints;

    private Vector3 scale;

    private void Start()
    {
        scale = transform.localScale;
        transform.localScale = Vector3.zero;

        foreach (var railPoint in railPoints)
        {
            railPoint.gameObject.SetActive(false);
        }
    }
    public int GetCost()
    {
        return 0;
    }

    public void GetPurchased()
    {
        transform.DOScale(scale, 0.25f).OnComplete(() =>
        {
            EventManager.OnTrainEvent.Invoke(this);
        });
    }
}
