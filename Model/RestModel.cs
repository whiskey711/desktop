using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace Uvic_Ecg_Model
{
    public class RestModel<T>
    {
        [JsonProperty] private Entity<T> entity;
        [JsonProperty] private Feed<T> feed;
        [JsonProperty] private string errorMessage;
        [JsonConstructor]
        public RestModel(Entity<T> entity, string errorInfo, Feed<T> feed)
        {
            this.errorMessage = errorInfo;
            this.entity = entity;
            this.feed = feed;
        }
        public Entity<T> Entity { get => entity; set => entity = value; }
        public Feed<T> Feed { get => feed; set => feed = value; }
        public string ErrorMessage { get => errorMessage; set => errorMessage = value; }
    }
}
