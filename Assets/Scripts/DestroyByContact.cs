using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    public int livesValue;
    private GameController gameController;

    private void Start()
    {
        GameObject gameContollerObject = GameObject.FindWithTag("GameController");
        if(gameContollerObject != null){
            gameController = gameContollerObject.GetComponent<GameController>();
        }
        if(gameContollerObject == null){
            Debug.Log("Cannot find 'gameController' script");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boundry" || other.tag == "Enemy"){
            return;
        }
        if(explosion != null){
            Instantiate(explosion, transform.position, transform.rotation);
        }
        if(other.tag == "Player") {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.RemoveLife(livesValue);
        } else {
            gameController.AddScore(scoreValue);
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
