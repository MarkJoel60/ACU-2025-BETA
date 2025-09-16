// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.SingleGroupAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.CS;

[PXDBBinary]
[PXSelector(typeof (Search<RelationGroup.groupMask, Where<RelationGroup.active, Equal<boolTrue>>>), SubstituteKey = typeof (RelationGroup.groupName), DescriptionField = typeof (RelationGroup.description))]
[PXUIField(DisplayName = "Default Restriction Group")]
public class SingleGroupAttribute : 
  PXAggregateAttribute,
  IPXRowSelectedSubscriber,
  IPXFieldSelectingSubscriber
{
  public virtual void RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null || !(sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal) is byte[] val) || !this.isMultiple(val))
      return;
    sender.SetAltered(((PXEventSubscriberAttribute) this)._FieldName, true);
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.ReturnValue is byte[] returnValue)
    {
      if (returnValue.Length == 0)
      {
        e.ReturnValue = (object) null;
      }
      else
      {
        if (!this.isMultiple(returnValue))
          return;
        e.ReturnValue = (object) PXMessages.LocalizeNoPrefix("<MULTIPLE>");
        if (!(e.ReturnState is PXFieldState))
          return;
        ((PXFieldState) e.ReturnState).Enabled = false;
      }
    }
    else
    {
      if (!(e.ReturnValue is string) || !string.Equals((string) e.ReturnValue, "System.Byte[]") || e.Row == null || sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal) == null)
        return;
      e.ReturnValue = (object) null;
    }
  }

  public virtual void SubstituteFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (!(e.NewValue is string) || e.Row == null || !(sender.GetValue(e.Row, ((PXEventSubscriberAttribute) this)._FieldOrdinal) is byte[] val) || !this.isMultiple(val))
      return;
    e.NewValue = (object) val;
    ((CancelEventArgs) e).Cancel = true;
  }

  public virtual void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers) where ISubscriber : class
  {
    base.GetSubscriber<ISubscriber>(subscribers);
    if (!(typeof (ISubscriber) == typeof (IPXFieldVerifyingSubscriber)))
      return;
    for (int index = 0; index < ((List<PXEventSubscriberAttribute>) this._Attributes).Count; ++index)
    {
      if (this._Attributes[index] is PXSelectorAttribute)
        subscribers.Remove(this._Attributes[index] as ISubscriber);
    }
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    PXGraph.FieldUpdatingEvents fieldUpdating = sender.Graph.FieldUpdating;
    Type itemType = sender.GetItemType();
    string fieldName = ((PXEventSubscriberAttribute) this)._FieldName;
    SingleGroupAttribute singleGroupAttribute = this;
    // ISSUE: virtual method pointer
    PXFieldUpdating pxFieldUpdating = new PXFieldUpdating((object) singleGroupAttribute, __vmethodptr(singleGroupAttribute, SubstituteFieldUpdating));
    fieldUpdating.AddHandler(itemType, fieldName, pxFieldUpdating);
  }

  private bool isMultiple(byte[] val)
  {
    int num = 0;
    for (int index = 0; index < val.Length; ++index)
    {
      if (val[index] != (byte) 0)
      {
        ++num;
        if (val[index] != (byte) 1 && val[index] != (byte) 2 && val[index] != (byte) 4 && val[index] != (byte) 8 && val[index] != (byte) 16 /*0x10*/ && val[index] != (byte) 32 /*0x20*/ && val[index] != (byte) 64 /*0x40*/ && val[index] != (byte) 128 /*0x80*/)
          ++num;
        if (num > 1)
          return true;
      }
    }
    return false;
  }

  public static void PopulateNeighbours<Field>(
    PXSelectBase currentSelect,
    PXSelectBase<Neighbour> neighboursSelect,
    Type left,
    Type right)
    where Field : IBqlField
  {
    RelationGroup relationGroup = (RelationGroup) PXSelectorAttribute.Select<Field>(currentSelect.Cache, currentSelect.Cache.Current);
    if (relationGroup == null)
      return;
    foreach (PXResult<Neighbour> pxResult in neighboursSelect.Select(Array.Empty<object>()))
    {
      Neighbour neighbour = PXResult<Neighbour>.op_Implicit(pxResult);
      if (neighbour.LeftEntityType == left.FullName || neighbour.RightEntityType == right.FullName)
      {
        if (neighbour.CoverageMask.Length < relationGroup.GroupMask.Length)
        {
          byte[] coverageMask = neighbour.CoverageMask;
          Array.Resize<byte>(ref coverageMask, relationGroup.GroupMask.Length);
          neighbour.CoverageMask = coverageMask;
        }
        if (neighbour.InverseMask.Length < relationGroup.GroupMask.Length)
        {
          byte[] inverseMask = neighbour.InverseMask;
          Array.Resize<byte>(ref inverseMask, relationGroup.GroupMask.Length);
          neighbour.InverseMask = inverseMask;
        }
        if (neighbour.WinCoverageMask.Length < relationGroup.GroupMask.Length)
        {
          byte[] winCoverageMask = neighbour.WinCoverageMask;
          Array.Resize<byte>(ref winCoverageMask, relationGroup.GroupMask.Length);
          neighbour.WinCoverageMask = winCoverageMask;
        }
        if (neighbour.WinInverseMask.Length < relationGroup.GroupMask.Length)
        {
          byte[] winInverseMask = neighbour.WinInverseMask;
          Array.Resize<byte>(ref winInverseMask, relationGroup.GroupMask.Length);
          neighbour.WinInverseMask = winInverseMask;
        }
        for (int index = 0; index < relationGroup.GroupMask.Length; ++index)
        {
          if (relationGroup.GroupType == "EE")
            neighbour.InverseMask[index] = (byte) ((uint) neighbour.InverseMask[index] | (uint) relationGroup.GroupMask[index]);
          else if (relationGroup.GroupType == "IE")
            neighbour.CoverageMask[index] = (byte) ((uint) neighbour.CoverageMask[index] | (uint) relationGroup.GroupMask[index]);
          else if (relationGroup.GroupType == "EO")
            neighbour.WinInverseMask[index] = (byte) ((uint) neighbour.WinInverseMask[index] | (uint) relationGroup.GroupMask[index]);
          else if (relationGroup.GroupType == "IO")
            neighbour.WinCoverageMask[index] = (byte) ((uint) neighbour.WinCoverageMask[index] | (uint) relationGroup.GroupMask[index]);
        }
        neighboursSelect.Update(neighbour);
      }
    }
    Neighbour neighbour1 = new Neighbour();
    neighbour1.LeftEntityType = left.FullName;
    neighbour1.RightEntityType = right.FullName;
    if (neighboursSelect.Locate(neighbour1) != null)
      return;
    if (relationGroup.GroupType == "EE")
    {
      neighbour1.InverseMask = relationGroup.GroupMask;
      neighbour1.CoverageMask = new byte[relationGroup.GroupMask.Length];
      neighbour1.WinInverseMask = new byte[relationGroup.GroupMask.Length];
      neighbour1.WinCoverageMask = new byte[relationGroup.GroupMask.Length];
    }
    else if (relationGroup.GroupType == "IE")
    {
      neighbour1.CoverageMask = relationGroup.GroupMask;
      neighbour1.InverseMask = new byte[relationGroup.GroupMask.Length];
      neighbour1.WinInverseMask = new byte[relationGroup.GroupMask.Length];
      neighbour1.WinCoverageMask = new byte[relationGroup.GroupMask.Length];
    }
    else if (relationGroup.GroupType == "EO")
    {
      neighbour1.WinInverseMask = relationGroup.GroupMask;
      neighbour1.WinCoverageMask = new byte[relationGroup.GroupMask.Length];
      neighbour1.InverseMask = new byte[relationGroup.GroupMask.Length];
      neighbour1.CoverageMask = new byte[relationGroup.GroupMask.Length];
    }
    else if (relationGroup.GroupType == "IO")
    {
      neighbour1.WinCoverageMask = relationGroup.GroupMask;
      neighbour1.WinInverseMask = new byte[relationGroup.GroupMask.Length];
      neighbour1.InverseMask = new byte[relationGroup.GroupMask.Length];
      neighbour1.CoverageMask = new byte[relationGroup.GroupMask.Length];
    }
    neighboursSelect.Insert(neighbour1);
  }
}
