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

    public float DockPosOffset = 0.6f;

    private bool IsEnabled = true;

    private bool IsRoot = false;

    [SerializeField] private GameObject AttachedObject;
    [SerializeField] private ObjectData AttachedData;
    [SerializeField] private AudioClip AttachSound;
    [SerializeField] private AudioClip DetachSound;
    private ObjectData ThisData;

    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.GetComponent<ObjectData>() != null) {
            ThisData = gameObject.GetComponent<ObjectData>();
        }
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    public void DetachCurrObject() {
        if(AttachedObject == null) {
            return;
        }
        foreach(Socket s in AttachedData.GetSockets()) {
            s.DetachCurrObject();
        }
        audio.clip = DetachSound;
        audio.Play();
        SetAttachedSockets(false);
        Rigidbody arb = AttachedObject.GetComponent<Rigidbody>();
        AttachedData.Remove("isAttached");
        AttachedData = null;
        arb.isKinematic = false;
        AttachedObject.transform.SetParent(null);
        AttachedObject = null;
    }

    void SetAttachedObject(GameObject newObj) {
        if(AttachedObject != null) {
            DetachCurrObject();
        }
        audio.clip = AttachSound;
        audio.Play();
        AttachedObject = newObj;
        AttachedObject.transform.SetParent(transform);
        AttachedObject.transform.SetPositionAndRotation(transform.position + (transform.forward * -1f * DockPosOffset), transform.rotation);
        Rigidbody arb = AttachedObject.GetComponent<Rigidbody>();
        arb.isKinematic = true;
        AttachedData = AttachedObject.GetComponent<ObjectData>();
        AttachedData.Add("isAttached");
        SetAttachedSockets(true);
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
        if(!IsEnabled) {
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
            if(otherScr.Contains(tag) == true) {
                return;
            }
        }
        if(otherScr.Contains("isHeld")) {
            return;
        }
        if(otherScr.Contains("isAttached")) {
            return;
        }
        SetAttachedObject(other.gameObject);
    }

    private void SetAttachedSockets(bool value) {
        foreach(Socket s in AttachedData.GetSockets()) {
            // s.gameObject.SetActive(value);
            s.SetEnabled(value);
            print("set socket to " + value);
        }
    }

    public void SetEnabled(bool value) {
        if(IsRoot) {
            print("Error: can't enable or disable a socket attached to a root object");
            return;
        }
        IsEnabled = value;
    }

    public void SetIsRoot(bool value) { // only run when object spawned
        IsRoot = value;
        IsEnabled = value; // because a root is always enabled, while a non-root object will always be non-enabled at start
    }

    public ObjectData GetData() {
        return AttachedData;
    }

    public string GetDescriptionText() {
        if(IsEnabled == false) {
            return "This socket is disabled...";
        }
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