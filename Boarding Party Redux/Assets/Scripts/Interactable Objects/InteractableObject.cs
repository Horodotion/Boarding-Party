using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    [Header("Interactable Object")]
    public bool onContact;
    public Collider triggerZone;
    public bool interacted;

    public virtual void Awake()
    {
        foreach (Collider col in GetComponents<BoxCollider>())
        {
            if (col.isTrigger)
            {
                triggerZone = col;
                break;
            }
        }
    }

    public virtual void OnTriggerEnter(Collider col)
    {
        if (!interacted && col.gameObject.tag == "Player" && col.gameObject.GetComponent<PlayerController>() != null)
        {
            if (onContact == true)
            {
                Interact(col.gameObject.GetComponent<PlayerController>());
            }
        }
    }

    public virtual void Interact(PlayerController player)
    {
        interacted = true;
    }
}
