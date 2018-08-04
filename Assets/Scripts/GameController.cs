using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject hazard;
    public Vector3 spawnValue;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    private static int score;
    public Text livesText;
    public static int lives = 3;

    public Text restartText;
    public Text gameoverText;

    private bool gameOver;
    private bool restart;
    private bool playerDestroyed;

    private void Start()
    {
        gameOver = false;
        restart = false;
        playerDestroyed = false;
        restartText.text = "";
        gameoverText.text = "";
        StartCoroutine(SpawnWaves());
        //score = 0;
        //lives = 0;
        UpdateScore();
        UpdateLives();
    }

    private void Update()
    {
        if(restart){
            if(Input.GetKeyDown(KeyCode.R)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                score = 0;
            }
        }
        if(Input.GetKey("escape")){
            Application.Quit();
        }
    }

    IEnumerator SpawnWaves(){
        yield return new WaitForSeconds(startWait);
        while(true){
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValue.x, spawnValue.x), spawnValue.y, spawnValue.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if(gameOver){
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }

            if (playerDestroyed)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void RemoveLife(int removedLives){
        Debug.Log(lives);
        lives = lives - removedLives;
        UpdateLives();
        if (lives == 0)
        {
            GameOver();
            return;
        }
        playerDestroyed = true;
    }

    void UpdateLives()
    {
        livesText.text = "Lives: " + lives;
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver(){
        gameoverText.text = "Game Over!";
        gameOver = true;
        lives = 3;
    }
}
