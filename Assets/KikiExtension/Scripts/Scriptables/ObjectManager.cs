using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    [SerializeField]
	private Camera orthoCamera;

    public Camera OrthoCamera { get => orthoCamera; set => orthoCamera = value; }
    public Transform FruitObject { get => fruitObject; set => fruitObject = value; }
    public Transform LinesParent { get => linesParent; set => linesParent = value; }
    public Transform MainCamera { get => mainCamera; set => mainCamera = value; }
    public Transform CamereaPos2 { get => camereaPos2; set => camereaPos2 = value; }
    public Transform OrderPlate { get => orderPlate; set => orderPlate = value; }

    [SerializeField]
    private Transform fruitObject;

    [SerializeField]
    private Transform linesParent;

    [SerializeField]
    private Transform mainCamera;
    [SerializeField]
    private Transform camereaPos2;
    [SerializeField]
    private Transform orderPlate;


}