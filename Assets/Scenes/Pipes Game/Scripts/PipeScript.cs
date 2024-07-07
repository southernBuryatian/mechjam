using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    private bool isConnectedToStartNode;
    float[] rotations = { 0, 90, 180, 270 };
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, rotations.Length);
        gameObject.transform.Rotate(0, 0, 90, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        turn();
    }

    public virtual void turn()
    {
        gameObject.transform.Rotate(0, 0, 90, Space.Self);
    }
}
