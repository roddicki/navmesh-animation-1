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


	void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
		anim = this.GetComponent<Animator>();
        // Don’t update position automatically
        agent.updatePosition = false;
		agent.updateRotation = true;
		SpeedMultiplier = 0.4f;
	}

	void Update ()
	{
		//character.Move (Vector3.forward + Vector3.right, false, false);
		// wander
		if (agent.pathPending != true && agent.remainingDistance < 1) {
			agent.SetDestination (Wander());
			agent.speed = Random.Range(1,6);
		}

        // move
        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;
        // character.Move (Vector3.forward, false, false);
		//agent.destination = target.position;

		if (agent.remainingDistance > agent.stoppingDistance) 
		{
            character.Move (agent.desiredVelocity, false, false);
			//character.Move (agent.desiredVelocity * agent.speed * SpeedMultiplier, false, false);
			//character.Move (Vector3.forward + Vector3.right, false, false);
		} 
		else 
		{
			character.Move (Vector3.zero, false, false);

		}

		// Pull character towards agent
		if (worldDeltaPosition.magnitude > agent.radius)
		{
			//transform.position = agent.nextPosition - 1.9f*worldDeltaPosition;
		}

	}

	void OnAnimatorMove () {
		// Update postion to agent position
		//transform.position = agent.nextPosition;

		// Update position based on animation movement using navigation surface height
		Vector3 position = anim.rootPosition;
		transform.position = position;
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
