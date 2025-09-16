// Decompiled with JetBrains decompiler
// Type: PX.Data.Archiving.PXDBGotReadyForArchiveAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Metadata;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Archiving;

/// <summary>
/// Indicates that the target field is used to identify when a record has got ready for being archived.
/// </summary>
public class PXDBGotReadyForArchiveAttribute : PXDBDateAttribute
{
  private static readonly ConcurrentDictionary<System.Type, string> ArchivableTables = new ConcurrentDictionary<System.Type, string>();

  protected internal override void SetBqlTable(System.Type bqlTable)
  {
    base.SetBqlTable(bqlTable);
    if (this.AttributeLevel != PXAttributeLevel.Type || PXDatabase.IsVirtualTable(bqlTable))
      return;
    PXDBGotReadyForArchiveAttribute.ArchivableTables.AddOrUpdate(bqlTable, this.FieldName, (Func<System.Type, string, string>) ((table, field) => this.FieldName));
  }

  public static IEnumerable<System.Type> GetArchivableTables()
  {
    DacMetadata.InitializationCompleted.Wait();
    return (IEnumerable<System.Type>) PXDBGotReadyForArchiveAttribute.ArchivableTables.Keys.ToArray<System.Type>();
  }

  public static string GetDateFieldOf(System.Type table)
  {
    DacMetadata.InitializationCompleted.Wait();
    string str;
    return !PXDBGotReadyForArchiveAttribute.ArchivableTables.TryGetValue(table, out str) ? (string) null : str;
  }
}
