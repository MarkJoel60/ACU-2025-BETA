// Decompiled with JetBrains decompiler
// Type: PX.Api.Models.ImportModel
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Api.Models;

public class ImportModel
{
  public Command[] Commands { get; set; }

  public Filter[] Filters { get; set; }

  public string[][] Data { get; set; }

  public bool IncludedHeaders { get; set; }

  public bool BreakOnError { get; set; }

  public bool BreakOnIcorrectTarget { get; set; }
}
