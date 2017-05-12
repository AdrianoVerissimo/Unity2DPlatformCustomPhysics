using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
	public float minGroundNormalY = .65f;

	public float gravityModifier = 1f; //valor utilizado para modificar a gravidade padrão da Physics2D

	protected Vector2 targetVelocity; //velocidade horizontal

	protected bool grounded;
	protected Vector2 groundNormal;

	protected Rigidbody2D rb2d;
	protected Vector2 velocity;

	protected ContactFilter2D contactFilter;
	protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
	protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D> (16);

	protected const float minMoveDistance = 0.001f;
	protected const float shellRadius = 0.01f;

	//quando o objeto é habilitado e ativado
	void OnEnable()
	{
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Use this for initialization
	void Start ()
	{
		contactFilter.useTriggers = false;

		//define a máscara de layers a ser usada como a padrão definida em Physics2D
		contactFilter.SetLayerMask (Physics2D.GetLayerCollisionMask (gameObject.layer));
		contactFilter.useLayerMask = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		targetVelocity = Vector2.zero;
		ComputeVelocity ();
	}

	//método utilizado para calcular a velocidade do objeto e ser substituído
	protected virtual void ComputeVelocity()
	{

	}

	void FixedUpdate()
	{
		//atribui uma velocidade baseando-se na gravidade e modificador de gravidade
		velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
		velocity.x = targetVelocity.x;

		grounded = false; //sempre reseta definindo que o objeto não está no chão

		//determina a próxima posição do objeto após a determinação da velocidade
		Vector2 deltaPosition = velocity * Time.deltaTime;

		//vetor responsável para mover pelo chão.
		//groundNormal.x possui seu valor negativo para inverter o lado aonde a coordenada X da linha normal está apontando.
		Vector2 moveAlongGround = new Vector2 (groundNormal.y, -groundNormal.x);

		//determina a movimentação horizontal
		Vector2 move = moveAlongGround * deltaPosition.x;
		Movement (move, false);

		//determina a nova movimentação vertical
		move = Vector2.up * deltaPosition.y;
		Movement (move, true);
	}

	//move o objeto atualizando a sua posição de acordo com o parâmetro passado
	void Movement(Vector2 move, bool yMovement)
	{
		float distance = move.magnitude;

		if (distance > minMoveDistance)
		{
			//junta todas as colisões de RigidBody2D, lança para um local e pega a quantidade de colisões que ocorreram
			//Obs: hitBuffer é uma variável de saída e retorna o RayCastHit2D de acordo com o objeto que colidiu
			int count = rb2d.Cast (move, contactFilter, hitBuffer, distance + shellRadius);
			hitBufferList.Clear ();

			//para cada acerto ocorrido
			for (int i = 0; i < count; i++)
			{
				hitBufferList.Add (hitBuffer[i]); //adiciona o objeto que acertou na lista
			}

			for (int i = 0; i < hitBufferList.Count; i++)
			{
				Vector2 currentNormal = hitBufferList [i].normal;

				//verifica o ângulo da linha "normal" e detecta se o objeto está no chão
				if (currentNormal.y > minGroundNormalY)
				{
					grounded = true; //define que esncostou no chão

					//se é permitido mover verticalmente
					if (yMovement)
					{
						groundNormal = currentNormal;
						currentNormal.x = 0;
					}
				}

				//pega a relação da direção entre os dois vetores
				float projection = Vector2.Dot (velocity, currentNormal);

				//a velocidade de ambos os objetos não estão na mesma direção
				if (projection < 0)
				{
					//atualiza a velocidade proporcionalmente a direção do objeto em que encostou
					//ou seja, se encostou em um objeto plano, parará, pois um está parado enquanto outro se movimenta
					velocity = velocity - projection * currentNormal;
				}

				//atualiza a distância devido as colisões ocorridas
				float modifiedDistance = hitBufferList [i].distance - shellRadius;
				distance = modifiedDistance < distance ? modifiedDistance : distance; //pega a menor distância

			}

		}

		rb2d.position = rb2d.position + move.normalized * distance;
	}
}
