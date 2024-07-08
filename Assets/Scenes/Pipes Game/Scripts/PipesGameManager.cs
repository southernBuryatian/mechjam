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
        "0211213232231113132223241",
        "2011223132133212131241113",
        "2111022321131322331241113",
        "2011213313321211331241113",
        "0111223321131322313241113",
        "0112222132311312211131411",
        "0211223313231212331121141",
        "0112223132311312131141131",
        "0112222132311312211141131"
    };
    

    // Start is called before the first frame update
    void Start()
    {
        int randomNumber = Random.Range(0, 8);
        string currentChallenge = challenges[randomNumber];
        Pipes = new GameObject[currentChallenge.Length];
        for (int i = 0; i < currentChallenge.Length; i++)
        {
            int y = (4 - i/5);
            int x = i - (i/5)*5;
            Pipes[i] = currentChallenge[i] switch
            {
                '0' => Instantiate(StartingNode, new Vector2((float)x, (float)y), Quaternion.identity),
                '1' => Instantiate(StraightPipe, new Vector2((float)x, (float)y), Quaternion.identity),
                '2' => Instantiate(TurningPipe, new Vector2((float)x, (float)y), Quaternion.identity),
                '3' => Instantiate(TPipe, new Vector2((float)x, (float)y), Quaternion.identity),
                _ => Instantiate(EndingNode, new Vector2((float)x, (float)y), Quaternion.identity)
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
