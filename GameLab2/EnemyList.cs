using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace GameLab2
{
    public class EnemyTemplate
    {
        public string Name { get; set; }
        public string IconName { get; set; }
        public int BaseLife { get; set; }
        public double LifeModifier { get; set; }
        public int BaseGold { get; set; }
        public double GoldModifier { get; set; }
        public double SpawnChance { get; set; }
    }
    class EnemyList
    {
        private List<EnemyTemplate> templates = new List<EnemyTemplate>();
        private Random rng = new Random();

        public void LoadFromJson(string path)
        {
            if (!File.Exists(path)) return;

            string jsonFromFile = File.ReadAllText(path);
            templates.Clear();

            using (JsonDocument doc = JsonDocument.Parse(jsonFromFile))
            {
                foreach (JsonElement element in doc.RootElement.EnumerateArray())
                {
                    EnemyTemplate template = new EnemyTemplate
                    {
                        Name = element.GetProperty("Name").GetString(),
                        IconName = element.GetProperty("IconName").GetString(),
                        BaseLife = element.GetProperty("BaseLife").GetInt32(),
                        LifeModifier = element.GetProperty("LifeModifier").GetDouble(),
                        BaseGold = element.GetProperty("BaseGold").GetInt32(),
                        GoldModifier = element.GetProperty("GoldModifier").GetDouble(),
                        SpawnChance = element.GetProperty("SpawnChance").GetDouble()
                    };
                    templates.Add(template);
                }
            }

            NormalizeChances();
        }

        private void NormalizeChances()
        {
            if (templates.Count == 0) return;

            double sum = 0;
            for (int i = 0; i < templates.Count; i++)
                sum += templates[i].SpawnChance;

            if (sum > 0)
            {
                for (int i = 0; i < templates.Count; i++)
                    templates[i].SpawnChance /= sum;
            }
        }

        private EnemyTemplate FindByChance(double chance)
        {
            double sum = 0;
            for (int i = 0; i < templates.Count; i++)
            {
                sum += templates[i].SpawnChance;
                if (sum >= chance) return templates[i];
            }
            return templates.Count > 0 ? templates[0] : null;
        }

        public Enemy GetNextEnemy(int defeatedCount)
        {
            EnemyTemplate template = FindByChance(rng.NextDouble());

            if (template == null)
            {
                return new Enemy("Заглушка", new BigNumber("100"), new BigNumber("10"), "default.png");
            }

            BigNumber hpValue = new BigNumber(template.BaseLife.ToString());
            BigNumber goldValue = new BigNumber(template.BaseGold.ToString());

            for (int k = 0; k < defeatedCount; k++)
            {
                hpValue = hpValue * (1.0 + template.LifeModifier);
                goldValue = goldValue * (1.0 + template.GoldModifier);
            }

            return new Enemy(template.Name, hpValue, goldValue, template.IconName);
        }
    }
}
