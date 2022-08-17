using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class StartInvincibility : Simulation.Event<StartInvincibility>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            if (model.player.health.currentHP > 0)
            {
                GameController.Instance.playerIsInvincible = true;
                IEnumerator startInvincibility = model.player.StartInvincibilty();
                model.player.StartCoroutine(startInvincibility);
            }
        }
    }
}
