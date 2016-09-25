using UnityEngine;

public class InputManager : MonoBehaviour {

	public enum InputType{
		LEFT,
		RIGHT,
		BOTH,
		NONE
	}

	float halfScreenWidth;
	float [] touchX;
	public InputType MappedInput {get; private set;}
	
	
	void Start(){

		halfScreenWidth = Screen.width/2;
		touchX = new float[2];
		touchX[0]=touchX[1] = -1f;

		MappedInput = InputType.NONE;
	}

	
	void Update () {
		
		for(int i=0;i< Input.touchCount && i<2;i++){
			touchX[i] = Input.GetTouch(i).position.x;
		}

		mapInput();
	}

	private void mapInput(){
		if(touchX[0] != -1f){
			if(touchX[0]<halfScreenWidth){
				if(touchX[1]> halfScreenWidth){
					MappedInput = InputType.BOTH;
				}else MappedInput = InputType.LEFT;
			}else{
				if(touchX[1]!= -1f && touchX[1]< halfScreenWidth){
					MappedInput = InputType.BOTH;
				}else MappedInput = InputType.RIGHT;
			}

		}else{
			MappedInput = InputType.NONE;
		}

		touchX[0] = touchX[1] = -1f;
	
	}
}
