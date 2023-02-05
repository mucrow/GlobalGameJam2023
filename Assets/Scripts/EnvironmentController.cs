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
    Tile[] dirt;
    
    [SerializeField]
    Tilemap _tilemap;

    
    public void buildTile(Transform playerTransform)
    {
        // var tileData = FindATile();

        // use the player's position to determine the target position of the tile we're placing
        // basically it's (player.x, player.y - 1)
        var pos = new Vector3Int(Mathf.FloorToInt(playerTransform.position.x), Mathf.FloorToInt(playerTransform.position.y - 1), 0);

        // modify the world

        if (!_tilemap.HasTile(pos)) {
            Tile dirtTile = dirt[3];
            _tilemap.SetTile(pos, dirtTile);
        }
    }

    public void digTile(Transform playerTransform, bool facingRight, float isDownPressed)
    {
        var tileData = FindATile();
        Vector3Int pos = new Vector3Int(0, 0, 0);

        //dig below
        if (Math.Abs(isDownPressed) > 0.1) {
            // use the player's position to determine the target position of the tile we're placing
            // basically it's (player.x, player.y - 1)
            pos = new Vector3Int(Mathf.FloorToInt(playerTransform.position.x), Mathf.FloorToInt(playerTransform.position.y - 1), 0);
            _tilemap.SetTile(pos, null);
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
            _tilemap.SetTile(pos, null);
      }
    }
      
    // returns a non-empty tile in the range of (-10, -10) to (10, 10) in the world
    TileBase FindATile() {
        for (int y = -10; y < 10; ++y) {
        for (int x = -10; x < 10; ++x) {
            var tileData = _tilemap.GetTile(new Vector3Int(x, y, 0));
            if (tileData != null) {
            Debug.Log(tileData);
            return tileData;
            }
        }
        }
        return null;
    }
}
