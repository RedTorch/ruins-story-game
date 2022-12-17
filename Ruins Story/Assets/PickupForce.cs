using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupForce : MonoBehaviour
{
    public Rigidbody PullTarget;
    public Vector3 PullDestination;
    private Transform cam;
    // [SerializeField] private AnimationCurve PullForce;

    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Physics.Raycast(cam.position, cam.forward, out hit, 20f)) {
            Grab(hit.rigidbody);
        }
        else if(Input.GetButton("Fire1") && PullTarget != null) {
            PullDestination = cam.position + (cam.forward * 2f);
            PullTarget.velocity = Vector3.Normalize(PullDestination - PullTarget.transform.position) * Vector3.Distance(PullDestination, PullTarget.transform.position) * 10f;
        }
        else {
            LetGo();
        }
    }

    void Grab(Rigidbody grabbedObj) {
        if(grabbedObj == null) {
            // what to do if object has no rigidbody (e.g. play "shwump" failed pickup sound)
            return;
        }
        PullTarget = grabbedObj;
        PullTarget.useGravity = false;
        // play "successful" sound
    }

    void LetGo() {
        if(PullTarget != null) {
            PullTarget.useGravity = true;
            PullTarget = null;
        }
    }
}