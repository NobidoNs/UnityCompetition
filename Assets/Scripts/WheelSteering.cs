using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class VRSteeringWheel : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Transform interactorHand;
    private float initialAngle;
    private float currentSteeringAngle;

    [Header("Settings")]
    [SerializeField] private float maxSteeringAngle = 180f;
    [SerializeField] private float wheelReturnSpeed = 45f;
    [SerializeField] private bool smoothReturn = true;

    public float SteeringValue => Mathf.Clamp(currentSteeringAngle / maxSteeringAngle, -1f, 1f);

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        interactorHand = args.interactorObject.transform;
        initialAngle = CalculateHandAngle();
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        interactorHand = null;
    }

    private void Update()
    {
        if (interactorHand != null)
        {
            float handAngle = CalculateHandAngle();
            currentSteeringAngle = handAngle - initialAngle;
            currentSteeringAngle = Mathf.Clamp(currentSteeringAngle, -maxSteeringAngle, maxSteeringAngle);
            ApplyRotation();
        }
        else if (smoothReturn && currentSteeringAngle != 0)
        {
            currentSteeringAngle = Mathf.MoveTowards(currentSteeringAngle, 0, wheelReturnSpeed * Time.deltaTime);
            ApplyRotation();
        }
    }

    private float CalculateHandAngle()
    {
        Vector3 localHandDirection = transform.InverseTransformDirection(interactorHand.forward);
        return Mathf.Atan2(localHandDirection.x, localHandDirection.z) * Mathf.Rad2Deg;
    }

    private void ApplyRotation()
    {
        transform.localRotation = Quaternion.Euler(0, currentSteeringAngle, 0);
    }
}