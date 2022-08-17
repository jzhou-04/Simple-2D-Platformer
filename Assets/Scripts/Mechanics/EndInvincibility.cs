using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class EndInvincibility : Simulation.Event<EndInvincibility>
    {
        public override void Execute()
        {
            GameController.Instance.playerIsInvincible = false;
        }
    }
}
