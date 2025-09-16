// Decompiled with JetBrains decompiler
// Type: PX.Data.RecordDataConsistencyIssue
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

[PXHidden]
public class RecordDataConsistencyIssue : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(IsKey = true)]
  public string ID { get; set; }

  public string Name { get; set; }

  public System.DateTime Tm { get; set; }

  public string Message { get; set; }
}
