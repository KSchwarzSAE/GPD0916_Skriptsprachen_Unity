using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{

    public Transform m_nextBuildingElementLocation;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Place(PrimitiveType.Cube);

        if (Input.GetKeyDown(KeyCode.W))
            MoveForward();

        if (Input.GetKeyDown(KeyCode.S))
            MoveBack();
        
        if (Input.GetKeyDown(KeyCode.A))
            MoveLeft();

        if (Input.GetKeyDown(KeyCode.D))
            MoveRight();

        if (Input.GetKeyDown(KeyCode.Q))
            MoveUp();

        if (Input.GetKeyDown(KeyCode.E))
            MoveDown();
    }

    public void Place(PrimitiveType _type)
    {
        GameObject obj = GameObject.CreatePrimitive(_type);
        obj.transform.position = m_nextBuildingElementLocation.position;
        // obj.transform.rotation = m_nextBuildingElementLocation.rotation;
    }

    public void MoveLeft()
    {
        transform.position = transform.position - transform.right;
    }

    public void MoveRight()
    {
        transform.position = transform.position + transform.right;
    }

    public void MoveForward()
    {
        transform.position = transform.position + transform.forward;
    }

    public void MoveBack()
    {
        transform.position = transform.position - transform.forward;
    }

    public void MoveUp()
    {
        transform.position = transform.position + transform.up;
    }

    public void MoveDown()
    {
        transform.position = transform.position - transform.up;
    }

}
