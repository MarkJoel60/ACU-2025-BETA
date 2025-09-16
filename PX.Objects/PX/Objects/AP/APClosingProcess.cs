// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APClosingProcess
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
namespace PX.Objects.AP;

public class APClosingProcess : FinPeriodClosingProcessBase<
#nullable disable
APClosingProcess, FinPeriod.aPClosed>
{
  protected static BqlCommand OpenDocumentsQuery { get; } = PXSelectBase<APRegister, PXSelectJoin<APRegister, LeftJoin<APAdjust, On<APAdjust.adjgDocType, Equal<APRegister.docType>, And<APAdjust.adjgRefNbr, Equal<APRegister.refNbr>>>, LeftJoin<PX.Objects.GL.Branch, On<APRegister.branchID, Equal<PX.Objects.GL.Branch.branchID>>, LeftJoin<APClosingProcess.APTran, On<APRegister.docType, Equal<APClosingProcess.APTran.tranType>, And<APRegister.refNbr, Equal<APClosingProcess.APTran.refNbr>>>, LeftJoin<TranBranch, On<APClosingProcess.APTran.branchID, Equal<TranBranch.branchID>>, LeftJoin<AdjustingBranch, On<APAdjust.adjgBranchID, Equal<AdjustingBranch.branchID>>, LeftJoin<AdjustedBranch, On<APAdjust.adjdBranchID, Equal<AdjustedBranch.branchID>>, LeftJoin<APInvoice, On<APRegister.docType, Equal<APInvoice.docType>, And<APRegister.refNbr, Equal<APInvoice.refNbr>>>, LeftJoin<APPayment, On<APRegister.docType, Equal<APPayment.docType>, And<APRegister.refNbr, Equal<APPayment.refNbr>>>, LeftJoin<GLTranDoc, On<APRegister.docType, Equal<GLTranDoc.tranType>, And<APRegister.refNbr, Equal<GLTranDoc.refNbr>, And<GLTranDoc.tranModule, Equal<BatchModule.moduleAP>>>>, LeftJoin<BAccountR, On<APRegister.vendorID, Equal<BAccountR.bAccountID>>, LeftJoin<PX.Objects.CA.CashAccount, On<APPayment.cashAccountID, Equal<PX.Objects.CA.CashAccount.cashAccountID>>, LeftJoin<CashAccountBranch, On<PX.Objects.CA.CashAccount.branchID, Equal<CashAccountBranch.branchID>>>>>>>>>>>>>>, Where2<Where<APRegister.voided, NotEqual<True>, And<APRegister.scheduled, NotEqual<True>, And<APRegister.rejected, NotEqual<True>, And2<Where<APPayment.externalPaymentCanceled, PX.Data.IsNull, Or<APPayment.externalPaymentCanceled, Equal<False>>>, And2<Where<APPayment.externalPaymentVoided, PX.Data.IsNull, Or<APPayment.externalPaymentVoided, Equal<False>>>, PX.Data.And<Where<APAdjust.adjgFinPeriodID, PX.Data.IsNull, And2<Where<APRegister.released, NotEqual<True>, Or<APRegister.status, Equal<APDocStatus.underReclassification>>>, And2<Where2<FinPeriodClosingProcessBase.WhereFinPeriodInRange<APRegister.finPeriodID, PX.Objects.GL.Branch.organizationID>, Or2<FinPeriodClosingProcessBase.WhereFinPeriodInRange<APClosingProcess.APTran.finPeriodID, TranBranch.organizationID>, PX.Data.Or<FinPeriodClosingProcessBase.WhereFinPeriodInRange<APRegister.finPeriodID, CashAccountBranch.organizationID>>>>, Or<APAdjust.released, Equal<False>, PX.Data.And<Where2<FinPeriodClosingProcessBase.WhereFinPeriodInRange<APAdjust.adjgFinPeriodID, AdjustingBranch.organizationID>, Or2<FinPeriodClosingProcessBase.WhereFinPeriodInRange<APAdjust.adjgFinPeriodID, AdjustedBranch.organizationID>, PX.Data.Or<Where2<FinPeriodClosingProcessBase.WhereFinPeriodInRange<APRegister.finPeriodID, CashAccountBranch.organizationID>, And<APRegister.released, NotEqual<True>>>>>>>>>>>>>>>>>, PX.Data.Or<Where<APRegister.voided, NotEqual<True>, And<APRegister.prebooked, Equal<True>, And<APRegister.released, NotEqual<True>, And<APRegister.rejected, NotEqual<True>, PX.Data.And<Where2<FinPeriodClosingProcessBase.WhereFinPeriodInRange<APRegister.finPeriodID, PX.Objects.GL.Branch.organizationID>, Or2<FinPeriodClosingProcessBase.WhereFinPeriodInRange<APClosingProcess.APTran.finPeriodID, TranBranch.organizationID>, PX.Data.Or<FinPeriodClosingProcessBase.WhereFinPeriodInRange<APRegister.finPeriodID, CashAccountBranch.organizationID>>>>>>>>>>>, OrderBy<Asc<APRegister.finPeriodID, Asc<GLTranDoc.tranPeriodID, Asc<APRegister.docType, Asc<APRegister.refNbr, Asc<GLTranDoc.lineNbr>>>>>>>.Config>.GetCommand();

  protected override FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule[] CheckingRules { get; } = new FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule[1]
  {
    new FinPeriodClosingProcessBase.UnprocessedObjectsCheckingRule()
    {
      ReportID = "AP656100",
      ErrorMessage = "Unreleased documents exist for the {0} financial period.",
      CheckCommand = APClosingProcess.OpenDocumentsQuery
    }
  };

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
  public class APTran : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    /// <summary>
    /// Identifier of the <see cref="T:PX.Objects.GL.Branch">Branch</see>, to which the transaction belongs.
    /// </summary>
    /// <value>
    /// Corresponds to the <see cref="P:PX.Objects.GL.Branch.BranchID">Branch.BranchID</see> field.
    /// </value>
    [Branch(typeof (APRegister.branchID), null, true, true, true)]
    public virtual int? BranchID { get; set; }

    /// <summary>[key] The type of the transaction.</summary>
    /// <value>
    /// The field is determined by the type of the parent <see cref="T:PX.Objects.AP.APRegister">document</see>.
    /// For the list of possible values see <see cref="P:PX.Objects.AP.APRegister.DocType" />.
    /// </value>
    [APDocType.List]
    [PXDBString(3, IsKey = true, IsFixed = true)]
    [PXDBDefault(typeof (APRegister.docType))]
    [PXUIField(DisplayName = "Tran. Type", Visibility = PXUIVisibility.Visible, Visible = false)]
    public virtual string TranType { get; set; }

    /// <summary>
    /// [key] Reference number of the parent <see cref="T:PX.Objects.AP.APRegister">document</see>.
    /// </summary>
    [PXDBString(15, IsUnicode = true, IsKey = true)]
    [PXDBDefault(typeof (APRegister.refNbr))]
    [PXUIField(DisplayName = "Reference Nbr.", Visibility = PXUIVisibility.Visible, Visible = false)]
    [PXParent(typeof (PX.Data.Select<APRegister, Where<APRegister.docType, Equal<Current<APClosingProcess.APTran.tranType>>, And<APRegister.refNbr, Equal<Current<APClosingProcess.APTran.refNbr>>>>>))]
    public virtual string RefNbr { get; set; }

    /// <summary>
    /// <see cref="!:PX.Objects.GL.Obsolete.FinPeriod">Financial period</see>, which the line is associated with.
    /// </summary>
    /// <value>
    /// Defaults to the <see cref="P:PX.Objects.AP.APRegister.FinPeriodID">document's financial period</see>.
    /// </value>
    [FinPeriodID(null, typeof (APClosingProcess.APTran.branchID), null, null, null, null, true, false, null, null, typeof (APRegister.tranPeriodID), true, true)]
    public virtual string FinPeriodID { get; set; }

    public abstract class branchID : BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    APClosingProcess.APTran.branchID>
    {
    }

    public abstract class tranType : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APClosingProcess.APTran.tranType>
    {
    }

    public abstract class refNbr : BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APClosingProcess.APTran.refNbr>
    {
    }

    public abstract class finPeriodID : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      APClosingProcess.APTran.finPeriodID>
    {
    }
  }
}
