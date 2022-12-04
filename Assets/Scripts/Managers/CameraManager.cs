using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    private CinemachineVirtualCamera cam;
    private CinemachineComponentBase camComponent;
    private Transform firstTarget;

    private float firstDistance;

    private void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        camComponent = cam.GetCinemachineComponent(CinemachineCore.Stage.Body);
        firstTarget = cam.Follow;
        firstDistance = (camComponent as CinemachineFramingTransposer).m_CameraDistance;
        
    }
    private void OnEnable()
    {
        EventManager.OnDriveEvent.AddListener(ChangeCameraTarget);
        EventManager.OnExitCarEvent.AddListener(ChangeCameraTarget);
    }
    private void ChangeCameraTarget()
    {
        ChangeCameraTarget(firstTarget, firstDistance);
    }

    private void ChangeCameraTarget(Transform transform, float distance)
    {
        cam.Follow = transform;

        if (camComponent is CinemachineFramingTransposer)
        {
            var currentDistance = (camComponent as CinemachineFramingTransposer).m_CameraDistance;

            DOTween.To(() => currentDistance, x => currentDistance = x, distance, 0.25f)
                .OnUpdate(() =>
            {
                (camComponent as CinemachineFramingTransposer).m_CameraDistance = currentDistance;
            });
        }
    }
}
