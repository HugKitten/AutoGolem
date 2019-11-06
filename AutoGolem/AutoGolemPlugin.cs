using ExileCore;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using ExileCore.Shared;
using SharpDX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoGolem
{
    public class AutoGolemPlugin : BaseSettingsPlugin<AutoGolemSettings>
    {
        private void OnToggleSSkillSetNodeChange()
        {
            Input.RegisterKey(this.Settings.ToggleSkillSetNode);
        }

        public override bool Initialise()
        {
            Input.RegisterKey(this.Settings.ToggleSkillSetNode);
            this.Settings.ToggleSkillSetNode.OnValueChanged += OnToggleSSkillSetNodeChange;
            return true;
        }

        public override Job Tick()
        {
            var player = this.GameController.Player;
            var life = player.GetComponent<Life>();
            var deployedObjects = player.GetComponent<Actor>().DeployedObjects;
            var entities = this.GameController.Entities;
            var aliveEnties = deployedObjects.Select(o => o.Entity).Where(e => e != null && e.IsAlive).ToList();
            
            // Return if forground
            if (!GameController.Window.IsForeground())
            {
                return null;
            }

            // Return if invalid local player
            if (!GameController.Game.IngameState.Data.LocalPlayer.IsValid)
            {
                return null;
            }

            // Return if disabled
            if (!this.Settings.Enable)
            {
                return null;
            }

            // Return if life
            if (this.Settings.MinLife.Value / 100F > life.HPPercentage)
            {
                return null;
            }

            // Return if shield
            if (this.Settings.MinShield / 100F > life.ESPercentage)
            {
                return null;
            }

            // Return if distance
            if (this.Settings.MinDistance > 0)
            {
                foreach (var entity in entities)
                {
                    if (entity.IsAlive && entity.IsHostile)
                    {
                        float distance = (player.Pos - entity.Pos).Length();
                        if (distance <= this.Settings.MinDistance)
                        {
                            return null;
                        }
                    }
                }
            }

            // Get which golems are alive
            bool chaosAlive = aliveEnties.Any(e => e.Path.Contains("ChaosElemental"));
            bool fireAlive = aliveEnties.Any(e => e.Path.Contains("FireElemental"));
            bool iceAlive = aliveEnties.Any(e => e.Path.Contains("IceElemental"));
            bool lightningAlive = aliveEnties.Any(e => e.Path.Contains("LightningGolem"));
            bool stoneAlive = aliveEnties.Any(e => e.Path.Contains("RockGolem"));
            bool bestialUrsa = aliveEnties.Any(e => e.Path.Contains("DropBearUniqueSummoned"));

            // Get keys to press
            List<Keys> pressMe = new List<Keys>();
            if (this.Settings.ChaosGolem.Enable && !chaosAlive)
            {
                pressMe.Add(this.Settings.ChaosGolem.HotKey);
            }
            if (this.Settings.FireGolem.Enable && !fireAlive)
            {
                pressMe.Add(this.Settings.FireGolem.HotKey);
            }
            if (this.Settings.IceGolem.Enable && !iceAlive)
            {
                pressMe.Add(this.Settings.IceGolem.HotKey);
            }
            if (this.Settings.LightingGolem.Enable && !lightningAlive)
            {
                pressMe.Add(this.Settings.LightingGolem.HotKey);
            }
            if (this.Settings.StoneGolem.Enable && !stoneAlive)
            {
                pressMe.Add(this.Settings.StoneGolem.HotKey);
            }
            if (this.Settings.BestialUrsa.Enable && !bestialUrsa)
            {
                pressMe.Add(this.Settings.BestialUrsa.HotKey);
            }

            foreach(var press in pressMe)
            {
                if (press.HasFlag(this.Settings.ToggleSkillSetNode))
                {
                    Input.KeyDown(press);
                    Input.KeyUp(press);
                }
                else if (!Input.IsKeyDown(this.Settings.ToggleSkillSetNode))
                {
                    Input.KeyDown(press);
                    Input.KeyUp(press);
                }
            }

            return null;
        }
    }
}
