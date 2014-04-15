#pragma strict

var destroyAfter : float = 10;
private var timer : float;

function Start () {

}

function Update () {
	timer += Time.deltaTime;
	if(timer > destroyAfter){
		Destroy(gameObject);
	}
}