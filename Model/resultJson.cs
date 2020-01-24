using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace Uvic_Ecg_Model
{
    public class ResultJson
    {
        [JsonProperty] private String message;
        public ResultJson(String error)
        {
            this.message = error;
        }
        public string Message { get => message; set => message = value; }
    }
}
