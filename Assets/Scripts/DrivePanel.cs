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
    private void OnEnable()
    {
        EventManager.OnExitCarEvent.AddListener(()=> coll.enabled = true);
    }
    public void Interact(Interactor interactor)
    {
        if (!interactor.TryGetComponent(out Character character)) return;
        coll.enabled = false;

        character.StackManager.TransferMetal(car.StackManager);


        car.SetDriver(character);

        character.Rb.isKinematic = true;
        character.Coll.enabled = false;

        character.transform.DOMove(car.DriveSeat.position, 0.5f)
            .OnComplete(()=> 
            {
                interactor.transform.SetParent(car.DriveSeat);
                EventManager.OnDriveEvent.Invoke(car.transform, 85f);
            });

        character.transform.DORotate(car.DriveSeat.eulerAngles, 0.5f);
        character.transform.DOScale(0, 0.75f);

    }
}
