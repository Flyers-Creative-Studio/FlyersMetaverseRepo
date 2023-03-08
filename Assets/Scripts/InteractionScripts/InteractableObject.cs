using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField] private List<string> tagsOfInteractableObjects = new List<string>() { "Player" };


    private void OnTriggerEnter(Collider other)
    {
        if (tagsOfInteractableObjects.Contains(other.tag)) OnInteraction(InteractionType.enter, other.tag);
    }

    private void OnTriggerExit(Collider other)
    {
        if (tagsOfInteractableObjects.Contains(other.tag)) OnInteraction(InteractionType.exit, other.tag);
    }
    private void OnTriggerStay(Collider other)
    {
        if (tagsOfInteractableObjects.Contains(other.tag)) OnInteraction(InteractionType.stay, other.tag);
    }

    public abstract void OnInteraction(InteractionType interactionType, string tag);

}
