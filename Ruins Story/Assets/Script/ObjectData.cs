using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    [SerializeField] private string DescText = "A simple object";
    [SerializeField] private List<string> Tags;
    // vvv this is of format "data header, value"
    [SerializeField] private Dictionary<string, float> Dats = new Dictionary<string, float>();

    [SerializeField] private List<Socket> MySockets;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform) {
            if (child.gameObject.GetComponent<Socket>() != null) {
                MySockets.Add(child.gameObject.GetComponent<Socket>());
            }
        }
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

    public bool Dat_Add(string key, float val = 0f) {
        return Dats.TryAdd(key, val);
    }

    public bool Dat_Remove(string key) {
        return Dats.Remove(key);
    }

    public bool Dat_Contains(string key) {
        return Dats.ContainsKey(key);
    }

    public float Dat_GetValue(string key) {
        return Dats[key];
    }

    public string GetTagsText() {
        string ret = "Tags: ";
        for(int i=0;i<Tags.Count;i++)
        {
            ret = ret + Tags[i] + ", ";
        }
        return ret;
    }

    public string GetDescriptionText() {
        return "Description: " + DescText;
    }

    public Socket[] GetSockets() {
        Socket[] ret = new Socket[MySockets.Count];
        for(int i=0;i<MySockets.Count;i++) {
            ret[i] = MySockets[i];
        }
        return ret;
    }
}
