using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class EnergyBarColorChange : MonoBehaviour
{
    public Slider slider; // Reference to the Slider component
    public Image fillImage; // The Image component of the slider's fill area

    // Sprites corresponding to different fill colors
    public Sprite redSprite;   // Sprite for red color
    public Sprite yellowSprite; // Sprite for yellow color
    public Sprite greenSprite;  // Sprite for green color

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Updates the sprite based on the current value of the slider
        UpdateFillSprite(slider.value / slider.maxValue);
    }

    void UpdateFillSprite(float percentage)
    {
        // Change the sprite based on the percentage value of the slider
        if (percentage < 0.33f)
        {
            fillImage.sprite = redSprite; // Use red sprite if below 33%
        }
        else if (percentage < 0.66f)
        {
            fillImage.sprite = yellowSprite; // Use yellow sprite if between 33% and 66%
        }
        else
        {
            fillImage.sprite = greenSprite; // Use green sprite if above 66%
        }
    }
}
