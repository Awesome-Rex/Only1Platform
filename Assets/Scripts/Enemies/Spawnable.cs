using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
    public float delay;

    public static IEnumerator spawnEnemy(Spawnable target/*not already instantiated*/)
    {
        target = Instantiate(target.gameObject).GetComponent<Spawnable>();

        foreach (Transform obj in target.GetComponentsInChildren<Transform>())
        {
            foreach (SpawnSetting setting in obj.GetComponents<SpawnSetting>())
            {
                if (setting.changeBefore)
                {
                    setting.change(obj.gameObject);
                }
            }
        }

        Spawnable newTarget = Instantiate(target.gameObject).GetComponent<Spawnable>();
        target.gameObject.SetActive(false);


        GameObject replacement = null;
        foreach (Transform obj in newTarget.GetComponentsInChildren<Transform>())
        {
            if (obj.gameObject.tag != "PlaceHolder") {
                GameObject createdReplacement = null;

                if (obj.GetComponent<Enemy>() != null)
                {
                    //warning

                    createdReplacement = Instantiate(GameplayComponents.main.warning.gameObject);
                    createdReplacement.GetComponent<Animator>().SetFloat("Delay", target.delay);

                } else if (obj.GetComponent<Enemy>() == null)
                {
                    //blank

                    createdReplacement = Instantiate(GameplayComponents.main.blank.gameObject);
                }

                if (obj.transform == newTarget.transform)
                {
                    replacement = createdReplacement;
                }

                createdReplacement.transform.position = obj.transform.position;
                createdReplacement.transform.rotation = obj.rotation;
                createdReplacement.transform.localScale = obj.localScale;

                createdReplacement.GetComponent<PlaceHolder>().original = obj.gameObject/* original prefab file, gotta fix*/;

                //make correct family tree

                Tools.ReplaceGameObject(obj.gameObject, createdReplacement);
            }
        }

        yield return new WaitForSeconds(target.delay);

        Destroy(replacement.gameObject);

        target.gameObject.SetActive(true);

        foreach (SpawnSetting setting in target.GetComponentsInChildren<SpawnSetting>())
        {
            if (!setting.changeBefore)
            {
                setting.change(setting.gameObject);
            }
        }
    }
}
