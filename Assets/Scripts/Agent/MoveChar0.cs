using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class MoveChar0 : MonoBehaviour
{
	[Range (0.0f, 2.0f)]
	public float SpeedMultiplier;
	public Transform target;
	private NavMeshAgent agent;

	public ThirdPersonCharacter character;

	// THID SCRIPT WORKS
	void Start ()
	{
		agent = GetComponent<NavMeshAgent> ();
		agent.updateRotation = false;
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

		//agent.destination = target.position;

		if (agent.remainingDistance > agent.stoppingDistance) 
		{
			character.Move (agent.desiredVelocity * agent.speed * SpeedMultiplier, false, false);
			//character.Move (Vector3.forward + Vector3.right, false, false);
		} 
		else 
		{
			character.Move (Vector3.zero, false, false);

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
