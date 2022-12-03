using UnityEngine;
using DG.Tweening;

public class DrivePanel : MonoBehaviour, IPurchasable, IInteractable
{
    private Vector3 scale;
    [SerializeField] private Car car;

    private Collider coll;
    private void Start()
    {
        scale = transform.localScale;
        coll = GetComponent<Collider>();

        transform.localScale = Vector3.zero;
    }
    public int GetCost()
    {
        return 0;
    }

    public void GetPurchased()
    {
        transform.DOScale(scale, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => coll.enabled = true); ;
    }

    public void Interact(Interactor interactor)
    {
        coll.enabled = false;

        EventManager.OnDriveEvent.Invoke(car.transform, 75f);

        interactor.GetComponent<Collider>().enabled = false;

        interactor.transform.DOMove(car.DriveSeat.position, 0.5f);
        interactor.transform.DORotate(car.DriveSeat.eulerAngles, 0.5f);
        interactor.transform.DOScale(0, 0.75f);
    }
}
