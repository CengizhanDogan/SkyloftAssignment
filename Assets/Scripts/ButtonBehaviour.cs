using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonBehaviour : MonoBehaviour
{
    private Button button;
    private Vector3 scale;
    private void Start()
    {
        scale = transform.localScale;
        button = GetComponent<Button>();
        button.onClick.AddListener(ButtonPress);
        DisableButton(true);
    }

    private void OnEnable()
    {
        EventManager.OnDriveEvent.AddListener(EnableButton);
    }

    private void EnableButton(Transform t, float f)
    {
        transform.DOScale(scale, 0.5f).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            button.interactable = true;
        });
    }
    private void DisableButton(bool isStart)
    {
        button.interactable = false;
        if (isStart) transform.localScale = Vector3.zero;
        else transform.DOScale(Vector3.zero, 0.5f);
    }

    private void ButtonPress()
    {
        EventManager.OnExitCarEvent.Invoke();
        DisableButton(false);
    }
}
