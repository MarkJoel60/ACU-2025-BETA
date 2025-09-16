// Decompiled with JetBrains decompiler
// Type: PX.Data.BusinessProcess.Event
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data.BusinessProcess;

public class Event
{
  public Guid Id { get; set; }

  public Guid DefinitionId { get; set; }

  public Guid? RefNoteID { get; set; }

  public string RefEntityType { get; set; }

  public string Name { get; set; }

  public int RetryLimit { get; set; }

  public int Retry { get; set; }

  public System.DateTime CreatedDateTimeUtc { get; set; }

  /// <summary>
  /// Collection of matched old/new rows. First item is old row, second item is new row
  /// </summary>
  public MatchedRow[] Rows { get; set; }

  public Tuple<string, string[]>[] KeyFields { get; set; }

  public string ConnectionString { get; set; }

  public string CompanyId { get; set; }

  public int? Branch { get; set; }

  public string Timezone { get; set; }

  public bool RunSynchronously { get; set; }
}
