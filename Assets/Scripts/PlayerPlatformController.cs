using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformController : PhysicsObject {

	public float maxSpeed = 7f;
	public float jumpTakeOffSpeed = 7f;

	// Use this for initialization
	void Start () {
		
	}

	//substitui o método responsável por calcular a velocidade do objeto
	protected override void ComputeVelocity()
	{
		//reseta a movimentação
		Vector2 move = Vector2.zero;

		//pega a movimentação horizontal
		move.x = Input.GetAxis ("Horizontal");

		//se apertou botão de pulo e está no chão
		if (Input.GetButtonDown ("Jump") && grounded)
		{
			velocity.y = jumpTakeOffSpeed; //adicionar velocidade vertical para pular
		}
		else if (Input.GetButtonUp ("Jump")) //soltou o botão de pulo, então diminuir velocidade vertical para parar o pulo
		{
			if (velocity.y > 0)
			{
				velocity.y = velocity.y * .5f;
			}
		}

		//faz a movimentação ocorrer
		targetVelocity = move * maxSpeed;
	}
}
