using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Climb : InteractableObject {

        public override void OnInteraction(InteractionType interactionType, string tag, bool isStay = false) {
            if (interactionType == InteractionType.enter) {
            Scene1Manager.Instance.player.SetInteraction(() => {
                   
                  
                }, "Climb");
            }
            if (interactionType == InteractionType.exit) {
            Scene1Manager.Instance.player.HideInteractionUI();
            }
        }
      
    }
