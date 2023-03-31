using Newtonsoft.Json;
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

        private static void LoadMonsterName()
        {
            HttpClient httpClient = new();
            string responseJson = httpClient.GetStringAsync("https://www.dnd5eapi.co/api/monsters").Result;

            MonstersResponse monstersResponse = JsonConvert.DeserializeObject<MonstersResponse>(responseJson);

            _monsterResults = monstersResponse.results;
            _monsterIndexNames = _monsterResults.Select(entry => entry.name).ToArray();
        }

        public static void ImportData(string name, MonsterType monsterType)
        {

        }
    }
}
