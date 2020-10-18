using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class SetBackgroundSpriteOrder : MonoBehaviour
{
    private TilemapRenderer rend;
    [Range(0,10)]
    [SerializeField]
    private int offset;
    // Start is called before the first frame update
    void Start()
    {
        rend = transform.GetComponentInChildren<TilemapRenderer>();
        UpdateSortOrder();
    }




    private void UpdateSortOrder()
    {
        int value = -30000;

        rend.sortingOrder = value + offset;
    }
}
