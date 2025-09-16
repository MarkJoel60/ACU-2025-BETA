// Decompiled with JetBrains decompiler
// Type: PX.Data.Archiving.ArchivableTablesAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Archiving;

public class ArchivableTablesAttribute : PXStringListAttribute, IPXFieldUpdatedSubscriber
{
  private string[] _fullNames;

  public System.Type TypeNameField { get; set; }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    IEnumerable<System.Type> archivableTables = PXDBGotReadyForArchiveAttribute.GetArchivableTables();
    if (archivableTables != null && archivableTables.Count<System.Type>() > 0)
    {
      this._fullNames = archivableTables.Select<System.Type, string>((Func<System.Type, string>) (t => t.FullName)).ToArray<string>();
      this._AllowedValues = archivableTables.Select<System.Type, string>((Func<System.Type, string>) (t => t.Name)).ToArray<string>();
      this._AllowedLabels = archivableTables.Select<System.Type, string>((Func<System.Type, string>) (t => PXCacheNameAttribute.GetName(t) ?? t.Name)).ToArray<string>();
    }
    else
    {
      string[] strArray = new string[1]{ "" };
      this._fullNames = strArray;
      this._AllowedValues = strArray;
      this._AllowedLabels = strArray;
    }
    base.FieldSelecting(sender, e);
  }

  void IPXFieldUpdatedSubscriber.FieldUpdated(PXCache cache, PXFieldUpdatedEventArgs e)
  {
    if (!(this.TypeNameField != (System.Type) null) || e.Row == null)
      return;
    if (cache.GetValue(e.Row, this._FieldOrdinal) is string str)
    {
      int index = Array.IndexOf<string>(this._AllowedValues, str);
      if (index >= 0)
      {
        cache.SetValue(e.Row, this.TypeNameField.Name, (object) this._fullNames[index]);
        return;
      }
    }
    cache.SetValue(e.Row, this.TypeNameField.Name, (object) null);
  }
}
