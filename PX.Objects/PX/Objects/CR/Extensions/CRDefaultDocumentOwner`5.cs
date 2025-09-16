// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRDefaultDocumentOwner`5
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Objects.EP;
using System;
using System.Linq;

#nullable disable
namespace PX.Objects.CR.Extensions;

/// <exclude />
public class CRDefaultDocumentOwner<TGraph, TMaster, FClassID, FOwnerID, FWorkgroupID> : 
  PXGraphExtension<TGraph>
  where TGraph : PXGraph
  where TMaster : class, IAssign, IBqlTable, new()
  where FClassID : class, IBqlField
  where FOwnerID : class, IBqlField
  where FWorkgroupID : class, IBqlField
{
  protected virtual void _(PX.Data.Events.FieldUpdated<FClassID> e)
  {
    object obj;
    if (e.Row == null || e.NewValue == ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<FClassID>, object, object>) e).OldValue || this.Base.IsCopyPasteContext || this.Base.IsImport && !this.Base.IsMobile || ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FClassID>>) e).Cache.GetStatus(e.Row) != 2 || !((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FClassID>>) e).Cache.RaiseFieldDefaulting<FOwnerID>(e.Row, ref obj))
      return;
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FClassID>>) e).Cache.SetValue<FOwnerID>(e.Row, obj);
    ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<FClassID>>) e).Cache.SetValue<FWorkgroupID>(e.Row, (object) null);
  }

  [PXOverride]
  public void Persist(Action basePersist)
  {
    this.AssignOwner();
    basePersist();
  }

  protected virtual void AssignOwner()
  {
    PXCache cach = this.Base.Caches[typeof (TMaster)];
    object current = cach.Current;
    PXEntryStatus status = cach.GetStatus(current);
    if (current == null || status == 2 && cach.GetValue<FClassID>(current) == null || status != 2 && object.Equals(cach.GetValue<FClassID>(current), cach.GetValueOriginal<FClassID>(current)))
      return;
    PXView pxView = (PXView) null;
    foreach (PXSelectorAttribute selectorAttribute in cach.GetAttributesOfType<PXSelectorAttribute>(current, typeof (FClassID).Name))
      pxView = new PXView((PXGraph) this.Base, true, selectorAttribute.PrimarySelect);
    if (pxView == null)
      return;
    string str = cach.GetValue<FClassID>(current) as string;
    if (!(pxView.SelectSingle(new object[1]{ (object) str }) is CRBaseClass crBaseClass) || !(crBaseClass.DefaultOwner == "A"))
      return;
    TMaster copy = cach.CreateCopy(current) as TMaster;
    new EPAssignmentProcessor<TMaster>((PXGraph) this.Base).Assign(copy, crBaseClass.DefaultAssignmentMapID);
    cach.Update((object) copy);
  }

  protected virtual void _(PX.Data.Events.FieldDefaulting<FOwnerID> e)
  {
    if (e.Row == null)
      return;
    PXView pxView = (PXView) null;
    foreach (PXSelectorAttribute selectorAttribute in ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FOwnerID>>) e).Cache.GetAttributesOfType<PXSelectorAttribute>(e.Row, typeof (FClassID).Name))
      pxView = new PXView((PXGraph) this.Base, true, selectorAttribute.PrimarySelect);
    if (pxView == null)
      return;
    string str = ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FOwnerID>>) e).Cache.GetValue<FClassID>(e.Row) as string;
    if (!(pxView.SelectSingle(new object[1]{ (object) str }) is CRBaseClass crBaseClass))
      return;
    switch (crBaseClass.DefaultOwner)
    {
      case "N":
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FOwnerID>, object, object>) e).NewValue = ((PX.Data.Events.Event<PXFieldDefaultingEventArgs, PX.Data.Events.FieldDefaulting<FOwnerID>>) e).Cache.GetValue<FOwnerID>(e.Row);
        break;
      case "C":
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FOwnerID>, object, object>) e).NewValue = (object) (int?) ((PXResult) ((IQueryable<PXResult<PX.Objects.CR.Contact>>) PXSelectBase<PX.Objects.CR.Contact, PXViewOf<PX.Objects.CR.Contact>.BasedOn<SelectFromBase<PX.Objects.CR.Contact, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<BAccountR>.On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<BAccountR.defContactID, Equal<PX.Objects.CR.Contact.contactID>>>>>.And<BqlOperand<BAccountR.parentBAccountID, IBqlInt>.IsEqual<PX.Objects.CR.Contact.bAccountID>>>>>.Where<BqlOperand<PX.Objects.CR.Contact.contactID, IBqlInt>.IsEqual<BqlField<AccessInfo.contactID, IBqlInt>.FromCurrent>>>.ReadOnly.Config>.Select((PXGraph) this.Base, Array.Empty<object>())).FirstOrDefault<PXResult<PX.Objects.CR.Contact>>())?.GetItem<PX.Objects.CR.Contact>()?.ContactID;
        break;
      default:
        ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<FOwnerID>, object, object>) e).NewValue = (object) null;
        break;
    }
  }

  protected virtual void _(PX.Data.Events.FieldSelecting<FOwnerID> e)
  {
    this.FieldSelectingOwnerOrWorkgroup<FOwnerID>(e);
  }

  protected virtual void _(PX.Data.Events.FieldSelecting<FWorkgroupID> e)
  {
    this.FieldSelectingOwnerOrWorkgroup<FWorkgroupID>(e);
  }

  private void FieldSelectingOwnerOrWorkgroup<TField>(PX.Data.Events.FieldSelecting<TField> e) where TField : class, IBqlField
  {
    if (e.Row == null)
      return;
    PXView pxView = (PXView) null;
    foreach (PXSelectorAttribute selectorAttribute in ((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<TField>>) e).Cache.GetAttributesOfType<PXSelectorAttribute>(e.Row, typeof (FClassID).Name))
      pxView = new PXView((PXGraph) this.Base, true, selectorAttribute.PrimarySelect);
    if (pxView == null)
      return;
    string str = ((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<TField>>) e).Cache.GetValue<FClassID>(e.Row) as string;
    if (!(pxView.SelectSingle(new object[1]{ (object) str }) is CRBaseClass crBaseClass))
      return;
    bool flag = crBaseClass.DefaultOwner != "A" || object.Equals(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<TField>>) e).Cache.GetValue<FClassID>(e.Row), ((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<TField>>) e).Cache.GetValueOriginal<FClassID>(e.Row));
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<TField>>) e).ReturnState = (object) PXFieldState.CreateInstance(((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<TField>>) e).ReturnState, (System.Type) null, new bool?(), new bool?(), new int?(), new int?(), new int?(), (object) null, (string) null, (string) null, (string) null, (string) null, (PXErrorLevel) 0, new bool?(flag), new bool?(), new bool?(), (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
  }
}
