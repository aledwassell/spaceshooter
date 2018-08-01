using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject hazard;
    public Vector3 spawnValue;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    private int score;
    public Text livesText;
    public int lives;

    public Text restartText;
    public Text gameoverText;



    private void Start()
    {
        StartCoroutine(SpawnWaves());
        score = 0;
        //lives = 0;
        UpdateScore();
        UpdateLives();
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
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    public void RemoveLife(int removedLives){
        lives = lives - removedLives;
        UpdateLives();
    }

    void UpdateLives()
    {
        livesText.text = "Lives: " + lives;
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
