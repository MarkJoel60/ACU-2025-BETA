// Decompiled with JetBrains decompiler
// Type: PX.Data.PXPersonalDataFieldAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
public class PXPersonalDataFieldAttribute : PXEventSubscriberAttribute
{
  public bool IsKey;
  public object DefaultValue;
  internal static Dictionary<System.Type, List<PXPersonalDataFieldAttribute>> Fields = new Dictionary<System.Type, List<PXPersonalDataFieldAttribute>>();

  public virtual bool IsKeyValueTable => false;

  internal static IEnumerable<System.Type> TablesWithFieldsSpecified
  {
    get
    {
      DacMetadata.InitializationCompleted.Wait();
      return (IEnumerable<System.Type>) PXPersonalDataFieldAttribute.Fields.Keys;
    }
  }

  protected internal override void SetBqlTable(System.Type bqlTable)
  {
    base.SetBqlTable(bqlTable);
    if (bqlTable != this._BqlTable)
      return;
    lock (((ICollection) PXPersonalDataFieldAttribute.Fields).SyncRoot)
    {
      List<PXPersonalDataFieldAttribute> source;
      if (!PXPersonalDataFieldAttribute.Fields.TryGetValue(this._BqlTable, out source))
        PXPersonalDataFieldAttribute.Fields[this._BqlTable] = source = new List<PXPersonalDataFieldAttribute>();
      PXPersonalDataFieldAttribute val = this;
      if (!source.All<PXPersonalDataFieldAttribute>((Func<PXPersonalDataFieldAttribute, bool>) (_ => _.FieldName != val.FieldName)))
        return;
      source.Add(val);
    }
  }

  public static bool GetPersonalDataFields(System.Type table, out List<PXPersonalDataFieldAttribute> list)
  {
    list = (List<PXPersonalDataFieldAttribute>) null;
    DacMetadata.InitializationCompleted.Wait();
    if (PXPersonalDataFieldAttribute.Fields.TryGetValue(table, out list))
      return list.Any<PXPersonalDataFieldAttribute>((Func<PXPersonalDataFieldAttribute, bool>) (_ => _.GetType() == typeof (PXPersonalDataFieldAttribute.Value)));
    list = new List<PXPersonalDataFieldAttribute>();
    return false;
  }

  public override void CacheAttached(PXCache sender)
  {
    sender.Graph.FieldSelecting.AddHandler(sender.GetItemType(), this.FieldName, new PXFieldSelecting(this.FieldSelecting));
  }

  private void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null)
      return;
    object obj = sender.GetValue(e.Row, "PseudonymizationStatus");
    if (obj == null)
      return;
    bool flag = ((int) obj & 1) == 1;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (System.Type) null, enabled: new bool?(!flag), visibility: PXUIVisibility.Visible);
  }

  public class Value : PXPersonalDataFieldAttribute
  {
    public override bool IsKeyValueTable => true;
  }
}
