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

    private Vector3 prevPosition;

	// Use this for initialization
	void Start () {
        cam = Camera.main;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        state = CharacterState.IDLE;

    }
	
	// Update is called once per frame
	void Update () {
        UpdateState();
        if (state == CharacterState.MOVING) {
            animator.SetFloat("Speed", getVelocity());
            Vector3 nextPoint = agent.path.corners[1];
            transform.rotation = Quaternion.LookRotation(new Vector3(nextPoint.x, transform.position.y, nextPoint.z) - transform.position);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit)) {
                Debug.Log("Set position of agent to: " + hit.point);
                agent.SetDestination(hit.point);
            }
        }

        prevPosition = transform.position;
	}

    private float getVelocity() {
        float velocity = ((transform.position - prevPosition).magnitude / Time.deltaTime / agent.speed) * 2;
        Debug.Log("Velocity is: " + velocity);
        return velocity;
    }

    private void UpdateState() {
        float dist = agent.remainingDistance;
        if (agent.pathStatus == NavMeshPathStatus.PathComplete && agent.remainingDistance == 0) {
            if (state == CharacterState.MOVING) {
                animator.SetBool("Walking", false);
            }
            state = CharacterState.IDLE;
        }
        else {
            if (state == CharacterState.IDLE) {
                animator.SetBool("Walking", true);
            }
            state = CharacterState.MOVING;
        }
    }
}
