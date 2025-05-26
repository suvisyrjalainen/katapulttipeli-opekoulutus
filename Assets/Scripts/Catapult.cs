using UnityEngine;

public class Catapult : MonoBehaviour
{

    public Transform launchPosition; // Katapultin kärki, josta hahmo lähtee
    public Rigidbody characterRigidbody; // Hahmon Rigidbody

    private Quaternion bunnyStartPointRotation; // Hahmon transform

    public float launchForce = 15f; // Voima, jolla hahmo ammutaan

    //Tämä ampuu hahmon suoraan eteenpäin
    public Vector3 launchDirection = new Vector3(1, 1, 0); // Lentosuunnan määrittely

    public Animator catapultAnimator;

    
    public float rotationSpeed = 50f; // Nopeus, jolla katapultti pyörii

    
    public Transform turret; // ← TÄMÄ on Catapult_Rotator GameObject



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bunnyStartPointRotation = characterRigidbody.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
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
        characterRigidbody.transform.parent = null; // Irrota pupu katapultista ennen lentoa
        catapultAnimator.SetTrigger("Launch"); // Käynnistä animaatio
        launchDirection = turret.forward + Vector3.up * 0.6f; // hieman ylös + eteen
        characterRigidbody.AddForce(launchDirection.normalized * launchForce, ForceMode.Impulse);
    }

    void ResetCatapult()
    {
        characterRigidbody.isKinematic = true; // Estä fysiikan vaikutus
        characterRigidbody.transform.position = launchPosition.position; // Aseta hahmo takaisin katapultin kärkeen
        characterRigidbody.transform.rotation = bunnyStartPointRotation; // Aseta hahmon rotaatio takaisin katapultin kärkeen
        characterRigidbody.linearVelocity = Vector3.zero; // Nollaa nopeus
        characterRigidbody.angularVelocity = Vector3.zero; // Nollaa kulmanopeus

        // Tee pupusta taas katapultin "lapsi", jotta se seuraa kääntöjä
        characterRigidbody.transform.parent = launchPosition;
    }

    void HandleRotation()
    {
        float rotation = Input.GetAxis("Horizontal"); // ← ohjaus nuolinäppäimillä tai A/D
        Debug.Log(rotation);

        turret.Rotate(Vector3.up, rotation * rotationSpeed * Time.deltaTime);

        // Päivitetään pupu lähtöpisteeseen kun pupua pyöritetään, ettei pupu jää käännöksessä:                                                    
        if (characterRigidbody.isKinematic)
        {
            characterRigidbody.transform.position = launchPosition.position;
            characterRigidbody.transform.rotation = launchPosition.rotation * Quaternion.Euler(0, 180, 0); // Jos tarvitaan käännös
        }
    }
}
