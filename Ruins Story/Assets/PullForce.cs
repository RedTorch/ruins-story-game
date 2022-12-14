using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullForce : MonoBehaviour
{
    public GameObject PullTarget;
    private Transform cam;
    // [SerializeField] private AnimationCurve PullForce;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(PullTarget != null) {
            PullTarget.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(transform.position - PullTarget.transform.position) * Vector3.Distance(transform.position, PullTarget.transform.position) * Time.deltaTime * 10f);
        }
        else if(Input.GetButton("Fire1") && Physics.Raycast(cam.position, cam.forward, out hit, 20f)) {
            
        }
    }
}
