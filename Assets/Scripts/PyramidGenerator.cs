using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidGenerator : MonoBehaviour
{
    public GameObject circle;

    public int rows;
    public float step;
    public float circleSize;

    [ContextMenu("del")]
    public void Delete()
    {
        foreach (Transform transform1 in transform)
        {
            DestroyImmediate(transform1.gameObject);
        }
    }
    [ContextMenu("setscale")]
    public void setscale()
    {
        foreach (Transform transform1 in transform)
        {
            transform1.localScale = new Vector2(circleSize, circleSize);
        }
    }

    [ContextMenu("gen")]
    public void Generate()
    {
        for (int i = 0; i < rows; i++)
        {
            float rowPosition = i * -step;
            int numberOfBlocksInRow = i + 1;

            for (int j = 0; j < numberOfBlocksInRow; j++)
            {
                float blockPosition = j * -step - i * -step / 2f;

                // Create a cube at the specified position
                GameObject cube = Instantiate(circle);
                cube.transform.localScale = new Vector2(circleSize, circleSize);
                cube.transform.position = new Vector3(blockPosition, rowPosition, 0f);
                cube.transform.SetParent(this.transform);
            }
        }
    }
}
