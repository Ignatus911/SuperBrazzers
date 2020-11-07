using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PointsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnedText;
    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] public Camera mainCamera;
    private Canvas myCanvas;


    private void Awake()

    {
        myCanvas = GetComponent<Canvas>();
    }

    public void SpawnPoints(int points, Transform positionInWorldSpace)
    {
        var textOnScreen = InstantiateTextOnCanvas(positionInWorldSpace);
        textOnScreen.GetComponentInChildren<Text>().text = points.ToString();
    }

    public void Spawn1UP(Transform positionInWorldSpace)
    {
        var textOnScreen = InstantiateTextOnCanvas(positionInWorldSpace);
        textOnScreen.GetComponentInChildren<Text>().text = "1UP";
    }

    private GameObject InstantiateTextOnCanvas(Transform positionInWorldSpace)
    {
        Vector2 viewportPosition = mainCamera.WorldToViewportPoint(positionInWorldSpace.position);
        Vector2 proportionalPosition = new Vector2(viewportPosition.x * canvasRectTransform.sizeDelta.x, viewportPosition.y * canvasRectTransform.sizeDelta.y);
        Vector2 positionOncanvas = (proportionalPosition * myCanvas.scaleFactor);
        var textOnScreen = Instantiate(spawnedText, canvasRectTransform);
        textOnScreen.GetComponent<RectTransform>().position = positionOncanvas;
        return textOnScreen;
    }
}
