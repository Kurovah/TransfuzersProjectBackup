using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using CodeMonkey.Utils;
using Vector3 = UnityEngine.Vector3;

public class GridTester : MonoBehaviour
{
  private TowerGrid<GridObject> _gridXZ;
  
  private void Awake()
  {
    int gridWidth = 10;
    int gridHeight = 10;
    float cellSize = 10.0f;
    _gridXZ = new TowerGrid<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, 
      (TowerGrid<GridObject> g, int x, int z) => new GridObject(g,x, z));
  }

  public class GridObject
  {
    private TowerGrid<GridObject> _towerGrid;
    private int x;
    private int z;

    public GridObject(TowerGrid<GridObject> towerGrid, int x, int z)
    {
      this._towerGrid = towerGrid;
      this.x = x;
      this.z = z;
    }
    
    public override string ToString()
    {
      return x + ", " + z;
    }
    
  }
  
}
