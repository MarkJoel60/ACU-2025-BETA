// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.ToSiteAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.Common.Bql;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.IN;

[PXDBInt]
[PXUIField]
public class ToSiteAttribute : SiteAttribute
{
  public ToSiteAttribute()
    : this(typeof (INTransferType.twoStep), typeof (AccessInfo.branchID))
  {
  }

  public ToSiteAttribute(Type transferTypeField)
    : this(transferTypeField, typeof (AccessInfo.branchID))
  {
  }

  public ToSiteAttribute(Type transferTypeField, Type restrictionBranchId)
  {
    ToSiteAttribute.BranchScopeDimensionSelector dimensionSelector = this.PrepareSelectorAttr(transferTypeField, restrictionBranchId);
    ((PXAggregateAttribute) this)._Attributes[this._SelAttrIndex] = (PXEventSubscriberAttribute) dimensionSelector;
    dimensionSelector.CacheGlobal = true;
    dimensionSelector.DescriptionField = typeof (INSite.descr);
  }

  private ToSiteAttribute.BranchScopeDimensionSelector PrepareSelectorAttr(
    Type transferTypeField,
    Type restrictionBranchId)
  {
    Type type1;
    if (!typeof (IBqlField).IsAssignableFrom(transferTypeField))
      type1 = transferTypeField;
    else
      type1 = typeof (Current<>).MakeGenericType(transferTypeField);
    Type type2 = ((IBqlTemplate) BqlTemplate.OfCommand<Search<INSite.siteID, Where2<Where<INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>, And<Match<Current<AccessInfo.userName>>>>, Or<BqlPlaceholder.A, Equal<INTransferType.twoStep>, And<INSite.siteID, NotEqual<SiteAnyAttribute.transitSiteID>>>>>>.Replace<BqlPlaceholder.A>(type1)).ToType();
    BqlCommand command = BqlTemplate.OfCommand<Select<INRegister, Where<BqlPlaceholder.A, Equal<INTransferType.twoStep>>>>.Replace<BqlPlaceholder.A>(transferTypeField).ToCommand();
    return new ToSiteAttribute.BranchScopeDimensionSelector(restrictionBranchId, "INSITE", type2, typeof (INSite.siteCD), command, new Type[2]
    {
      typeof (INSite.siteCD),
      typeof (INSite.descr)
    });
  }

  protected class BranchScopeDimensionSelector : PXDimensionSelectorAttribute
  {
    private int InterbranchRestrictorAttributeId = -1;
    protected BqlCommand _BranchScopeCondition;

    protected PXRestrictorAttribute InterbranchRestrictor
    {
      get
      {
        return (PXRestrictorAttribute) ((PXAggregateAttribute) this)._Attributes[this.InterbranchRestrictorAttributeId];
      }
    }

    public BranchScopeDimensionSelector(
      Type restrictionBranchId,
      string dimension,
      Type type,
      Type substituteKey,
      BqlCommand branchScopeCondition,
      params Type[] fieldList)
      : base(dimension, type, substituteKey, fieldList)
    {
      this._BranchScopeCondition = branchScopeCondition;
      ((PXAggregateAttribute) this)._Attributes.Add((PXEventSubscriberAttribute) new InterBranchRestrictorAttribute(BqlTemplate.OfCondition<Where<SameOrganizationBranch<INSite.branchID, Current<BqlPlaceholder.A>>>>.Replace<BqlPlaceholder.A>(restrictionBranchId).ToType()));
      this.InterbranchRestrictorAttributeId = ((List<PXEventSubscriberAttribute>) ((PXAggregateAttribute) this)._Attributes).Count - 1;
    }

    public BranchScopeDimensionSelector(
      string dimension,
      Type type,
      Type substituteKey,
      BqlCommand branchScopeCondition,
      params Type[] fieldList)
      : this(typeof (AccessInfo.branchID), dimension, type, substituteKey, branchScopeCondition, fieldList)
    {
    }

    public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
    {
      if (this._BranchScopeCondition.Meet(sender, sender.Current, Array.Empty<object>()))
      {
        using (new PXReadBranchRestrictedScope())
          base.FieldVerifying(sender, e);
      }
      else
        base.FieldVerifying(sender, e);
    }

    public void SelectorFieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
    {
      if (!this._BranchScopeCondition.Meet(sender, sender.Current, Array.Empty<object>()))
        return;
      using (new PXReadBranchRestrictedScope())
      {
        this.SelectorAttribute.FieldVerifying(sender, e);
        this.InterbranchRestrictor.FieldVerifying(sender, e);
      }
    }

    protected bool RaiseFieldSelectingInternal(
      PXCache sender,
      object row,
      ref object returnValue,
      bool forceState)
    {
      PXFieldSelectingEventArgs selectingEventArgs = new PXFieldSelectingEventArgs((object) null, (object) null, true, true);
      this.FieldSelecting(sender, selectingEventArgs);
      returnValue = selectingEventArgs.ReturnState;
      return !((CancelEventArgs) selectingEventArgs).Cancel;
    }

    public virtual void CacheAttached(PXCache sender)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ToSiteAttribute.BranchScopeDimensionSelector.\u003C\u003Ec__DisplayClass9_0 cDisplayClass90 = new ToSiteAttribute.BranchScopeDimensionSelector.\u003C\u003Ec__DisplayClass9_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.sender = sender;
      // ISSUE: reference to a compiler-generated field
      base.CacheAttached(cDisplayClass90.sender);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      cDisplayClass90.sender.Graph.FieldVerifying.AddHandler(((PXEventSubscriberAttribute) this).BqlTable, ((PXEventSubscriberAttribute) this)._FieldName, new PXFieldVerifying((object) this, __methodptr(SelectorFieldVerifying)));
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      cDisplayClass90.sender.Graph.FieldUpdating.RemoveHandler(((PXEventSubscriberAttribute) this).BqlTable, ((PXEventSubscriberAttribute) this)._FieldName, new PXFieldUpdating((object) this, __methodptr(FieldUpdating)));
      // ISSUE: reference to a compiler-generated field
      PXGraph.FieldUpdatingEvents fieldUpdating = cDisplayClass90.sender.Graph.FieldUpdating;
      Type bqlTable = ((PXEventSubscriberAttribute) this).BqlTable;
      string fieldName = ((PXEventSubscriberAttribute) this)._FieldName;
      ToSiteAttribute.BranchScopeDimensionSelector dimensionSelector = this;
      // ISSUE: virtual method pointer
      PXFieldUpdating pxFieldUpdating = new PXFieldUpdating((object) dimensionSelector, __vmethodptr(dimensionSelector, FieldUpdating));
      fieldUpdating.AddHandler(bqlTable, fieldName, pxFieldUpdating);
      object returnValue = (object) null;
      // ISSUE: reference to a compiler-generated field
      this.RaiseFieldSelectingInternal(cDisplayClass90.sender, (object) null, ref returnValue, true);
      string viewName = ((PXFieldState) returnValue).ViewName;
      // ISSUE: reference to a compiler-generated field
      PXView view = cDisplayClass90.sender.Graph.Views[viewName];
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.outerview = new PXView(cDisplayClass90.sender.Graph, true, view.BqlSelect);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PXView pxView = cDisplayClass90.sender.Graph.Views[viewName] = new PXView(cDisplayClass90.sender.Graph, true, view.BqlSelect, (Delegate) new PXSelectDelegate((object) cDisplayClass90, __methodptr(\u003CCacheAttached\u003Eb__0)));
      if (!this._DirtyRead)
        return;
      pxView.IsReadOnly = false;
    }

    public virtual void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
    {
      if (this._BranchScopeCondition.Meet(sender, sender.Current, Array.Empty<object>()))
      {
        using (new PXReadBranchRestrictedScope())
          base.FieldUpdating(sender, e);
      }
      else
        base.FieldUpdating(sender, e);
    }
  }
}
