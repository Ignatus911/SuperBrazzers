using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PointsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnedText;
    [SerializeField] private RectTransform canvasRectTransform;
    [SerializeField] public Camera mainCamera;
    private RectTransform rectTransform;
    private Vector2 uiOffset;


    private void Awake()

    {
        rectTransform = GetComponent<RectTransform>();
        uiOffset = new Vector2((float)canvasRectTransform.sizeDelta.x / 2f, (float)canvasRectTransform.sizeDelta.y / 2f);
        Debug.Log("Canvas center = " + uiOffset.x + "  " + uiOffset.y);
    }

    public void SpawnPoints(int points, Transform positionInWorldSpace)
    {
        //
        Vector2 viewportPosition = mainCamera.WorldToViewportPoint(positionInWorldSpace.position);
        Debug.Log("ViewportPosition = " + viewportPosition.x + "  " + viewportPosition.y);
        Vector2 proportionalPosition = new Vector2(viewportPosition.x * canvasRectTransform.sizeDelta.x, viewportPosition.y * canvasRectTransform.sizeDelta.y);
        Debug.Log("proportionalPosition = " + proportionalPosition.x + "  " + proportionalPosition.y);
        rectTransform.localPosition = proportionalPosition - uiOffset;
        Debug.Log("result localposition = " + rectTransform.localPosition.x + "  " + rectTransform.localPosition.y);
        //
        spawnedText.GetComponentInChildren<Text>().text = points.ToString();
        spawnedText.GetComponent<RectTransform>().position = rectTransform.localPosition;
        var textOnScreen = Instantiate(spawnedText, canvasRectTransform);
        //textOnScreen.GetComponent<AnimatedText>().SetPosition(rectTransform.localPosition);
        Debug.Log("result worldposition = " + rectTransform.position.x + "  " + rectTransform.position.y);
        //public static Object Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);
    }

    public void Spawn1UP()
    {
        GetComponent<Text>().text = "1UP";
    }
}
