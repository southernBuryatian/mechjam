using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using System.Linq;
using System.Runtime.CompilerServices;
using System;

public class PipesGameManager : MonoBehaviour, IRotationListener
{
    public GameObject StartingNode;
    public GameObject EndingNode;
    public GameObject StraightPipe;
    public GameObject TPipe;
    public GameObject TurningPipe;

    private GameObject[] Pipes;
    private int startingCoordinate;
    private int endingCoordinatedinate;
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
        int randomNumber = UnityEngine.Random.Range(0, 8);
        string currentChallenge = challenges[randomNumber];
        Pipes = new GameObject[currentChallenge.Length];
        for (int i = 0; i < currentChallenge.Length; i++)
        {
            int y = (4 - i/5);
            int x = i - (i/5)*5;
            switch (currentChallenge[i])
            {
                case '0':
                    Pipes[i] = Instantiate(StartingNode, new Vector2((float)x, (float)y), Quaternion.identity);
                    startingCoordinate = i;
                    break;
                case '1':
                    Pipes[i] = Instantiate(StraightPipe, new Vector2((float)x, (float)y), Quaternion.identity);
                    Pipes[i].GetComponent<PipeScript>().SetActiveSides(new int[] { 1, 3 }); 
                    break;
                case '2':
                    Pipes[i] = Instantiate(TurningPipe, new Vector2((float)x, (float)y), Quaternion.identity);
                    Pipes[i].GetComponent<PipeScript>().SetActiveSides(new int[] { 1, 2 });
                    break;
                case '3':
                    Pipes[i] = Instantiate(TPipe, new Vector2((float)x, (float)y), Quaternion.identity);
                    Pipes[i].GetComponent<PipeScript>().SetActiveSides(new int[] { 1, 2, 3 });
                    break;
                default:
                    Pipes[i] = Instantiate(EndingNode, new Vector2((float)x, (float)y), Quaternion.identity);
                    endingCoordinatedinate = i;
                    break;
            }
            IRotatable maybyRotatable = Pipes[i].GetComponent<IRotatable>();
            if (maybyRotatable != null)
            {
                maybyRotatable.SetRotationListener(this);
                maybyRotatable.SetPosition(i);
            }
        }
        CheckIsComplete();
    }

    void IRotationListener.OnRotationChanged(int position)
    {
        if (CheckIsComplete())
        {
            Debug.Log($"Game is finished successfully.");
        }
        IRotatable maybyRotatable = Pipes[position].GetComponent<IRotatable>();
    }

    private bool CheckIsComplete()
    {
        throw new NotImplementedException();
    }
}
