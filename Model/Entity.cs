using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace Uvic_Ecg_Model
{
    public class Entity<T>
    {
        [JsonProperty] private string id;
        [JsonProperty] private T model;
        public Entity()
        {
        }
        public string Id { get => id; set => id = value; }
        public T Model { get => model; set => model = value; }
    }
}
