using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D;

public class ColourBeatSync : MonoBehaviour
{
    public float bpm;

    public float intervals;

    public List<Color> colourRange;

    //private SpriteShapeParameters spriteRenderer;
    private SpriteRenderer spriteRenderer;

    public IEnumerator cycle ()
    {
        while (true)
        {

            yield return new WaitForSecondsRealtime((60 / bpm) * intervals);

            StopCoroutine(colorChange());
            StartCoroutine(colorChange());
        }
    }

    public IEnumerator colorChange ()
    {
        float timePassed = 0f;

        Color startColour = spriteRenderer.color;
        Color targetColour = colourRange[Random.Range(0, colourRange.Count)];
        while (targetColour == startColour)
        {
            targetColour = colourRange[Random.Range(0, colourRange.Count)];
        }

        while (timePassed < (60 / bpm) * intervals)
        {
            spriteRenderer.color = Color.Lerp(startColour, targetColour, timePassed / ((60 / bpm) * intervals));

            timePassed += Time.deltaTime;

            yield return null;
        }
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.color = colourRange[Random.Range(0, colourRange.Count)];
        StartCoroutine(cycle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
