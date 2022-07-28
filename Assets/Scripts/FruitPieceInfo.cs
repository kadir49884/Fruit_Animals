using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PieceType
{
    piece0,
    piece1,
    piece2,
    piece3,
    piece4,
    piece5,
}

public class FruitPieceInfo : MonoBehaviour
{

    [SerializeField]
    private PieceType pieceType;

    public PieceType PieceType1 { get => pieceType; set => pieceType = value; }

}
