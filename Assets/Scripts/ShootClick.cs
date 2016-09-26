using UnityEngine;
using System.Collections;

public class ShootClick : MonoBehaviour {
    
    public GameObject shootObj = null;
    public Transform from = null;
    public float distance = 3.0f;
    public float speed = 10.0f;
    public float slowReloadTime = 1.0f;
    public float fastReloadTime = 0.1f;
    public LoaderScript fastShootS;
    private float reloadTime;
    private bool canFast = true;
    private bool fastShoot = false;
    private float last = 0.0f;
	
	// Update is called once per frame
	void Update () {
        reloadTime = fastShoot ? fastReloadTime : slowReloadTime;
        if (Input.GetAxis("Fire1") > 0.5f && Time.time - last > reloadTime)
        {
            Vector3 start = from.position;
            Vector3 direction = from.forward;
            GameObject proj = (GameObject)Instantiate(shootObj, start + direction*distance, from.rotation);
            proj.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.VelocityChange);
            last = Time.time;
        }
        if(canFast && Input.GetAxis("Fire2") > 0.5f)
        {
            Debug.Log("START");
            canFast = true;
            fastShoot = true;
            fastShootS.StartDecrease();
        } else if (!fastShoot) {
            fastShootS.StartLoad();
        }
	}

    public void EnableFastShoot()
    {
        canFast = true;
    }
    public void DisableFastShoot()
    {
        canFast = false;
        fastShoot = false;
        fastShootS.StartLoad();
    }

    void OnDestroy()
    {
        GameObject.FindObjectOfType<Spawner>().FinishGame();
    }
}
