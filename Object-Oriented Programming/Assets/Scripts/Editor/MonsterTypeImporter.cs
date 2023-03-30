using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

namespace MonsterQuest
{
    public static class MonsterTypeImporter
    {
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
        }
    }
}
