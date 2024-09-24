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
        // Ÿ�ϸ��� ó������ ������ �ҷ����� �ݺ���   
        for (int y = tilemap.origin.y; y < tilemap.origin.y + tilemap.size.y; y += (int)tilemap.cellSize.y)
        {
            for (int x = tilemap.origin.x; x < tilemap.origin.x + tilemap.size.x; x += (int)tilemap.cellSize.x)
            {
                // ���������� �̿��Ͽ�, Ư�� ������ �浹ü�� �ִ���, �������� �������� �Ͽ� ������ üũ�Ѵ�.
                Collider2D collider = Physics2D.OverlapCircle(new Vector2(x, y), 0.4f);

                if (collider != null)
                {
                    Instantiate(checker, new Vector3(x + tilemap.tileAnchor.x, y + tilemap.tileAnchor.y, 0), Quaternion.identity);
                }
            }

            #region Ÿ�ϸ��� Ÿ�� ���� ���θ� üũ�ϴ� ���
            // Ÿ�ϸ��� Ÿ���� �ִ���, �������� �������� ������ üũ
            // Ÿ�ϸ�.Has.Tile = �ش��ϴ� ��ġ�� Ÿ�ϸ��� �����ϴ��� bool������ ��ȯ�ϴ� �Լ�
            // bool hasTile = tilemap.HasTile(new Vector3Int(x, y, 0));

            // if (hasTile)
            //{
            //   Instantiate(checker, new Vector3(x + tilemap.tileAnchor.x, y + tilemap.tileAnchor.y, 0), Quaternion.identity);
            //}
            #endregion
        }

    }
}
