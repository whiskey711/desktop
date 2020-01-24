using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace Uvic_Ecg_Model
{
    public class Feed<T>
    {
        [JsonProperty] private string id;
        [JsonProperty] private long? totalNum;
        [JsonProperty] private long? pageSize;
        [JsonProperty] private long? pageNum;
        [JsonProperty] private List<Entity<T>> entities;
        public Feed()
        {
        }
        public string Id { get => id; set => id = value; }
        public long? TotalNum { get => totalNum; set => totalNum = value; }
        public long? PageSize { get => pageSize; set => pageSize = value; }
        public long? PageNum { get => pageNum; set => pageNum = value; }
        public List<Entity<T>> Entities { get => entities; set => entities = value; }
    }
}
