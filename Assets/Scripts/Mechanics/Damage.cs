using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    public class Damage : Simulation.Event<Damage>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        public bool triggerInvincibility = true;

        public override void Execute()
        {
            //TODO: Optimize
            Health health = GameController.Instance.objectToBeDamaged.GetComponent<Health>();
            if (GameController.Instance.objectToBeDamaged.tag != "Player" 
                || (health != null && (!GameController.Instance.playerIsInvincible || GameController.Instance.bypassInvincibility)))
            {
                health.Decrement(GameController.Instance.damageAmount, triggerInvincibility);
            }
        }
    }
}
