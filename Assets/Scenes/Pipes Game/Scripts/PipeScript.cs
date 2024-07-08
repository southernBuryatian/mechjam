using System.Collections;
using System.Collections.Generic;
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

    public void turn()
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

    public int GetRotation()
    {
        return (int) gameObject.transform.eulerAngles.z/1;
    }

    public void SetRotationListener(IRotationListener listener)
    {
        gameManager = listener;
    }
}
