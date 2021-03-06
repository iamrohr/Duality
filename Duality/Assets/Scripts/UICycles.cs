using UnityEngine;

public class UICycles : Singleton<UICycles>
{
    public Vector2 [,] Grid;
    float Vertical, Horizontal;
    int Columns, Rows;
    public new Camera camera;
    public GameObject scoreCycle;
    public int score = 0;

    void Start()
    {
        camera = Camera.main;
        //Create grid of positions
        //Vertical = (int)camera.orthographicSize;

        Horizontal = Screen.width - 80;
        Vertical = Screen.height;

        Columns = 16;
        Rows = 9;

        Grid = new Vector2[Columns, Rows];

        for (int i = 0; i < Columns; i++)
        {
            for (int j = 0; j < Rows; j++)
            {
                Grid[i, Rows - j - 1] = (new Vector2((Horizontal * i / Columns + 40), (Vertical * j / Rows + 30)));
            }
        }
    }

    private void SpawnCycle(int gridPos)
    {
        var x = gridPos % Grid.GetLength(0);
        var y = gridPos / Grid.GetLength(0);
        
        Instantiate(scoreCycle, (Vector2) camera.ScreenToWorldPoint(Grid[x, y]), Quaternion.identity, transform);
    }
    
    public void SpawnNextCycle()
    {
        SpawnCycle(score);
        score ++;
        EnemyManager.Instance.IncreaseDifficulty();
    }

}
