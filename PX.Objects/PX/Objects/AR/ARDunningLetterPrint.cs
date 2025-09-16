// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDunningLetterPrint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR.MigrationMode;
using PX.Objects.CM;
using PX.Objects.CR;
using PX.Objects.CS;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

#nullable enable
namespace PX.Objects.AR;

public class ARDunningLetterPrint : PXGraph<
#nullable disable
ARDunningLetterPrint>
{
  public PXCancel<ARDunningLetterPrint.PrintParameters> Cancel;
  public PXFilter<ARDunningLetterPrint.PrintParameters> Filter;
  [PXViewName("Dunning Letter")]
  public PXSelect<ARDunningLetter> docs;
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessing<ARDunningLetterPrint.DetailsResult, ARDunningLetterPrint.PrintParameters> Details;
  public ARSetupNoMigrationMode ARSetup;
  [PXViewName("AR Contact")]
  public PXSelectJoin<PX.Objects.CR.Contact, InnerJoin<Customer, On<PX.Objects.CR.Contact.contactID, Equal<Customer.defBillContactID>>>, Where<Customer.bAccountID, Equal<Current<ARDunningLetter.bAccountID>>>> contact;
  public PXAction<ARDunningLetterPrint.PrintParameters> ViewDocument;
  [PXViewName("Main Contact")]
  public PXSelect<PX.Objects.CR.Contact> DefaultCompanyContact;

  public ARDunningLetterPrint()
  {
    PX.Objects.AR.ARSetup current = ((PXSelectBase<PX.Objects.AR.ARSetup>) this.ARSetup).Current;
    if (current.AutoReleaseDunningLetter.GetValueOrDefault())
    {
      PXDefaultAttribute.SetDefault<ARDunningLetterPrint.PrintParameters.action>(((PXSelectBase) this.Filter).Cache, (object) 0);
      PXUIFieldAttribute.SetEnabled<ARDunningLetterPrint.PrintParameters.showAll>(((PXSelectBase) this.Filter).Cache, (object) null, true);
    }
    ((PXSelectBase) this.Details).Cache.AllowDelete = false;
    ((PXSelectBase) this.Details).Cache.AllowInsert = false;
    ((PXProcessingBase<ARDunningLetterPrint.DetailsResult>) this.Details).SetSelected<ARInvoice.selected>();
    ((PXProcessing<ARDunningLetterPrint.DetailsResult>) this.Details).SetProcessCaption("Process");
    ((PXProcessing<ARDunningLetterPrint.DetailsResult>) this.Details).SetProcessAllCaption("Process All");
    PXUIFieldAttribute.SetVisible<ARDunningLetterPrint.PrintParameters.orgBAccountID>(((PXSelectBase) this.Filter).Cache, (object) null, current.PrepareDunningLetters != "A");
  }

  [PXUIField]
  [PXButton(VisibleOnProcessingResults = true)]
  public virtual IEnumerable viewDocument(PXAdapter adapter)
  {
    if (((PXSelectBase<ARDunningLetterPrint.DetailsResult>) this.Details).Current != null)
    {
      ARDunningLetter arDunningLetter = PXResultset<ARDunningLetter>.op_Implicit(PXSelectBase<ARDunningLetter, PXSelect<ARDunningLetter, Where<ARDunningLetter.dunningLetterID, Equal<Required<ARDunningLetterPrint.DetailsResult.dunningLetterID>>>>.Config>.Select((PXGraph) this, new object[1]
      {
        (object) ((PXSelectBase<ARDunningLetterPrint.DetailsResult>) this.Details).Current.DunningLetterID
      }));
      if (arDunningLetter != null)
      {
        ARDunningLetterMaint instance = PXGraph.CreateInstance<ARDunningLetterMaint>();
        ((PXSelectBase<ARDunningLetter>) instance.Document).Current = arDunningLetter;
        PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 1);
      }
    }
    return adapter.Get();
  }

  /// <summary>
  /// Generates a list of documents that meet the filter criteria.
  /// This list is used for display in the processing screen
  /// </summary>
  /// <returns>List of Dunning Letters</returns>
  protected virtual IEnumerable details()
  {
    ARDunningLetterPrint dunningLetterPrint = this;
    ARDunningLetterPrint.PrintParameters header = ((PXSelectBase<ARDunningLetterPrint.PrintParameters>) dunningLetterPrint.Filter).Current;
    if (header != null)
    {
      PXSelectBase<ARDunningLetter> pxSelectBase = (PXSelectBase<ARDunningLetter>) new PXSelectJoinGroupBy<ARDunningLetter, InnerJoin<ARDunningLetterDetail, On<ARDunningLetterDetail.dunningLetterID, Equal<ARDunningLetter.dunningLetterID>>>, Where<ARDunningLetter.dunningLetterDate, Between<Required<ARDunningLetter.dunningLetterDate>, Required<ARDunningLetter.dunningLetterDate>>>, Aggregate<GroupBy<ARDunningLetter.dunningLetterID, Sum<ARDunningLetterDetail.overdueBal>>>, OrderBy<Asc<ARDunningLetter.bAccountID>>>((PXGraph) dunningLetterPrint);
      if (((PXSelectBase<ARDunningLetterPrint.PrintParameters>) dunningLetterPrint.Filter).Current.Action.GetValueOrDefault() == 4)
        pxSelectBase.WhereAnd<Where<ARDunningLetter.released, Equal<False>, And<ARDunningLetter.voided, Equal<False>>>>();
      else
        pxSelectBase.WhereAnd<Where<ARDunningLetter.released, Equal<True>, And<ARDunningLetter.voided, Equal<False>>>>();
      List<object> objectList = new List<object>()
      {
        (object) header.BeginDate,
        (object) header.EndDate
      };
      if (((PXSelectBase<PX.Objects.AR.ARSetup>) dunningLetterPrint.ARSetup).Current.PrepareDunningLetters != "A" && header.OrgBAccountID.HasValue)
      {
        pxSelectBase.WhereAnd<Where<ARDunningLetter.branchID, Inside<Required<ARDunningLetterPrint.PrintParameters.orgBAccountID>>>>();
        objectList.Add((object) header.OrgBAccountID);
      }
      foreach (PXResult<ARDunningLetter, ARDunningLetterDetail> pxResult in pxSelectBase.Select(objectList.ToArray()))
      {
        ARDunningLetter arDunningLetter = PXResult<ARDunningLetter, ARDunningLetterDetail>.op_Implicit(pxResult);
        ARDunningLetterDetail dunningLetterDetail = PXResult<ARDunningLetter, ARDunningLetterDetail>.op_Implicit(pxResult);
        int? action = ((PXSelectBase<ARDunningLetterPrint.PrintParameters>) dunningLetterPrint.Filter).Current.Action;
        int num1 = 0;
        bool? nullable1;
        if (action.GetValueOrDefault() == num1 & action.HasValue)
        {
          nullable1 = header.ShowAll;
          bool flag = false;
          if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
          {
            nullable1 = arDunningLetter.DontPrint;
            if (!nullable1.GetValueOrDefault())
            {
              nullable1 = arDunningLetter.Printed;
              if (nullable1.GetValueOrDefault())
                continue;
            }
            else
              continue;
          }
        }
        action = ((PXSelectBase<ARDunningLetterPrint.PrintParameters>) dunningLetterPrint.Filter).Current.Action;
        if (action.GetValueOrDefault() == 1)
        {
          nullable1 = header.ShowAll;
          bool flag = false;
          if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
          {
            nullable1 = arDunningLetter.DontEmail;
            if (!nullable1.GetValueOrDefault())
            {
              nullable1 = arDunningLetter.Emailed;
              if (nullable1.GetValueOrDefault())
                continue;
            }
            else
              continue;
          }
        }
        action = ((PXSelectBase<ARDunningLetterPrint.PrintParameters>) dunningLetterPrint.Filter).Current.Action;
        if (action.GetValueOrDefault() == 2)
        {
          nullable1 = header.ShowAll;
          bool flag = false;
          if (nullable1.GetValueOrDefault() == flag & nullable1.HasValue)
          {
            nullable1 = arDunningLetter.DontEmail;
            if (!nullable1.GetValueOrDefault())
            {
              nullable1 = arDunningLetter.Emailed;
              if (nullable1.GetValueOrDefault())
                continue;
            }
            else
              continue;
          }
        }
        ARDunningLetterPrint.DetailsResult detailsResult1 = new ARDunningLetterPrint.DetailsResult()
        {
          DunningLetterID = arDunningLetter.DunningLetterID
        };
        ARDunningLetterPrint.DetailsResult detailsResult2 = (ARDunningLetterPrint.DetailsResult) ((PXSelectBase) dunningLetterPrint.Details).Cache.Locate((object) detailsResult1);
        if (detailsResult2 == null)
          ((PXSelectBase) dunningLetterPrint.Details).Cache.SetStatus((object) detailsResult1, (PXEntryStatus) 5);
        else
          detailsResult1 = detailsResult2;
        detailsResult1.BranchID = arDunningLetter.BranchID;
        detailsResult1.CustomerId = arDunningLetter.BAccountID;
        detailsResult1.DunningLetterID = arDunningLetter.DunningLetterID;
        detailsResult1.DunningLetterDate = arDunningLetter.DunningLetterDate;
        detailsResult1.DunningLetterLevel = arDunningLetter.DunningLetterLevel;
        ARDunningLetterPrint.DetailsResult detailsResult3 = detailsResult1;
        Decimal? dunningFee = arDunningLetter.DunningFee;
        Decimal? nullable2;
        if (dunningFee.HasValue)
        {
          dunningFee = arDunningLetter.DunningFee;
          Decimal num2 = 0M;
          if (dunningFee.GetValueOrDefault() < num2 & dunningFee.HasValue)
          {
            nullable2 = new Decimal?(0M);
            goto label_27;
          }
        }
        nullable2 = arDunningLetter.DunningFee;
label_27:
        detailsResult3.DunningFee = nullable2;
        detailsResult1.CuryID = PXAccess.GetBranch(arDunningLetter.BranchID)?.BaseCuryID;
        detailsResult1.LastLevel = arDunningLetter.LastLevel;
        detailsResult1.DontEmail = arDunningLetter.DontEmail;
        detailsResult1.DontPrint = arDunningLetter.DontPrint;
        detailsResult1.Emailed = arDunningLetter.Emailed;
        detailsResult1.Printed = arDunningLetter.Printed;
        detailsResult1.DocBal = dunningLetterDetail.OverdueBal;
        detailsResult1.NoteID = arDunningLetter.NoteID;
        yield return (object) detailsResult1;
      }
      ((PXSelectBase) dunningLetterPrint.Details).Cache.IsDirty = false;
    }
  }

  protected virtual IEnumerable defaultCompanyContact()
  {
    return (IEnumerable) OrganizationMaint.GetDefaultContactForCurrentOrganization((PXGraph) this);
  }

  protected virtual void PrintParameters_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    ARDunningLetterPrint.PrintParameters row = (ARDunningLetterPrint.PrintParameters) e.Row;
    if (row == null)
      return;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    ARDunningLetterPrint.\u003C\u003Ec__DisplayClass15_0 cDisplayClass150 = new ARDunningLetterPrint.\u003C\u003Ec__DisplayClass15_0();
    // ISSUE: reference to a compiler-generated field
    cDisplayClass150.filter = (ARDunningLetterPrint.PrintParameters) ((PXSelectBase) this.Filter).Cache.CreateCopy((object) row);
    int? action = row.Action;
    if (action.HasValue)
    {
      switch (action.GetValueOrDefault())
      {
        case 0:
          // ISSUE: reference to a compiler-generated method
          ((PXProcessingBase<ARDunningLetterPrint.DetailsResult>) this.Details).SetAsyncProcessDelegate(new Func<List<ARDunningLetterPrint.DetailsResult>, CancellationToken, System.Threading.Tasks.Task>(cDisplayClass150.\u003CPrintParameters_RowSelected\u003Eb__0));
          break;
        case 1:
          // ISSUE: method pointer
          ((PXProcessingBase<ARDunningLetterPrint.DetailsResult>) this.Details).SetProcessDelegate(new PXProcessingBase<ARDunningLetterPrint.DetailsResult>.ProcessListDelegate((object) cDisplayClass150, __methodptr(\u003CPrintParameters_RowSelected\u003Eb__1)));
          break;
        case 2:
          // ISSUE: method pointer
          ((PXProcessingBase<ARDunningLetterPrint.DetailsResult>) this.Details).SetProcessDelegate(new PXProcessingBase<ARDunningLetterPrint.DetailsResult>.ProcessListDelegate((object) cDisplayClass150, __methodptr(\u003CPrintParameters_RowSelected\u003Eb__2)));
          break;
        case 3:
          // ISSUE: reference to a compiler-generated method
          ((PXProcessingBase<ARDunningLetterPrint.DetailsResult>) this.Details).SetAsyncProcessDelegate(new Func<List<ARDunningLetterPrint.DetailsResult>, CancellationToken, System.Threading.Tasks.Task>(cDisplayClass150.\u003CPrintParameters_RowSelected\u003Eb__3));
          break;
        case 4:
          // ISSUE: method pointer
          ((PXProcessingBase<ARDunningLetterPrint.DetailsResult>) this.Details).SetProcessDelegate(new PXProcessingBase<ARDunningLetterPrint.DetailsResult>.ProcessListDelegate((object) cDisplayClass150, __methodptr(\u003CPrintParameters_RowSelected\u003Eb__4)));
          row.ShowAll = new bool?(false);
          break;
      }
    }
    PXUIFieldAttribute.SetEnabled<ARDunningLetterPrint.PrintParameters.showAll>(sender, (object) row, row.Action.GetValueOrDefault() != 4);
    bool flag = this.IsPrintingAllowed(row);
    PXUIFieldAttribute.SetVisible<ARDunningLetterPrint.PrintParameters.printWithDeviceHub>(sender, (object) row, flag);
    PXUIFieldAttribute.SetVisible<ARDunningLetterPrint.PrintParameters.definePrinterManually>(sender, (object) row, flag);
    PXUIFieldAttribute.SetVisible<ARDunningLetterPrint.PrintParameters.printerID>(sender, (object) row, flag);
    PXUIFieldAttribute.SetVisible<ARDunningLetterPrint.PrintParameters.numberOfCopies>(sender, (object) row, flag);
    bool? nullable;
    if (PXContext.GetSlot<AUSchedule>() == null)
    {
      PXCache pxCache1 = sender;
      ARDunningLetterPrint.PrintParameters printParameters1 = row;
      nullable = row.PrintWithDeviceHub;
      int num1 = nullable.GetValueOrDefault() ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<ARDunningLetterPrint.PrintParameters.definePrinterManually>(pxCache1, (object) printParameters1, num1 != 0);
      PXCache pxCache2 = sender;
      ARDunningLetterPrint.PrintParameters printParameters2 = row;
      nullable = row.PrintWithDeviceHub;
      int num2 = nullable.GetValueOrDefault() ? 1 : 0;
      PXUIFieldAttribute.SetEnabled<ARDunningLetterPrint.PrintParameters.numberOfCopies>(pxCache2, (object) printParameters2, num2 != 0);
      PXCache pxCache3 = sender;
      ARDunningLetterPrint.PrintParameters printParameters3 = row;
      nullable = row.PrintWithDeviceHub;
      int num3;
      if (nullable.GetValueOrDefault())
      {
        nullable = row.DefinePrinterManually;
        num3 = nullable.GetValueOrDefault() ? 1 : 0;
      }
      else
        num3 = 0;
      PXUIFieldAttribute.SetEnabled<ARDunningLetterPrint.PrintParameters.printerID>(pxCache3, (object) printParameters3, num3 != 0);
    }
    nullable = row.PrintWithDeviceHub;
    if (nullable.GetValueOrDefault())
    {
      nullable = row.DefinePrinterManually;
      if (nullable.GetValueOrDefault())
        return;
    }
    row.PrinterID = new Guid?();
  }

  protected virtual bool IsPrintingAllowed(ARDunningLetterPrint.PrintParameters row)
  {
    if (!PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() || row == null)
      return false;
    int? action = row.Action;
    int num = 0;
    return action.GetValueOrDefault() == num & action.HasValue;
  }

  protected virtual void PrintParameters_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    if (sender.ObjectsEqual<ARDunningLetterPrint.PrintParameters.action>(e.Row, e.OldRow) && sender.ObjectsEqual<ARDunningLetterPrint.PrintParameters.definePrinterManually>(e.Row, e.OldRow) && sender.ObjectsEqual<ARDunningLetterPrint.PrintParameters.printWithDeviceHub>(e.Row, e.OldRow) || ((PXSelectBase<ARDunningLetterPrint.PrintParameters>) this.Filter).Current == null || !PXAccess.FeatureInstalled<FeaturesSet.deviceHub>() || !((PXSelectBase<ARDunningLetterPrint.PrintParameters>) this.Filter).Current.PrintWithDeviceHub.GetValueOrDefault() || !((PXSelectBase<ARDunningLetterPrint.PrintParameters>) this.Filter).Current.DefinePrinterManually.GetValueOrDefault() || PXContext.GetSlot<AUSchedule>() != null && ((PXSelectBase<ARDunningLetterPrint.PrintParameters>) this.Filter).Current.PrinterID.HasValue && !((ARDunningLetterPrint.PrintParameters) e.OldRow).PrinterID.HasValue)
      return;
    ((PXSelectBase<ARDunningLetterPrint.PrintParameters>) this.Filter).Current.PrinterID = new NotificationUtility((PXGraph) this).SearchPrinter("Customer", "AR661000", ((PXGraph) this).Accessinfo.BranchID);
  }

  protected virtual void DetailsResult_RowPersisting(PXCache sedner, PXRowPersistingEventArgs e)
  {
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void PrintParameters_PrinterName_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    ARDunningLetterPrint.PrintParameters row = (ARDunningLetterPrint.PrintParameters) e.Row;
    if (row == null || this.IsPrintingAllowed(row))
      return;
    e.NewValue = (object) null;
  }

  protected virtual void _(
    PX.Data.Events.FieldDefaulting<ARDunningLetterPrint.PrintParameters.orgBAccountID> e)
  {
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ARDunningLetterPrint.PrintParameters.orgBAccountID>, object, object>) e).NewValue = (object) null;
    ((PX.Data.Events.FieldDefaultingBase<PX.Data.Events.FieldDefaulting<ARDunningLetterPrint.PrintParameters.orgBAccountID>>) e).Cancel = true;
  }

  public static async System.Threading.Tasks.Task Print(
    ARDunningLetterPrint.PrintParameters filter,
    List<ARDunningLetterPrint.DetailsResult> list,
    bool markOnly,
    CancellationToken cancellationToken)
  {
    bool flag1 = false;
    ARDunningLetterUpdate instance = PXGraph.CreateInstance<ARDunningLetterUpdate>();
    PXReportRequiredException reportRedirectException = (PXReportRequiredException) null;
    Dictionary<PrintSettings, PXReportRequiredException> dictionary1 = new Dictionary<PrintSettings, PXReportRequiredException>();
    foreach (ARDunningLetterPrint.DetailsResult statement in list)
    {
      int? dunningLetterId = statement.DunningLetterID;
      ARDunningLetter arDunningLetter = PXResultset<ARDunningLetter>.op_Implicit(((PXSelectBase<ARDunningLetter>) instance.DL).Select(new object[1]
      {
        (object) dunningLetterId.Value
      }));
      PXProcessing<ARDunningLetterPrint.DetailsResult>.SetCurrentItem((object) statement);
      bool? nullable = arDunningLetter.Released;
      bool flag2 = false;
      if (!(nullable.GetValueOrDefault() == flag2 & nullable.HasValue))
      {
        nullable = arDunningLetter.Voided;
        if (!nullable.GetValueOrDefault())
        {
          if (markOnly)
          {
            nullable = filter.ShowAll;
            if (!nullable.GetValueOrDefault())
            {
              arDunningLetter.DontPrint = new bool?(true);
              ((PXSelectBase) instance.docs).Cache.Update((object) arDunningLetter);
              PXProcessing<ARDunningLetterPrint.DetailsResult>.SetProcessed();
              continue;
            }
            continue;
          }
          Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
          dictionary2["ARDunningLetter.DunningLetterID"] = dunningLetterId.ToString();
          nullable = arDunningLetter.Printed;
          if (!nullable.GetValueOrDefault())
          {
            arDunningLetter.Printed = new bool?(true);
            ((PXSelectBase) instance.docs).Cache.Update((object) arDunningLetter);
            PXProcessing<ARDunningLetterPrint.DetailsResult>.SetProcessed();
          }
          string customerReportId = ARDunningLetterPrint.GetCustomerReportID((PXGraph) instance, "AR661000", statement);
          reportRedirectException = PXReportRequiredException.CombineReport(reportRedirectException, customerReportId, dictionary2, (CurrentLocalization) null);
          dictionary1 = SMPrintJobMaint.AssignPrintJobToPrinter(dictionary1, dictionary2, (IPrintable) filter, new Func<string, string, int?, Guid?>(new NotificationUtility((PXGraph) instance).SearchPrinter), "Customer", "AR661000", customerReportId, arDunningLetter.BranchID, (CurrentLocalization) null);
          continue;
        }
      }
      PXProcessing<ARDunningLetterPrint.DetailsResult>.SetError("Document Status is invalid for processing.");
      flag1 = true;
    }
    ((PXAction) instance.Save).Press();
    if (reportRedirectException != null)
    {
      int num = await SMPrintJobMaint.CreatePrintJobGroups(dictionary1, cancellationToken) ? 1 : 0;
      throw reportRedirectException;
    }
    if (flag1)
      throw new PXException("One or more items are not processed");
    reportRedirectException = (PXReportRequiredException) null;
  }

  public static string GetCustomerReportID(
    PXGraph graph,
    string reportID,
    ARDunningLetterPrint.DetailsResult statement)
  {
    return ARDunningLetterPrint.GetCustomerReportID(graph, reportID, statement.CustomerId, statement.BranchID);
  }

  public static string GetCustomerReportID(
    PXGraph graph,
    string reportID,
    int? customerID,
    int? branchID)
  {
    return new NotificationUtility(graph).SearchCustomerReport(reportID, customerID, branchID);
  }

  public static void Email(
    ARDunningLetterPrint.PrintParameters filter,
    List<ARDunningLetterPrint.DetailsResult> list,
    bool markOnly)
  {
    ARDunningLetterUpdate instance = PXGraph.CreateInstance<ARDunningLetterUpdate>();
    int num = 0;
    bool flag = false;
    foreach (ARDunningLetterPrint.DetailsResult detailsResult in list)
    {
      try
      {
        instance.EMailDL(detailsResult.DunningLetterID.Value, markOnly, filter.ShowAll.GetValueOrDefault());
        PXProcessing<ARDunningLetterPrint.DetailsResult>.SetCurrentItem((object) detailsResult);
        PXProcessing<ARDunningLetterPrint.DetailsResult>.SetProcessed();
      }
      catch (Exception ex)
      {
        PXProcessing<ARDunningLetterPrint.DetailsResult>.SetError(num, ex);
        flag = true;
      }
      ++num;
    }
    if (flag)
      throw new PXException("The mail send has failed.");
  }

  public static void Release(
    ARDunningLetterPrint.PrintParameters filter,
    List<ARDunningLetterPrint.DetailsResult> list)
  {
    if (list.Count <= 0)
      return;
    bool flag = false;
    ARDunningLetterMaint instance = PXGraph.CreateInstance<ARDunningLetterMaint>();
    int num = 0;
    foreach (ARDunningLetterPrint.DetailsResult detailsResult in list)
    {
      try
      {
        ARDunningLetter doc = PXResultset<ARDunningLetter>.op_Implicit(PXSelectBase<ARDunningLetter, PXSelect<ARDunningLetter, Where<ARDunningLetter.dunningLetterID, Equal<Required<ARDunningLetterPrint.DetailsResult.dunningLetterID>>>>.Config>.Select((PXGraph) instance, new object[1]
        {
          (object) detailsResult.DunningLetterID
        }));
        ARDunningLetterMaint.ReleaseProcess(instance, doc);
        PXProcessing<ARDunningLetterPrint.DetailsResult>.SetProcessed();
      }
      catch (Exception ex)
      {
        flag = true;
        PXProcessing<ARDunningLetterPrint.DetailsResult>.SetError(num, ex);
      }
      ++num;
    }
    if (flag)
      throw new PXException("One or more items are not released");
  }

  protected class ActionTypes
  {
    public const int Print = 0;
    public const int Email = 1;
    public const int MarkDontEmail = 2;
    public const int MarkDontPrint = 3;
    public const int Release = 4;

    public class ListAttribute : PXIntListAttribute
    {
      public ListAttribute()
        : base(new int[5]{ 0, 1, 2, 3, 4 }, new string[5]
        {
          "Print Dunning Letter",
          "Email Dunning Letter",
          "Mark as Do not Email",
          "Mark as Do not Print",
          "Release Dunning Letter"
        })
      {
      }
    }
  }

  [Serializable]
  public class PrintParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage, IPrintable
  {
    protected bool? _PrintWithDeviceHub;
    protected bool? _DefinePrinterManually = new bool?(false);
    protected Guid? _PrinterID;
    protected int? _NumberOfCopies;

    [PXDBInt]
    [PXDefault(4)]
    [PXUIField(DisplayName = "Action")]
    [ARDunningLetterPrint.ActionTypes.List]
    public virtual int? Action { get; set; }

    [Organization(true, Required = false)]
    public int? OrganizationID { get; set; }

    [BranchOfOrganization(typeof (ARDunningLetterPrint.PrintParameters.organizationID), true, null, null, Required = false)]
    public int? BranchID { get; set; }

    [OrganizationTree(typeof (ARDunningLetterPrint.PrintParameters.organizationID), typeof (ARDunningLetterPrint.PrintParameters.branchID), null, true, Required = false)]
    public int? OrgBAccountID { get; set; }

    [PXDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? BeginDate { get; set; }

    [PXDate]
    [PXDefault(typeof (AccessInfo.businessDate))]
    [PXUIField]
    public virtual DateTime? EndDate { get; set; }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Show All", Enabled = false)]
    public virtual bool? ShowAll { get; set; }

    [PXDBBool]
    [PXDefault(typeof (FeatureInstalled<FeaturesSet.deviceHub>))]
    [PXUIField(DisplayName = "Print with DeviceHub")]
    public virtual bool? PrintWithDeviceHub
    {
      get => this._PrintWithDeviceHub;
      set => this._PrintWithDeviceHub = value;
    }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Define Printer Manually")]
    public virtual bool? DefinePrinterManually
    {
      get => this._DefinePrinterManually;
      set => this._DefinePrinterManually = value;
    }

    [PXPrinterSelector]
    public virtual Guid? PrinterID
    {
      get => this._PrinterID;
      set => this._PrinterID = value;
    }

    [PXDBInt(MinValue = 1)]
    [PXDefault(1)]
    [PXFormula(typeof (Selector<ARDunningLetterPrint.PrintParameters.printerID, SMPrinter.defaultNumberOfCopies>))]
    [PXUIField]
    public virtual int? NumberOfCopies
    {
      get => this._NumberOfCopies;
      set => this._NumberOfCopies = value;
    }

    public abstract class action : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterPrint.PrintParameters.action>
    {
    }

    public abstract class organizationID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterPrint.PrintParameters.organizationID>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterPrint.PrintParameters.branchID>
    {
    }

    public abstract class orgBAccountID : IBqlField, IBqlOperand
    {
    }

    public abstract class beginDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDunningLetterPrint.PrintParameters.beginDate>
    {
    }

    public abstract class endDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDunningLetterPrint.PrintParameters.endDate>
    {
    }

    public abstract class showAll : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDunningLetterPrint.PrintParameters.showAll>
    {
    }

    public abstract class printWithDeviceHub : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDunningLetterPrint.PrintParameters.printWithDeviceHub>
    {
    }

    public abstract class definePrinterManually : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDunningLetterPrint.PrintParameters.definePrinterManually>
    {
    }

    public abstract class printerID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      ARDunningLetterPrint.PrintParameters.printerID>
    {
    }

    public abstract class numberOfCopies : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterPrint.PrintParameters.numberOfCopies>
    {
    }
  }

  [Serializable]
  public class DetailsResult : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Selected")]
    public virtual bool? Selected { get; set; }

    [PXDBInt(IsKey = true)]
    [PXUIField(Enabled = false)]
    public virtual int? DunningLetterID { get; set; }

    [PXDefault]
    [Branch(null, null, true, true, true, DescriptionField = typeof (PX.Objects.GL.Branch.branchID))]
    [PXUIField(DisplayName = "Branch")]
    public virtual int? BranchID { get; set; }

    [PXUIField]
    [Customer(DescriptionField = typeof (Customer.acctName))]
    public virtual int? CustomerId { get; set; }

    [PXDBDate]
    [PXDefault(TypeCode.DateTime, "01/01/1900")]
    [PXUIField(DisplayName = "Dunning Letter Date")]
    public virtual DateTime? DunningLetterDate { get; set; }

    [PXDBInt]
    [PXDefault]
    [PXUIField(DisplayName = "Dunning Letter Level")]
    public virtual int? DunningLetterLevel { get; set; }

    [PXDBBaseCuryMaxPrecision]
    [PXUIField(DisplayName = "Overdue Balance")]
    public virtual Decimal? DocBal { get; set; }

    [PXDBBool]
    [PXUIField(DisplayName = "Final Reminder")]
    public virtual bool? LastLevel { get; set; }

    [PXDBBaseCuryMaxPrecision]
    [PXUIField]
    public virtual Decimal? DunningFee { get; set; }

    [PXDBString(5, IsUnicode = true)]
    [PXUIField(DisplayName = "Currency")]
    [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
    public virtual string CuryID { get; set; }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Don't Print")]
    public virtual bool? DontPrint { get; set; }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Printed")]
    public virtual bool? Printed { get; set; }

    [PXDBBool]
    [PXDefault(true)]
    [PXUIField(DisplayName = "Don't Email")]
    public virtual bool? DontEmail { get; set; }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Emailed")]
    public virtual bool? Emailed { get; set; }

    [PXNote]
    public virtual Guid? NoteID { get; set; }

    public abstract class selected : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDunningLetterPrint.DetailsResult.selected>
    {
    }

    public abstract class dunningLetterID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterPrint.DetailsResult.dunningLetterID>
    {
    }

    public abstract class branchID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterPrint.DetailsResult.branchID>
    {
    }

    public abstract class customerId : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterPrint.DetailsResult.customerId>
    {
    }

    public abstract class dunningLetterDate : 
      BqlType<
      #nullable enable
      IBqlDateTime, DateTime>.Field<
      #nullable disable
      ARDunningLetterPrint.DetailsResult.dunningLetterDate>
    {
    }

    public abstract class dunningLetterLevel : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      ARDunningLetterPrint.DetailsResult.dunningLetterLevel>
    {
    }

    public abstract class docBal : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDunningLetterPrint.DetailsResult.docBal>
    {
    }

    public abstract class lastLevel : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDunningLetterPrint.DetailsResult.lastLevel>
    {
    }

    public abstract class dunningFee : 
      BqlType<
      #nullable enable
      IBqlDecimal, Decimal>.Field<
      #nullable disable
      ARDunningLetterPrint.DetailsResult.dunningFee>
    {
    }

    public abstract class curyID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARDunningLetterPrint.DetailsResult.curyID>
    {
    }

    public abstract class dontPrint : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDunningLetterPrint.DetailsResult.dontPrint>
    {
    }

    public abstract class printed : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDunningLetterPrint.DetailsResult.printed>
    {
    }

    public abstract class dontEmail : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDunningLetterPrint.DetailsResult.dontEmail>
    {
    }

    public abstract class emailed : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      ARDunningLetterPrint.DetailsResult.emailed>
    {
    }

    public abstract class noteID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      ARDunningLetterPrint.DetailsResult.noteID>
    {
    }
  }
}
