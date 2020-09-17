using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


[CreateAssetMenu(menuName = "Character Data")]
public class CharacterData : ScriptableObject
{
    public GameObject model;
    public int currentHealth = 10;
    public int maxHealth = 10;
}

