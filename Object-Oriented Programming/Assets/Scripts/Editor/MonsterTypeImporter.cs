using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using UnityEngine;

namespace MonsterQuest
{
    public static class MonsterTypeImporter
    {
        private static MonsterResult[] _monsterResults;
        private static string[] _monsterIndexNames;

        public static string[] monsterIndexNames
        {
            get
            {
                if (_monsterResults == null)
                {
                    LoadMonsterName();
                }

                return _monsterIndexNames;
            }
        }

        private class MonsterResult
        {
            public string index;
            public string name;
            public string url;
        }

        private class MonstersResponse
        {
            public int count;
            public MonsterResult[] results;
        }

        private static HttpClient httpClient = new();

        private static void LoadMonsterName()
        {
            string responseJson = httpClient.GetStringAsync("https://www.dnd5eapi.co/api/monsters").Result;

            MonstersResponse monstersResponse = JsonConvert.DeserializeObject<MonstersResponse>(responseJson);

            _monsterResults = monstersResponse.results;
            _monsterIndexNames = _monsterResults.Select(entry => entry.name).ToArray();
        }

        public static void ImportData(string name, MonsterType monsterType)
        {
            string monsterIndex = _monsterResults.Where(monsterResult => monsterResult.name == name).First().index;

            string responseJson = httpClient.GetStringAsync($"https://www.dnd5eapi.co/api/monsters/{monsterIndex}").Result;

            JObject monsterData = JObject.Parse(responseJson);
            monsterType.displayName = (string)monsterData["name"];
            monsterType.sizeCategory = Enum.Parse<SizeCategory>((string)monsterData["size"]);
            monsterType.hitPoint = (string)monsterData["hit_points_roll"];
            monsterType.alignment = (string)monsterData["alignment"];

            JArray arrays = (JArray)monsterData["armor_class"];
            foreach (JToken array in arrays)
            {
                monsterType.armorClass = (int)array["value"];
            }

            monsterType.abilityScores.strenght.score = (int)monsterData["strength"];
            monsterType.abilityScores.dexterity.score = (int)monsterData["dexterity"];
            monsterType.abilityScores.constitusion.score = (int)monsterData["constitution"];
            monsterType.abilityScores.wisdom.score = (int)monsterData["wisdom"];
            monsterType.abilityScores.intelligence.score = (int)monsterData["intelligence"];
            monsterType.abilityScores.charisma.score = (int)monsterData["charisma"];

        }
    }
}
