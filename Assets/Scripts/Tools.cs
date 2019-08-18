using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools
{
    public enum HDirection
    {
        Left, Right
    }

    public static float RoundOffset (float num, float round)
    {
        if ((Mathf.Round(num / round) * round) % (round*2) == 0)
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

    public static bool ExistsTag (List<GameObject> list, string tagName)
    {
        if (list.Exists(element => element.tag == tagName) && list.Count > 0)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public static GameObject FindWithTag (List<GameObject> list, string tagName)
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
}
