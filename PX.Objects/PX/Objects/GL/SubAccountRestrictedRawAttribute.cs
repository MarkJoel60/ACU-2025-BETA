// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.SubAccountRestrictedRawAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.SM;
using System;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.GL;

/// <summary>
/// Base Attribute for SubCD field. Aggregates PXFieldAttribute, PXUIFieldAttribute and PXDimensionAttribute.
/// PXDimensionAttribute selector returns only records that are visible for the current user.
/// </summary>
[PXDBString(30, InputMask = "", PadSpaced = true)]
[PXUIField]
public sealed class SubAccountRestrictedRawAttribute : 
  SubAccountRawAttribute,
  IPXFieldSelectingSubscriber
{
  public SubAccountRestrictedRawAttribute()
    : base((DimensionLookupMode) 1)
  {
  }

  public virtual void CacheAttached(PXCache sender)
  {
    ((PXAggregateAttribute) this).CacheAttached(sender);
    Users users = PXResultset<Users>.op_Implicit(PXSelectBase<Users, PXSelect<Users, Where<Users.username, Equal<Current<AccessInfo.userName>>>>.Config>.Select(sender.Graph, Array.Empty<object>()));
    if (users != null && users.GroupMask != null)
      ((PXDimensionAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex]).Restrictions = new GroupHelper.ParamsPair[1][]
      {
        GroupHelper.GetParams(typeof (Users), typeof (SegmentValue), users.GroupMask)
      };
    // ISSUE: method pointer
    sender.Graph.Views["_SUBACCOUNT_RestrictedSegments_"] = new PXView(sender.Graph, true, (BqlCommand) new Select<PXDimensionAttribute.SegmentValue>(), (Delegate) new PXSelectDelegate<short?, string>((object) (PXDimensionValueLookupModeAttribute) ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex], __methodptr(SegmentSelect)));
  }

  public void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (!(e.ReturnState is PXFieldState))
      return;
    ((PXFieldState) e.ReturnState).ViewName = "_SUBACCOUNT_RestrictedSegments_";
  }

  public override void GetSubscriber<ISubscriber>(List<ISubscriber> subscribers)
  {
    base.GetSubscriber<ISubscriber>(subscribers);
    if (!(typeof (ISubscriber) == typeof (IPXFieldSelectingSubscriber)))
      return;
    subscribers.Remove(this as ISubscriber);
    subscribers.Add(this as ISubscriber);
  }
}
