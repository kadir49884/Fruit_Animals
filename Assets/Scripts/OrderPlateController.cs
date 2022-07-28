using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderPlateController : MonoBehaviour
{
    

    void Start()
    {
        GameManager.Instance.OrderPlateReady += OrderPlateFunction;
    }

    private void OrderPlateFunction()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<Rigidbody>() != null)
            {
                transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }


}
