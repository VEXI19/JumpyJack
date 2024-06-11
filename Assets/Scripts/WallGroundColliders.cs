using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallGroundColliders : MonoBehaviour
{
    enum Position
    {
        left, right, top, bottom
    }
    private void CreateGameObject(Position position)
    {
        Destroy(this.GetComponent<WallGroundColliders>());
        GameObject wallRight = Instantiate(this.gameObject);
        foreach (Transform child in wallRight.transform)
        {
            Destroy(child.gameObject);
        }
        switch (position)
        {
            case Position.left:
                wallRight.transform.position = new Vector2(this.transform.position.x - this.transform.localScale.x / 2, this.transform.position.y);
                wallRight.transform.localScale = new Vector2(0.01f, this.transform.localScale.y - 0.015f);
                wallRight.layer = 7;
                break;
            case Position.right:
                wallRight.transform.position = new Vector2(this.transform.position.x + this.transform.localScale.x / 2, this.transform.position.y);
                wallRight.transform.localScale = new Vector2(0.01f, this.transform.localScale.y - 0.015f);
                wallRight.layer = 7;
                break;
            case Position.top:
                wallRight.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + this.transform.localScale.y / 2);
                wallRight.transform.localScale = new Vector2(this.transform.localScale.x, 0.01f);
                wallRight.layer = 6;
                break;
            case Position.bottom:
                wallRight.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - this.transform.localScale.y / 2);
                wallRight.transform.localScale = new Vector2(this.transform.localScale.x, 0.01f);
                wallRight.layer = 6;
                break;
        }

        wallRight.transform.parent = this.transform;
        wallRight.GetComponent<TilemapCollider2D>().isTrigger = true;
    }

    void Start()
    {
        CreateGameObject(Position.left);
        CreateGameObject(Position.right);
        CreateGameObject(Position.top);
        CreateGameObject(Position.bottom);
    }

}
