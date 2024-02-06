using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 50f;
    [SerializeField] float damage = 10f; 

    Transform player; // Player's transform
    NavMeshAgent agent; // Navmesh Agent

    Score score; // Score

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<FPSController>().transform; // Find the player in the scene and assign transform to player variable
        agent = GetComponent<NavMeshAgent>(); // Get navmeshagent component
        score = FindObjectOfType<Score>(); // Find the score in scene
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.position; // Make the enemy track the player constantly
    }

    public void TakeDamage(float damage) // Take damage
    {
        health -= damage;

        if(health <= 0f) // If Health is below zero, die
        {
            Die();
        }
    }

    void Die() // Die
    {
        score.AddScore(100);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<FPSController>().TakeDamage(damage);
        }
    }

}
