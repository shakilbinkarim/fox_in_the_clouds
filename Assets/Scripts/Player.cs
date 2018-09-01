using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{

	[SerializeField] private float speed = 8.0f;
	[SerializeField] private float maxVelocity = 4.0f;
	[SerializeField] private float offset = 0.5f;

	private Rigidbody2D playerRigidBody;
	private Animator anim;
	private float minX, maxX, maxY;

	// Called Before Start
	private void Awake ()
	{
		playerRigidBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	// Use this for initialization
	private void Start ()
	{
		SetBounds();
	}

	private void Update()
	{
		if (transform.position.x < minX)
		{
			Vector3 temp = transform.position;
			temp.x = minX;
			transform.position = temp;
		}

		if (transform.position.x > maxX)
		{
			Vector3 temp = transform.position;
			temp.x = maxX;
			transform.position = temp;
		}
		if (transform.position.y > maxY)
		{
			Vector3 temp = transform.position;
			temp.y = maxY;
			transform.position = temp;
			gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0.5f, 0.0f));
		}
	}

	private void FixedUpdate()
	{
		//PlayerMoveKeyboard();
		PlayerMoveTouch();
	}

	private void PlayerMoveKeyboard()
	{
		float forceX = 0.0f;
		float vel = Mathf.Abs(playerRigidBody.velocity.x);
		float horizontalAxisRaw = Input.GetAxisRaw("Horizontal");
		if (horizontalAxisRaw > 0)
		{
			anim.SetBool("walk", true);
			if (vel < maxVelocity)
			{
				forceX = speed;
			}
			FlipPlayer(-0.5f);
		}
		else if (horizontalAxisRaw < 0)
		{
			anim.SetBool("walk", true);
			if (vel < maxVelocity)
			{
				forceX = -speed;
			}
			FlipPlayer(0.5f);
		}
		else
		{
			anim.SetBool("walk", false);
		}
		playerRigidBody.AddForce(new Vector2(forceX, 0.0f));
	}

	private void PlayerMoveTouch()
	{
		float forceX = 0.0f;
		float vel = Mathf.Abs(playerRigidBody.velocity.x);
		if (TouchControls.WALK_RIGHT)
		{
			anim.SetBool("walk", true);
			if (vel < maxVelocity)
			{
				forceX = speed;
			}
			FlipPlayer(-0.5f);
		}
		else if (TouchControls.WALK_LEFT)
		{
			anim.SetBool("walk", true);
			if (vel < maxVelocity)
			{
				forceX = -speed;
			}
			FlipPlayer(0.5f);
		}
		else if (TouchControls.IDLE)
		{
			anim.SetBool("walk", false);
		}
		playerRigidBody.AddForce(new Vector2(forceX, 0.0f));
	}

	private void FlipPlayer(float flipFactor)
	{
		Vector3 temp = transform.localScale;
		temp.x = flipFactor;
		transform.localScale = temp;
	}

	private void SetBounds()
	{
		Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
		maxX = bounds.x - offset;
		minX = -bounds.x + offset;
		maxY = bounds.y - (offset * 3.0f);
	}

}
