using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class MoveChar : MonoBehaviour
{
    public ThirdPersonCharacter character;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        character.Move (Vector3.forward*0.4f, false, false);
    }

    void OnAnimatorMove () {
		// Update postion to agent position
//		transform.position = agent.nextPosition;

		// Update position based on animation movement using navigation surface height
		Vector3 position = anim.rootPosition;
		//position.y = agent.nextPosition.y;
		transform.position = position;
	}
}
