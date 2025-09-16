// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.TimeCardRelease
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using PX.TM;
using System;
using System.Collections;

#nullable enable
namespace PX.Objects.EP;

[TableDashboardType]
public class TimeCardRelease : PXGraph<
#nullable disable
TimeCardRelease>
{
  public PXCancel<TimeCardRelease.EPTimeCardRow> Cancel;
  public PXAction<TimeCardRelease.EPTimeCardRow> viewDetails;
  public PXSelect<EPEmployeeEx> Dummy;
  [PXViewName("Time Cards")]
  [PXFilterable(new System.Type[] {})]
  public PXProcessingJoin<TimeCardRelease.EPTimeCardRow, LeftJoin<EPEmployee, On<EPEmployee.bAccountID, Equal<TimeCardRelease.EPTimeCardRow.employeeID>>, LeftJoin<TimeCardRelease.EPApprovalLast, On<TimeCardRelease.EPApprovalLast.refNoteID, Equal<TimeCardRelease.EPTimeCardRow.noteID>>, LeftJoin<TimeCardRelease.EPApprovalEx, On<TimeCardRelease.EPApprovalLast.approvalID, Equal<TimeCardRelease.EPApprovalEx.approvalID>>, LeftJoin<EPEmployeeEx, On<EPEmployeeEx.defContactID, Equal<TimeCardRelease.EPApprovalEx.approvedByID>>>>>>, Where<TimeCardRelease.EPTimeCardRow.isApproved, Equal<True>, And2<Where<TimeCardRelease.EPTimeCardRow.isReleased, NotEqual<True>, Or<TimeCardRelease.EPTimeCardRow.isReleased, PX.Data.IsNull>>, And2<Where<TimeCardRelease.EPTimeCardRow.isHold, NotEqual<True>, Or<TimeCardRelease.EPTimeCardRow.isHold, PX.Data.IsNull>>, PX.Data.And<Where<TimeCardRelease.EPTimeCardRow.isRejected, NotEqual<True>, Or<TimeCardRelease.EPTimeCardRow.isRejected, PX.Data.IsNull>>>>>>, OrderBy<Asc<TimeCardRelease.EPTimeCardRow.timeCardCD>>> FilteredItems;
  public PXSetup<EPSetup> Setup;

  [PXMergeAttributes(Method = MergeMethod.Merge)]
  [PXUIField(DisplayName = "Approver Name", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
  protected virtual void EPEmployeeEx_AcctName_CacheAttached(PXCache sender)
  {
  }

  public TimeCardRelease()
  {
    this.FilteredItems.SetProcessCaption("Release");
    this.FilteredItems.SetProcessAllCaption("Release All");
    this.FilteredItems.SetSelected<EPTimeCard.selected>();
    this.FilteredItems.SetProcessDelegate<TimeCardMaint>(new PXProcessingBase<TimeCardRelease.EPTimeCardRow>.ProcessItemDelegate<TimeCardMaint>(TimeCardRelease.Release));
  }

  [PXUIField(DisplayName = "")]
  [PXEditDetailButton]
  public virtual IEnumerable ViewDetails(PXAdapter adapter)
  {
    TimeCardRelease.EPTimeCardRow current = this.FilteredItems.Current;
    if (current != null)
    {
      TimeCardMaint instance = PXGraph.CreateInstance<TimeCardMaint>();
      instance.Document.Current = (EPTimeCard) instance.Document.Search<EPTimeCard.timeCardCD>((object) current.TimeCardCD);
      PXRedirectRequiredException requiredException = new PXRedirectRequiredException((PXGraph) instance, true, "View Details");
      requiredException.Mode = PXBaseRedirectException.WindowMode.NewWindow;
      throw requiredException;
    }
    return adapter.Get();
  }

  public override IEnumerable ExecuteSelect(
    string viewName,
    object[] parameters,
    object[] searches,
    string[] sortcolumns,
    bool[] descendings,
    PXFilterRow[] filters,
    ref int startRow,
    int maximumRows,
    ref int totalRows)
  {
    if (viewName == "FilteredItems")
    {
      for (int index = 0; index < sortcolumns.Length; ++index)
      {
        if (string.Compare(sortcolumns[index], "WeekID_description", true) == 0)
          sortcolumns[index] = "WeekID";
      }
    }
    return base.ExecuteSelect(viewName, parameters, searches, sortcolumns, descendings, filters, ref startRow, maximumRows, ref totalRows);
  }

  public static void Release(TimeCardMaint timeCardMaint, TimeCardRelease.EPTimeCardRow timeCard)
  {
    timeCardMaint.Clear();
    timeCardMaint.Document.Current = (EPTimeCard) timeCardMaint.Document.Search<EPTimeCard.timeCardCD>((object) timeCard.TimeCardCD);
    if (timeCardMaint.Document.Current == null)
      throw new PXException("Timecard can not be released. Most probably current user has no right to view/release given timecard.");
    try
    {
      timeCardMaint.release.Press();
    }
    catch (Exception ex)
    {
      string message = $"Employee Time Card = {timeCard.TimeCardCD}; {(ex is PXOuterException ? $"{ex.Message}\r\n{string.Join("\r\n", ((PXOuterException) ex).InnerMessages)}" : ex.Message)}";
      if (!(ex is PXException))
        message = $"{message}; {ex.StackTrace}";
      throw new PXException(message);
    }
  }

  [PXHidden]
  [PXBreakInheritance]
  [Serializable]
  public class EPApprovalEx : EPApproval
  {
    [Owner(DisplayName = "Approved by", Visibility = PXUIVisibility.Visible, Enabled = false)]
    public override int? ApprovedByID
    {
      get => this._ApprovedByID;
      set => this._ApprovedByID = value;
    }

    [PXDBDate]
    [PXUIField(DisplayName = "Approve Date", Enabled = false)]
    public override System.DateTime? ApproveDate
    {
      get => this._ApproveDate;
      set => this._ApproveDate = value;
    }

    public new abstract class approvalID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardRelease.EPApprovalEx.approvalID>
    {
    }

    public new abstract class refNoteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      TimeCardRelease.EPApprovalEx.refNoteID>
    {
    }

    public new abstract class approvedByID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardRelease.EPApprovalEx.approvedByID>
    {
    }

    public new abstract class approveDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      TimeCardRelease.EPApprovalEx.approveDate>
    {
    }
  }

  [PXCacheName("Employee Time Card")]
  [Serializable]
  public class EPTimeCardRow : EPTimeCard
  {
    protected string _ApprovedByID;
    protected string _ApprovedByName;
    protected System.DateTime? _ApproveDate;

    [PXString(30, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Approved by", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string ApprovedByID
    {
      get => this._ApprovedByID;
      set => this._ApprovedByID = value;
    }

    [PXString(30, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Approver Name", Visibility = PXUIVisibility.SelectorVisible)]
    public virtual string ApprovedByName
    {
      get => this._ApprovedByName;
      set => this._ApprovedByName = value;
    }

    [PXDate]
    [PXUIField(DisplayName = "Approve Date", Enabled = false)]
    public virtual System.DateTime? ApproveDate
    {
      get => this._ApproveDate;
      set => this._ApproveDate = value;
    }

    public new abstract class timeCardCD : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardRelease.EPTimeCardRow.timeCardCD>
    {
    }

    public new abstract class employeeID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardRelease.EPTimeCardRow.employeeID>
    {
    }

    public new abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      TimeCardRelease.EPTimeCardRow.noteID>
    {
    }

    public new abstract class isApproved : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      TimeCardRelease.EPTimeCardRow.isApproved>
    {
    }

    public new abstract class isReleased : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      TimeCardRelease.EPTimeCardRow.isReleased>
    {
    }

    public new abstract class isHold : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      TimeCardRelease.EPTimeCardRow.isHold>
    {
    }

    public new abstract class isRejected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      TimeCardRelease.EPTimeCardRow.isRejected>
    {
    }

    public abstract class approvedByID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardRelease.EPTimeCardRow.approvedByID>
    {
    }

    public abstract class approvedByName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      TimeCardRelease.EPTimeCardRow.approvedByName>
    {
    }

    public abstract class approveDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, System.DateTime>.Field<
      #nullable disable
      TimeCardRelease.EPTimeCardRow.approveDate>
    {
    }
  }

  [PXHidden]
  [PXProjection(typeof (Select4<EPApproval, Where<EPApproval.approvalID, PX.Data.IsNotNull>, Aggregate<Max<EPApproval.approvalID, GroupBy<EPApproval.refNoteID>>>>), Persistent = false)]
  [Serializable]
  public class EPApprovalLast : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    protected int? _ApprovalID;
    protected Guid? _RefNoteID;

    [PXDBInt(IsKey = true, BqlField = typeof (EPApproval.approvalID))]
    [PXUIField(DisplayName = "ApprovalID", Visibility = PXUIVisibility.Service)]
    public virtual int? ApprovalID
    {
      get => this._ApprovalID;
      set => this._ApprovalID = value;
    }

    [PXDBGuid(false, BqlField = typeof (EPApproval.refNoteID))]
    [PXUIField(DisplayName = "References Nbr.")]
    public virtual Guid? RefNoteID
    {
      get => this._RefNoteID;
      set => this._RefNoteID = value;
    }

    public abstract class approvalID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      TimeCardRelease.EPApprovalLast.approvalID>
    {
    }

    public abstract class refNoteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      TimeCardRelease.EPApprovalLast.refNoteID>
    {
    }
  }
}
