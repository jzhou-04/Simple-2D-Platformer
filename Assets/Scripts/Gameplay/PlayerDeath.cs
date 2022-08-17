using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using Platformer.Unique;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player has died.
    /// </summary>
    /// <typeparam name="PlayerDeath"></typeparam>
    public class PlayerDeath : Simulation.Event<PlayerDeath>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var player = model.player;
            if (!player.health.IsAlive)
            {
                model.virtualCamera.m_Follow = null;
                model.virtualCamera.m_LookAt = null;
                player.transform.SetParent(null);
                player.gameObject.GetComponent<Collider2D>().enabled = false;
                player.controlEnabled = false;
                if (player.audioSource && player.ouchAudio)
                    player.audioSource.PlayOneShot(player.ouchAudio);
                player.animator.SetTrigger("hurt");
                player.animator.SetBool("dead", true);
                if (PoisonDamage.Instance != null) { PoisonDamage.Instance.StopPoison(); }
                if (EnemyRespawner.Instance != null) { EnemyRespawner.Instance.RespawnEnemies(); }
                if (BuffRespawner.Instance != null) { BuffRespawner.Instance.RespawnBuffs(); }
                Simulation.Schedule<PlayerSpawn>(2);
            }
        }
    }
}