// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DRBalanceValidation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.DR;

public class DRBalanceValidation : PXGraph<
#nullable disable
DRBalanceValidation>
{
  public PXFilter<DRBalanceValidation.DRBalanceValidationFilter> Filter;
  public PXCancel<DRBalanceValidation.DRBalanceValidationFilter> Cancel;
  [PXFilterable(new Type[] {})]
  public PXFilteredProcessing<DRBalanceValidation.DRBalanceType, DRBalanceValidation.DRBalanceValidationFilter> Items;
  public PXSetup<DRSetup> Setup;

  [InjectDependency]
  public IFinPeriodRepository FinPeriodRepository { get; set; }

  public virtual IEnumerable items()
  {
    bool found = false;
    foreach (DRBalanceValidation.DRBalanceType drBalanceType in ((PXSelectBase) this.Items).Cache.Inserted)
    {
      found = true;
      yield return (object) drBalanceType;
    }
    if (!found)
    {
      yield return (object) ((PXSelectBase<DRBalanceValidation.DRBalanceType>) this.Items).Insert(new DRBalanceValidation.DRBalanceType()
      {
        AccountType = "I"
      });
      yield return (object) ((PXSelectBase<DRBalanceValidation.DRBalanceType>) this.Items).Insert(new DRBalanceValidation.DRBalanceType()
      {
        AccountType = "E"
      });
      ((PXSelectBase) this.Items).Cache.IsDirty = false;
    }
  }

  public DRBalanceValidation()
  {
    DRSetup current = ((PXSelectBase<DRSetup>) this.Setup).Current;
    ((PXProcessing<DRBalanceValidation.DRBalanceType>) this.Items).SetProcessCaption("Process");
    ((PXProcessing<DRBalanceValidation.DRBalanceType>) this.Items).SetProcessAllCaption("Process All");
    ((PXProcessing<DRBalanceValidation.DRBalanceType>) this.Items).SetProcessTooltip("Recalculate deferred balances based on recognition transactions");
    ((PXProcessing<DRBalanceValidation.DRBalanceType>) this.Items).SetProcessAllTooltip("Recalculate deferred balances based on recognition transactions");
  }

  protected virtual bool PendingFullValidation
  {
    get
    {
      return ((PXSelectBase<DRSetup>) this.Setup).Current.PendingExpenseValidate.GetValueOrDefault() || ((PXSelectBase<DRSetup>) this.Setup).Current.PendingRevenueValidate.GetValueOrDefault();
    }
  }

  protected virtual void _(
    Events.RowSelected<DRBalanceValidation.DRBalanceValidationFilter> e)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    DRBalanceValidation.\u003C\u003Ec__DisplayClass12_0 cDisplayClass120 = new DRBalanceValidation.\u003C\u003Ec__DisplayClass12_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass120.\u003C\u003E4__this = this;
    bool flag = PXUIFieldAttribute.GetErrors(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<DRBalanceValidation.DRBalanceValidationFilter>>) e).Cache, (object) null, new PXErrorLevel[2]
    {
      (PXErrorLevel) 4,
      (PXErrorLevel) 5
    }).Any<KeyValuePair<string, string>>();
    ((PXProcessing<DRBalanceValidation.DRBalanceType>) this.Items).SetProcessEnabled(!flag && !this.PendingFullValidation);
    ((PXProcessing<DRBalanceValidation.DRBalanceType>) this.Items).SetProcessAllEnabled(!flag);
    // ISSUE: reference to a compiler-generated field
    cDisplayClass120.filter = e.Row;
    // ISSUE: reference to a compiler-generated field
    if (cDisplayClass120.filter == null)
      return;
    // ISSUE: method pointer
    ((PXProcessingBase<DRBalanceValidation.DRBalanceType>) this.Items).SetProcessDelegate(new PXProcessingBase<DRBalanceValidation.DRBalanceType>.ProcessListDelegate((object) cDisplayClass120, __methodptr(\u003C_\u003Eb__0)));
    // ISSUE: reference to a compiler-generated method
    PXCacheEx.Adjust<PXUIFieldAttribute>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<DRBalanceValidation.DRBalanceValidationFilter>>) e).Cache, (object) e.Row).For<DRBalanceValidation.DRBalanceValidationFilter.finPeriodID>(new Action<PXUIFieldAttribute>(cDisplayClass120.\u003C_\u003Eb__1));
  }

  protected virtual void _(
    Events.RowSelected<DRBalanceValidation.DRBalanceType> e)
  {
    if (((PXSelectBase<DRBalanceValidation.DRBalanceValidationFilter>) this.Filter).Current == null || e.Row == null)
      return;
    object finPeriodId = (object) ((PXSelectBase<DRBalanceValidation.DRBalanceValidationFilter>) this.Filter).Current.FinPeriodID;
    try
    {
      ((PXSelectBase) this.Filter).Cache.RaiseFieldVerifying<DRBalanceValidation.DRBalanceValidationFilter.finPeriodID>((object) ((PXSelectBase<DRBalanceValidation.DRBalanceValidationFilter>) this.Filter).Current, ref finPeriodId);
    }
    catch (PXSetPropertyException ex)
    {
      PXCacheEx.Adjust<PXUIFieldAttribute>(((Events.Event<PXRowSelectedEventArgs, Events.RowSelected<DRBalanceValidation.DRBalanceType>>) e).Cache, (object) e.Row).For<DRBalanceValidation.DRBalanceType.selected>((Action<PXUIFieldAttribute>) (a => a.Enabled = false));
    }
  }

  protected virtual void _(
    Events.FieldDefaulting<DRBalanceValidation.DRBalanceValidationFilter.finPeriodID> e)
  {
    if (!this.PendingFullValidation)
      return;
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<DRBalanceValidation.DRBalanceValidationFilter.finPeriodID>, object, object>) e).NewValue = (object) this.FinPeriodRepository.FindFirstPeriod(new int?(0)).FinPeriodID;
    ((Events.FieldDefaultingBase<Events.FieldDefaulting<DRBalanceValidation.DRBalanceValidationFilter.finPeriodID>>) e).Cancel = true;
  }

  protected virtual void _(
    Events.RowInserted<DRBalanceValidation.DRBalanceValidationFilter> e)
  {
    if (!this.PendingFullValidation)
      return;
    ((Events.Event<PXRowInsertedEventArgs, Events.RowInserted<DRBalanceValidation.DRBalanceValidationFilter>>) e).Cache.SetDefaultExt<DRBalanceValidation.DRBalanceValidationFilter.finPeriodID>((object) e.Row);
  }

  [Serializable]
  public class DRBalanceType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected bool? _Selected = new bool?(false);
    protected string _AccountType;

    [PXBool]
    [PXDefault(false)]
    [PXUIField]
    public bool? Selected
    {
      get => this._Selected;
      set => this._Selected = value;
    }

    [PXString(1, IsKey = true)]
    [PXDefault("I")]
    [LabelList(typeof (DeferredAccountType))]
    [PXUIField(DisplayName = "Balance Type", Enabled = false)]
    public virtual string AccountType
    {
      get => this._AccountType;
      set => this._AccountType = value;
    }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      DRBalanceValidation.DRBalanceType.selected>
    {
    }

    public abstract class accountType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRBalanceValidation.DRBalanceType.accountType>
    {
    }
  }

  [Serializable]
  public class DRBalanceValidationFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [FinPeriodNonLockedSelector]
    [PXUIField(DisplayName = "Fin. Period")]
    public virtual string FinPeriodID { get; set; }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DRBalanceValidation.DRBalanceValidationFilter.finPeriodID>
    {
    }
  }
}
