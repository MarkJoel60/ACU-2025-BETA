// Decompiled with JetBrains decompiler
// Type: PX.Owin.DeviceHub.ScanJob
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Owin.DeviceHub;

public class ScanJob : BaseJob
{
  public string ScannerName { get; set; }

  public string EntityScreenID { get; set; }

  public Guid EntityNoteID { get; set; }

  public string EntityPrimaryViewName { get; set; }

  public int PaperSource { get; set; }

  public int PixelType { get; set; }

  public int Resolution { get; set; }

  public int FileType { get; set; }

  public string FileName { get; set; }

  public string RequestingUserName { get; set; }

  public List<ScanJobParameter> Parameters { get; set; } = new List<ScanJobParameter>();

  public void AddParameter(string viewName, string pName, string pValue)
  {
    this.Parameters.Add(new ScanJobParameter()
    {
      ViewName = viewName,
      ParameterName = pName,
      ParameterValue = pValue
    });
  }
}
