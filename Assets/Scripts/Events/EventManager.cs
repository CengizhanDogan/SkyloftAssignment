using UnityEngine.Events;

public static class EventManager
{
	public static DriveEvent OnDriveEvent = new DriveEvent();
	public static ExitCarEvent OnExitCarEvent = new ExitCarEvent();
	public static TrainEvent OnTrainEvent = new TrainEvent();
}