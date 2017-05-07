using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
	public float gravityModifier = 1f; //valor utilizado para modificar a gravidade padrão da Physics2D

	protected Rigidbody2D rb2d;
	protected Vector2 velocity;

	//quando o objeto é habilitado e ativado
	void OnEnable()
	{
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
		//atribui uma velocidade baseando-se na gravidade e modificador de gravidade
		velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;

		//determina a próxima posição do objeto após a determinação da velocidade
		Vector2 deltaPosition = velocity * Time.deltaTime;

		//determina a nova movimentação vertical
		Vector2 move = Vector2.up * deltaPosition.y;

		//atualiza posição
		Movement (move);
	}

	//move o objeto atualizando a sua posição de acordo com o parâmetro passado
	void Movement(Vector2 move)
	{
		rb2d.position = rb2d.position + move;
	}
}
