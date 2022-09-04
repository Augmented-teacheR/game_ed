using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Car car;
    [SerializeField]
    private Train train;
    [SerializeField]
    private CodeBlock codeBlock;

    public Car Car { get => car; set => car = value; }
    public Train Train { get => train; set => train = value; }
    public CodeBlock CodeBlock { get => codeBlock; set => codeBlock = value; }

    public void play()
    {
        car.Go(0,0,5);
        train.Beggin();
        Debug.Log("Button clicked");
    }
}
