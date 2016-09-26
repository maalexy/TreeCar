using UnityEngine;
using System.Collections;

public class DieTime : MonoBehaviour {

    public float aliveTime = 10.0f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, aliveTime);
	}
	
	// Update is called once per frame
	void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "StaticUntarget" && col.gameObject.tag != gameObject.tag) Destroy(col.gameObject);
        if (col.gameObject.tag == "Enemy") GameObject.FindObjectOfType<ScoreCount>().AddScore();
    }
}
