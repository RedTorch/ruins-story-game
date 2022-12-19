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
        if(AttachedObject != null) {
            return;
        }
        string ps = "Trigger entered - ";
        ObjectData otherScr = other.GetComponent<ObjectData>();
        if(otherScr == null) {
            print(ps + "other does not have ObjectData");
            return;
        }
        bool containsAcceptedTags = false;
        foreach(string tag in AcceptedTags) {
            if(otherScr.Contains("key") == false) {
                containsAcceptedTags = true;
                break;
            }
        }
        if(otherScr.Contains("isHeld")) {
            print(ps + "other is being held by Player");
            return;
        }
        SetAttachedObject(other.gameObject);
        print(ps + "setting other as attached gameobj");
    }

    public ObjectData GetData() {
        return AttachedData;
    }
}