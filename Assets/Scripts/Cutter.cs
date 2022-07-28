using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : MonoBehaviour
{

    private Vector3 startPos;
    private GameObject selectedObject;
    private Camera ortoCam;
    private Vector3 newPos;
    private Transform fruitObject;
    private FruitController fruitController;
    private ObjectManager objectManager;
    private int cuttingCount;
    private int cuttingIndex;

    [SerializeField]
    private List<Transform> fruitPiecesList = new List<Transform>();
    [SerializeField]
    private List<Transform> lineList = new List<Transform>();


    private void Start()
    {
        startPos = transform.position;

        objectManager = ObjectManager.Instance;
        ortoCam = objectManager.OrthoCamera;
        fruitObject = objectManager.FruitObject;
        fruitController = fruitObject.GetComponent<FruitController>();

        for (int i = 0; i < fruitController.FruitPiece.Count; i++)
        {
            fruitPiecesList.Add(fruitController.FruitPiece[i]);
        }

        for (int i = 0; i < objectManager.LinesParent.childCount; i++)
        {
            lineList.Add(objectManager.LinesParent.GetChild(i));
        }

    }

    private void Update()
    {

        if (Input.GetMouseButton(0))
        {
            newPos = ortoCam.ScreenToWorldPoint(Input.mousePosition);
            newPos.y *= 1.2f;
            newPos.z = 0;
            transform.position = newPos;
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (selectedObject == null)
        //    {
        //        RaycastHit hit = CastRay();

        //        if (hit.collider != null)
        //        {
        //            if (!hit.collider.CompareTag("CutterTag"))
        //            {
        //                return;
        //            }
        //            selectedObject = hit.collider.gameObject;
        //        }
        //    }

        //}

        //if (Input.GetMouseButtonUp(0))
        //{
        //    selectedObject = null;
        //    transform.position = startPos;
        //}

        //if (selectedObject != null && Input.GetMouseButton(0))
        //{
        //    Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
        //    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        //    selectedObject.transform.position = new Vector3(worldPosition.x, worldPosition.y, worldPosition.z);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.transform.CompareTag("Cutable"))
        {
            cuttingCount++;
            other.transform.GetComponent<BoxCollider>().enabled = false;
            if (cuttingCount > 1)
            {
                fruitPiecesList[cuttingIndex].GetComponent<Rigidbody>().isKinematic = false;
                lineList[cuttingIndex].gameObject.SetActive(false);

                if (cuttingIndex < lineList.Count - 1)
                {
                    lineList[cuttingIndex + 1].gameObject.SetActive(true);
                }

                if (cuttingIndex == lineList.Count - 1)
                {
                    Transform lastPiece = fruitPiecesList[cuttingIndex + 1];
                    DOVirtual.DelayedCall(0.3f, () =>
                    {
                        lastPiece.GetComponent<Rigidbody>().isKinematic = false;
                    });

                    DOVirtual.DelayedCall(1f, () =>
                    {
                        Destroy(fruitObject.gameObject);
                        objectManager.MainCamera.DOMove(objectManager.CamereaPos2.position, 0.5f).SetEase(Ease.Linear);
                        objectManager.MainCamera.DORotate(objectManager.CamereaPos2.eulerAngles, 0.5f).SetEase(Ease.Linear);
                        transform.GetComponent<BoxCollider>().enabled = false;
                        GameManager.Instance.OrderPlateReady();
                        
                    });
                }

                cuttingIndex++;
                cuttingCount = 0;
            }
        }
    }

    //private RaycastHit CastRay()
    //{
    //    Vector3 screenMousePosFar = new Vector3(
    //        Input.mousePosition.x,
    //        Input.mousePosition.y,
    //        Camera.main.farClipPlane);
    //    Vector3 screenMousePosNear = new Vector3(
    //        Input.mousePosition.x,
    //        Input.mousePosition.y,
    //        Camera.main.nearClipPlane);
    //    Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
    //    Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
    //    RaycastHit hit;
    //    Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

    //    return hit;
    //}
}
