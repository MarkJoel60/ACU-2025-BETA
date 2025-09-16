// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.Feedback.CellBound
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using Newtonsoft.Json;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.AP.InvoiceRecognition.Feedback;

internal class CellBound
{
  [JsonProperty("page")]
  public short Page { get; set; }

  [JsonProperty("table")]
  public short Table { get; set; }

  [JsonProperty("columns")]
  public List<short> Columns { get; set; }

  [JsonProperty("row")]
  public short Row { get; set; }

  [JsonProperty("detailColumn")]
  public string DetailColumn { get; set; }

  [JsonProperty("detailRow")]
  public short DetailRow { get; set; }
}
