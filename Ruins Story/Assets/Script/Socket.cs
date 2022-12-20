using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    [Tooltip("A list of tags for dockable objects.\nWill be rejected if does not contain any of these.")]
    public string[] AcceptedTags;
    [Tooltip("A list of tags which will prevent an object from docking.")]
    public string[] RejectedTags;

    [SerializeField] private GameObject AttachedObject;
    [SerializeField] private ObjectData AttachedData;
    private ObjectData ThisData;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.GetComponent<ObjectData>() != null) {
            ThisData = gameObject.GetComponent<ObjectData>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    public void DetachCurrObject() {
        Rigidbody arb = AttachedObject.GetComponent<Rigidbody>();
        AttachedData = null;
        arb.isKinematic = false;
        AttachedObject.transform.SetParent(null);
        AttachedObject = null;
    }

    void SetAttachedObject(GameObject newObj) {
        if(AttachedObject != null) {
            DetachCurrObject();
        }
        AttachedObject = newObj;
        AttachedObject.transform.SetParent(transform);
        AttachedObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
        Rigidbody arb = AttachedObject.GetComponent<Rigidbody>();
        arb.isKinematic = true;
        AttachedData = AttachedObject.GetComponent<ObjectData>();
    }

    void OnTriggerStay(Collider other) {
        /* Could be optimized by having OnTriggerEnter check the tags of the object 
        and add it to an approval/ rejection list. OnTriggerStay just needs to check 
        whether the object is held or let go. OnTriggerExit removes the object from 
        the approve/reject list. This would avoid having to re-judge the object every frame,
        which is only useful if the object's tags change mid-collision (unlikely). */
        ObjectData otherScr = other.GetComponent<ObjectData>();
        if(AttachedObject != null) {
            return;
        }
        if(otherScr == null) {
            return;
        }
        bool containsAcceptedTags = false;
        foreach(string tag in AcceptedTags) {
            if(otherScr.Contains(tag) == true) {
                containsAcceptedTags = true;
                break;
            }
        }
        if(containsAcceptedTags == false) {
            return;
        }
        foreach(string tag in RejectedTags) {
            if(otherScr.Contains(tag) == false) {
                return;
            }
        }
        if(otherScr.Contains("isHeld")) {
            return;
        }
        SetAttachedObject(other.gameObject);
    }

    public ObjectData GetData() {
        return AttachedData;
    }

    public string GetDescriptionText() {
        string ret = "Description: A simple socket\n";
        if(AcceptedTags.Length != 0) {
            ret = ret + "Accepts:" + String.Join(", ", AcceptedTags) + "\n";
        }
        if(RejectedTags.Length != 0) {
            ret = ret + "Rejects:" + String.Join(", ", RejectedTags);
        }
        return ret;
    }
}