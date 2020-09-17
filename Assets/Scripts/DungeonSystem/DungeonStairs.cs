using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DungeonSystem
{
    public class DungeonStairs : MonoBehaviour, IInteractable
    {
        
        public void Interact(IInteractor interactor)
        {
            throw new NotImplementedException();
        }

        public bool IsColliding(Collider2D collider)
        {
            throw new NotImplementedException();
        }
        

        private void LoadNextFloor()
        {
            if (IsLastFloor())
            {
                string hubWorld = WorldState.GetHubName();
                UnityUtilities.LoadLevel(hubWorld);
            }
            else
            {

            }
        }

        private bool IsLastFloor()
        {
            return false;
        }
    }
}
