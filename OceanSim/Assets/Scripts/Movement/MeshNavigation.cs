using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CharacterState {
    IDLE,
    MOVING
}

public class MeshNavigation : MonoBehaviour {

    private Camera cam;
    private NavMeshAgent agent;
    public CharacterState state;
    public Animator animator;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        state = CharacterState.IDLE;
    }
	
	// Update is called once per frame
	void Update () {
        float dist=agent.remainingDistance;
        if (agent.pathStatus==NavMeshPathStatus.PathComplete && agent.remainingDistance == 0) {
            if(state == CharacterState.MOVING) {
                Debug.Log("Stopped walking");
                animator.SetBool("Walking", false);
            }
            state = CharacterState.IDLE;
        } else {
            if(state == CharacterState.IDLE) {
                Debug.Log("Started walking");
                animator.SetBool("Walking", true);
            }
            state = CharacterState.MOVING;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit)) {
                Debug.Log("Set position of agent to: " + hit.point);
                agent.SetDestination(hit.point);
            }
        }
	}
}
