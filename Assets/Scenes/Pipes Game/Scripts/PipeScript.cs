using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PipeScript : MonoBehaviour, IRotatable
{
    private IRotationListener gameManager;
    private int gridPosition = -1;
    private bool isConnectedToStartNode;
    private int[] activeSides;

    public void SetActiveSides(int[] activeSides)
    {
        this.activeSides = activeSides;
    }

    void SetGameManage(PipesGameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    void SetGridPosition(int position)
    {
        gridPosition = position;
    }

    void Start()
    {
        int rand = Random.Range(0, 3);
        gameObject.transform.Rotate(0, 0, rand*90, Space.Self);
    }

    void Update()
    {

    }

    private void OnMouseDown()
    {
        turn();
    }

    private void turn()
    {
        gameObject.transform.Rotate(0, 0, 90, Space.Self);
        if (gameManager != null)
        {
            gameManager.OnRotationChanged(gridPosition);
        }
    }

    public void SetPosition(int position)
    {
        gridPosition = position;
    }

    private int GetRotation()
    {
        return (int) gameObject.transform.eulerAngles.z/1;
    }

    public void SetRotationListener(IRotationListener listener)
    {
        gameManager = listener;
    }

    internal List<int> GetActiveAnglesForCurrectRotation()
    {
        List<int> result = new List<int>();
        int currentRotation = GetRotation();
        Debug.Log($"Pipe currentRotation {currentRotation}.");
        int correctedRitation = currentRotation switch
        {
            0 => currentRotation,
            _ => 360 - currentRotation
        };
        for (int i = 0; i < activeSides.Length; i++)
        {
            int correctedSide = (activeSides[i] + correctedRitation / 90) % 4;
            Debug.Log($"Pipe correctedSide {correctedSide}.");
            result.Add(correctedSide);
        }
        return result;
    }

    internal bool IsConnectedToSide(int sideCode)
    {
        return sideCode switch
        {
            0 => GetActiveAnglesForCurrectRotation().Contains(2),
            1 => GetActiveAnglesForCurrectRotation().Contains(3),
            2 => GetActiveAnglesForCurrectRotation().Contains(0),
            _ => GetActiveAnglesForCurrectRotation().Contains(1)
        };
    }
}
