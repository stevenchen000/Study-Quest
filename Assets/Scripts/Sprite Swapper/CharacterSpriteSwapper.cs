using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SpriteSwapper
{
    [Serializable]
    public class SpriteBodyPart
    {
        public SpriteRenderer rend;
        public string partName;
        public Sprite sprite;
    }

    [ExecuteInEditMode]
    public class CharacterSpriteSwapper : MonoBehaviour
    {
        [SerializeField]
        private CharacterSpriteSet sprites;
        private CharacterSpriteSet previousSprites;
        [SerializeField]
        private List<SpriteBodyPart> parts = new List<SpriteBodyPart>();


        private Dictionary<string, SpriteBodyPart> _parts = new Dictionary<string, SpriteBodyPart>();

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (Application.isEditor && !Application.isPlaying)
            {
                var renderers = transform.GetComponentsInChildren<SpriteRenderer>();
                RemoveExtraBodyParts(renderers);
                AddMissingBodyParts(renderers);
                UpdateBodyParts();

                if(sprites != previousSprites)
                {
                    previousSprites = sprites;
                    UpdatePartsFromSet(sprites);
                }
            }
        }


        public void ChangeBodyPart(string partName, Sprite newSprite)
        {
            for(int i = 0; i < parts.Count; i++)
            {
                SpriteBodyPart part = parts[i];
                if(string.Compare(part.partName, partName, true) == 0)
                {
                    if (newSprite != null)
                    {
                        part.sprite = newSprite;
                        part.rend.sprite = newSprite;
                    }
                }
            }
        }

        public void ChangePartSet(CharacterSpriteSet set)
        {
            sprites = set;
            UpdatePartsFromSet(set);
        }

        public void UpdatePartsFromSet(CharacterSpriteSet set)
        {
            List<SpriteSetData> data = set.data;
            for(int i = 0; i < data.Count; i++)
            {
                SpriteSetData part = data[i];
                string partName = part.partName;
                Sprite partSprite = part.sprite;

                ChangeBodyPart(partName, partSprite);
            }
        }

        private void UpdateBodyParts()
        {
            for (int i = 0; i < parts.Count; i++)
            {
                SpriteBodyPart part = parts[i];
                if (part.sprite != null)
                {
                    part.rend.sprite = part.sprite;
                }
            }
        }



        private void AddMissingBodyParts(SpriteRenderer[] renderers)
        {
            for(int i = 0; i < renderers.Length; i++)
            {
                SpriteRenderer rend = renderers[i];
                if (!HasBodyPart(rend))
                {
                    SpriteBodyPart part = new SpriteBodyPart() { rend = rend };
                    parts.Add(part);
                }
            }
        }

        private void RemoveExtraBodyParts(SpriteRenderer[] renderers)
        {
            int index = 0;
            while (index < parts.Count)
            {
                SpriteBodyPart part = parts[index];
                if (part.rend == null || !HasRenderer(part.rend, renderers))
                {
                    parts.RemoveAt(index);
                    continue;
                }
                index++;
            }
        }

        private bool HasRenderer(SpriteRenderer rend, SpriteRenderer[] renderers)
        {
            bool result = false;
            for(int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i] == rend)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private bool HasBodyPart(SpriteRenderer rend)
        {
            bool result = false;

            for(int i = 0; i < parts.Count; i++)
            {
                if(parts[i].rend == rend)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
    }
}