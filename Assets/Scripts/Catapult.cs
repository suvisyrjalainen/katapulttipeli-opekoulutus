using UnityEngine;

public class Catapult : MonoBehaviour
{

    public Transform launchPosition; // Katapultin kärki, josta hahmo lähtee
    public Rigidbody characterRigidbody; // Hahmon Rigidbody

    private Quaternion bunnyStartPointRotation; // Hahmon transform

    public float launchForce = 15f; // Voima, jolla hahmo ammutaan

    //Tämä ampuu hahmon suoraan eteenpäin
    public Vector3 launchDirection = new Vector3(1, 1, 0); // Lentosuunnan määrittely

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bunnyStartPointRotation = characterRigidbody.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Kun pelaaja painaa "space" näppäintä, ammutaan hahmo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {

            Debug.Log("reset");
            ResetCatapult();
        }

    }

    void Launch()
    {
        characterRigidbody.isKinematic = false; // Anna fysiikan vaikuttaa
        characterRigidbody.AddForce(launchDirection.normalized * launchForce, ForceMode.Impulse);
    }
    
    void ResetCatapult()
    {
        characterRigidbody.isKinematic = true; // Estä fysiikan vaikutus
        characterRigidbody.transform.position = launchPosition.position; // Aseta hahmo takaisin katapultin kärkeen
        characterRigidbody.transform.rotation = bunnyStartPointRotation; // Aseta hahmon rotaatio takaisin katapultin kärkeen
        characterRigidbody.linearVelocity = Vector3.zero; // Nollaa nopeus
        characterRigidbody.angularVelocity = Vector3.zero; // Nollaa kulmanopeus
    }
}
