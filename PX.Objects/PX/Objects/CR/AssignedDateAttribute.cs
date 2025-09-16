// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.AssignedDateAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
[PXDBDate(PreserveTime = true)]
public class AssignedDateAttribute : PXAggregateAttribute
{
  private readonly System.Type _workgorupID;
  private readonly System.Type _ownerID;

  public AssignedDateAttribute(System.Type workgroupID, System.Type ownerID)
  {
    this._workgorupID = workgroupID;
    this._ownerID = ownerID;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    System.Type itemType = sender.GetItemType();
    if (string.IsNullOrEmpty(sender.GetField(this._workgorupID)))
      throw new Exception($"Field '{this._workgorupID.Name}' can not be not found in table '{itemType.Name}'");
    if (string.IsNullOrEmpty(sender.GetField(this._ownerID)))
      throw new Exception($"Field '{this._ownerID.Name}' can not be not found in table '{itemType.Name}'");
    // ISSUE: method pointer
    sender.Graph.RowInserted.AddHandler(itemType, new PXRowInserted((object) this, __methodptr(RowInsertedHandler)));
    // ISSUE: method pointer
    sender.Graph.RowUpdated.AddHandler(itemType, new PXRowUpdated((object) this, __methodptr(RowUpdatedHandler)));
  }

  private void RowUpdatedHandler(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.GetValue(e.Row, this._ownerID.Name) == null && sender.GetValue(e.Row, this._workgorupID.Name) == null)
    {
      sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) null);
    }
    else
    {
      if (object.Equals(sender.GetValue(e.Row, this._ownerID.Name), sender.GetValue(e.OldRow, this._ownerID.Name)) && object.Equals(sender.GetValue(e.Row, this._workgorupID.Name), sender.GetValue(e.OldRow, this._workgorupID.Name)))
        return;
      sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, (object) PXTimeZoneInfo.Now);
    }
  }

  private void RowInsertedHandler(PXCache sender, PXRowInsertedEventArgs e)
  {
    object obj = (object) null;
    if (sender.GetValue(e.Row, this._ownerID.Name) != null || sender.GetValue(e.Row, this._workgorupID.Name) != null)
      obj = (object) PXTimeZoneInfo.Now;
    sender.SetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal, obj);
  }
}
