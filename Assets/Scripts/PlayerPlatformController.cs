using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformController : PhysicsObject {

	public float maxSpeed = 7f;
	public float jumpTakeOffSpeed = 7f;

	private SpriteRenderer spriteRenderer;
	private Animator animator;

	// Use this for initialization
	void Awake () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		animator = GetComponent<Animator> ();
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

		//verifica quando espelhar a imagem horizontalmente
		bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f) );
		if (flipSprite)
		{
			spriteRenderer.flipX = !spriteRenderer.flipX;
		}

		animator.SetBool ("grounded", grounded); //ativa a animação de personagem parado se estiver no chão

		animator.SetFloat ("velocityX", Mathf.Abs(velocity.x) / maxSpeed); //ativa a animação de andar caso esteja se movendo horizontalmente

		//faz a movimentação ocorrer
		targetVelocity = move * maxSpeed;
	}
}
