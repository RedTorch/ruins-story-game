using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupForce : MonoBehaviour
{
    public Rigidbody PullTarget;
    public Vector3 PullDestination;
    public float GrabPosOffset = 3f;

    public float GrabRange = 20f;

    private bool isLocked = false;
    // [SerializeField] private AnimationCurve PullForce;

    private Transform cam;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Physics.Raycast(cam.position, cam.forward, out hit, GrabRange)) {
            Grab(hit.rigidbody);
        }
        else if(Input.GetButton("Fire1") && PullTarget != null) {
            PullDestination = cam.position + (cam.forward * GrabPosOffset);
            if(isLocked) {
                PullTarget.transform.position = PullDestination;
            }
            else {
                if(Vector3.Distance(PullDestination, PullTarget.transform.position)<0.1f) {
                    isLocked = true;
                }
                PullTarget.velocity = Vector3.Normalize(PullDestination - PullTarget.transform.position) * Vector3.Distance(PullDestination, PullTarget.transform.position) * 10f;
            }
        }
        else {
            LetGo();
        }
    }

    void Grab(Rigidbody grabbedObj) {
        if(grabbedObj == null || grabbedObj.GetComponent<ObjectData>() == null) {
            // what to do if object has no rigidbody (e.g. play "shwump" failed pickup sound)
            return;
        }
        if(grabbedObj.transform.parent != null && grabbedObj.transform.parent.GetComponent<Socket>() != null) {
            grabbedObj.transform.parent.GetComponent<Socket>().DetachCurrObject();
        }
        PullTarget = grabbedObj;
        PullTarget.useGravity = false;
        grabbedObj.GetComponent<ObjectData>().Add("isHeld");
        // play "successful" sound
    }

    void LetGo() {
        if(PullTarget == null || PullTarget.GetComponent<ObjectData>() == null) {
            return;
        }
        float throwForce = GrabRange/4f;
        PullTarget.velocity = cam.forward * throwForce;
        PullTarget.GetComponent<ObjectData>().Remove("isHeld");
        PullTarget.useGravity = true;
        PullTarget = null;
        isLocked = false;
    }
}