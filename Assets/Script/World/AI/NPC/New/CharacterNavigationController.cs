using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNavigationController : MonoBehaviour
{

    [SerializeField] public Animator _animator;
    [SerializeField] public Vector3 destination;
    public Vector3 lastPosition;
    [SerializeField] public bool reachedDestination;
    [SerializeField] public float stopDistance = 1;
    [SerializeField] public float rotationSpeed;
    [SerializeField] public float minSpeed, maxSpeed;
    public float movementSpeed;
    public Vector3 velocity;

    public void Start()
    {
        movementSpeed = Random.Range(minSpeed, maxSpeed);
    }

    public void Update()
    {
        if (transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;

            float destinationDistance = destinationDirection.magnitude;

            if (destinationDistance >= stopDistance)
            {
                reachedDestination = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
            else
            {
                reachedDestination = true;
            }

            velocity = (transform.position - lastPosition) / Time.deltaTime;
            velocity.y = 0;
            var velocityMagnitude = velocity.magnitude;
            //lastPosition = transform.position;
            velocity = velocity.normalized;
            var fwdDotProduct = Vector3.Dot(transform.forward, velocity);
            var rightDotProduct = Vector3.Dot(transform.right, velocity);

            _animator.SetFloat("Horizontal", rightDotProduct);
            _animator.SetFloat("Forward", fwdDotProduct);
        }
    }

    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }
}