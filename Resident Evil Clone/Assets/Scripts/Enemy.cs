using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float MaxHealth = 5f;

    private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        currentHealth = MaxHealth; // Initialize currentHealth with MaxHealth
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(Player.position);
    }


    public void TakeDamage(float damage)
    {
        Debug.Log("Zombie has taken: " + damage);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
