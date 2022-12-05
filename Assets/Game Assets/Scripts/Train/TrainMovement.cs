using UnityEngine;
using System;
using System.Collections.Generic;
using DG.Tweening;

public class TrainMovement : MonoBehaviour
{
    [SerializeField] private float trainSpeed;
    private List<Transform> rails;

    private int railPoint;

    [SerializeField] private List<Transform> wheels;

    private void OnEnable()
    {
        EventManager.OnTrainEvent.AddListener(GetRails);
    }

    private void GetRails(TrainRoad trainRoad)
    {
        rails = trainRoad.RailPoints;
        MoveOnTrain();
    }

    private void MoveOnTrain()
    {
        transform.DORotate(rails[railPoint].eulerAngles, 0.25f).SetEase(Ease.Linear);
        transform.DOMove(rails[railPoint].position, trainSpeed).SetSpeedBased().SetEase(Ease.Linear).OnComplete(() =>
        {
            railPoint++;
            if (railPoint >= rails.Count)
            {
                railPoint = 0;
            }
            //Goes next point after movement is finished.
            MoveOnTrain();
        }); 
    }
    private void Update()
    {
        foreach (var wheel in wheels)
        {
            wheel.Rotate(Vector3.forward * trainSpeed * 10 * Time.deltaTime);
        }
    }
}
