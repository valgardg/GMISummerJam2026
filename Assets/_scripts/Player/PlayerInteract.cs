using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private Interactable currentInteractable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentInteractable?.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var interactable = other.GetComponent<Interactable>();

        if (interactable != null)
        {
            if (currentInteractable != null && currentInteractable != interactable)
            {
                currentInteractable.ToggleInteractionPrompt();
            }

            currentInteractable = interactable;
            interactable.ToggleInteractionPrompt();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var interactable = other.GetComponent<Interactable>();

        if (interactable == currentInteractable)
        {
            interactable.ToggleInteractionPrompt();
            currentInteractable = null;
        }
    }
}