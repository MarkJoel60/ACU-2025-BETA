// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.ProFormaProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.TM;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

public class ProFormaProcess : PXGraph<
#nullable disable
ProFormaProcess>
{
  public PXCancel<ProFormaProcess.ProFormaFilter> Cancel;
  public PXFilter<ProFormaProcess.ProFormaFilter> Filter;
  public PXAction<ProFormaProcess.ProFormaFilter> viewDocumentRef;
  public PXAction<ProFormaProcess.ProFormaFilter> viewDocumentProject;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessing<PMProforma, ProFormaProcess.ProFormaFilter> Items;

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable ViewDocumentRef(PXAdapter adapter)
  {
    PMProforma current = ((PXSelectBase<PMProforma>) this.Items).Current;
    if (current == null)
      return adapter.Get();
    ProformaEntry instance = PXGraph.CreateInstance<ProformaEntry>();
    ((PXSelectBase<PMProforma>) instance.Document).Current = PXResultset<PMProforma>.op_Implicit(((PXSelectBase<PMProforma>) instance.Document).Search<PMProforma.refNbr>((object) current.RefNbr, new object[1]
    {
      (object) current.ARInvoiceDocType
    })) ?? throw new PXSetPropertyException(PXMessages.LocalizeFormat("'{0}' cannot be found in the system. Please verify whether you have proper access rights to this object.", new object[1]
    {
      (object) current.RefNbr
    }));
    ProjectAccountingService.NavigateToScreen((PXGraph) instance);
    return adapter.Get();
  }

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable ViewDocumentProject(PXAdapter adapter)
  {
    if (((PXSelectBase<PMProforma>) this.Items).Current != null)
      ProjectAccountingService.NavigateToProjectScreen(((PXSelectBase<PMProforma>) this.Items).Current.ProjectID);
    return adapter.Get();
  }

  public virtual IEnumerable items()
  {
    ProFormaProcess proFormaProcess = this;
    ProFormaProcess.ProFormaFilter current = ((PXSelectBase<ProFormaProcess.ProFormaFilter>) proFormaProcess.Filter).Current;
    PXSelectBase<PMProforma> pxSelectBase = (PXSelectBase<PMProforma>) new PXSelectJoin<PMProforma, InnerJoinSingleTable<PX.Objects.AR.Customer, On<PMProforma.customerID, Equal<PX.Objects.AR.Customer.bAccountID>>>, Where<PMProforma.hold, Equal<False>, And<PMProforma.released, Equal<False>, And<PMProforma.status, Equal<ProformaStatus.open>, And<Match<PX.Objects.AR.Customer, Current<AccessInfo.userName>>>>>>>((PXGraph) proFormaProcess);
    pxSelectBase.WhereAnd<Where<PMProforma.invoiceDate, LessEqual<Current<ProFormaProcess.ProFormaFilter.endDate>>>>();
    if (current.BeginDate.HasValue)
      pxSelectBase.WhereAnd<Where<PMProforma.invoiceDate, GreaterEqual<Current<ProFormaProcess.ProFormaFilter.beginDate>>>>();
    if (current.OwnerID.HasValue)
      pxSelectBase.WhereAnd<Where<PMProforma.ownerID, Equal<Current<ProFormaProcess.ProFormaFilter.currentOwnerID>>>>();
    if (current.WorkGroupID.HasValue)
      pxSelectBase.WhereAnd<Where<PMProforma.workgroupID, Equal<Current<ProFormaProcess.ProFormaFilter.workGroupID>>>>();
    if (((PXSelectBase<ProFormaProcess.ProFormaFilter>) proFormaProcess.Filter).Current.ShowAll.GetValueOrDefault())
      pxSelectBase.WhereAnd<Where<PMProforma.hold, Equal<False>>>();
    int startRow = PXView.StartRow;
    int num = 0;
    foreach (PXResult<PMProforma> pxResult in ((PXSelectBase) pxSelectBase).View.Select(PXView.Currents, (object[]) null, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num))
    {
      PMProforma pmProforma1 = PXResult<PMProforma>.op_Implicit(pxResult);
      PMProforma pmProforma2 = (PMProforma) ((PXSelectBase) proFormaProcess.Items).Cache.Locate((object) pmProforma1);
      if (pmProforma2 != null)
        pmProforma1.Selected = pmProforma2.Selected;
      yield return (object) pmProforma1;
      PXView.StartRow = 0;
    }
  }

  public ProFormaProcess()
  {
    ((PXProcessing<PMProforma>) this.Items).SetProcessCaption("Release");
    ((PXProcessing<PMProforma>) this.Items).SetProcessAllCaption("Release All");
    PXUIFieldAttribute.SetVisible<PMProforma.curyID>(((PXSelectBase) this.Items).Cache, (object) null, PXAccess.FeatureInstalled<FeaturesSet.multicurrency>());
  }

  public virtual void ProFormaFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    object row = e.Row;
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<PMProforma>) this.Items).SetProcessDelegate<ProformaEntry>(ProFormaProcess.\u003C\u003Ec.\u003C\u003E9__9_0 ?? (ProFormaProcess.\u003C\u003Ec.\u003C\u003E9__9_0 = new PXProcessingBase<PMProforma>.ProcessItemDelegate<ProformaEntry>((object) ProFormaProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CProFormaFilter_RowSelected\u003Eb__9_0))));
  }

  [PXHidden]
  [ExcludeFromCodeCoverage]
  [Serializable]
  public class ProFormaFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _OwnerID;
    protected int? _WorkGroupID;
    protected bool? _MyOwner;
    protected bool? _MyWorkGroup;
    protected bool? _ShowAll;
    protected DateTime? _BeginDate;
    protected DateTime? _EndDate;

    [PXDBInt]
    [CRCurrentOwnerID]
    public virtual int? CurrentOwnerID { get; set; }

    [SubordinateOwner(DisplayName = "Assigned To")]
    public virtual int? OwnerID
    {
      get => !this._MyOwner.GetValueOrDefault() ? this._OwnerID : this.CurrentOwnerID;
      set => this._OwnerID = value;
    }

    [PXDBInt]
    [PXUIField(DisplayName = "Workgroup")]
    [PXSelector(typeof (Search<EPCompanyTree.workGroupID, Where<EPCompanyTree.workGroupID, IsWorkgroupOrSubgroupOfContact<Current<AccessInfo.contactID>>>>), SubstituteKey = typeof (EPCompanyTree.description))]
    public virtual int? WorkGroupID
    {
      get => !this._MyWorkGroup.GetValueOrDefault() ? this._WorkGroupID : new int?();
      set => this._WorkGroupID = value;
    }

    [PXDBBool]
    [PXUIField(DisplayName = "Me")]
    [PXDefault(false)]
    public virtual bool? MyOwner
    {
      get => this._MyOwner;
      set => this._MyOwner = value;
    }

    [PXDBBool]
    [PXUIField(DisplayName = "My")]
    [PXDefault(false)]
    public virtual bool? MyWorkGroup
    {
      get => this._MyWorkGroup;
      set => this._MyWorkGroup = value;
    }

    [PXDBBool]
    [PXUIField(DisplayName = "Show All")]
    [PXDefault(false)]
    public virtual bool? ShowAll
    {
      get => this._ShowAll;
      set => this._ShowAll = value;
    }

    [PXDate]
    [PXUIField]
    [PXUnboundDefault]
    public virtual DateTime? BeginDate
    {
      get => this._BeginDate;
      set => this._BeginDate = value;
    }

    [PXDBDate]
    [PXUIField]
    [PXDefault(typeof (AccessInfo.businessDate))]
    public virtual DateTime? EndDate
    {
      get => this._EndDate;
      set => this._EndDate = value;
    }

    public abstract class currentOwnerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProFormaProcess.ProFormaFilter.currentOwnerID>
    {
    }

    public abstract class ownerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProFormaProcess.ProFormaFilter.ownerID>
    {
    }

    public abstract class workGroupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ProFormaProcess.ProFormaFilter.workGroupID>
    {
    }

    public abstract class myOwner : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ProFormaProcess.ProFormaFilter.myOwner>
    {
    }

    public abstract class myWorkGroup : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ProFormaProcess.ProFormaFilter.myWorkGroup>
    {
    }

    public abstract class showAll : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ProFormaProcess.ProFormaFilter.showAll>
    {
    }

    public abstract class beginDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ProFormaProcess.ProFormaFilter.beginDate>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ProFormaProcess.ProFormaFilter.endDate>
    {
    }
  }
}
