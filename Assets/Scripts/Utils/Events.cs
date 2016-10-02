
public class Events{

	public delegate void Call();
	public static event Call GameOverEvent;

	public static void CallGameOver(){
		if(GameOverEvent != null)
	        GameOverEvent();
	}
}
