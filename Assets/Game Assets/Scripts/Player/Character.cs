using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    private Vector3 scale;
    private Collider coll;
    public Collider Coll => coll;
    private Rigidbody rb;
    public Rigidbody Rb => rb;
    private StackManager stackManager;
    public StackManager StackManager => stackManager;

    private void Start()
    {
        coll = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        stackManager = GetComponent<StackManager>();

        scale = transform.localScale;
    }
    private void OnEnable()
    {
        EventManager.OnExitVehicleEvent.AddListener(EnableCharacter);
    }

    private void EnableCharacter()
    {
        transform.SetParent(null);
        transform.DOScale(scale, 0.5f).OnComplete(() =>
        {
            coll.enabled = true;
            rb.isKinematic = false;
        });
    }
}
