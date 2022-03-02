using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour
{
    [SerializeField]
    private GameObject lineGeneratorPrefab;

    [SerializeField]
    private GameObject linepointPrefab;


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = 0;
            CreatePointMarker(newPos);
        }


        if (Input.GetMouseButtonDown(1))
        {
            ClearAllPoints();
        }



        if (Input.GetKeyDown("e"))
        {
            GenerateNewLine();
        }
    }


    private void CreatePointMarker(Vector3 pointPosition)
    {
        Instantiate(linepointPrefab, pointPosition, Quaternion.identity);
    }

    private void ClearAllPoints()
    {
        GameObject[] allPoints = GameObject.FindGameObjectsWithTag("Pointmarker");

        foreach(GameObject p in allPoints)
        {
            Destroy(p);
        }
    }

    private void GenerateNewLine()
    {
        GameObject[] allPoints = GameObject.FindGameObjectsWithTag("Pointmarker");
        Vector3[] allPointPositions = new Vector3[allPoints.Length];

        if(allPoints.Length > 1 )
        {
            for(int i = 0; i < allPoints.Length; i++)
            {
                allPointPositions[i] = allPoints[i].transform.position; 
            }

            SpawnLineGenerator(allPointPositions);
        }
    }


    void SpawnLineGenerator(Vector3[] linePoints)
    {
         
        GameObject newLineGen = Instantiate(lineGeneratorPrefab);
        LineRenderer Lrend = newLineGen.GetComponent<LineRenderer>();


        Lrend.positionCount = linePoints.Length;

        Lrend.SetPositions(linePoints);

     
       // Destroy(newLineGen, 5);
    }
}
