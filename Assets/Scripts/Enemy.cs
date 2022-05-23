using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 2;

    Rigidbody rb;
    GameObject parentGameObject;
    ScoreBoard scoreBoard;

    // Start is called before the first frame update
    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        AddRigidBody();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddRigidBody()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }
    
    void OnParticleCollision(GameObject other)
    {
        ProccesHit();
        if (hitPoints < 1)
        {
            KillEnemy();
        }
    }

    void ProccesHit()
    {
        HitEnemy();
        hitPoints--;
        scoreBoard.IncreaseScore(scorePerHit);
    }

    void HitEnemy()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
    }

    void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }
}
