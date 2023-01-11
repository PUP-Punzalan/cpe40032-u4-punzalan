using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Component variables
    private Rigidbody enemyRigidbody;
    private GameObject player;

    // Variables
    public float enemySpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Storing components to their respective variables
        enemyRigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Store the local front axis of the Player
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        // Apply force from the local front axis of the player
        enemyRigidbody.AddForce(lookDirection * Time.deltaTime * enemySpeed);

        // Vertical boundary
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }
}
