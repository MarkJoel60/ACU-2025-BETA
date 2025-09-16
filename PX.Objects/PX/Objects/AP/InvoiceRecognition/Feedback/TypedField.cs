// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.Feedback.TypedField
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PX.CloudServices.DocumentRecognition;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition.Feedback;

internal class TypedField : Field
{
  [JsonConverter(typeof (StringEnumConverter))]
  [JsonProperty("type")]
  public FieldTypes? Type { get; set; }

  [JsonProperty("entityId")]
  public string EntityId { get; set; }
}
