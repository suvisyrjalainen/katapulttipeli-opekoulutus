using UnityEngine;
using UnityEngine.UI; // muista tämä!
using TMPro;
public class Catapult : MonoBehaviour
{

    public Transform launchPosition; // Katapultin kärki, josta hahmo lähtee
    public Rigidbody characterRigidbody; // Hahmon Rigidbody

    private Quaternion bunnyStartPointRotation; // Hahmon transform

    public Slider forceSlider;
    public TextMeshProUGUI forceValueText; // UI-elementti, johon voima näytetään
    public float launchForce = 15f; // Voima, jolla hahmo ammutaan

    public Slider liftSlider;
    public float verticalLift = 0.5f;

     public TextMeshProUGUI liftValueText; // UI-elementti, johon voima näytetään

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
        if (forceValueText != null)
        {
            forceValueText.text = "Voima: " + Mathf.RoundToInt(launchForce);
        }
        if (liftValueText != null)
        {
            liftValueText.text = "Suunta: " + verticalLift;
        }

        if (forceSlider != null)
        {
            launchForce = forceSlider.value;
        }

        if (liftSlider != null)
        {
            verticalLift = liftSlider.value;
        }

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
        launchDirection = turret.forward + Vector3.up * verticalLift; // hieman ylös + eteen
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
