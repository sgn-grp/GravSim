using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class uidatafields
{
    public TMP_InputField massfield;
    public TMP_InputField ini_pos_x;
    public TMP_InputField ini_pos_y;
    public TMP_InputField ini_pos_z;
    public TMP_InputField ini_vel_x;
    public TMP_InputField ini_vel_y;
    public TMP_InputField ini_vel_z;
}

public class UIManager : MonoBehaviour
{
    public GameObject inputprefab;
    public GameObject buttonprefab;
    public GameObject textprefab;

    public RectTransform uiconatiner;

    public GameObject mvobjprefab;
    public Transform gravsimmanager;
    
    GameObject objnoipt;
    GameObject loadobjectsbutton;
    public GameObject todesui;

    public GameObject loadbutton;
    public GameObject startbutton;

    int objno;
    private List<uidatafields> datalist = new List<uidatafields>();

    void Start()
    {
        objnoipt = Instantiate(inputprefab, uiconatiner);
        loadobjectsbutton = Instantiate(loadbutton, uiconatiner);
        loadobjectsbutton.GetComponent<Button>().onClick.AddListener(onloadclick);
    }
    void onloadclick()
    {
        objno = int.Parse(objnoipt.GetComponent<TMP_InputField>().text);

        Destroy(objnoipt);
        Destroy(loadobjectsbutton);

        for (int i = 0; i < objno; i++)
        {
            datalist.Add(addnewobject($"Object {i}:"));
        }

        loadobjectsbutton = Instantiate(startbutton, uiconatiner);
        loadobjectsbutton.GetComponent<Button>().onClick.AddListener(onstartclick);
    }

    void onstartclick()
    {
        for (int i = 0; i < objno; i++)
        {
            GameObject newobject = Instantiate(mvobjprefab, new Vector3(float.Parse(datalist[i].ini_pos_x.text), float.Parse(datalist[i].ini_pos_y.text), float.Parse(datalist[i].ini_pos_z.text)), Quaternion.identity, gravsimmanager);
            newobject.GetComponent<moveObject>().mass = float.Parse(datalist[i].massfield.text);
            newobject.GetComponent<moveObject>().inx = float.Parse(datalist[i].ini_vel_x.text);
            newobject.GetComponent<moveObject>().iny = float.Parse(datalist[i].ini_vel_y.text);
            newobject.GetComponent<moveObject>().inz = float.Parse(datalist[i].ini_vel_z.text);
            newobject.GetComponent<moveObject>().setvelocity();
        }
        Destroy(todesui);
        gravsimmanager.GetComponent<Gravity>().startsimulation();
    }

    uidatafields addnewobject(string objectname)
    {
        uidatafields objectfields = new uidatafields();
        Instantiate(textprefab, uiconatiner).GetComponent<TMP_Text>().text = objectname;

        objectfields.massfield = Instantiate(inputprefab, uiconatiner).GetComponent<TMP_InputField>();
        objectfields.massfield.text = "Mass";

        objectfields.ini_pos_x = Instantiate(inputprefab, uiconatiner).GetComponent<TMP_InputField>();
        objectfields.ini_pos_x.text = "Initial x position";

        objectfields.ini_pos_y = Instantiate(inputprefab, uiconatiner).GetComponent<TMP_InputField>();
        objectfields.ini_pos_y.text = "Initial y position";

        objectfields.ini_pos_z = Instantiate(inputprefab, uiconatiner).GetComponent<TMP_InputField>();
        objectfields.ini_pos_z.text = "Initial z position";

        objectfields.ini_vel_x = Instantiate(inputprefab, uiconatiner).GetComponent<TMP_InputField>();
        objectfields.ini_vel_x.text = "Initial x velocity";

        objectfields.ini_vel_y = Instantiate(inputprefab, uiconatiner).GetComponent<TMP_InputField>();
        objectfields.ini_vel_y.text = "Initial y velocity";

        objectfields.ini_vel_z = Instantiate(inputprefab, uiconatiner).GetComponent<TMP_InputField>();
        objectfields.ini_vel_z.text = "Initial z velocity";

        return objectfields;
    }

}
