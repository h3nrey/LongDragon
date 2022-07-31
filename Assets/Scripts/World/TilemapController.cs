using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TilemapController : MonoBehaviour
{
    private Tilemap tilemap;
    // Start is called before the first frame update
    void Start() {
        tilemap = GetComponent<Tilemap>();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "projectille") {
            Vector3Int cellPos = tilemap.WorldToCell(other.attachedRigidbody.position);
            print($"cell pos: {cellPos}");
            tilemap.SetTile(cellPos, null);
        }
    }
}
