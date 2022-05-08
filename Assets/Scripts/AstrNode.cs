using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeVarit
{
    AlkupisteNodeVari = 0,
    ClosedListNodeVari,
    EsteNodeVari,
    KulkematonNodeVari,
    LoppuPisteNodeVari,
    OpenListNodeVari,
    PerusNodeVari,
    ReittiNodeVari,


}
public class AstrNode : MonoBehaviour
{
    public bool alkupiste = false;
    public bool loppupiste = false;
    public bool kulkematonNode = false;

    public float f = 0;
    public float g = 0;

    public float h = 0;

    public AstrNode vanhempi;

    public Material[] varimateriaalit;

    public ArrayList naapurinodet = new ArrayList();
    public AstrNode[] naapurinodetst;

    public void VaihdaNodeVari(NodeVarit nodenvarikoodi)
         {
             kulkematonNode = true;
             MeshRenderer mr = GetComponent<MeshRenderer>();
             mr.sharedMaterial = varimateriaalit[(int)nodenvarikoodi];
         }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
