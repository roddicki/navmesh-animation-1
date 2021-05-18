using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class MoveChar1 : MonoBehaviour
{
    [Range (0.0f, 2.0f)]
	public float SpeedMultiplier;

	private NavMeshAgent agent;
	private Animator anim;
	public ThirdPersonCharacter character;

	// ray hit
	public Camera cam;

	void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
		anim = this.GetComponent<Animator>();
        // Don’t update position automatically
        agent.updatePosition = false;
		agent.updateRotation = false;
		SpeedMultiplier = 0.4f;
	}

	void Update ()
	{
		// wander
		if (agent.pathPending != true && agent.remainingDistance < 1) {
			agent.speed = 3; //Random.Range (1, 6);
			agent.SetDestination (Wander());
		}

		// use mouse click as desination
		if (Input.GetMouseButtonDown (0)) {
			agent.speed = 3; //Random.Range (1, 6);
			agent.SetDestination (GoToClick ());

		}


		//******* move agent *********
		Vector3 worldDeltaPosition = agent.nextPosition - transform.position;
        // character.Move (Vector3.forward, false, false);
		// agent.destination = target.position;

		// decrease speed with distance
		if (agent.remainingDistance/2 < agent.speed && agent.remainingDistance/2 > 1.0f)
		{
			agent.speed = agent.remainingDistance/2;
		}
		

		if (agent.remainingDistance > agent.stoppingDistance) 
		{
			character.Move (agent.desiredVelocity * agent.speed/2, false, false);
		} 
		else 
		{
			character.Move (Vector3.zero, false, false);

		}

		// Pull character towards agent
		if (worldDeltaPosition.magnitude > agent.radius)
		{
			// Pull character towards agent
			//transform.position = agent.nextPosition - 0.99f*worldDeltaPosition;
			// Pull agent towards character
			agent.nextPosition = transform.position + 0.6f * worldDeltaPosition;
		}
		//******* move agent *********




	}

	void OnAnimatorMove () {
		// Update postion to agent position
		//transform.position = agent.nextPosition;

		// Update position based on animation movement using navigation surface height
		Vector3 position = anim.rootPosition;
		transform.position = position;
	}

	// go to mousee click
	Vector3 GoToClick ()
	{
		Ray ray = cam.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit)) {
			return hit.point;
		} else {
			return Vector3.zero;
		}
	}

	// return random position to move to
	Vector3 Wander()
	{
		Vector3 randomDirection = Random.insideUnitSphere * 10;
		randomDirection += agent.transform.position;
		NavMeshHit hit;
		NavMesh.SamplePosition (randomDirection, out hit, 10, 1);
		Vector3 finalPosition = hit.position;
		return finalPosition;
	}
}
