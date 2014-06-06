using UnityEngine;
using System.Collections;

public class PlayerIndicator : MonoBehaviour {
	Camera camMain,camMandeep,camMustafa;
	bool showMaps;
	Transform myCar;
	float ArrowSize=35;
	void Start() {
		myCar = this.gameObject.transform.parent.gameObject.transform;
		
		camMain=GameObject.Find ("MiniMap-main").GetComponent<Camera>();
		camMandeep=GameObject.Find ("MiniMap-mandeep").GetComponent<Camera>();
		camMustafa=GameObject.Find ("MiniMap-mustafa").GetComponent<Camera>();
	}
	
	public void changeColorForPersonalCar(Material mat){
		renderer.material = mat;
	}
	
	void Update () {
		
		//change the value of arrow size and not this
		transform.localScale = Vector3.Lerp (transform.localScale, new Vector3 (ArrowSize, 1, ArrowSize), 2*Time.deltaTime);
		
		transform.rotation = Quaternion.identity;
		float rotY=myCar.rotation.eulerAngles.y;
		Vector3 pos = myCar.position;
		pos.y += 1000f;
		transform.position = pos;
		transform.Rotate (Vector3.up*(rotY+90));
		
		if(Input.GetKeyDown (KeyCode.M)){
			showMaps=!showMaps;
			if(showMaps){
				camMain.farClipPlane=3000f;
				camMandeep.farClipPlane=3000f;
				camMustafa.farClipPlane=3000f;
			}
			else{
				camMain.farClipPlane=1f;
				camMandeep.farClipPlane=1f;
				camMustafa.farClipPlane=1f;
			}
		}
	}
}