using System;
using System.Collections.Generic;
using UnityEngine;

public class PipesGameManager : MonoBehaviour
{
    public GameObject StartingNode;
    public GameObject EndingNode;
    public GameObject StraightPipe;
    public GameObject TPipe;
    public GameObject TurningPipe;

    private const int DEFAULT_GRID_POSITION = -1;
    private GameObject[] Pipes;
    private int startingCoordinate;
    private int endingCoordinatedinate;

    private Queue<int> pipeIdToProccess = new Queue<int>();
    private HashSet<int> activePipeIds = new HashSet<int>();

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
            PipeScript maybyPipeScript = Pipes[i].GetComponent<PipeScript>();
            if (maybyPipeScript != null)
            {
                maybyPipeScript.SetRotationListener(this);
                maybyPipeScript.SetPosition(i);
            }
        }
        if (CheckIsComplete())
        {
            ClearData();
            Start();
        }
    }

    internal void OnRotationChanged(int position)
    {
        if (CheckIsComplete())
        {
            Debug.Log($"Game is finished successfully.");
            ClearData();
        }
    }

    private bool CheckIsComplete()
    {
        pipeIdToProccess.Clear();
        activePipeIds.Clear();  
        pipeIdToProccess = new Queue<int>();
        activePipeIds = new HashSet<int>();
        activePipeIds.Add(startingCoordinate);
        pipeIdToProccess.Enqueue(startingCoordinate);
        return ProccessPipeId();
    }

    private bool ProccessPipeId()
    {   
        if (pipeIdToProccess.Count > 0)
        {
            int idToProcess = pipeIdToProccess.Dequeue();
            Debug.Log($"pipeIdToProccess.Dequeue {idToProcess}.");
            List<int> sidesToCheck = GetActiveSidesForId(idToProcess);
            for (int i = 0; i < sidesToCheck.Count; i++)
            {
                Debug.Log($"sideToCheck {sidesToCheck[i]} for {idToProcess}.");
                int coordinateToCheckConnection = sidesToCheck[i] switch
                {
                    0 => idToProcess - 5,
                    1 => idToProcess + 1,
                    2 => idToProcess + 5,
                    _ => idToProcess - 1
                };
                if (coordinateToCheckConnection == endingCoordinatedinate)
                {
                    return true;
                }
                Debug.Log($"ÑoordinateToCheckConnection {coordinateToCheckConnection}.");
                if (!activePipeIds.Contains(coordinateToCheckConnection))
                {
                    if (Pipes[coordinateToCheckConnection].GetComponent<PipeScript>().IsConnectedToSide(sidesToCheck[i]))
                    {
                        Debug.Log($"ÑoordinateToCheckConnection {coordinateToCheckConnection} connected.");
                        activePipeIds.Add(coordinateToCheckConnection);
                        pipeIdToProccess.Enqueue(coordinateToCheckConnection);
                    }
                }
            }
            return ProccessPipeId();
        } else return false;
    }

    private List<int> GetActiveSidesForId(int idToProcess)
    {
        if (idToProcess == startingCoordinate)
        {
            return idToProcess switch
            {
                0 => new List<int> { 1, 2 },
                4 => new List<int> { 2, 3 },
                _ => new List<int> { 1, 2, 3 }
            };
        } else
        {
            int y = (4 - idToProcess / 5);
            int x = idToProcess - (idToProcess / 5) * 5;
            List<int> result = Pipes[idToProcess].GetComponent<PipeScript>().GetActiveAnglesForCurrectRotation();
            if (y == 4)
            {
                result.Remove(0);
            }
            if (y == 0)
            {
                result.Remove(2);
            }
            if (x == 0)
            {
                result.Remove(3);
            }
            if (x == 4)
            {
                result.Remove(1);
            }
            return result;
        }
    }

    private void ClearData()
    {
        for (int i = 0; i < Pipes.Length; i++) Destroy(Pipes[i]);
        Array.Clear(Pipes, 0, Pipes.Length);
        startingCoordinate = DEFAULT_GRID_POSITION;
        endingCoordinatedinate = DEFAULT_GRID_POSITION;
        pipeIdToProccess.Clear();
        activePipeIds.Clear();
    }
}
