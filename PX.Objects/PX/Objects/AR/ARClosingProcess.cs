// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARClosingProcess
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.Common;
using PX.Objects.CR;
using PX.Objects.GL;
using PX.Objects.GL.FinPeriods.TableDefinition;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.AR;

public class ARClosingProcess : FinPeriodClosingProcessBase<
#nullable disable
ARClosingProcess, FinPeriod.aRClosed>
{
  public bool ExcludePendingProcessingDocs { get; set; }

  protected static BqlCommand OpenDocumentsQuery { get; } = PXSelectBase<ARRegister, PXSelectJoin<ARRegister, LeftJoin<ARAdjust, On<ARAdjust.adjgDocType, Equal<ARRegister.docType>, And<ARAdjust.adjgRefNbr, Equal<ARRegister.refNbr>>>, LeftJoin<ARClosingProcess.ARTran, On<ARRegister.docType, Equal<ARClosingProcess.ARTran.tranType>, And<ARRegister.refNbr, Equal<ARClosingProcess.ARTran.refNbr>>>, LeftJoin<PX.Objects.GL.Branch, On<ARRegister.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<TranBranch, On<ARClosingProcess.ARTran.branchID, Equal<TranBranch.branchID>>, LeftJoin<AdjustingBranch, On<ARAdjust.adjgBranchID, Equal<AdjustingBranch.branchID>>, LeftJoin<AdjustedBranch, On<ARAdjust.adjdBranchID, Equal<AdjustedBranch.branchID>>, LeftJoin<ARInvoice, On<ARRegister.docType, Equal<ARInvoice.docType>, And<ARRegister.refNbr, Equal<ARInvoice.refNbr>>>, LeftJoin<ARPayment, On<ARRegister.docType, Equal<ARPayment.docType>, And<ARRegister.refNbr, Equal<ARPayment.refNbr>>>, LeftJoin<GLTranDoc, On<ARRegister.docType, Equal<GLTranDoc.tranType>, And<ARRegister.refNbr, Equal<GLTranDoc.refNbr>, And<GLTranDoc.tranModule, Equal<BatchModule.moduleAR>>>>, LeftJoin<BAccountR, On<ARRegister.customerID, Equal<BAccountR.bAccountID>>, LeftJoin<PX.Objects.CA.CashAccount, On<ARPayment.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>>, LeftJoin<CashAccountBranch, On<PX.Objects.CA.CashAccount.branchID, Equal<CashAccountBranch.branchID>>>>>>>>>>>>>>, Where<ARRegister.voided, NotEqual<True>, And<ARRegister.scheduled, NotEqual<True>, And<ARRegister.rejected, NotEqual<True>, And2<Where<ARRegister.released, NotEqual<True>, Or<ARAdjust.released, Equal<False>>>, And<Where<ARAdjust.adjgFinPeriodID, IsNull, And<ARRegister.released, NotEqual<True>, And2<Where2<FinPeriodClosingProcessBase.WhereFinPeriodInRange<ARRegister.finPeriodID, PX.Objects.GL.Branch.organizationID>, Or2<FinPeriodClosingProcessBase.WhereFinPeriodInRange<ARClosingProcess.ARTran.finPeriodID, TranBranch.organizationID>, Or<FinPeriodClosingProcessBase.WhereFinPeriodInRange<ARRegister.finPeriodID, CashAccountBranch.organizationID>>>>, Or<ARAdjust.released, Equal<False>, And<Where2<FinPeriodClosingProcessBase.WhereFinPeriodInRange<ARAdjust.adjgFinPeriodID, AdjustingBranch.organizationID>, Or<FinPeriodClosingProcessBase.WhereFinPeriodInRange<ARAdjust.adjgFinPeriodID, AdjustedBranch.organizationID>>>>>>>>>>>>>, OrderBy<Asc<ARRegister.finPeriodID, Asc<GLTranDoc.tranPeriodID, Asc<ARRegister.docType, Asc<ARRegister.refNbr, Asc<GLTranDoc.lineNbr>>>>>>>.Config>.GetCommand();

  public override void ClosePeriod(FinPeriod finPeriod)
  {
    this.ExcludePendingProcessingDocs = true;
    base.ClosePeriod(finPeriod);
  }

  protected override void _(
    PX.Data.Events.RowSelected<FinPeriodClosingProcessBase.FinPeriodClosingProcessParameters> e)
  {
    base._(e);
    this.ExcludePendingProcessingDocs = e.Row.Action == "Close";
  }

  protected override FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule[] CheckingRules
  {
    get
    {
      BqlCommand bqlCommand = this.ExcludePendingProcessingDocs ? ARClosingProcess.OpenDocumentsQuery.WhereAnd<Where<ARRegister.pendingProcessing, NotEqual<True>>>() : ARClosingProcess.OpenDocumentsQuery;
      return new FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule[1]
      {
        new FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule()
        {
          ReportID = "AR656100",
          ErrorMessage = "Unreleased documents exist for the {0} financial period.",
          CheckCommand = bqlCommand
        }
      };
    }
  }

  public override List<(string ReportID, IPXResultset ReportData)> GetReportsData(
    int? organizationID,
    string fromPeriodID,
    string toPeriodID)
  {
    return ((IEnumerable<FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule>) this.CheckingRules).Select<FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule, (string, IPXResultset)>((Func<FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule, (string, IPXResultset)>) (checker => (checker.ReportID, this.GetResultset(checker.CheckCommand, organizationID, fromPeriodID, toPeriodID, new string[1]
    {
      "docType"
    })))).Where<(string, IPXResultset)>((Func<(string, IPXResultset), bool>) (tuple =>
    {
      IPXResultset reportData = tuple.ReportData;
      return (reportData != null ? reportData.GetRowCount() : 0) > 0;
    })).ToList<(string, IPXResultset)>();
  }

  [PXHidden]
  public class ARTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.GL.Branch">Branch</see>, to which the transaction belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID">Branch.BranchID</see> field.
    /// </value>
    [Branch(typeof (ARRegister.branchID), null, true, true, true)]
    public virtual int? BranchID { get; set; }

    /// <summary>[key] The type of the transaction.</summary>
    /// <value>
    /// The field is determined by the type of the parent <see cref="T:PX.Objects.AR.ARRegister">document</see>.
    /// For the list of possible values see <see cref="P:PX.Objects.AR.ARRegister.DocType" />.
    /// </value>
    [ARDocType.List]
    [PXDBString(3, IsKey = true, IsFixed = true)]
    [PXDBDefault(typeof (ARRegister.docType))]
    [PXUIField]
    public virtual string TranType { get; set; }

    /// <summary>
    /// [key] Reference number of the parent <see cref="T:PX.Objects.AR.ARRegister">document</see>.
    /// </summary>
    [PXDBString(15, IsUnicode = true, IsKey = true)]
    [PXDBDefault(typeof (ARRegister.refNbr))]
    [PXUIField]
    [PXParent(typeof (Select<ARRegister, Where<ARRegister.docType, Equal<Current<ARClosingProcess.ARTran.tranType>>, And<ARRegister.refNbr, Equal<Current<ARClosingProcess.ARTran.refNbr>>>>>))]
    public virtual string RefNbr { get; set; }

    /// <summary>
    /// <see cref="!:PX.Objects.GL.Obsolete.FinPeriod">Financial period</see>, which the line is associated with.
    /// </summary>
    /// <value>
    /// Defaults to the <see cref="P:PX.Objects.AR.ARRegister.FinPeriodID">document's financial period</see>.
    /// </value>
    [FinPeriodID(null, typeof (ARClosingProcess.ARTran.branchID), null, null, null, null, true, false, null, null, typeof (ARRegister.tranPeriodID), true, true)]
    public virtual string FinPeriodID { get; set; }

    public abstract class branchID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARClosingProcess.ARTran.branchID>
    {
    }

    public abstract class tranType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARClosingProcess.ARTran.tranType>
    {
    }

    public abstract class refNbr : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARClosingProcess.ARTran.refNbr>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      ARClosingProcess.ARTran.finPeriodID>
    {
    }
  }
}
