using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LineHandler : MonoBehaviour
{
    [SerializeField] private List<Vector2> points = new List<Vector2>();
    [SerializeField] Sprite selected;
    [SerializeField] private float animSpeed = 5f;
    [SerializeField] private float fadeDuration = 1f;

    private LineRenderer lr;
    
    private Vector2[] pPositions;

    private int ind = 0;
    private float delay = 2f;

    private int currentIndex;
    private bool isAnimating;


    void Start()
    {
        lr = GetComponent<LineRenderer>();
        pPositions = DataHandler.instance.pointsConverted;
        currentIndex = 0;
        isAnimating = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                ConnectInSequence(hit.collider.transform);
            }
        }
    }

    //This method checks if a clicked point is being clicked in the correct sequence and if it hasn't been clicked before.
    //If the conditional statement is true, then point's texture changes, and its number text starts to fade out.
    //Additionaly, the clicked point is stored in a new array that keeps track of all previously clicked points.
    //If the line drawing animation is not currently playing, a new method is called that iterates through that array.
    private void ConnectInSequence(Transform finalPoint)
    {
        Vector2 currentlyClicked = new Vector2(finalPoint.position.x, finalPoint.position.y);

        if (pPositions[ind] == currentlyClicked && !points.Contains(pPositions[ind]))
        {
            SpriteRenderer sr = finalPoint.GetComponent<SpriteRenderer>();
            TextMeshPro pointText = finalPoint.GetComponentInChildren<TextMeshPro>();
            StartCoroutine(FadeOut(pointText));
            sr.sprite = selected;
            points.Add(currentlyClicked);
            ind++;

            if (!isAnimating)
            {
                StartCoroutine(StartNextAnimation());         
            }        
        }
    }

    //This method is used to iterate through the clicked points array
    //and select the starting and ending points to be sent to the line animation method.
    //An if statement is used to connect last and first points after every point has already been clicked.
    //This is achieved by checking whether the size of clicked points array is equal to the size of the initial points data array.
    //Once all the points have been connected, the main menu scene is loaded after a certain delay.
    private IEnumerator StartNextAnimation()
    {
        isAnimating = true;

        for (int i = currentIndex; i < points.Count - 1; i++)
        {
            Vector2 startPoint = points[i];
            Vector2 endPoint = points[i + 1];
            yield return StartCoroutine(AnimateLine(startPoint, endPoint));
            currentIndex++;
            
        }
        if (points.Count == pPositions.Length)
        {
            yield return StartCoroutine(AnimateLine(points[points.Count - 1], points[0]));
            Invoke("LoadMainMenu", delay);
        }

        isAnimating = false;
    }
    
    //This method loads main menu scene
    private void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }


    //This method is used to animate a line between two given points.
    //The animation duration is calculated by dividing the distance of given points by the specified animation speed.
    //The while loop runs as long as the elapsed time is less than the duration. The elapsed time is increased inside the loop.
    //In the while loop, the line animation is achieved by using linear interpolation between the starting point and ending point.
    //That new interpolated vector is set every frame while loop is still running, creating a line animation.
    //In the end, instead of using the interpolated vector, the ending point is set at its index. 
    private IEnumerator AnimateLine(Vector2 point1, Vector2 point2)
    {
        float distance = Vector2.Distance(point1, point2);
        float duration = distance / animSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            Vector2 currentPos = Vector2.Lerp(point1, point2, t);

            lr.positionCount = currentIndex + 2;
            lr.SetPosition(currentIndex, point1);
            lr.SetPosition(currentIndex + 1, currentPos);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        lr.SetPosition(currentIndex + 1, point2);
    }

    //This method is used to create fade out animation for the point's number text.
    //It takes text's current color and using linear interpolation it gradually transitions to the
    //target end color, which has its alpha value set to 0.
    private IEnumerator FadeOut(TextMeshPro text)
    {
        Color startColor = text.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {          
            float t = elapsedTime / fadeDuration;
            Color currentColor = Color.Lerp(startColor, endColor, t);

            text.color = currentColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        text.color = endColor;
    }
}
