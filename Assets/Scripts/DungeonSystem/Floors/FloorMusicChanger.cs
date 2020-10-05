using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonSystem{
    public class FloorMusicChanger : MonoBehaviour
    {
        
        public void PlayCombatSong(){
            DungeonManager dungeon = FindObjectOfType<DungeonManager>();
            AudioClip song = dungeon.GetCombatMusic();
            AudioManager.PlaySong(song);
        }
    }
}