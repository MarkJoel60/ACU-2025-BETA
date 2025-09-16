// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.ShipmentRelatedReports
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.SO;

public class ShipmentRelatedReports
{
  public List<string> LaserLabels { get; } = new List<string>();

  public List<FileInfo> LabelFiles { get; } = new List<FileInfo>();

  public PXReportRequiredException ReportRedirect { get; set; }
}
