using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [Header("Interactable Info")]
    public float sphereCastRadius = 0.5f;
    public int interactableLayerIndex;
    private Vector3 raycastPos;
    public GameObject lookObject;
    private PhysicsObject physicsObject;
    private Camera mainCamera;

    [Header("Pickup")]
    [SerializeField] private Transform pickupParent;
    public GameObject currentlyPickedUpObject;
    private Rigidbody pickupRB;

    [Header("ObjectFollow")]
    [SerializeField] private float minSpeed = 0f;
    [SerializeField] private float maxSpeed = 300f;
    [SerializeField] private float maxDistance = 10f;
    private float currentSpeed = 0f;
    private float currentDistance = 0f;

    [Header("Rotation")]
    public float rotationSpeed = 100f;
    Quaternion lookRot;

    // Start is called before the first frame update
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(pickupParent.position, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        raycastPos = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;
        if (Physics.SphereCast(raycastPos, sphereCastRadius, mainCamera.transform.forward, out hit, maxDistance, 1 << interactableLayerIndex))
        {
            lookObject = hit.collider.transform.root.gameObject;
        }
        else
        {
            lookObject = null;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (currentlyPickedUpObject == null)
            {
                if (lookObject != null)
                {
                    PickUpObject();
                }
            }
            else if (currentlyPickedUpObject != null)
            {
                ThrowObject();
            }
            else
            {
                BreakConnection();
            }
        }
        if (currentlyPickedUpObject != null && currentDistance > maxDistance) BreakConnection();
    }

    private void FixedUpdate()
    {
        if (currentlyPickedUpObject != null)
        {
            currentDistance = Vector3.Distance(pickupParent.position, pickupRB.position);
            currentSpeed = Mathf.SmoothStep(minSpeed, maxSpeed, currentDistance / maxDistance);
            currentSpeed *= Time.fixedDeltaTime;
            Vector3 direction = pickupParent.position - pickupRB.position;
            pickupRB.velocity = direction.normalized * currentSpeed;

            lookRot = Quaternion.LookRotation(mainCamera.transform.position - pickupRB.position);
            lookRot = Quaternion.Slerp(mainCamera.transform.rotation, lookRot, rotationSpeed * Time.fixedDeltaTime);
            pickupRB.MoveRotation(lookRot);
        }
    }

    public void ThrowObject()
    {
        pickupRB.constraints = RigidbodyConstraints.None;
        pickupRB.AddForce(mainCamera.transform.forward * 250);
        currentlyPickedUpObject = null;
        physicsObject.pickedUp = false;
        currentDistance = 0;
    }

    public void BreakConnection()
    {
        pickupRB.constraints = RigidbodyConstraints.None;
        currentlyPickedUpObject = null;
        physicsObject.pickedUp = false;
        currentDistance = 0;
    }

    public void PickUpObject()
    {
        physicsObject = lookObject.GetComponentInChildren<PhysicsObject>();
        currentlyPickedUpObject = lookObject;
        pickupRB = currentlyPickedUpObject.GetComponent<Rigidbody>();
        pickupRB.constraints = RigidbodyConstraints.FreezeRotation;
        physicsObject.playerInteractions = this;
        StartCoroutine(physicsObject.PickUp());
    }


}

