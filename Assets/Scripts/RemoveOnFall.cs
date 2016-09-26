using UnityEngine;
using System.Collections;

public class RemoveOnFall : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if (transform.position.y < -5) Destroy(gameObject);
	}
}
