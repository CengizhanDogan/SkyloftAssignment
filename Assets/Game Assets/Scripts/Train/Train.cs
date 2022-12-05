using UnityEngine;
using DG.Tweening;

public class Train : MonoBehaviour, IUnlockable
{
    [SerializeField] private int cost;

    private Vector3 scale;
    private StackManager stackManager;
    public StackManager StackManager => stackManager;

    [SerializeField] private ParticleSystem particle;
    private void Start()
    {
        stackManager = GetComponent<StackManager>();
        scale = transform.localScale;
        transform.localScale = Vector3.zero;
    }
    public int GetCost()
    {
        return cost;
    }

    public void GetUnlocked()
    {
        transform.DOScale(scale, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => particle.Play());
    }
}
