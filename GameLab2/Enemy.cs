using System;
using System.Collections.Generic;
using System.Text;

namespace GameLab2
{
    internal class Enemy
    {
        private string name;
        private BigNumber maxHitPoints;
        private BigNumber currentHitPoints;
        private BigNumber goldReward;
        private bool isDead;
        private string icon;
        public string Name { get => name; private set => name = value; }
        public BigNumber MaxHitPoints { get => maxHitPoints; private set => maxHitPoints = value; }
        public BigNumber CurrentHitPoints { get => currentHitPoints; private set => currentHitPoints = value; }
        public BigNumber GoldReward { get => goldReward; private set => goldReward = value; }
        public bool IsDead { get => isDead; private set => isDead = value; }
        public string Icon { get => icon; private set => icon = value; }

        public Enemy(string name, BigNumber maxHitPoints, BigNumber goldReward, string icon = "")
        {
            this.name = name;
            this.maxHitPoints = maxHitPoints;
            this.currentHitPoints = maxHitPoints;
            this.goldReward = goldReward;
            this.icon = icon;
            this.isDead = false;
        }
    }
}

