using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public bool simulate = false;
    public float Gravitationalconstant = 1.0f;
    private List<float> masses;
    private List<Transform> transforms;
    private List<moveObject> scripts;

    public void startsimulation()
    {
        simulate = true;
        List<GameObject> children = GetChildren();
        transforms = new List<Transform>(Getcomponent<Transform>(children));
        scripts = new List<moveObject>(Getcomponent<moveObject>(children));

        masses = new List<float>();
        foreach (moveObject child in scripts)
        {
            masses.Add(child.mass);
        }
    }

    void FixedUpdate()
    {
        if (!simulate) return;

        int numofelems = masses.Count;
        for (int i = 1; i < numofelems; i++)
        {
            for (int j = 0; j < i; j++)
            {
                Vector3 diffofposition = transforms[j].position - transforms[i].position;
                float distancesqr = diffofposition.sqrMagnitude;

                if (distancesqr < 0.00001f) continue;

                Vector3 directiontoobject = diffofposition.normalized;
                float mag = Gravitationalconstant * masses[i] * masses[j] / distancesqr;
                scripts[i].velocity += ((directiontoobject * mag) / masses[i]) * Time.fixedDeltaTime;
                scripts[j].velocity += (((-directiontoobject) * mag) / masses[j]) * Time.fixedDeltaTime;
            }
        }
    }


    List<GameObject> GetChildren()
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }
        return children;
    }


    List<T> Getcomponent<T>(List<GameObject> parent)
    {
        List<T> returncomponents = new List<T>();
        foreach (GameObject child in parent)
        {
            returncomponents.Add(child.GetComponent<T>());
        }

        return returncomponents;
    }
}
