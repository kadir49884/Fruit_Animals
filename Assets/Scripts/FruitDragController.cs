using UnityEngine;
using System.Collections;
using DG.Tweening;



public enum DragPieceType
{
    piece0,
    piece1,
    piece2,
    piece3,
    piece4,
    piece5,
}

public class FruitDragController : MonoBehaviour
{

    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 startPos;
    private bool isStatic;


    

    [SerializeField]
    private DragPieceType dragPieceType;

    public DragPieceType DragPieceType1 { get => dragPieceType; set => dragPieceType = value; }

    private GameManager gameManager;


    private void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.OrderPlateReady += OrderPlateReady;
    }

    private void OrderPlateReady()
    {
        DOVirtual.DelayedCall(1f, () =>
        {
            transform.GetComponent<MeshCollider>().isTrigger = true;
            transform.GetComponent<Rigidbody>().isKinematic = true;
            startPos = transform.position;
        });
    }


    void OnMouseDown()
    {

        if (isStatic)
        { 
            return; 
        }

        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        offset.y = startPos.y;
    }

    void OnMouseDrag()
    {


        if (isStatic)
        {
            return;
        }

        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        curPosition.y = startPos.y;
        transform.position = curPosition;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<FruitPieceInfo>() == null)
        {
            return;
        }
        if ((int)other.gameObject.GetComponent<FruitPieceInfo>().PieceType1 == (int)dragPieceType)
        {
            transform.GetComponent<MeshCollider>().enabled = false;
            other.gameObject.GetComponent<MeshCollider>().enabled = false;


            transform.DORotate(other.transform.eulerAngles, 0.2f);

            transform.DOMove(other.transform.position, 0.3f);

            DOVirtual.DelayedCall(0.1f, () =>
            {
                transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.1f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    transform.DOScale(new Vector3(1, 1, 1), 0.1f).SetEase(Ease.Linear);
                });
                other.gameObject.SetActive(false);
                isStatic = true;
            });

            
        }
    }

}