using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//herda do script PhysicsObject para que a movimentação estabelecida nele possa ser alterada e utilizada
public class AutoMoveCrate : PhysicsObject {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		targetVelocity = Vector2.left; //faz andar para esquerda
	}
}
