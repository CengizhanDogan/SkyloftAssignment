using UnityEngine.Events;

public static class EventManager
{
	public static DriveEvent OnDriveEvent = new DriveEvent();
	public static ExitVehicleEvent OnExitVehicleEvent = new ExitVehicleEvent();
	public static TrainEvent OnTrainEvent = new TrainEvent();
}