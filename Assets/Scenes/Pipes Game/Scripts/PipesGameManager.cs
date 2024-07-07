using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using System.Linq;
using System.Runtime.CompilerServices;

public class PipesGameManager : MonoBehaviour
{
    public GameObject StartingNode;
    public GameObject EndingNode;
    public GameObject StraightPipe;
    public GameObject TPipe;
    public GameObject TurningPipe;

    private GameObject[] Pipes;
    private List<string> challenges = new List<string>
    {
        "0 2 1 1 2 1 3 2 3 2 2 3 1 1 1 3 1 3 2 2 2 3 2 4 1",
        "2 0 1 1 2 2 3 1 3 2 1 3 3 2 1 2 1 3 1 2 4 1 1 1 3",
        "2 1 1 1 0 2 2 3 2 1 1 3 1 3 2 2 3 3 1 2 4 1 1 1 3",
        "2 0 1 1 2 1 3 3 1 3 3 2 1 2 1 1 3 3 1 2 4 1 1 1 3",
        "0 1 1 1 2 2 3 3 2 1 1 3 1 3 2 2 3 1 3 2 4 1 1 1 3"
    };
    

    // Start is called before the first frame update
    void Start()
    {
        int randomNumber = Random.Range(0, 5);
        int[] numbers = challenges[randomNumber].Split(' ')
                         .Select(int.Parse)
                         .ToArray();
        Pipes = new GameObject[numbers.Length];
        for (int i = 0; i < numbers.Length; i++)
        {
            int y = (4 - i/5);
            int x = i - (i/5)*5;
            Pipes[i] = numbers[i] switch
            {
                0 => Instantiate(StartingNode, new Vector2((float)x, (float)y), Quaternion.identity),
                1 => Instantiate(StraightPipe, new Vector2((float)x, (float)y), Quaternion.identity),
                2 => Instantiate(TurningPipe, new Vector2((float)x, (float)y), Quaternion.identity),
                3 => Instantiate(TPipe, new Vector2((float)x, (float)y), Quaternion.identity),
                _ => Instantiate(EndingNode, new Vector2((float)x, (float)y), Quaternion.identity)
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
