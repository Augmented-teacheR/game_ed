using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * <summary>
 * Enum that defines the type of movement the car has.
 * </summary>
 */
public enum MovementType
{
    time, velocity, distance, finished
}
/**
 * <summary>
 * Contains the behaviour of the car.
 * Can determine the velocity, distance and time the car will have,
 * depending on the variables provided by the user.
 * </summary>
 */
public class Car : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector3 position;

    [SerializeField]
    private float distance = 0;
    [SerializeField]
    private float time = 0;
    [SerializeField]
    private float velocity = 0;
    [SerializeField]
    private float defaultVelocity = 5;

    [SerializeField]
    private MovementType state = MovementType.finished;
    private bool isTimerCounting = false;
    private bool isMovingToDistance = false;

    private IEnumerator timerCorutine;
    private IEnumerator distanceCorutine;

    private AudioSource audioSource;
    public AudioClip driveSound, crashSound, clapSound;

    private bool isCarAudioStarted = false;


    private void Awake()
    {
        position = transform.localPosition;
        timerCorutine = StartTimer();
        distanceCorutine = WaitForDistance();
        audioSource = GetComponent<AudioSource>();
    }

    /**
     * <summary>
     * Defines the distance, time and velocity of the car and
     * calls for the state of the car to be set.
     * <param name="distance">Max traveling distance for the car.</param>
     * <param name="time">Max moving time for the car.</param>
     * <param name="velocity">Speed set for the car.</param>
     * </summary>
     */
    public void Go(float distance, float time, float velocity)
    {
        this.distance = distance != 0 ? distance : this.distance;
        this.time = time != 0 ? time : this.time;
        this.velocity = velocity != 0 ? velocity : this.velocity;

        this.state = SetState(this.distance, this.time, this.velocity);
    }

    /**
     * <summary>
     * Chooses the corresponding state for the car, depending on the parameters.
     * </summary>
     * <param name="distance">Max traveling distance for the car.</param>
     * <param name="time">Max moving time for the car.</param>
     * <param name="velocity">Speed set for the car.</param>
     */
    private MovementType SetState(float distance, float time, float velocity)
    {
        if(distance != 0 && time != 0)
        {
            speed = distance / time;
            return MovementType.velocity;
        }
        if (velocity != 0)
        {
            speed = velocity;
            return MovementType.velocity;
        }
        if(distance != 0)
        {
            speed = defaultVelocity;
            return MovementType.distance;
        }
        if(time != 0)
        {
            speed = defaultVelocity;
            return MovementType.time;
        }
        return MovementType.finished;
    }

    private void Update()
    {
        if(state != MovementType.finished)
        {
            StartMovement();
        }
    }

    /**
     * <summary>
     * Considering the MovementState of the car,
     * calls the corresponding kind of movement.
     * </summary>
     */
    private void StartMovement()
    {
        if (!isCarAudioStarted)
        {
            audioSource.clip = driveSound;
            audioSource.Play();
            Debug.Log("AUDIO PLAYING");
            isCarAudioStarted = true;
        }
        switch (state)
        {
            case MovementType.velocity:
                if(distance == 0 && time == 0)
                {
                    VelocityDependantMovement();
                }
                else
                {
                    TimeAndDistanceDependantMovement();
                }
                break;
            case MovementType.time:
                TimeDependantMovement();
                break;
            case MovementType.distance:
                DistanceDependantMovement();
                break;
            case MovementType.finished:
                break;
        }
    }

    /**
     * <summary>
     * Moves the car considering the set speed.
     * </summary>
     */
    private void VelocityDependantMovement()
    {
        float x = transform.localPosition.x;
        float y = transform.localPosition.y;
        float z = transform.localPosition.z + speed * Time.deltaTime;

        transform.localPosition = new Vector3(x, y, z);
    }
    /**
     * <summary>
     * Call for the car to move at a certain velocity and starts a timer to stop it.
     * </summary>
     */
    private void TimeDependantMovement()
    {
        VelocityDependantMovement();
        if (!isTimerCounting) StartCoroutine(timerCorutine);
    }

    /**
     * <summary>
     * Timer to stop the car after a certain delay.
     * </summary>
     */
    private IEnumerator StartTimer()
    {
        isTimerCounting = true;
        yield return new WaitForSecondsRealtime(time);
        state = MovementType.finished;
        isTimerCounting = false;
        Debug.Log("Time Out");
        if (isMovingToDistance)
        {
            StopCoroutine(distanceCorutine);
            isMovingToDistance = false;
        }
    }
    /**
     * <summary>
     * Call for the car to move at a certain velocity and starts a corutine that stops it after a distance.
     * </summary>
     */
    private void DistanceDependantMovement()
    {
        VelocityDependantMovement();
        if (!isMovingToDistance) StartCoroutine(distanceCorutine);
    }
    /**
     * <summary>
     * This was a redundant function we did not notice.
     * </summary>
     */
    private void TimeAndDistanceDependantMovement()
    {
        VelocityDependantMovement();
        if (!isTimerCounting) StartCoroutine(timerCorutine);
    }

    /**
     * <summary>
     * Corutine that stops the car if it has driven a certain distance.
     * </summary>
     */
    private IEnumerator WaitForDistance()
    {
        isMovingToDistance = true;
        yield return new WaitUntil(() => Vector3.Distance(position, transform.position) > distance - 0.05);
        state = MovementType.finished;
        isMovingToDistance = false;
        Debug.Log("Distance Reached");
        if (isTimerCounting)
        {
            StopCoroutine(timerCorutine);
            isTimerCounting = false;
        }
    }
    /**
     * <summary>
     * When entering a trigger, the car is stopped and the outcome is decided.
     * </summary>
     */
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Obstacle") || other.gameObject.tag.Equals("SuccessArea"))
        {
            if(other.gameObject.tag.Equals("Obstacle"))
            {
                audioSource.clip = crashSound;
                audioSource.Play();
            }
            else
            {
                audioSource.clip = clapSound;
                audioSource.Play();
            }
            state = MovementType.finished;
            StartCoroutine(EndScene());
        }

    }

    private IEnumerator EndScene()
    {
        yield return new WaitForSecondsRealtime(2);
        SceneManager.LoadScene("EndScreen");
    }
}
