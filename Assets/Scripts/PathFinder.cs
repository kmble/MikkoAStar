using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    ArrayList openList = new ArrayList();
    ArrayList closedList = new ArrayList();


    public AstrNode[] openListVisible;
    public AstrNode[] closedListVisible;

    public AstrNode aloitusNode;

    public AstrNode lopetusNode;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetKeyDown("p"))
        {
            Debug.Log("Started!");
            StartCoroutine("PolunetsintaKayntiin");
        }
    }

    IEnumerator PolunetsintaKayntiin()
    {
        Debug.Log("Metodi käyntiin");
        GameObject[] kaikkiNodet = GameObject.FindGameObjectsWithTag("AstarNode");

        for (int i = 0; i <kaikkiNodet.Length; i++)
            {
                GameObject tarkastaEsteNode = kaikkiNodet[i];
                AstrNode nodescripti =  tarkastaEsteNode.GetComponent<AstrNode>();
            if (nodescripti.alkupiste == true)
                {
                    aloitusNode = nodescripti;
                }
            if (nodescripti.loppupiste == true)
                {
                    lopetusNode = nodescripti;
                }
            }
        
        AstrNode kasiteltavaNode = aloitusNode;
        openList.Add (kasiteltavaNode);
        kasiteltavaNode.VaihdaNodeVari(NodeVarit.OpenListNodeVari);
        yield return new WaitForSeconds(1f);


        for (int mainloopindex = 0; mainloopindex < 10000; mainloopindex++ )
        {
            openList.Remove(kasiteltavaNode);
            closedList.Add(kasiteltavaNode);
            kasiteltavaNode.VaihdaNodeVari(NodeVarit.ClosedListNodeVari);

            openListVisible = new AstrNode[openList.Count];
            openList.CopyTo(openListVisible);

            closedListVisible = new AstrNode[closedList.Count];
            closedList.CopyTo(closedListVisible);

            KasitellaanNode( kasiteltavaNode, kasiteltavaNode.naapurinodetst );

            AstrNode pieninFarvoNode = null;
            float pieninLoydettyFarvo = float.MaxValue;
            for (int openi = 0; openi < openList.Count; openi++)
                {
                    AstrNode openlistNode = (AstrNode)openList[openi];
                    if (openlistNode.f < pieninLoydettyFarvo)
                    {
                        pieninLoydettyFarvo = openlistNode.f;
                        pieninFarvoNode = openlistNode;
                    }

                }
            kasiteltavaNode = pieninFarvoNode;
            //yield return new WaitForSeconds(1f);





        if (kasiteltavaNode.loppupiste == true)
            {
                Debug.Log("LOPPUPISTE LÖYDETTY");
                mainloopindex = 10000;
            }
            if (openList.Count <=0)
            {
                Debug.Log("EI REITTIÄ!!!!!!!!!");
            }
        }

        // yield return new WaitForSeconds(0.5f);
        AstrNode reittinode = lopetusNode;
        for (int final = 0; final < 500; final++)
        {
            reittinode.VaihdaNodeVari(NodeVarit.ReittiNodeVari);
            reittinode = reittinode.vanhempi;
        }
        
        Debug.Log("Poistutaan metodista ");
    }

    void KasitellaanNode(AstrNode itseNode, AstrNode[] naapurinodet)
    {
    for (int i = 0; i < naapurinodet.Length; i++)
        {
        AstrNode naapuri = naapurinodet[i];

        bool naapuriOliClosedListassa = false;
        for (int closedi = 0; closedi < closedList.Count; closedi++)
        {
            AstrNode closedListNode = (AstrNode)closedList[closedi];
            if(naapuri.name == closedListNode.name)
            {
                naapuriOliClosedListassa = true;
                closedi = closedList.Count;
            }
        }
        if (naapuriOliClosedListassa == false && naapuri.kulkematonNode == false)
            {
            bool naapurioliopenlistassa = false;
            for (int openin = 0; openin < openList.Count; openin++)
                {
                    AstrNode openlistassanode = (AstrNode)openList[openin];
                    if (openlistassanode.name == naapuri.name)
                    {
                        naapurioliopenlistassa = true;
                        openin = openList.Count;
                    }
                }
                if (naapurioliopenlistassa == false)
                {
                    openList.Add(naapuri);
                    naapuri.VaihdaNodeVari(NodeVarit.OpenListNodeVari);

                    naapuri.g = itseNode.g+1;
                    naapuri.vanhempi = itseNode;
                }
                else if (naapurioliopenlistassa == true)
                {
                    Debug.Log("Naapuri oli jo openlistalla: "+ naapuri.g + " vs " +itseNode.g+1);
                if (itseNode.g+1 < naapuri.g )
                    {
                        naapuri.g = itseNode.g+1;
                        naapuri.vanhempi = naapuri;
                    }
                }


                float manhattanx = lopetusNode.transform.position.x - naapuri.transform.position.x;
                float manhattanz = lopetusNode.transform.position.z - naapuri.transform.position.z;

                naapuri.h = Mathf.Abs(manhattanx) + Mathf.Abs(manhattanz);
                naapuri.f = naapuri.g + naapuri.h;
            }




        }
    }
}
