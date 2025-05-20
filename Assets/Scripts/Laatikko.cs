using UnityEngine;

public class Laatikko : MonoBehaviour
{
     private bool hasScored = false;
     public int laatikonScore = 10; // Muuttuja pistemäärälle

void OnCollisionEnter(Collision collision)
{
    if (!hasScored && collision.gameObject.CompareTag("pistealue"))
    {
        hasScored = true;
        ScoreManager.Instance.AddScore(laatikonScore); // Lisää pisteet ScoreManageriin

    }
}
}

/*
//Tässä jos haluaa että jos laatikko vaan liikkuu niin saa pisteitä
private bool hasScored = false;
    public int score = 10;
    public float movementThreshold = 0.5f; // Minimum distance to move to score
    private Vector3 initialPosition;

 private void Start()
    {
        // Store the initial position when the object is created
        initialPosition = transform.position;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!hasScored && collision.gameObject.CompareTag("PisteAlue"))
        {
            // Calculate distance moved from initial position
            float distanceMoved = Vector3.Distance(transform.position, initialPosition);

            // Check if movement exceeds threshold
            if (distanceMoved >= movementThreshold)
            {
                hasScored = true;
                Debug.Log("Scored " + score + " points! Moved: " + distanceMoved.ToString("F2") + " units");

            }
        }
    }*/

