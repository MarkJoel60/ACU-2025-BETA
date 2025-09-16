// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Services.CustomFilterSerializer
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

#nullable disable
namespace PX.Data.Api.Services;

public class CustomFilterSerializer : ICustomFilterSerializer
{
  private JsonSerializerSettings _serializerSettings = new JsonSerializerSettings()
  {
    TypeNameHandling = (TypeNameHandling) 3,
    MetadataPropertyHandling = (MetadataPropertyHandling) 1,
    Converters = (IList<JsonConverter>) new List<JsonConverter>()
    {
      (JsonConverter) new CustomFilterSerializer.NumericJsonConverter()
    },
    SerializationBinder = (ISerializationBinder) new CustomFilterSerializer.KnownTypesBinder()
    {
      KnownTypes = (IList<System.Type>) new List<System.Type>()
      {
        typeof (PXBaseRedirectException.Filter[]),
        typeof (PXBaseRedirectException.Filter),
        typeof (PXFilterRow[]),
        typeof (PXFilterRow),
        typeof (int[]),
        typeof (string[])
      }
    }
  };

  public string Serialize(PXBaseRedirectException.Filter[] filters)
  {
    return filters != null && filters.Length != 0 ? HttpUtility.UrlEncode(Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject((object) filters, this._serializerSettings)))) : (string) null;
  }

  public PXBaseRedirectException.Filter[] Deserialize(string filters)
  {
    return !string.IsNullOrEmpty(filters) ? JsonConvert.DeserializeObject<PXBaseRedirectException.Filter[]>(Encoding.UTF8.GetString(Convert.FromBase64String(HttpUtility.UrlDecode(filters))), this._serializerSettings) : (PXBaseRedirectException.Filter[]) null;
  }

  private class NumericJsonConverter : JsonConverter
  {
    public virtual bool CanConvert(System.Type objectType) => objectType == typeof (object);

    public virtual bool CanRead => true;

    public virtual bool CanWrite => false;

    public virtual object ReadJson(
      JsonReader reader,
      System.Type objectType,
      object existingValue,
      JsonSerializer serializer)
    {
      return reader.ValueType == typeof (long) && objectType == typeof (object) ? (object) Convert.ToInt32(reader.Value) : serializer.Deserialize(reader);
    }

    public virtual void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      throw new NotImplementedException();
    }
  }

  public class KnownTypesBinder : ISerializationBinder
  {
    public IList<System.Type> KnownTypes { get; set; }

    public System.Type BindToType(string assemblyName, string typeName)
    {
      return this.KnownTypes.SingleOrDefault<System.Type>((Func<System.Type, bool>) (t => t.Name == typeName));
    }

    public void BindToName(System.Type serializedType, out string assemblyName, out string typeName)
    {
      assemblyName = (string) null;
      typeName = serializedType.Name;
    }
  }
}
