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

    bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        player = FindObjectOfType<FPSController>().transform; // Find the player in the scene and assign transform to player variable
        agent = GetComponent<NavMeshAgent>(); // Get navmeshagent component
        score = FindObjectOfType<Score>(); // Find the score in scene

        SetRigidBodyState(true);
        SetColliderState(false);
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
        isDead = true;
        agent.speed = 0f;
        GetComponent<Animator>().enabled = false;
        SetRigidBodyState(false);
        SetColliderState(true);
        score.AddScore(100);
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDead)
        {
            collision.gameObject.GetComponent<FPSController>().TakeDamage(damage);
        }
    }

    void SetRigidBodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;
    }

    void SetColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }

        GetComponent<Collider>().enabled = !state;
    }

}
