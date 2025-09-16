// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ClockInClockOut.ClockInClockOutBase`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.PM;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.EP.ClockInClockOut;

[PXInternalUseOnly]
public abstract class ClockInClockOutBase<TGraph, TEntity> : 
  PXGraphExtension<TGraph>,
  IClockInClockOut
  where TGraph : PXGraph
  where TEntity : class, IBqlTable, new()
{
  public static bool IsActive() => PXAccess.FeatureInstalled<PX.Objects.CS.FeaturesSet.clockInClockOut>();

  protected virtual void _(PX.Data.Events.RowPersisted<TEntity> e)
  {
    if (e.Operation != PXDBOperation.Delete || e.TranStatus != PXTranStatus.Open)
      return;
    Guid? nullable = e.Cache.GetValue((object) e.Row, this.NoteIdField) as Guid?;
    if (!nullable.HasValue)
      return;
    PXUpdate<Set<EPTimeLog.relatedEntityID, Null>, EPTimeLog, Where<EPTimeLog.relatedEntityID, Equal<Required<EPTimeLog.relatedEntityID>>>>.Update((PXGraph) this.Base, (object) nullable);
  }

  public virtual TimeLogData GetTimeLogData(Guid? noteID)
  {
    return new TimeLogData()
    {
      ProjectID = ProjectDefaultAttribute.NonProject(),
      TaskID = new int?(),
      DocumentNbr = (string) null,
      Summary = (string) null
    };
  }

  public virtual System.Type DbTable => this.Base.PrimaryItemType;

  public virtual string NoteIdField => EntityHelper.GetNoteField(this.DbTable);

  public virtual Guid? SelectEntityID(Dictionary<string, string> fields)
  {
    Guid? nullable = new Guid?();
    HashSet<string> hashSet = this.Base.GetPrimaryCache().Keys.ToHashSet<string>();
    List<PXDataField> pxDataFieldList = new List<PXDataField>()
    {
      new PXDataField(this.NoteIdField)
    };
    foreach (KeyValuePair<string, string> field in fields)
    {
      if (hashSet.Contains(field.Key))
      {
        string str = field.Value.TrimEnd().TrimEnd('+');
        pxDataFieldList.Add(new PXDataField(field.Key));
        pxDataFieldList.Add((PXDataField) new PXDataFieldValue(field.Key, (object) str));
      }
    }
    using (PXDataRecord pxDataRecord = PXDatabase.SelectSingle(this.DbTable, pxDataFieldList.ToArray()))
      return (Guid?) pxDataRecord?.GetGuid(0);
  }
}
