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
        }
    }
}
