using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    [SerializeField] private List<string> Tags;
    // could add more fields in the future; "Tags" is just a general-purpose group

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(string key) {
        Tags.Add(key);
    }

    public bool Remove(string key) {
        return Tags.Remove(key);
    }

    public bool Contains(string key) {
        return Tags.Contains(key);
    }
}
