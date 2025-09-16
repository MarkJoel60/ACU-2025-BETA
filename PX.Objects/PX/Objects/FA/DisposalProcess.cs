// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.DisposalProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.Automation;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.Common;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.FA;

public class DisposalProcess : PXGraph<
#nullable disable
DisposalProcess>
{
  public PXCancel<DisposalProcess.DisposalFilter> Cancel;
  public PXFilter<DisposalProcess.DisposalFilter> Filter;
  [PXFilterable(new Type[] {})]
  [PXViewDetailsButton(typeof (FixedAsset.assetCD))]
  public PXFilteredProcessingJoin<FixedAsset, DisposalProcess.DisposalFilter, InnerJoin<FADetails, On<FixedAsset.assetID, Equal<FADetails.assetID>>, LeftJoin<PX.Objects.GL.Account, On<FixedAsset.fAAccountID, Equal<PX.Objects.GL.Account.accountID>>>>> Assets;
  public PXSelect<FABookBalance> bookbalances;
  public PXSetup<FASetup> fasetup;

  public DisposalProcess()
  {
    ((PXSelectBase<FASetup>) this.fasetup).Current = (FASetup) null;
    FASetup current = ((PXSelectBase<FASetup>) this.fasetup).Current;
    if (!((PXSelectBase<FASetup>) this.fasetup).Current.UpdateGL.GetValueOrDefault())
      throw new PXSetupNotEnteredException<FASetup>("This operation is not available in initialization mode. To exit the initialization mode, select the '{1}' checkbox on the '{0}' screen.", new object[1]
      {
        (object) PXUIFieldAttribute.GetDisplayName<FASetup.updateGL>(((PXSelectBase) this.fasetup).Cache)
      });
    PXUIFieldAttribute.SetDisplayName<PX.Objects.GL.Account.accountClassID>(((PXGraph) this).Caches[typeof (PX.Objects.GL.Account)], "Fixed Assets Account Class");
    if (((PXSelectBase<FASetup>) this.fasetup).Current.AutoReleaseDisp.GetValueOrDefault())
      return;
    ((PXProcessing<FixedAsset>) this.Assets).SetProcessCaption("Prepare");
    ((PXProcessing<FixedAsset>) this.Assets).SetProcessAllCaption("Prepare All");
  }

  public virtual BqlCommand GetSelectCommand(DisposalProcess.DisposalFilter filter)
  {
    BqlCommand selectCommand = ((PXSelectBase) new PXSelectJoin<FixedAsset, InnerJoin<FADetails, On<FixedAsset.assetID, Equal<FADetails.assetID>>, LeftJoin<PX.Objects.GL.Account, On<FixedAsset.fAAccountID, Equal<PX.Objects.GL.Account.accountID>>>>, Where<FADetails.status, NotEqual<FixedAssetStatus.disposed>, And<FADetails.status, NotEqual<FixedAssetStatus.hold>, And<FADetails.status, NotEqual<FixedAssetStatus.suspended>>>>>((PXGraph) this)).View.BqlSelect;
    if (filter.BookID.HasValue)
      selectCommand = BqlCommand.AppendJoin<InnerJoin<FABookBalance, On<FABookBalance.assetID, Equal<FixedAsset.assetID>>>>(selectCommand).WhereAnd<Where<FABookBalance.bookID, Equal<Current<ProcessAssetFilter.bookID>>>>();
    if (filter.ClassID.HasValue)
      selectCommand = selectCommand.WhereAnd<Where<FixedAsset.classID, Equal<Current<ProcessAssetFilter.classID>>>>();
    if (filter.OrgBAccountID.HasValue)
      selectCommand = selectCommand.WhereAnd<Where<FixedAsset.branchID, Inside<Current2<DisposalProcess.DisposalFilter.orgBAccountID>>>>();
    if (filter.ParentAssetID.HasValue)
      selectCommand = selectCommand.WhereAnd<Where<FixedAsset.parentAssetID, Equal<Current<ProcessAssetFilter.parentAssetID>>>>();
    return selectCommand;
  }

  protected virtual IEnumerable assets()
  {
    DisposalProcess.DisposalFilter current = ((PXSelectBase<DisposalProcess.DisposalFilter>) this.Filter).Current;
    int startRow = PXView.StartRow;
    int num = 0;
    List<PXFilterRow> pxFilterRowList = new List<PXFilterRow>();
    foreach (PXFilterRow filter in PXView.Filters)
    {
      if (filter.DataField.ToLower() == "status")
        filter.DataField = "FADetails__Status";
      pxFilterRowList.Add(filter);
    }
    List<object> objectList = this.GetSelectCommand(current).CreateView((PXGraph) this, mergeCache: true).Select(PXView.Currents, (object[]) null, PXView.Searches, PXView.SortColumns, PXView.Descendings, pxFilterRowList.ToArray(), ref startRow, PXView.MaximumRows, ref num);
    PXView.StartRow = 0;
    return (IEnumerable) objectList;
  }

  protected virtual void DisposalFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    DisposalProcess.DisposalFilter row = (DisposalProcess.DisposalFilter) e.Row;
    if (row == null)
      return;
    PXFilteredProcessingJoin<FixedAsset, DisposalProcess.DisposalFilter, InnerJoin<FADetails, On<FixedAsset.assetID, Equal<FADetails.assetID>>, LeftJoin<PX.Objects.GL.Account, On<FixedAsset.fAAccountID, Equal<PX.Objects.GL.Account.accountID>>>>> assets1 = this.Assets;
    int? disposalMethodId = row.DisposalMethodID;
    int num1 = disposalMethodId.HasValue ? 1 : 0;
    ((PXProcessing<FixedAsset>) assets1).SetProcessEnabled(num1 != 0);
    PXFilteredProcessingJoin<FixedAsset, DisposalProcess.DisposalFilter, InnerJoin<FADetails, On<FixedAsset.assetID, Equal<FADetails.assetID>>, LeftJoin<PX.Objects.GL.Account, On<FixedAsset.fAAccountID, Equal<PX.Objects.GL.Account.accountID>>>>> assets2 = this.Assets;
    disposalMethodId = row.DisposalMethodID;
    int num2 = disposalMethodId.HasValue ? 1 : 0;
    ((PXProcessing<FixedAsset>) assets2).SetProcessAllEnabled(num2 != 0);
    ((PXProcessingBase<FixedAsset>) this.Assets).SetProcessWorkflowAction(row.Action, new object[9]
    {
      (object) row.DisposalDate,
      (object) row.DisposalPeriodID,
      (object) row.DisposalAmt,
      (object) row.DisposalMethodID,
      (object) row.DisposalAccountID,
      (object) row.DisposalSubID,
      (object) row.DisposalAmtMode,
      (object) (row.ActionBeforeDisposal == "D"),
      (object) row.Reason
    });
    PXUIFieldAttribute.SetEnabled<DisposalProcess.DisposalFilter.disposalAmt>(sender, e.Row, row.DisposalAmtMode == "A");
    PXUIFieldAttribute.SetEnabled<FixedAsset.disposalAmt>(((PXSelectBase) this.Assets).Cache, (object) null, ((PXSelectBase<DisposalProcess.DisposalFilter>) this.Filter).Current.DisposalAmtMode == "M");
  }

  protected virtual void DisposalFilter_DisposalMethodID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<DisposalProcess.DisposalFilter.disposalAccountID>(e.Row);
    sender.SetDefaultExt<DisposalProcess.DisposalFilter.disposalSubID>(e.Row);
  }

  [PXDBInt(IsKey = true)]
  [PXSelector(typeof (Search<FixedAsset.assetID>), SubstituteKey = typeof (FixedAsset.assetCD), CacheGlobal = true, DescriptionField = typeof (FixedAsset.description))]
  [PXUIField(DisplayName = "Asset ID", Enabled = false)]
  public virtual void FABookBalance_AssetID_CacheAttached(PXCache sender)
  {
  }

  protected virtual void FixedAsset_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    FixedAsset row = (FixedAsset) e.Row;
    if (row == null)
      return;
    int? assetId = row.AssetID;
    int num = 0;
    if (assetId.GetValueOrDefault() < num & assetId.HasValue)
      return;
    FADetails det = PXResultset<FADetails>.op_Implicit(PXSelectBase<FADetails, PXSelect<FADetails, Where<FADetails.assetID, Equal<Required<FADetails.assetID>>>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.AssetID
    }));
    try
    {
      ((PXGraph) this).GetExtension<DisposalProcess.DisposalProcessFixedAssetChecksExtension>().CheckIfAssetCanBeDisposed(row, det, ((PXSelectBase<FASetup>) this.fasetup).Current, ((PXSelectBase<DisposalProcess.DisposalFilter>) this.Filter).Current.DisposalDate.Value, ((PXSelectBase<DisposalProcess.DisposalFilter>) this.Filter).Current.DisposalPeriodID, ((PXSelectBase<DisposalProcess.DisposalFilter>) this.Filter).Current.ActionBeforeDisposal == "D");
    }
    catch (PXException ex)
    {
      PXUIFieldAttribute.SetEnabled<FixedAsset.selected>(sender, (object) row, false);
      sender.RaiseExceptionHandling<FixedAsset.selected>((object) row, (object) null, (Exception) new PXSetPropertyException(ex.MessageNoNumber, (PXErrorLevel) 3));
    }
    if (!(((PXSelectBase<DisposalProcess.DisposalFilter>) this.Filter).Current.DisposalAmtMode == "M") || !row.Selected.GetValueOrDefault() || row.DisposalAmt.HasValue)
      return;
    sender.RaiseExceptionHandling<FixedAsset.disposalAmt>((object) row, (object) null, (Exception) new PXSetPropertyException("'{0}' cannot be empty.", new object[1]
    {
      (object) PXUIFieldAttribute.GetDisplayName<FixedAsset.disposalAmt>(sender)
    }));
  }

  public class DisposalProcessFixedAssetChecksExtension : 
    FixedAssetChecksExtensionBase<DisposalProcess>
  {
  }

  [Serializable]
  public class DisposalFilter : ProcessAssetFilter
  {
    protected DateTime? _DisposalDate;
    protected string _DisposalPeriodID;
    protected Decimal? _DisposalAmt;
    protected int? _DisposalMethodID;
    protected int? _DisposalAccountID;
    protected int? _DisposalSubID;
    protected string _DisposalAmtMode;
    protected string _Reason;

    [PXWorkflowMassProcessing(DisplayName = "Action")]
    public virtual string Action { get; set; }

    [Organization(true)]
    public override int? OrganizationID { get; set; }

    [BranchOfOrganization(typeof (DisposalProcess.DisposalFilter.organizationID), true, null, typeof (FeaturesSet.multipleCalendarsSupport))]
    public override int? BranchID { get; set; }

    [OrganizationTree(typeof (DisposalProcess.DisposalFilter.organizationID), typeof (DisposalProcess.DisposalFilter.branchID), null, false)]
    public int? OrgBAccountID { get; set; }

    [PXDBDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField(DisplayName = "Disposal Date")]
    public virtual DateTime? DisposalDate
    {
      get => this._DisposalDate;
      set => this._DisposalDate = value;
    }

    [PXUIField(DisplayName = "Disposal Period")]
    [FABookPeriodOpenInGLSelector(null, null, null, false, null, typeof (DisposalProcess.DisposalFilter.disposalDate), typeof (DisposalProcess.DisposalFilter.branchID), null, typeof (DisposalProcess.DisposalFilter.organizationID), null)]
    public virtual string DisposalPeriodID
    {
      get => this._DisposalPeriodID;
      set => this._DisposalPeriodID = value;
    }

    [PXDBBaseCury(null, null)]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Proceeds Amount")]
    public virtual Decimal? DisposalAmt
    {
      get => this._DisposalAmt;
      set => this._DisposalAmt = value;
    }

    [PXDBInt]
    [PXDefault]
    [PXSelector(typeof (Search<FADisposalMethod.disposalMethodID>), SubstituteKey = typeof (FADisposalMethod.disposalMethodCD), DescriptionField = typeof (FADisposalMethod.description))]
    [PXUIField(DisplayName = "Disposal Method", Required = true)]
    public virtual int? DisposalMethodID
    {
      get => this._DisposalMethodID;
      set => this._DisposalMethodID = value;
    }

    [PXDefault(typeof (Coalesce<Search<FADisposalMethod.proceedsAcctID, Where<FADisposalMethod.disposalMethodID, Equal<Current<DisposalProcess.DisposalFilter.disposalMethodID>>>>, Search<FASetup.proceedsAcctID>>))]
    [Account(DisplayName = "Proceeds Account", DescriptionField = typeof (PX.Objects.GL.Account.description))]
    public virtual int? DisposalAccountID
    {
      get => this._DisposalAccountID;
      set => this._DisposalAccountID = value;
    }

    [PXDefault(typeof (Coalesce<Search<FADisposalMethod.proceedsSubID, Where<FADisposalMethod.disposalMethodID, Equal<Current<DisposalProcess.DisposalFilter.disposalMethodID>>>>, Search<FASetup.proceedsSubID>>))]
    [SubAccount(typeof (DisposalProcess.DisposalFilter.disposalAccountID), typeof (DisposalProcess.DisposalFilter.branchID), false, DisplayName = "Proceeds Sub.", DescriptionField = typeof (PX.Objects.GL.Sub.description))]
    public virtual int? DisposalSubID
    {
      get => this._DisposalSubID;
      set => this._DisposalSubID = value;
    }

    [PXDBString(1, IsFixed = true)]
    [PXUIField(DisplayName = "Proceeds Allocation")]
    [DisposalProcess.DisposalFilter.disposalAmtMode.List]
    [PXDefault("A")]
    public virtual string DisposalAmtMode
    {
      get => this._DisposalAmtMode;
      set => this._DisposalAmtMode = value;
    }

    [PXDBString]
    [PXDefault("S")]
    [PXUIField(DisplayName = "Before Disposal")]
    [DisposalProcess.DisposalFilter.actionBeforeDisposal.List]
    public virtual string ActionBeforeDisposal { get; set; }

    [PXDBString(30, IsUnicode = true)]
    [PXUIField(DisplayName = "Reason")]
    public virtual string Reason
    {
      get => this._Reason;
      set => this._Reason = value;
    }

    public abstract class action : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DisposalProcess.DisposalFilter.action>
    {
    }

    public new abstract class organizationID : IBqlField, IBqlOperand
    {
    }

    public new abstract class branchID : IBqlField, IBqlOperand
    {
    }

    public abstract class orgBAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      DisposalProcess.DisposalFilter.orgBAccountID>
    {
    }

    public abstract class disposalDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      DisposalProcess.DisposalFilter.disposalDate>
    {
    }

    public abstract class disposalPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DisposalProcess.DisposalFilter.disposalPeriodID>
    {
    }

    public abstract class disposalAmt : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      DisposalProcess.DisposalFilter.disposalAmt>
    {
    }

    public abstract class disposalMethodID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      DisposalProcess.DisposalFilter.disposalMethodID>
    {
    }

    public abstract class disposalAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      DisposalProcess.DisposalFilter.disposalAccountID>
    {
    }

    public abstract class disposalSubID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      DisposalProcess.DisposalFilter.disposalSubID>
    {
    }

    public abstract class disposalAmtMode : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DisposalProcess.DisposalFilter.disposalAmtMode>
    {
      public const string Automatic = "A";
      public const string Manual = "M";

      public class ListAttribute : PXStringListAttribute
      {
        public ListAttribute()
          : base(new string[2]{ "A", "M" }, new string[2]
          {
            "Automatic",
            "Manual"
          })
        {
        }
      }

      public class automatic : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        DisposalProcess.DisposalFilter.disposalAmtMode.automatic>
      {
        public automatic()
          : base("A")
        {
        }
      }

      public class manual : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        DisposalProcess.DisposalFilter.disposalAmtMode.manual>
      {
        public manual()
          : base("M")
        {
        }
      }
    }

    public abstract class actionBeforeDisposal : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DisposalProcess.DisposalFilter.actionBeforeDisposal>
    {
      public const string Depreciate = "D";
      public const string Suspend = "S";

      public class ListAttribute : PXStringListAttribute
      {
        public ListAttribute()
          : base(new string[2]{ "D", "S" }, new string[2]
          {
            "Depreciate",
            "Suspend"
          })
        {
        }
      }

      public class depreciate : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        DisposalProcess.DisposalFilter.actionBeforeDisposal.depreciate>
      {
        public depreciate()
          : base("D")
        {
        }
      }

      public class suspend : 
        BqlType<
        #nullable enable
        IBqlString, string>.Constant<
        #nullable disable
        DisposalProcess.DisposalFilter.actionBeforeDisposal.suspend>
      {
        public suspend()
          : base("S")
        {
        }
      }
    }

    public abstract class reason : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      DisposalProcess.DisposalFilter.reason>
    {
    }
  }
}
