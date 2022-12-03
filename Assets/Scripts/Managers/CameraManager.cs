using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    private CinemachineVirtualCamera cam;
    private void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }
    private void OnEnable()
    {
        EventManager.OnDriveEvent.AddListener(ChangeCameraTarget);
    }
    private void OnDisable()
    {
        EventManager.OnDriveEvent.RemoveListener(ChangeCameraTarget);
    }
    private void ChangeCameraTarget(Transform transform, float distance)
    {
        cam.Follow = transform;
        CinemachineComponentBase componentBase = cam.GetCinemachineComponent(CinemachineCore.Stage.Body);
        if (componentBase is CinemachineFramingTransposer)
        {
            var currentDistance = (componentBase as CinemachineFramingTransposer).m_CameraDistance;

            DOTween.To(() => currentDistance, x => currentDistance = x, distance, 0.25f)
                .OnUpdate(() =>
            {
                (componentBase as CinemachineFramingTransposer).m_CameraDistance = currentDistance;
            });
        }
    }
}
