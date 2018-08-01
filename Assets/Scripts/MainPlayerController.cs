using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
public class Boundries
{
    public float xMin, xMax, zMin, zMax;
}

[System.Serializable]
public class Engine
{
    public float jetLength;
}

public class MainPlayerController : MonoBehaviour {
    
    public float speed;
    private Rigidbody rb;
    private AudioSource audio;
    public float tilt;
    public Boundries boundry;
    public Engine engines;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float nextFire;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if((Input.GetButton("Fire1") || Input.GetKey("space")) && Time.time > nextFire){
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audio.Play();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;

        rb.position = new Vector3
            (
                Mathf.Clamp(rb.position.x, boundry.xMin, boundry.xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, boundry.zMin, boundry.zMax)
            );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
