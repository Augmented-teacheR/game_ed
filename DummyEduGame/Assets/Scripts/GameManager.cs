using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * <summary>
 * Organizes the level scene and is in charge of initiating movement in the scene.
 * </summary>
 */
public class GameManager : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField]
    private Car car;
    [SerializeField]
    private Train train;
    [SerializeField]
    private CodeBlock codeBlock;
    [SerializeField]
    private CarInteraction carInteraction;

    [Separator]
    [Header("Car Variables")]
    [SerializeField]
    private float carTime;
    [SerializeField]
    private float carDistance;
    [SerializeField]
    private float carVelocity;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    public Car Car { get => car; set => car = value; }
    public Train Train { get => train; set => train = value; }
    public CodeBlock CodeBlock { get => codeBlock; set => codeBlock = value; }
    public CarInteraction CarInteraction { get => carInteraction; set => carInteraction = value; }

    /**
     * <summary>
     * Starts the movement of the car and the train.
     * </summary>
     */
    private void Play()
    {
        SetCarValues();
        car.Go(carDistance, carTime, carVelocity);
        train.Begin();
        Debug.Log("Button clicked");
    }

    /**
     * <summary>
     * Sets the variables that define the car´s speed, max traveling distance  and max traveling time.
     * </summary>
     */
    private void SetCarValues()
    {
        codeBlock.ReadValues();
        this.carDistance = codeBlock.GetReadDistance();
        this.carTime = codeBlock.GetReadTime();
        this.carVelocity = codeBlock.GetReadVelocity();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /**
     * <summary>
     * When this corutine is called, it waits until the car is in the right
     * position and then initiates the car´s movement.
     * </summary>
     */
    public IEnumerator StartCar()
    {
        Debug.Log("Start car corutine called");
        yield return new WaitUntil(() => CarInteraction.PlayStarted);
        Debug.Log("Play Started");
        yield return new WaitForSeconds(3);
        Play();
    }
}