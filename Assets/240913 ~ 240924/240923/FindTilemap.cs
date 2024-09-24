using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FindTilemap : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] GameObject checker;

    private void Start()
    {
        // 타일맵을 처음부터 끝까지 불러오는 반복문   
        for (int y = tilemap.origin.y; y < tilemap.origin.y + tilemap.size.y; y += (int)tilemap.cellSize.y)
        {
            for (int x = tilemap.origin.x; x < tilemap.origin.x + tilemap.size.x; x += (int)tilemap.cellSize.x)
            {
                // 물리엔진을 이용하여, 특정 지점에 충돌체가 있는지, 없는지를 기준으로 하여 벽돌을 체크한다.
                Collider2D collider = Physics2D.OverlapCircle(new Vector2(x, y), 0.4f);

                if (collider != null)
                {
                    Instantiate(checker, new Vector3(x + tilemap.tileAnchor.x, y + tilemap.tileAnchor.y, 0), Quaternion.identity);
                }
            }

            #region 타일맵의 타일 존재 여부를 체크하는 방법
            // 타일맵의 타일이 있는지, 없는지를 기준으로 벽톨을 체크
            // 타일맵.Has.Tile = 해당하는 위치에 타일맵이 존재하는지 bool형으로 반환하는 함수
            // bool hasTile = tilemap.HasTile(new Vector3Int(x, y, 0));

            // if (hasTile)
            //{
            //   Instantiate(checker, new Vector3(x + tilemap.tileAnchor.x, y + tilemap.tileAnchor.y, 0), Quaternion.identity);
            //}
            #endregion
        }

    }
}
