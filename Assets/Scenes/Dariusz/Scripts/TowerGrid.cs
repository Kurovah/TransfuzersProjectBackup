using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CodeMonkey.Utils;

public class TowerGrid<TGridObject>
{
    public event EventHandler<OnGridObjectChangedEventArgs> OnGridObjectChanged;
    public class OnGridObjectChangedEventArgs : EventArgs {
        public OnGridObjectChangedEventArgs(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int x;
        public int y;
    }

    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition, halfCellSize;
    private TGridObject[,] gridArray;

    public TowerGrid(int width, int height, float cellSize, Vector3 originPosition, Func<TowerGrid<TGridObject>, int, int, TGridObject> createGridObject) {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        halfCellSize = new Vector3(0.5f, 0.5f) * cellSize;

        gridArray = new TGridObject[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++) {
            for (int y = 0; y < gridArray.GetLength(1); y++) {
                gridArray[x, y] = createGridObject(this, x, y);
            }
        }

        bool showDebug = true;
        if (showDebug) {
            TextMesh[,] debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++) {
                for (int y = 0; y < gridArray.GetLength(1); y++) {
                    debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y]?.ToString(), null, GetWorldPosition(x, y),
                        30, Color.white, TextAnchor.MiddleCenter, TextAlignment.Center);
                    Debug.DrawLine(GetWorldPosition(x, y) - halfCellSize, GetWorldPosition(x, y + 1) - halfCellSize, Color.white, float.MaxValue);
                    Debug.DrawLine(GetWorldPosition(x, y) - halfCellSize, GetWorldPosition(x + 1, y) - halfCellSize, Color.white, float.MaxValue);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height) - halfCellSize, GetWorldPosition(width, height) - halfCellSize, Color.white, float.MaxValue);
            Debug.DrawLine(GetWorldPosition(width, 0) - halfCellSize, GetWorldPosition(width, height) - halfCellSize, Color.white, float.MaxValue);

            OnGridObjectChanged += (object sender, OnGridObjectChangedEventArgs eventArgs) => {
                debugTextArray[eventArgs.x, eventArgs.y].text = gridArray[eventArgs.x, eventArgs.y]?.ToString();
            };
        }
    }

    public int GetWidth() => width;
    public int GetHeight() => height;
    public float GetCellSize() => cellSize;

    public Vector3 GetWorldPosition(int x, int y) 
    {
        return new Vector3(x + 0.5f, y + 0.5f) * cellSize + originPosition;
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y) 
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    public void SetGridObject(int x, int y, TGridObject value) 
    {
        if (x >= 0 && y >= 0 && x < width && y < height) 
        {
            gridArray[x, y] = value;
            if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs(x, y));
        }
    }

    public void TriggerGridObjectChanged(int x, int y) {
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridObjectChangedEventArgs(x, y));
    }

    public void SetGridObject(Vector3 worldPosition, TGridObject value) {
        GetXY(worldPosition, out var x, out var y);
        SetGridObject(x, y, value);
    }

    public TGridObject GetGridObject(int x, int y) {
        if (x >= 0 && y >= 0 && x < width && y < height) {
            return gridArray[x, y];
        } else {
            return default(TGridObject);
        }
    }

    public TGridObject GetGridObject(Vector3 worldPosition) {
        GetXY(worldPosition, out var x, out var y);
        return GetGridObject(x, y);
    }

    public void RefreshTileText(int x, int y)
    {
        OnGridObjectChanged(this, new OnGridObjectChangedEventArgs(x, y));
    }
}
