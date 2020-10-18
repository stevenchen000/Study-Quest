using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SpriteOrderData
{
    public SpriteRenderer renderer;
    public int layerOffset;

    public SpriteOrderData(SpriteRenderer rend)
    {
        renderer = rend;
    }
}

[ExecuteInEditMode]
public class SpriteOrderBasedOnY : MonoBehaviour
{
    [SerializeField]
    private List<SpriteOrderData> renderers = new List<SpriteOrderData>();
    
    // Start is called before the first frame update
    void Start()
    {
        var rends = transform.GetComponentsInChildren<SpriteRenderer>();
        RemoveExtraParts(rends);
        AddMissingParts(rends);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRenderersBasedOnDistance();
    }




    private void UpdateRenderersBasedOnDistance()
    {
        float y = transform.position.y;

        for(int i = 0; i < renderers.Count; i++)
        {
            SpriteRenderer rend = renderers[i].renderer;
            int offset = renderers[i].layerOffset;
            rend.sortingOrder = (int)(-y * 3) + offset;
        }
    }



    private void AddMissingParts(SpriteRenderer[] rends)
    {
        for(int i = 0; i < rends.Length; i++)
        {
            SpriteRenderer rend = rends[i];
            if (!RendererExists(rend))
            {
                SpriteOrderData data = new SpriteOrderData(rend);
                renderers.Add(data);
            }
        }
    }

    private void RemoveExtraParts(SpriteRenderer[] rends)
    {
        int index = 0;
        while(index < renderers.Count)
        {
            SpriteRenderer rend = renderers[index].renderer;
            if(rend == null || rends.Contains(rend))
            {
                renderers.RemoveAt(index);
                continue;
            }
            index++;
        }
    }

    private bool RendererExists(SpriteRenderer rend)
    {
        bool result = false;
        for(int i = 0; i < renderers.Count; i++)
        {
            if(renderers[i].renderer == rend)
            {
                result = true;
                break;
            }
        }
        return result;
    }
}
