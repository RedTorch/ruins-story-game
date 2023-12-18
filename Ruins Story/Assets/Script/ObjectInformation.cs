using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;

public class ObjectInformation : MonoBehaviour
{
    public GameObject TextContainer;
    public TMP_Text Text;

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
        if(Physics.Raycast(cam.position, cam.forward, out hit, 20f)) {
            if(hit.collider.gameObject.GetComponent<ObjectData>() != null) {
                ObjectData objDat = hit.collider.gameObject.GetComponent<ObjectData>();
                TextContainer.SetActive(true);
                Text.text = objDat.GetDescriptionText() + "\n" + objDat.GetTagsText();
            }
            else if(hit.collider.gameObject.GetComponent<Socket>() != null) {
                Socket socket = hit.collider.gameObject.GetComponent<Socket>();
                TextContainer.SetActive(true);
                Text.text = socket.GetDescriptionText();
            }
            else {
                TextContainer.SetActive(false);
            }
        }
        else {
            TextContainer.SetActive(false);
        }
    }
}
