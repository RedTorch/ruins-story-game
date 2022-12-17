using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachObj : MonoBehaviour
{
    public GameObject AttachedObject;

    // Start is called before the first frame update
    void Start()
    {
        //
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    void DetachCurrObject() {
        Rigidbody arb = AttachedObject.GetComponent<Rigidbody>();
        arb.isKinematic = true;
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
        arb.isKinematic = false;
    }

    void OnTriggerEnter(Collider other) {
        string ps = "Trigger entered - ";
        ObjectData otherScr = other.GetComponent<ObjectData>();
        if(otherScr == null) {
            print(ps + "other does not have ObjectData");
            return;
        }
        if(otherScr.Contains("key") == false) {
            print(ps + "other is not tagged as key");
            return;
        }
        SetAttachedObject(other.gameObject);
        print(ps + "setting other as attached gameobj");
    }
}