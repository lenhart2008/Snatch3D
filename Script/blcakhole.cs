using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blcakhole : MonoBehaviour {
    public float moveSpeed = 10f;
    public float curspeed = 0;
    public float step = 0.001f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        curspeed += step;
        if (curspeed > moveSpeed)
        {
            curspeed = moveSpeed;
        }
        Vector3 v = transform.position - other.transform.position;
        other.GetComponent<Rigidbody>().AddForce(v.normalized * curspeed);
    }
}
