using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitController : MonoBehaviour
{

    [SerializeField]
    private List<Transform> fruitPiece = new List<Transform>();

    public List<Transform> FruitPiece { get => fruitPiece; set => fruitPiece = value; }
}
