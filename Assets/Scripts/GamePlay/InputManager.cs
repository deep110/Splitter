﻿using UnityEngine;

public class InputManager : MonoBehaviour {

	public enum InputType{
		Left,
		Right,
		Both,
		None
	}

	private float halfScreenWidth;
	private float [] touchX;
	public InputType MappedInput {get; private set;}
	
	
	void Start(){

		halfScreenWidth = Screen.width/2;
		touchX = new float[2];
		touchX[0]= touchX[1] = -1f;

		MappedInput = InputType.None;
	}

	
	void FixedUpdate () {
		
		for(int i=0;i< Input.touchCount && i<2;i++){
			touchX[i] = Input.GetTouch(i).position.x;
		}
		//close the application when back button is pressed in android
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}

		MapInput();
//		MapKeyBoardInput();
	}

	private void MapInput(){
		if(touchX[0] > -1f){
			if(touchX[0]<halfScreenWidth){
				if(touchX[1]> halfScreenWidth){
					MappedInput = InputType.Both;
				}else MappedInput = InputType.Left;
			}else{
				if(touchX[1] > 0 && touchX[1]< halfScreenWidth){
					MappedInput = InputType.Both;
				}else MappedInput = InputType.Right;
			}

		}else{
			MappedInput = InputType.None;
		}

		touchX[0] = touchX[1] = -1f;
	
	}

	private void MapKeyBoardInput(){
		float h = Input.GetAxisRaw("Horizontal");

		if(h>0){
			MappedInput = InputType.Right;
		}else if(h<0){
			MappedInput = InputType.Left;
		}else{
			MappedInput = InputType.None;
		}

		if(Input.GetButton("Jump")){
			 MappedInput = InputType.Both;
		}

		
	}
}
