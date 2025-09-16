// Decompiled with JetBrains decompiler
// Type: PX.Data.Reports.DAC.ReportFieldInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable enable
namespace PX.Data.Reports.DAC;

internal class ReportFieldInfo
{
  public 
  #nullable disable
  string Name { get; set; }

  public string Caption { get; set; }

  public TypeCode Type { get; set; }

  public string ViewName { get; set; }

  public int Precision { get; set; }

  public IEnumerable<(string Value, string Label)> Values { get; set; }

  public 
  #nullable enable
  PXFieldState? State { get; set; }
}
