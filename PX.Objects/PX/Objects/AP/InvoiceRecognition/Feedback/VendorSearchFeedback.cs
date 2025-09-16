// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.Feedback.VendorSearchFeedback
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PX.Objects.AP.InvoiceRecognition.Feedback.VendorSearch;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition.Feedback;

internal class VendorSearchFeedback
{
  internal static readonly JsonSerializerSettings Settings = new JsonSerializerSettings()
  {
    NullValueHandling = (NullValueHandling) 1,
    ContractResolver = (IContractResolver) new DefaultContractResolver()
    {
      NamingStrategy = (NamingStrategy) new CamelCaseNamingStrategy()
    }
  };

  [JsonProperty("$version")]
  public byte Version { get; set; } = 1;

  public List<Search> Searches { get; set; }

  public Dictionary<string, Candidate> Candidates { get; set; }

  public Found Winner { get; set; }

  public override string ToString()
  {
    return JsonConvert.SerializeObject((object) this, VendorSearchFeedback.Settings);
  }
}
