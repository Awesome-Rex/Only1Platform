using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCone : MonoBehaviour
{
    private SpriteMask bottomCover;
    private SpriteMask topCover;

    private Sprite square;
    private Sprite diagonalSquare;

    public float degrees
    {
        get
        {
            return _degrees;
        }
        set
        {
            if (bottomCover == null || topCover == null || square == null || diagonalSquare == null)
            {
                bottomCover = transform.GetChild(1).GetComponent<SpriteMask>();
                topCover = transform.GetChild(2).GetComponent<SpriteMask>();

                square = Resources.Load<Sprite>("Sprites/SquareTC");
                Debug.Log(Resources.Load<Sprite>("Sprites/SquareTC"));
                diagonalSquare = Resources.Load<Sprite>("Sprites/DiagonalSquare");
            }

            _degrees = value;
            if (value <= 180f)
            {
                bottomCover.sprite = square;
                topCover.sprite = square;

                bottomCover.transform.localScale = new Vector3(radius * 2f, radius, 1f);
                topCover.transform.localScale = new Vector3(radius * 2f, radius, 1f);

                bottomCover.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -(value / 2f)));
                topCover.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -(180f - (value / 2f))));
            }
            else
            {
                bottomCover.sprite = diagonalSquare;
                topCover.sprite = diagonalSquare;

                bottomCover.transform.localScale = new Vector3(radius * 2f, (1f - ((value - 180f) / 180f)) * (radius * 2), 1f);
                topCover.transform.localScale = new Vector3(radius * 2f, (1f - ((value - 180f) / 180f)) * (radius * 2), 1f);

                bottomCover.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 135f + (90f - (value - 180f) / 4f)));  /* radius * 2 / 2 */
                topCover.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, -135f - (90f - (value - 180f) / 4f)));
            }
        }
    }
    private float _degrees = 45f;

    public float radius
    {
        get {
            return _radius;
        }
        set
        {
            _radius = value;
            _degrees = degrees;
        }
    }
    private float _radius = 0.5f;


    public GameObject cone {
        get
        {
            return transform.GetChild(0).gameObject;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
