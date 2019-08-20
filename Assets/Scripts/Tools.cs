using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools
{
    public enum HDirection
    {
        Left, Right
    }

    public static float RoundOffset(float num, float round)
    {
        if ((Mathf.Round(num / round) * round) % (round * 2) == 0)
        {
            if ((Mathf.Round(num / round) * round) == Mathf.Floor(num / round) * round)
            {
                return Mathf.Ceil(num / round) * round;
            }
            else if ((Mathf.Round(num / round) * round) == Mathf.Ceil(num / round) * round)
            {
                return Mathf.Floor(num / round) * round;
            }
        }
        else
        {
            return Mathf.Round(num / round) * round;
        }

        return 0;
    }

    public static void startCoroutine(IEnumerator coroutine, MonoBehaviour origin)
    {
        origin.StartCoroutine(coroutine);
    }

    public static bool ExistsTag(List<GameObject> list, string tagName)
    {
        if (list.Exists(element => element.tag == tagName) && list.Count > 0)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public static GameObject FindWithTag(List<GameObject> list, string tagName)
    {
        if (list.Count > 0) {
            return list.Find(element => element.tag == tagName);
        } else
        {
            return null;
        }
    }

    public static IEnumerator CustomInvoke(System.Action function, float secs)
    {

        if (secs >= 0f) {
            yield return new WaitForSeconds(secs);
        } else
        {
            yield return null;
        }

        function();
    }

    public static float ReturnIfZero(float num)
    {
        if (num >= 0)
        {
            return num;
        } else
        {
            return 0;
        }
    }

    public struct Triangle
    {
        public Vector2[] points;

        public float area;

        public Triangle(Vector2[] points, float area)
        {
            this.points = points;
            this.area = area;
        }
    }

    public static Vector2 RandomTrianglePosition (Triangle triangle)
    {
        Vector2 position = new Vector2();

        float r1 = Random.Range(0.00f, 1.01f);
        float r2 = /*Random.Range(0.00f, 1.01f)*/ r1;
        /*float r1 = Random.Range(0, 2);
        float r2 = Random.Range(0, 2);*/

        Vector2 A = triangle.points[0];
        Vector2 B = triangle.points[1];
        Vector2 C = triangle.points[2];

        /*position.x = ((1f - Mathf.Sqrt(r1)) * A.x) + ((Mathf.Sqrt(r1) * (1f - r2)) * B.x) + ((Mathf.Sqrt(r1) * r2) * C.x);
        position.y = ((1f - Mathf.Sqrt(r1)) * A.y) + ((Mathf.Sqrt(r1) * (1f - r2)) * B.y) + ((Mathf.Sqrt(r1) * r2) * C.y);*/

        if (r1 + r2 > 1)
        {
            r1 = 1 - r1;
            r2 = 1 - r2;
        }

        float a = 1 - r1 - r2;
        float b = r1;
        float c = r2;

        position = (a * A) + (b * B) + (c * C);

        return position;
    }

    public static float PolygonArea(Vector2[] polygon)
    {
        int i, j;
        float area = 0;

        for (i = 0; i < polygon.Length; i++)
        {
            j = (i + 1) % polygon.Length;

            area += polygon[i].x * polygon[j].y;
            area -= polygon[i].y * polygon[j].x;
        }

        area /= 2;
        return (area < 0 ? -area : area);
    }

    public static Vector3 RandomPolygonPosition(Vector2[] points)
    {
        Triangulator tr = new Triangulator(points);

        int[] indices = tr.Triangulate();


        List<Triangle> triangles = new List<Triangle>();

        float totalArea = 0f;

        for (int i = 0; i < indices.Length; i += 3)
        {
            triangles.Add(new Triangle(new Vector2[] { points[indices[i]], points[indices[i + 1]], points[indices[i + 2]] }, Tools.PolygonArea(new Vector2 [] { points[indices[i]], points[indices[i + 1]], points[indices[i + 2]] })));

            totalArea += triangles[triangles.Count - 1].area;
        }

        float chosenTriangle = Random.Range(0f, totalArea);
        Triangle definedTriangle = new Triangle();

        float currentArea = 0f;
        for (int i = 0; i < triangles.Count; i++)
        {
            if (/*triangles[i].area*/currentArea < chosenTriangle && currentArea + triangles[i].area > chosenTriangle)
            {
                definedTriangle = triangles[i];

                break;
            }

            currentArea += triangles[i].area;
        }

        Debug.Log(definedTriangle.points[0].ToString() + definedTriangle.points[1].ToString() + definedTriangle.points[2].ToString());

        return Tools.RandomTrianglePosition(definedTriangle);
    }
}
