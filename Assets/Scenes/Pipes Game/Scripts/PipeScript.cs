using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeScript : MonoBehaviour
{
    private bool isConnectedToStartNode;

    // Start is called before the first frame update
    void Start()
    {

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
