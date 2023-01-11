using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Object variables
    private GameObject focalPoint;
    public GameObject powerupIndicator;

    // Component variables
    private Rigidbody playerRigidbody;
    private TrailRenderer playerTrailRenderer;

    // Material variables
    public Material whiteMaterial;
    public Material yellowMaterial;

    // Variables
    public float movementSpeed;
    public bool hasPowerup; // default value is false
    private float powerupStrength = 20f;

    // Start is called before the first frame update
    void Start()
    {
        // Storing components to variables
        playerRigidbody = GetComponent<Rigidbody>();
        playerTrailRenderer = GetComponent<TrailRenderer>();

        // Material to white
        GetComponent<Renderer>().material = whiteMaterial;
        playerTrailRenderer.material = whiteMaterial;
        playerTrailRenderer.startColor = Color.white;
        playerTrailRenderer.endColor = Color.white;

        // Storing object's focal point to a variable
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");

        playerRigidbody.AddForce(focalPoint.transform.forward * Time.deltaTime * verticalInput * movementSpeed);

        powerupIndicator.transform.position = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if player touches the powerup
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.SetActive(true); // reveal indicator

            // Change material to yellow
            GetComponent<Renderer>().material = yellowMaterial;
            playerTrailRenderer.material = yellowMaterial;
            playerTrailRenderer.startColor = Color.yellow;
            playerTrailRenderer.endColor = Color.yellow;

            // Destroy powerup
            Destroy(other.gameObject);

            // Start the coroutine
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    // IEnumerator is an interface that defines one method—PowerupCountdownRoutine, that returns
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(5); // timer, after the timer is done the script will continue
        hasPowerup = false;
        powerupIndicator.SetActive(false); // hide indicator

        // Change material to white
        GetComponent<Renderer>().material = whiteMaterial;
        playerTrailRenderer.material = whiteMaterial;
        playerTrailRenderer.startColor = Color.white;
        playerTrailRenderer.endColor = Color.white;
    }

    // Collision method
    private void OnCollisionEnter(Collision collision)
    {
        // if Player collides with Enemy
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            // Local component variables
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;

            // Apply force
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);

            // Sends a message
            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
        }
    }
}
