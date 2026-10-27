using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class BallInHole : MonoBehaviour
{
    public GameObject golfBall;  // Public variable to reference the golf ball object

    public TextMeshProUGUI gameOverText;  // Reference to the TextMeshPro text displayed at game over
    public RawImage gameOverImage;  // Reference to the Raw Image displayed at game over
    public TextMeshProUGUI parText;  // Reference to the Par Text

    // Start is called before the first frame update
    void Start()
    {
        // Ensure game over text and image are hidden at the start of the game
        gameOverText.gameObject.SetActive(false);
        gameOverImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object collided with is the designated golf ball
        if (other.gameObject == golfBall)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "You Have Putted The Ball ! \n Your Score is \n" + parText.text;

            gameOverImage.gameObject.SetActive(true);
            Time.timeScale = 0;  // Stop game time, effectively pausing the game
        }
    }
}
