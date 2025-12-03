using UnityEngine;
using UnityEngine.Windows;

interface IInteractable
{
    public void Interact();
}
public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractorRange;


    private ActionMap input;
    private void Awake()
    {
        input = new ActionMap();
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
    private void Update()
    {
        if (input.Player.Interact.WasPerformedThisFrame())
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractorRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)) 
                {
                    interactObj.Interact();
                }
            }
        }
    }
}
