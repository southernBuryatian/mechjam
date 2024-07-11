using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    private const int INITIAL_GRID_POSITION = -1;
    private PipesGameManager gameManager;
    private int gridPosition = INITIAL_GRID_POSITION;
    private int[] activeSides;

    void Start()
    {
        int rand = Random.Range(0, 3);
        gameObject.transform.Rotate(0, 0, rand*90, Space.Self);
    }

    internal void SetActiveSides(int[] activeSides)
    {
        this.activeSides = activeSides;
    }

    internal void SetPosition(int position)
    {
        gridPosition = position;
    }

    internal void SetRotationListener(PipesGameManager listener)
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

    private void OnMouseDown()
    {
        turn();
    }

    private void turn()
    {
        gameObject.transform.Rotate(0, 0, 90, Space.Self);
        gameObject.GetComponent<AudioSource>().Play();
        if (gameManager != null) gameManager.OnRotationChanged(gridPosition);
    }

    private int GetRotation()
    {
        return (int) gameObject.transform.eulerAngles.z/1;
    }
}
