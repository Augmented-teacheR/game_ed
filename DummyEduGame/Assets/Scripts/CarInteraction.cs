using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * <summary>
 * Class that defines how the interaction of an object (that contains
 * this component) will interact with a game object that has the "Car" tag.
 * </summary>
 */
public class CarInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject gameManager;
    private GameManager _gameManager;
    [SerializeField]
    private GameObject car;
    [SerializeField]
    private GameObject target;
    private bool isCarSet = false;
    private float startingCarPosition;
    private bool playStarted = false;

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();
    private float offset;

    [Header("Active Range")]
    [SerializeField]
    private float positiveLimit;
    [SerializeField]
    private float negativeLimit;
    [SerializeField]
    private float launchingPosition;


    public float StartingCarPosition { get => startingCarPosition; set => startingCarPosition = value; }
    public bool PlayStarted { get => playStarted; set => playStarted = value; }



    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        _gameManager = gameManager.GetComponent<GameManager>();
        _gameManager.CarInteraction = this;
        StartCoroutine(_gameManager.StartCar());
        StartCoroutine("FindTargetsWithDelay", .2f);
    }
    /**
     * <summary>
     * Corutine that calls the function to find the car object, every
     * set amount of seconds.
     * <param name="delay">Amount of time between checks.</param>
     * </summary>
     */
    IEnumerator FindTargetsWithDelay(float delay)
    {
        Debug.Log("Corutine started find target");
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindCar();
        }
    }
    /**
     * <summary>
     * Overlaping an invisible sphere over the object that has this script, 
     * it looks for the object with the tag "Car".
     * When found, sets the found object as the Car of the class.
     * </summary>
     */
    private void FindCar()
    {
        visibleTargets.Clear();

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            if (targetsInViewRadius[i].gameObject.tag.Equals("Car") && !isCarSet)
            {
                this.car = targetsInViewRadius[i].gameObject;
                StartingCarPosition = this.car.transform.localPosition.z;
                offset = this.car.transform.localPosition.x;
                isCarSet = true;
                Debug.Log("Car found");
            }
        }
    }

    private void Update()
    {
        MoveCar();
    }
    /**
     * <summary>
     * If the car has been found, the car will take a position relative to this object,
     * within the setted limits.
     * When the furthest back distance has been reached, the independent movement of
     * the car will begin.
     * </summary>
     */
    private void MoveCar()
    {
        if (isCarSet)
        {
            if ((car.transform.localPosition.z < StartingCarPosition + positiveLimit)
            && (car.transform.localPosition.z > StartingCarPosition + negativeLimit)
            && !PlayStarted)
            {
                car.transform.position = target.transform.position;
                car.transform.localPosition = new Vector3(offset, 0, car.transform.localPosition.z);
            }
            if (car.transform.localPosition.z < StartingCarPosition - .03)
            {
                PlayStarted = true;
            }
        }
    }

}
