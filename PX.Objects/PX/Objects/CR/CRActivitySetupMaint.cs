// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRActivitySetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Caching;
using PX.Common;
using PX.Data;
using PX.Objects.EP;
using System;

#nullable disable
namespace PX.Objects.CR;

public class CRActivitySetupMaint : PXGraph<CRActivitySetupMaint>
{
  [PXViewName("Activity Types")]
  public PXSelect<EPActivityType> ActivityTypes;
  public PXSelect<EPActivityType, Where<EPActivityType.isDefault, Equal<True>, And<EPActivityType.application, Equal<Required<EPActivityType.application>>>>> DefaultActivityTypes;
  public PXSave<EPActivityType> Save;
  public PXCancel<EPActivityType> Cancel;
  public PXSelect<EPSetup, Where<EPSetup.defaultActivityType, Equal<Current<EPActivityType.type>>>> epsetup;
  public PXSelect<CRActivity, Where<CRActivity.type, Equal<Current<EPActivityType.type>>>> Activities;

  [InjectDependency]
  protected ICacheControl<PageCache> PageCacheControl { get; set; }

  protected virtual void EPActivityType_IsDefault_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (!(e.Row is EPActivityType row))
      return;
    this.ValidateIsDefaultField(row, sender);
  }

  protected virtual void EPActivityType_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row is EPActivityType row)
      PXUIFieldAttribute.SetEnabled(sender, (object) row, !row.IsSystem.GetValueOrDefault());
    PXUIFieldAttribute.SetEnabled<EPActivityType.classID>(((PXSelectBase) this.ActivityTypes).Cache, (object) null, false);
  }

  protected virtual void EPActivityType_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    this.ValidateIsSystem(e.NewRow);
    if (sender.ObjectsEqual<EPActivityType.type>(e.Row, e.NewRow))
      return;
    this.ValidateUsage(e.Row, (PXSelectBase) this.epsetup, "This Activity Type can't be changed because it's used.");
    this.ValidateUsage(e.Row, (PXSelectBase) this.Activities, "This Activity Type can't be changed because it's used.");
  }

  protected virtual void EPActivityType_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    this.ValidateIsSystem(e.Row);
    this.ValidateUsage(e.Row, (PXSelectBase) this.epsetup, "This Activity Type can't be deleted because it's used.");
    this.ValidateUsage(e.Row, (PXSelectBase) this.Activities, "This Activity Type can't be deleted because it's used.");
  }

  protected virtual void _(PX.Data.Events.FieldSelecting<EPActivityType.type> e)
  {
    if (e.Row == null)
      return;
    PX.Data.Events.FieldSelecting<EPActivityType.type> fieldSelecting = e;
    object returnState = ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<EPActivityType.type>>) e).ReturnState;
    bool? nullable1 = new bool?(((PX.Data.Events.Event<PXFieldSelectingEventArgs, PX.Data.Events.FieldSelecting<EPActivityType.type>>) e).Cache.GetStatus(e.Row) != 2);
    bool? nullable2 = new bool?();
    bool? nullable3 = new bool?();
    int? nullable4 = new int?();
    int? nullable5 = new int?();
    int? nullable6 = new int?();
    bool? nullable7 = new bool?();
    bool? nullable8 = new bool?();
    bool? nullable9 = nullable1;
    PXFieldState instance = PXFieldState.CreateInstance(returnState, (System.Type) null, nullable2, nullable3, nullable4, nullable5, nullable6, (object) null, (string) null, (string) null, (string) null, (string) null, (PXErrorLevel) 0, nullable7, nullable8, nullable9, (PXUIVisibility) 0, (string) null, (string[]) null, (string[]) null);
    ((PX.Data.Events.FieldSelectingBase<PX.Data.Events.FieldSelecting<EPActivityType.type>>) fieldSelecting).ReturnState = (object) instance;
  }

  protected virtual void _(PX.Data.Events.FieldUpdated<EPActivityType.application> e)
  {
    if (e.Row == null || object.Equals(e.NewValue, ((PX.Data.Events.FieldUpdatedBase<PX.Data.Events.FieldUpdated<EPActivityType.application>, object, object>) e).OldValue))
      return;
    this.ValidateIsDefaultField(e.Row as EPActivityType, ((PX.Data.Events.Event<PXFieldUpdatedEventArgs, PX.Data.Events.FieldUpdated<EPActivityType.application>>) e).Cache);
  }

  private void ValidateIsDefaultField(EPActivityType row, PXCache cache)
  {
    if (!row.IsDefault.GetValueOrDefault())
      return;
    foreach (PXResult<EPActivityType> pxResult in ((PXSelectBase<EPActivityType>) this.DefaultActivityTypes).Select(new object[1]
    {
      (object) row.Application
    }))
    {
      EPActivityType epActivityType = PXResult<EPActivityType>.op_Implicit(pxResult);
      if (!(epActivityType.Type == row.Type))
      {
        epActivityType.IsDefault = new bool?(false);
        cache.Update((object) epActivityType);
      }
    }
    ((PXSelectBase) this.ActivityTypes).View.RequestRefresh();
  }

  private void ValidateUsage(object row, PXSelectBase select, string message)
  {
    if (select.View.SelectSingleBound(new object[1]{ row }, Array.Empty<object>()) != null)
      throw new PXException(message);
  }

  private void ValidateIsSystem(object row)
  {
    if (row != null && row is EPActivityType && (row as EPActivityType).IsSystem.GetValueOrDefault())
      throw new PXException("This is a predefined activity type, which cannot be deleted or changed.");
  }

  public virtual void Persist()
  {
    int num = ((PXSelectBase) this.ActivityTypes).Cache.Inserted.Count() != 0L ? 1 : 0;
    ((PXGraph) this).Persist();
    if (num == 0)
      return;
    ((ICacheControl) this.PageCacheControl).InvalidateCache();
  }
}
