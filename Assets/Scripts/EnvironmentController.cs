using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnvironmentController : MonoBehaviour
{
    [SerializeField]
    Tile bedrock;

    [SerializeField]
    Tile dirt;
    
    [SerializeField]
    Tilemap _tilemap;

    
    public void buildTile(Transform playerTransform)
    {
        // use the player's position to determine the target position of the tile we're placing
        // basically it's (player.x, player.y - 1)
        var pos = new Vector3Int(Mathf.FloorToInt(playerTransform.position.x), Mathf.FloorToInt(playerTransform.position.y - 1), 0);

        // modify the world
        if (!_tilemap.HasTile(pos)) {
            _tilemap.SetTile(pos, dirt);
        }
    }

    public void digTile(Transform playerTransform, bool facingRight, float isDownPressed)
    {
        Vector3Int pos = new Vector3Int(0, 0, 0);

        //dig below
        if (Math.Abs(isDownPressed) > 0.1) {
            // use the player's position to determine the target position of the tile we're placing
            // basically it's (player.x, player.y - 1)
            pos = new Vector3Int(Mathf.FloorToInt(playerTransform.position.x), Mathf.FloorToInt(playerTransform.position.y - 1), 0);
            if (_tilemap.GetTile(pos) != bedrock) {
                _tilemap.SetTile(pos, null);
            }
        } 
        //dig to the side
        else 
        // if (Math.Abs(runInput) > 0.1)
         {
            // need to check rotation of the character (facing forward or back)
            if (facingRight) {
            pos = new Vector3Int(Mathf.FloorToInt(playerTransform.position.x + 1), Mathf.FloorToInt(playerTransform.position.y), 0);
            } else {
            pos = new Vector3Int(Mathf.FloorToInt(playerTransform.position.x - 1), Mathf.FloorToInt(playerTransform.position.y), 0);
            }
            if (_tilemap.GetTile(pos) != bedrock) {
                _tilemap.SetTile(pos, null);
            }
      }
    }

}
