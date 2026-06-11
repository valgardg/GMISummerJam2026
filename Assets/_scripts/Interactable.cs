using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public GameObject interactionPromptPrefab;
    public float promptYOffset = 1.5f;

    [Header("Interaction event")]
    public UnityEvent onInteract;

    private GameObject promptInstance;

    public void ToggleInteractionPrompt()
    {
        if (promptInstance == null)
        {
            Canvas canvas = FindFirstObjectByType<Canvas>();
            promptInstance = Instantiate(interactionPromptPrefab, transform.position + Vector3.up * promptYOffset, Quaternion.identity, canvas.transform);
        }
        else
        {
            Destroy(promptInstance);
            promptInstance = null;
        }
    }

    public void Interact()
    {
        onInteract?.Invoke();
    }
}
