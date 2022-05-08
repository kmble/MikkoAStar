using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AStarEditor : MonoBehaviour
{
    [MenuItem("AStar/LuoRuudukko")]

    static void GeneroiRuudukko()
    {
        Debug.Log("LuoRuudukko klik");
        for (int ulompiindeksi = 0; ulompiindeksi < 50; ulompiindeksi++)
            {
            for (int indeksi = 0; indeksi < 50; indeksi++)
        
            {
                Debug.Log("For loop" +indeksi);
                GameObject uusiNode = (GameObject)Resources.Load("Node");
                GameObject nodekentassa = Instantiate(uusiNode);
                nodekentassa.transform.name = "Node_" +indeksi+"_"+ulompiindeksi;
                nodekentassa.transform.position = new Vector3(0f+ulompiindeksi, 0f, 0f+indeksi);
            }
        }       
    }
    [MenuItem("AStar/PoistaRuudukko")]

    static void TuhoaRuudukko()
    {
        GameObject[] kaikkiNodet = GameObject.FindGameObjectsWithTag("AstarNode");

    for (int i = 0; i <kaikkiNodet.Length; i++)
        {
            GameObject tuhottavaNode = kaikkiNodet[i];
            DestroyImmediate( tuhottavaNode );
        }
    }
    [MenuItem("AStar/TarkastaEsteet")]

    static void TarkastaEsteet()
    {
         GameObject[] kaikkiNodet = GameObject.FindGameObjectsWithTag("AstarNode");

    for (int i = 0; i <kaikkiNodet.Length; i++)
        {
            GameObject tarkastaEsteNode = kaikkiNodet[i];
            AstrNode nodescripti =  tarkastaEsteNode.GetComponent<AstrNode>();
            
            RaycastHit rh = new RaycastHit();
            if (Physics.Raycast(tarkastaEsteNode.transform.position, Vector3.up
            , out rh))
            {
                Debug.Log("Osuma! " +rh.collider.name);
                nodescripti.kulkematonNode = true;
                nodescripti.VaihdaNodeVari(NodeVarit.KulkematonNodeVari);
            }
            if (nodescripti.alkupiste == true)
            {
                Debug.Log("Alkupiste löydetty"+nodescripti.name);
                nodescripti.VaihdaNodeVari(NodeVarit.AlkupisteNodeVari);
            }
            if (nodescripti.loppupiste == true)
            {
                Debug.Log("Loppupiste löydetty"+nodescripti.name);
                nodescripti.VaihdaNodeVari(NodeVarit.LoppuPisteNodeVari);
            }
        }
    }

    [MenuItem("AStar/EtsiNaapuriNodet")]

    static void EtsiNaapuriNodet()
    {
        GameObject[] kaikkiNodet = GameObject.FindGameObjectsWithTag("AstarNode");

    for (int i = 0; i <kaikkiNodet.Length; i++)
        {
            GameObject nodejolleetsitaannaapuri = kaikkiNodet[i];
            AstrNode nodescripti =  nodejolleetsitaannaapuri.GetComponent<AstrNode>();
        
        for (int ni = 0; ni<kaikkiNodet.Length; ni++ )
            {
                GameObject potentiaalinennaapuri = kaikkiNodet[ni];
                AstrNode potentiaalinennaapuriscript = potentiaalinennaapuri.GetComponent<AstrNode>();
                
                if(potentiaalinennaapuri.name != nodejolleetsitaannaapuri.name)
                {
                    
                    float etaisyys = Vector3.Distance(nodejolleetsitaannaapuri.transform.position,
                    potentiaalinennaapuri.transform.position);

                    if (etaisyys < 1.8f)
                    {
                        nodescripti.naapurinodet.Add(potentiaalinennaapuriscript);
                    }
                }
            } 
        nodescripti.naapurinodetst = new AstrNode[nodescripti.naapurinodet.Count];

        nodescripti.naapurinodet.CopyTo(nodescripti.naapurinodetst);

        
        }
    }
}
