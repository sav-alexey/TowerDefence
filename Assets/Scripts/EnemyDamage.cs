using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{

    [SerializeField] ParticleSystem deathParticlePrefab;

    [SerializeField] int hitPoints;

    [SerializeField] AudioClip enemyHitSFX;

    Text scoreText;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        
    }

    // Update is called once per frame
    void Update()
    {   
        
        
    }

    private void OnParticleCollision(GameObject other) {
        // print("I'm hit");
        hitPoints--;
        GetComponent<AudioSource>().PlayOneShot(enemyHitSFX);
        if (hitPoints <= 0)
        {
            DestroyEnemy();
        }
    }

    public void DestroyEnemy(bool addScore = true){
        score = int.Parse(scoreText.text);
        score ++;
        scoreText.text = score.ToString();
        var deathFX = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        deathFX.Play();
        Destroy(deathFX.gameObject, deathFX.main.duration);
        Destroy(gameObject);
    }
}
