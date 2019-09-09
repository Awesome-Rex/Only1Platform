using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Reflection;

public static class Tools
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

    public static void ReplaceGameObject(GameObject target, GameObject result, bool inInspector = true)
    {
        Transform parent = target.transform.parent;
        List<Transform> children = target.transform.GetComponentsInChildren<Transform>().ToList();
        children.Remove(target.transform);

        foreach (Transform child in children)
        {
            child.SetParent(parent);
        }

        target.transform.SetParent(null);

        GameObject newOriginal = null;
        if (inInspector) {
            newOriginal = result;
        } else
        {
            newOriginal = Object.Instantiate(result);
        }

        newOriginal.transform.SetParent(parent);

        foreach (Transform child in children)
        {
            child.SetParent(newOriginal.transform);
        }

        Object.Destroy(target);
    }

    public static void ReplaceGameObjectFamily(GameObject targetParent, GameObject[] targetChildren, GameObject result, bool inInspector = true)
    {
        List<GameObject> placeHolders = new List<GameObject>();
        placeHolders.Add(targetParent);
        placeHolders.AddRange(targetChildren.ToList());

        Transform grandParent = null;
        List<Transform> grandChildren = new List<Transform>();

        foreach (GameObject placeHolder in placeHolders) {
            Transform parent = placeHolder.transform.parent;
            List<Transform> children = placeHolder.transform.GetComponentsInChildren<Transform>().ToList();
            children.Remove(placeHolder.transform);

            grandParent = parent;
            grandChildren = children;

            foreach (Transform child in children)
            {
                child.SetParent(parent);
            }

            placeHolder.transform.SetParent(null);

            

            Object.Destroy(placeHolder);
        }

        GameObject newOriginal = null;
        if (inInspector)
        {
            newOriginal = result;
        }
        else
        {
            newOriginal = Object.Instantiate(result);
        }

        newOriginal.transform.SetParent(grandParent);

        foreach (Transform child in grandChildren)
        {
            child.SetParent(newOriginal.transform);
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

    public struct Line
    {
        public Vector2[] points;

        public float distance;

        public Line(Vector2[] points)
        {
            this.points = points;
            this.distance = Vector2.Distance(points[0], points[1]);
        }
    }

    public static Vector3 RandomEdgePosition (Vector2[] points)
    {
        List<Line> lines = new List<Line>();

        float totalDistance = 0f;

        for (int i = 0; i < points.Length - 1; i++)
        {
            lines.Add(new Line(new Vector2[] { points[i], points[i + 1] }));

            totalDistance += lines[lines.Count - 1].distance;
        }

        float linePosition = Random.Range(0f, totalDistance + 0.01f);

        float currentDistance = 0f;
        for (int i = 0; i < lines.Count; i++)
        {
            if (currentDistance < linePosition && currentDistance + lines[i].distance > linePosition)
            {

                return Vector3.Lerp(lines[i].points[0], lines[i].points[1], (linePosition - currentDistance) / lines[i].distance);

            }

            currentDistance += lines[i].distance;
        }

        return new Vector3();
    }



    /*public static object GetValue(this UnityEditor.SerializedProperty property)
    {
        object obj = property.serializedObject.targetObject;

        FieldInfo field = null;
        foreach (var path in property.propertyPath.Split('.'))
        {
            var type = obj.GetType();
            field = type.GetField(path);
            obj = field.GetValue(obj);
        }
        return obj;
    }

    // Sets value from SerializedProperty - even if value is nested
    public static void SetValue(this UnityEditor.SerializedProperty property, object val)
    {
        object obj = property.serializedObject.targetObject;

        List<KeyValuePair<FieldInfo, object>> list = new List<KeyValuePair<FieldInfo, object>>();

        FieldInfo field = null;
        foreach (var path in property.propertyPath.Split('.'))
        {
            var type = obj.GetType();
            field = type.GetField(path);
            list.Add(new KeyValuePair<FieldInfo, object>(field, obj));
            obj = field.GetValue(obj);
        }

        // Now set values of all objects, from child to parent
        for (int i = list.Count - 1; i >= 0; --i)
        {
            list[i].Key.SetValue(list[i].Value, val);
            // New 'val' object will be parent of current 'val' object
            val = list[i].Value;
        }
    }

    public static object GetTargetObjectOfProperty(SerializedProperty prop)
    {
        if (prop == null) return null;

        var path = prop.propertyPath.Replace(".Array.data[", "[");
        object obj = prop.serializedObject.targetObject;
        var elements = path.Split('.');
        foreach (var element in elements)
        {
            if (element.Contains("["))
            {
                var elementName = element.Substring(0, element.IndexOf("["));
                var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                obj = GetValue_Imp(obj, elementName, index);
            }
            else
            {
                obj = GetValue_Imp(obj, element);
            }
        }
        return obj;
    }*/

    public static object GetParent(SerializedProperty prop)
    {
        var path = prop.propertyPath.Replace(".Array.data[", "[");
        object obj = prop.serializedObject.targetObject;
        var elements = path.Split('.');
        foreach (var element in elements.Take(elements.Length - 1))
        {
            if (element.Contains("["))
            {
                var elementName = element.Substring(0, element.IndexOf("["));
                var index = System.Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                obj = GetValue(obj, elementName, index);
            }
            else
            {
                obj = GetValue(obj, element);
            }
        }
        return obj;
    }

    public static object GetValue(object source, string name)
    {
        if (source == null)
            return null;
        var type = source.GetType();
        var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        if (f == null)
        {
            var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (p == null)
                return null;
            return p.GetValue(source, null);
        }
        return f.GetValue(source);
    }

    public static object GetValue(object source, string name, int index)
    {
        var enumerable = GetValue(source, name) as IEnumerable;
        var enm = enumerable.GetEnumerator();
        while (index-- >= 0)
            enm.MoveNext();
        return enm.Current;
    }

}
