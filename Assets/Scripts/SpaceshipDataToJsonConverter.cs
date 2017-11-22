using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class SpaceshipDataToJsonConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        SpaceshipData data = (SpaceshipData)value;

        string[,] tempParts = new string[data.MaxWidth, data.MaxHeight];

        for (int x = 0; x < data.MaxWidth; x++)
        {
            for (int y = 0; y < data.MaxHeight; y++)
            {
                tempParts[x, y] = JsonConvert.SerializeObject(data.BuiltParts[x, y], new BuiltPartToJsonConverter());
            }
        }

        JObject obj = new JObject
        {
            { "parts",       JArray.FromObject(tempParts)       },
            { "max_width",   JToken.FromObject(data.MaxWidth)   },
            { "max_height",  JToken.FromObject(data.MaxHeight)  }
        };

        obj.WriteTo(writer);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject obj = JObject.Load(reader);

        string[,] tempParts = new string[0,0];
        int maxWidth = 0;
        int maxHeight = 0;

        foreach (KeyValuePair<string, JToken> kvp in obj)
        {
            switch (kvp.Key)
            {
                case "parts":
                    tempParts = kvp.Value.ToObject<string[,]>();
                    break;
                case "max_width":
                    maxWidth = kvp.Value.ToObject<int>();
                    break;
                case "max_height":
                    maxHeight = kvp.Value.ToObject<int>();
                    break;
            }
        }

        BuiltPart[,] builtParts = new BuiltPart[maxWidth, maxHeight];

        for (int x = 0; x < maxWidth; x++)
        {
            for (int y = 0; y < maxHeight; y++)
            {
                builtParts[x, y] = JsonConvert.DeserializeObject<BuiltPart>(tempParts[x, y], new BuiltPartToJsonConverter());
            }
        }

        return new SpaceshipData(maxWidth, maxHeight, builtParts);
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType.IsAssignableFrom(typeof(SpaceshipData));
    }
}

public class BuiltPartToJsonConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        BuiltPart builtPart = (BuiltPart)value;

        JObject obj = new JObject
        {
            { "part_name",  JToken.FromObject(builtPart.PartName ?? "") },
            { "x",          JToken.FromObject(builtPart.x)              },
            { "y",          JToken.FromObject(builtPart.y)              },
            { "rotation",   JToken.FromObject(builtPart.rotation)       }
        };

        obj.WriteTo(writer);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject obj = JObject.Load(reader);

        BuiltPart builtPart;

        if (existingValue == null)
        {
            builtPart = new BuiltPart();
        }
        else
        {
            builtPart = (BuiltPart)existingValue;
        }

        foreach (KeyValuePair<string, JToken> kvp in obj)
        {
            switch(kvp.Key){
                case "part_name":
                    string partName = kvp.Value.ToObject<string>();

                    if (partName != null && partName != "" && PartManager.HasPart(partName))
                    {
                        builtPart.part = PartManager.GetPart(partName);
                    }

                    break;
                case "x":
                    builtPart.x = kvp.Value.ToObject<int>();
                    break;
                case "y":
                    builtPart.y = kvp.Value.ToObject<int>();
                    break;
                case "rotation":
                    builtPart.rotation = kvp.Value.ToObject<int>();
                    break;
            }
        }
        return builtPart;
    }
    
    public override bool CanConvert(Type objectType)
    {
        return objectType.IsAssignableFrom(typeof(BuiltPart));
    }
}
