using UnityEngine;
using UnityEngine.UI;

public class SlidePanel : MonoBehaviour
{
    [SerializeField] public RectTransform panelToSlide;
    [SerializeField] public Button OpenMenuButton;
    [SerializeField] public Button MoveButton;
    [SerializeField] public Button RotateButton;
    [SerializeField] public Button DeleteButton;
    public Vector2 startPosition = new Vector2 (-350,0);
    public Vector2 endPosition = new Vector2 (0,0);

    public Vector2 StartPositionMoveButton = new Vector2 (600,700);
    public Vector2 EndPositionMoveButton = new Vector2(400, 700);
    public Vector2 StartPositionRotateButton = new Vector2(600,500);
    public Vector2 EndPositionRotateButton = new Vector2(400,500);
    public Vector2 StartPositionDeleteButton = new Vector2(600,300);
    public Vector2 EndPositionDeleteButton = new Vector2(400,300);
    public float slideDuration = 0.3f;
    private bool IsOpen = false;
    private float timer = 0f;
    private bool isSlidingRight = false;
    private bool isSlidingLeft = false;

    void Start()
    {
        // Ensure the panel is initially at the start position
        panelToSlide.anchoredPosition = startPosition;
        MoveButton.GetComponent<RectTransform>().anchoredPosition = StartPositionMoveButton;
        RotateButton.GetComponent<RectTransform>().anchoredPosition = StartPositionRotateButton;
        DeleteButton.GetComponent<RectTransform>().anchoredPosition = StartPositionDeleteButton;
        OpenMenuButton.onClick.AddListener(Slide);
    }

    void Update()
    {
        if (isSlidingRight)
        {
            timer += Time.deltaTime;
            float ratio = Mathf.Clamp01(timer / slideDuration);
            panelToSlide.anchoredPosition = Vector2.Lerp(startPosition, endPosition, ratio);
            MoveButton.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(StartPositionMoveButton, EndPositionMoveButton, ratio);
            RotateButton.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(StartPositionRotateButton, EndPositionRotateButton, ratio);
            DeleteButton.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(StartPositionDeleteButton, EndPositionDeleteButton, ratio);
            if (ratio == 1f)
            {
                // Sliding is complete
                isSlidingRight = false;
            }
        }
        if (isSlidingLeft)
        {
            timer += Time.deltaTime;
            float ratio = Mathf.Clamp01(timer / slideDuration);
            panelToSlide.anchoredPosition = Vector2.Lerp(endPosition, startPosition, ratio);
            MoveButton.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(EndPositionMoveButton, StartPositionMoveButton, ratio);
            RotateButton.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(EndPositionRotateButton, StartPositionRotateButton, ratio);
            DeleteButton.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(EndPositionDeleteButton, StartPositionDeleteButton, ratio);
            if (ratio == 1f)
            {
                // Sliding is complete
                isSlidingLeft = false;
            }
        }
    }

    public void Slide()
    {
        if (IsOpen)
        {
            isSlidingLeft = true;
            timer = 0f;
            IsOpen = false;
            OpenMenuButton.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
        }
        else
        {
            isSlidingRight = true;
            timer = 0f;
            IsOpen = true;
            OpenMenuButton.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -90.0f);
        }
        
    }
}