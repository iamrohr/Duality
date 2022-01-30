using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICycles : MonoBehaviour
{
    public Vector2 [,] Grid;
    float Vertical, Horizontal;
    int Columns, Rows;
    public Camera camera;
    public GameObject scoreCycle;

    //Point counter
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Create grid of positions
        //Vertical = (int)camera.orthographicSize;


        Horizontal = Screen.width - 160;
        Vertical = Screen.height - 60;
        Debug.Log(Horizontal);

        Columns = 20;
        Rows = 20;

        Grid = new Vector2[Columns, Rows];

        for (int i = 0; i < Columns; i++)
        {
            for (int j = 0; j < Rows; j++)
            {
                Grid[i, Rows - j - 1] = camera.ScreenToWorldPoint(new Vector2((Horizontal * i / Columns + 80), (Vertical * j / Rows + 30)));
                //SpawnCycle(i, j, Grid[ i, Rows - j - 1]);
            }
        }

        //SpawnCycle(0,0,Grid[0,0]);

    }

    private void SpawnCycle(int score)
    {

    }

    public void SpawnNextCykle()
    {
        SpawnCycle(score);
        score ++;
    }

}
