using UnityEngine;
using System.Collections;

public class ShowHitBorder : MonoBehaviour {
	
	
	Color forAlpha;
	[HideInInspector] public bool shouldIncreseAlpha;
	float speed=0.05f;
	
	void Start(){
		guiTexture.pixelInset = new Rect (0,0,0,0);//works perfectly when 0,0,0,0
		forAlpha = guiTexture.color;
		forAlpha.a = 0;
		guiTexture.color = forAlpha;
	}
	
	
	void Update () {
		float a = forAlpha.a;
		if(shouldIncreseAlpha){
			if(a >= 0.95f) shouldIncreseAlpha = false;
			else a += speed;
		}
		else if(a > 0.05f) a -= speed;
		else a=0;
		
		//print ("The alpha should be "+a.ToString ());
		forAlpha.a = a;
		guiTexture.color = forAlpha;
	}
}