using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMap : MonoBehaviour
{
    public int width;
    public int height;
    [HideInInspector]
    public List<CellObject> cellObjects;
    public CellObject[,] CellObjectLookupTable;

    // Start is called before the first frame update
    void Start()
    {
        CellObjectLookupTable = new CellObject[width, height];
        cellObjects = new List<CellObject>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if (CellObjectLookupTable != null)
                {
                    CellObjectLookupTable[x, y] = null;
                }
            }
        }
        foreach (CellObject co in cellObjects)
        {
            co.MappingCellObject();
        }
        foreach (CellObject co in cellObjects)
        {
            co.UpdateCellObject();
        }
    }
}
