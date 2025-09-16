// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPApprovalProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.WorkflowAPI;
using PX.Objects.CM;
using PX.Objects.GL;
using PX.Objects.PM;
using PX.TM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;

#nullable enable
namespace PX.Objects.EP;

[TableAndChartDashboardType]
public class EPApprovalProcess : PXGraph<
#nullable disable
EPApprovalProcess>
{
  public PXSelect<PX.Objects.CR.BAccount> bAccount;
  public PXCancel<EPApprovalProcess.EPOwned> Cancel;
  public PXAction<EPApprovalProcess.EPOwned> EditDetail;
  [PXFilterable(new System.Type[] {})]
  public EPApprovalProcess.PXApprovalProcessing<EPApprovalProcess.EPOwned, Where<True, Equal<True>>, OrderBy<Desc<EPApproval.docDate, Asc<EPApproval.approvalID>>>> Records;
  public PXSetup<PX.Objects.EP.EPSetup> EPSetup;
  [PXHidden]
  public PXSelect<PMProject> Projects;
  public PXAction<EPApprovalProcess.EPOwned> Reject;
  public PXAction<EPApprovalProcess.EPOwned> RejectAll;
  public PXAction<EPApprovalProcess.EPOwned> ViewDocumentDetails;
  public PXFilter<PX.Objects.EP.ReassignApprovalFilter> ReassignApprovalFilter;
  public PXAction<EPApprovalProcess.EPOwned> ReassignApproval;

  [PXUIField]
  [PXEditDetailButton]
  public virtual IEnumerable editDetail(PXAdapter adapter)
  {
    EPApprovalProcess.OnEditDetail((PXGraph) this, ((PXSelectBase<EPApprovalProcess.EPOwned>) this.Records).Current, (PXRedirectHelper.WindowMode) 4);
    return adapter.Get();
  }

  public static void OnEditDetail(
    PXGraph graph,
    EPApprovalProcess.EPOwned record,
    PXRedirectHelper.WindowMode windowMode)
  {
    if (record == null || !record.RefNoteID.HasValue)
      return;
    bool flag = true;
    EntityHelper entityHelper = new EntityHelper(graph);
    Note note = entityHelper.SelectNote(record.RefNoteID);
    if (note != null && note.EntityType == typeof (EPExpenseClaim).FullName)
    {
      EPExpenseClaim epExpenseClaim = PXResultset<EPExpenseClaim>.op_Implicit(PXSelectBase<EPExpenseClaim, PXSelect<EPExpenseClaim, Where<EPExpenseClaim.noteID, Equal<Required<EPExpenseClaim.noteID>>>>.Config>.Select(graph, new object[1]
      {
        (object) note.NoteID
      }));
      if (epExpenseClaim != null)
      {
        if (PXResultset<EPExpenseClaim>.op_Implicit(((PXSelectBase<EPExpenseClaim>) PXGraph.CreateInstance<ExpenseClaimEntry>().ExpenseClaim).Search<EPExpenseClaim.refNbr>((object) epExpenseClaim.RefNbr, Array.Empty<object>())) == null)
          flag = false;
      }
      else
      {
        using (new PXReadBranchRestrictedScope())
        {
          if (PXResultset<EPExpenseClaim>.op_Implicit(PXSelectBase<EPExpenseClaim, PXSelect<EPExpenseClaim, Where<EPExpenseClaim.noteID, Equal<Required<EPExpenseClaim.noteID>>>>.Config>.Select(graph, new object[1]
          {
            (object) note.NoteID
          })) != null)
            throw new PXException("You are not allowed to view the details of this record.");
        }
      }
    }
    if (note != null && note.EntityType == typeof (EPTimeCard).FullName)
    {
      EPTimeCard epTimeCard = PXResultset<EPTimeCard>.op_Implicit(PXSelectBase<EPTimeCard, PXSelect<EPTimeCard, Where<EPTimeCard.noteID, Equal<Required<EPTimeCard.noteID>>>>.Config>.Select(graph, new object[1]
      {
        (object) note.NoteID
      }));
      if (epTimeCard != null && PXResultset<EPTimeCard>.op_Implicit(((PXSelectBase<EPTimeCard>) PXGraph.CreateInstance<TimeCardMaint>().Document).Search<EPTimeCard.timeCardCD>((object) epTimeCard.TimeCardCD, Array.Empty<object>())) == null)
        flag = false;
    }
    if (!flag)
      return;
    entityHelper.NavigateToRow(new Guid?(record.RefNoteID.Value), windowMode);
  }

  public EPApprovalProcess()
  {
    ((PXProcessing<EPApprovalProcess.EPOwned>) this.Records).SetProcessCaption("Approve");
    ((PXProcessing<EPApprovalProcess.EPOwned>) this.Records).SetProcessAllCaption("Approve All");
    ((PXProcessingBase<EPApprovalProcess.EPOwned>) this.Records).SetSelected<EPApprovalProcess.EPOwned.selected>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<EPApprovalProcess.EPOwned>) this.Records).SetProcessDelegate(EPApprovalProcess.\u003C\u003Ec.\u003C\u003E9__10_0 ?? (EPApprovalProcess.\u003C\u003Ec.\u003C\u003E9__10_0 = new PXProcessingBase<EPApprovalProcess.EPOwned>.ProcessListDelegate((object) EPApprovalProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003C\u002Ector\u003Eb__10_0))));
    this.Records.ConfirmationTitle = "Approve";
    this.Records.ConfirmationMessage = "Are you sure you want to approve all the listed records?";
    ((PXGraph) this).Actions.Move(nameof (RejectAll), this.Records.ScheduleActionKey, true);
    // ISSUE: method pointer
    ((PXAction) this.Reject).StateSelectingEvents += new PXFieldSelecting((object) this, __methodptr(\u003C\u002Ector\u003Eb__10_1));
    // ISSUE: method pointer
    ((PXAction) this.RejectAll).StateSelectingEvents += new PXFieldSelecting((object) this, __methodptr(\u003C\u002Ector\u003Eb__10_2));
    ((PXGraph) this).SidePanelActions.Add(new PXGraph.SidePanelAction(nameof (ViewDocumentDetails), "visibility", PXLocalizer.Localize("Quick View"), (string) null, (Placement) 0));
  }

  public virtual bool IsProcessing
  {
    get => false;
    set
    {
    }
  }

  [PXProcessButton(Category = "")]
  [PXUIField]
  public IEnumerable reject(PXAdapter adapter) => this.RunReject(adapter, false);

  [PXProcessButton(Category = "Approval")]
  [PXUIField]
  public IEnumerable rejectAll(PXAdapter adapter) => this.RunReject(adapter, true);

  private IEnumerable RunReject(PXAdapter adapter, bool all)
  {
    EPApprovalProcess epApprovalProcess = this;
    epApprovalProcess.Records.ConfirmationTitle = "Reject";
    epApprovalProcess.Records.ConfirmationMessage = "Are you sure you want to reject all the listed records?";
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<EPApprovalProcess.EPOwned>) epApprovalProcess.Records).SetProcessDelegate(EPApprovalProcess.\u003C\u003Ec.\u003C\u003E9__18_0 ?? (EPApprovalProcess.\u003C\u003Ec.\u003C\u003E9__18_0 = new PXProcessingBase<EPApprovalProcess.EPOwned>.ProcessListDelegate((object) EPApprovalProcess.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CRunReject\u003Eb__18_0))));
    foreach (object obj in ((PXGraph) epApprovalProcess).Actions[all ? epApprovalProcess.Records.ProcessAllActionKey : epApprovalProcess.Records.ProcessActionKey].Press(adapter))
      yield return obj;
  }

  public virtual IEnumerable records()
  {
    EPApprovalProcess epApprovalProcess = this;
    ((PXSelectBase) epApprovalProcess.Records).Cache.AllowInsert = false;
    ((PXSelectBase) epApprovalProcess.Records).Cache.AllowDelete = false;
    PXSelectBase<EPApprovalProcess.EPOwned> pxSelectBase = (PXSelectBase<EPApprovalProcess.EPOwned>) new PXSelect<EPApprovalProcess.EPOwned, Where<True, Equal<True>>, OrderBy<Desc<EPApproval.docDate, Asc<EPApproval.approvalID>>>>((PXGraph) epApprovalProcess);
    ((PXSelectBase) pxSelectBase).View.Clear();
    int startRow = PXView.StartRow;
    int num = 0;
    foreach (EPApprovalProcess.EPOwned epOwned in ((PXSelectBase) pxSelectBase).View.Select(PXView.Currents, (object[]) null, PXView.Searches, PXView.SortColumns, PXView.Descendings, PXView.PXFilterRowCollection.op_Implicit(PXView.Filters), ref startRow, PXView.MaximumRows, ref num))
      yield return (object) epOwned;
    PXView.StartRow = 0;
  }

  [PXUIField]
  [PXButton(DisplayOnMainToolbar = false)]
  public void viewDocumentDetails()
  {
    EPApprovalProcess.OnEditDetail((PXGraph) this, ((PXSelectBase<EPApprovalProcess.EPOwned>) this.Records).Current, (PXRedirectHelper.WindowMode) 5);
  }

  [PXProcessButton(Category = "Approval")]
  [PXUIField]
  public virtual IEnumerable reassignApproval(PXAdapter adapter)
  {
    if (((PXSelectBase<PX.Objects.EP.ReassignApprovalFilter>) this.ReassignApprovalFilter).AskExt() == 1)
    {
      if (!this.ReassignApprovalFilter.VerifyRequired())
        return adapter.Get();
      PX.Objects.EP.ReassignApprovalFilter filter = ((PXSelectBase<PX.Objects.EP.ReassignApprovalFilter>) this.ReassignApprovalFilter).Current;
      this.Records.ConfirmationTitle = "Reassign";
      this.Records.ConfirmationMessage = "Are you sure you want to reject all the listed records?";
      ((PXProcessingBase<EPApprovalProcess.EPOwned>) this.Records).SetProcessDelegate((Action<EPApprovalProcess.EPOwned, CancellationToken>) ((item, cancellationToken) =>
      {
        if (cancellationToken.IsCancellationRequested)
          return;
        PXProcessing<EPApproval>.SetCurrentItem((object) item);
        EPApprovalProcess instance = PXGraph.CreateInstance<EPApprovalProcess>();
        EPApprovalHelper.ReassignToContact((PXGraph) instance, (EPApproval) item, filter.NewApprover, filter.IgnoreApproversDelegations);
        item.Selected = new bool?(false);
        GraphHelper.MarkUpdated(((PXGraph) instance).Caches[typeof (EPApprovalProcess.EPOwned)], (object) item);
        ((PXGraph) instance).Persist();
        PXProcessing<EPApproval>.SetInfo("The record has been processed successfully.");
      }));
      GraphHelper.PressButton(((PXGraph) this).Actions[this.Records.ProcessActionKey]);
    }
    return adapter.Get();
  }

  protected virtual void EPOwned_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (!(e.Row is EPApprovalProcess.EPOwned row) || row.Selected.GetValueOrDefault())
      return;
    sender.SetStatus((object) row, (PXEntryStatus) 0);
  }

  public virtual bool IsDirty => false;

  protected static void Approve(List<EPApprovalProcess.EPOwned> items, bool approve)
  {
    EntityHelper entityHelper = new EntityHelper(new PXGraph());
    Dictionary<System.Type, PXGraph> dictionary = new Dictionary<System.Type, PXGraph>();
    bool flag = false;
    foreach (EPApprovalProcess.EPOwned epOwned in items)
    {
      try
      {
        PXProcessing<EPApproval>.SetCurrentItem((object) epOwned);
        if (!epOwned.RefNoteID.HasValue)
          throw new PXException("Record for approving not found, RefNoteId is undefined.");
        object entityRow = entityHelper.GetEntityRow(new Guid?(epOwned.RefNoteID.Value), true);
        System.Type cacheType = entityRow != null ? entityRow.GetType() : throw new PXException("Record for approving not found.");
        System.Type primaryGraphType = entityHelper.GetPrimaryGraphType(ref cacheType, ref entityRow, false);
        PXGraph instance;
        if (!dictionary.TryGetValue(primaryGraphType, out instance))
          dictionary.Add(primaryGraphType, instance = PXGraph.CreateInstance(primaryGraphType));
        if (PXResultset<EPApproval>.op_Implicit(PXSelectBase<EPApproval, PXSelectReadonly<EPApproval, Where<EPApproval.approvalID, Equal<Current<EPApproval.approvalID>>>>.Config>.SelectSingleBound(instance, new object[1]
        {
          (object) epOwned
        }, Array.Empty<object>())).Status == "P")
        {
          instance.Clear();
          instance.Caches[cacheType].Current = entityRow;
          instance.Caches[cacheType].SetStatus(entityRow, (PXEntryStatus) 0);
          string name = typeof (EPExpenseClaim.approved).Name;
          string key = approve ? nameof (Approve) : "Reject";
          if (((OrderedDictionary) instance.Actions).Contains((object) key))
          {
            instance.Actions[key].Press();
          }
          else
          {
            BqlCommand bqlSelect = instance.Views[instance.PrimaryView].BqlSelect;
            PXAdapter pxAdapter = new PXAdapter((PXView) new PXView.Dummy(instance, bqlSelect, new List<object>()
            {
              entityRow
            }));
            pxAdapter.Menu = key;
            if (((OrderedDictionary) instance.Actions).Contains((object) "Action"))
            {
              if (!EPApprovalProcess.CheckRights(primaryGraphType, cacheType))
                throw new PXException("You don't have access rights to approve document.");
              foreach (object obj in instance.Actions["Action"].Press(pxAdapter))
                ;
            }
            else
              throw new PXException(PXMessages.LocalizeFormatNoPrefixNLA("Automation for screen/graph {0} exists but is not configured properly. Failed to find action - 'Action'", new object[1]
              {
                (object) instance
              }));
          }
          instance.Persist();
        }
        PXProcessing<EPApproval>.SetInfo("The record has been processed successfully.");
      }
      catch (Exception ex)
      {
        flag = true;
        PXProcessing<EPApproval>.SetError(ex);
      }
    }
    if (flag)
      throw new PXOperationCompletedWithErrorException("At least one item has not been processed.");
  }

  private static bool CheckRights(System.Type graphType, System.Type cacheType)
  {
    List<string> stringList1 = (List<string>) null;
    List<string> stringList2 = (List<string>) null;
    PXSiteMapNode siteMapNode = PXSiteMapProviderExtensions.FindSiteMapNode(PXSiteMap.Provider, graphType);
    if (siteMapNode == null)
      return false;
    PXCacheRights pxCacheRights;
    PXAccess.Provider.GetRights(siteMapNode.ScreenID, graphType.Name, cacheType, ref pxCacheRights, ref stringList1, ref stringList2);
    string str = "Approve@Action";
    return stringList2 == null || !CompareIgnoreCase.IsInList(stringList2, str);
  }

  [PXProjection(typeof (Select2<EPApproval, LeftJoin<Note, On<Note.noteID, Equal<EPApproval.refNoteID>>>, Where<EPApproval.status, Equal<EPApprovalStatus.pending>, And<Where2<Where2<Where<EPApproval.ownerID, IsNotNull, And<EPApproval.ownerID, Equal<CurrentValue<AccessInfo.contactID>>>>, Or<Where<EPApproval.origOwnerID, IsNotNull, And<EPApproval.origOwnerID, Equal<CurrentValue<AccessInfo.contactID>>>>>>, Or<EPApproval.workgroupID, IsWorkgroupOfContact<CurrentValue<AccessInfo.contactID>>, Or<EPApproval.workgroupID, IsWorkgroupOrSubgroupOfContact<CurrentValue<AccessInfo.contactID>>>>>>>>), new System.Type[] {typeof (EPApproval)}, Persistent = true)]
  [Serializable]
  public class EPOwned : EPApproval
  {
    protected bool? _Selected = new bool?(false);
    private string _EntityType;
    private string _DocType;

    [PXRefNote(BqlTable = typeof (EPApproval), LastKeyOnly = true)]
    [PXUIField(DisplayName = "Reference Nbr.")]
    [PXNoUpdate]
    public override Guid? RefNoteID
    {
      get => this._RefNoteID;
      set => this._RefNoteID = value;
    }

    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Selected")]
    public virtual bool? Selected
    {
      get => this._Selected;
      set => this._Selected = value;
    }

    [PXInt]
    [PXUIField]
    [PXDBCalced(typeof (IIf<Where<EPApproval.status, Equal<EPApprovalStatus.pending>>, DateDiff<EPApproval.createdDateTime, Now, DateDiff.minute>, Zero>), typeof (int))]
    public override int? PendingWaitTime
    {
      get => this._PendingWaitTime;
      set => this._PendingWaitTime = value;
    }

    [PXBool]
    [PXUIField(DisplayName = "Escalated")]
    [PXDBCalced(typeof (IIf<Where<EPApproval.waitTime, Greater<Zero>, And<DateDiff<EPApproval.createdDateTime, Now, DateDiff.minute>, GreaterEqual<EPApproval.waitTime>>>, True, False>), typeof (bool))]
    public virtual bool? Escalated { get; set; }

    [PXDBInt(BqlTable = typeof (EPApproval))]
    [PXUIField(DisplayName = "Business Account")]
    [PXSelector(typeof (PX.Objects.CR.BAccount.bAccountID), SubstituteKey = typeof (PX.Objects.CR.BAccount.acctCD), DescriptionField = typeof (PX.Objects.CR.BAccount.acctName))]
    public override int? BAccountID
    {
      get => this._BAccountID;
      set => this._BAccountID = value;
    }

    [PXDBLong(BqlTable = typeof (EPApproval))]
    [CurrencyInfo]
    public override long? CuryInfoID
    {
      get => this._CuryInfoID;
      set => this._CuryInfoID = value;
    }

    [PXDBCurrency(typeof (EPApprovalProcess.EPOwned.curyInfoID), typeof (EPApprovalProcess.EPOwned.totalAmount), BqlTable = typeof (EPApproval))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    [PXUIField(DisplayName = "Total Amount")]
    public override Decimal? CuryTotalAmount
    {
      get => this._CuryTotalAmount;
      set => this._CuryTotalAmount = value;
    }

    [PXDBDecimal(4, BqlTable = typeof (EPApproval))]
    [PXDefault(TypeCode.Decimal, "0.0")]
    public override Decimal? TotalAmount
    {
      get => this._TotalAmount;
      set => this._TotalAmount = value;
    }

    [PXDBDate(PreserveTime = true, DisplayMask = "g", BqlTable = typeof (EPApproval))]
    [PXUIField(DisplayName = "Requested On")]
    public override DateTime? CreatedDateTime
    {
      get => this._CreatedDateTime;
      set => this._CreatedDateTime = value;
    }

    [PXDBString(BqlTable = typeof (Note))]
    public string EntityType
    {
      get => this._EntityType;
      set => this._EntityType = value;
    }

    [PXString]
    [PXUIField(DisplayName = "Type")]
    [PXFormula(typeof (ApprovalDocType<EPApprovalProcess.EPOwned.entityType, EPApproval.sourceItemType>))]
    public override string DocType
    {
      get => this._DocType;
      set => this._DocType = value;
    }

    [PXNote]
    public override Guid? NoteID
    {
      get => base.NoteID;
      set => base.NoteID = value;
    }

    public new abstract class refNoteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      EPApprovalProcess.EPOwned.refNoteID>
    {
    }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      EPApprovalProcess.EPOwned.selected>
    {
    }

    public new abstract class pendingWaitTime : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EPApprovalProcess.EPOwned.pendingWaitTime>
    {
    }

    public abstract class escalated : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      EPApprovalProcess.EPOwned.escalated>
    {
    }

    public new abstract class bAccountID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EPApprovalProcess.EPOwned.bAccountID>
    {
    }

    public new abstract class curyInfoID : 
      BqlType<
      #nullable enable
      IBqlLong, long>.Field<
      #nullable disable
      EPApprovalProcess.EPOwned.curyInfoID>
    {
    }

    public new abstract class curyTotalAmount : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      EPApprovalProcess.EPOwned.curyTotalAmount>
    {
    }

    public new abstract class totalAmount : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      EPApprovalProcess.EPOwned.totalAmount>
    {
    }

    public new abstract class createdDateTime : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      EPApprovalProcess.EPOwned.createdDateTime>
    {
    }

    public abstract class entityType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      EPApprovalProcess.EPOwned.entityType>
    {
    }

    public new abstract class docType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      EPApprovalProcess.EPOwned.docType>
    {
    }
  }

  public class PXApprovalProcessing<Table, Where, OrderBy>(PXGraph graph, Delegate handler) : 
    PXProcessing<Table, Where, OrderBy>(graph, handler)
    where Table : class, IBqlTable, new()
    where Where : IBqlWhere, new()
    where OrderBy : IBqlOrderBy, new()
  {
    public string ConfirmationTitle;
    public string ConfirmationMessage;

    public PXApprovalProcessing(PXGraph graph)
      : this(graph, (Delegate) null)
    {
    }

    [PXProcessButton]
    [PXUIField]
    protected virtual IEnumerable ProcessAll(PXAdapter adapter)
    {
      return this.ConfirmationMessage != null && adapter.ExternalCall && !PXLongOperation.Exists(((PXSelectBase) this)._Graph.UID) && ((PXSelectBase<Table>) this).Ask(this.ConfirmationTitle, this.ConfirmationMessage, (MessageButtons) 4) != 6 ? adapter.Get() : ((PXProcessing<Table>) this).ProcessAll(adapter);
    }

    protected virtual void _PrepareGraph<Table>() where Table : class, IBqlTable, new()
    {
      ((PXProcessing<Table>) this)._PrepareGraph<Table>();
      ((PXProcessing<Table>) this)._ScheduleButton.SetVisible(false);
    }

    public string ScheduleActionKey => "Schedule";

    public string ProcessActionKey => "Process";

    public string ProcessAllActionKey => "ProcessAll";
  }
}
