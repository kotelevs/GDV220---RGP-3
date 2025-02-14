using MPUIKIT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightPickup : MonoBehaviour, IInteractable
{
    [field: SerializeField] public GameObject interactUI { get; set; }
    [field: SerializeField] public bool bCanInteract { get; set; } = true;
    [field: SerializeField] public bool bInteracting { get; set; } = false;

    public GameObject flashlightUI;

    public AudioSource audioSource;

    public GameObject scrollingTextPF;

    // Update is called once per frame
    void Update()
    {
        // if this object isn't the current closest object to the player OR the interact option is in progress disable canvas
        if (PlayerInteract.instance.interactableObject != gameObject)
        {
            interactUI.SetActive(false);
        }
    }

    public void Interact()
    {
        if (!bInteracting)
        {
            bCanInteract = false;
            audioSource.Play();
            UIInteractPrompt.instance.Interact(UIInteractPrompt.instance.testFlashlight, Vector3.one, "Flashlight", "Those who live in the dark can't stand the light.");
            UIInteractPrompt.instance.flashlightPickupButton.SetActive(true);

        }
    }

    public void CloseInteractWindow()
    {
        UIInteractPrompt.instance.Close();
        flashlightUI.SetActive(true);
        Flashlight.instance.bHasFlashLight = true;
        // here is where you find the selected item

        var newFoundText = Instantiate(scrollingTextPF, gameObject.transform.position, Quaternion.identity);
        newFoundText.GetComponent<ScrollingUpTextUI>().textToDisplay = "+ Flashlight";

        Destroy(gameObject);
    }
}
