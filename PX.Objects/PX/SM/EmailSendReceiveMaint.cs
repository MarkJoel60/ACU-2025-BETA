// Decompiled with JetBrains decompiler
// Type: PX.SM.EmailSendReceiveMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.Automation;
using PX.Data.BQL;
using PX.Objects.CR;
using System;
using System.Collections;

#nullable enable
namespace PX.SM;

[Serializable]
public class EmailSendReceiveMaint : PXGraph<
#nullable disable
EmailSendReceiveMaint>
{
  [PXHidden]
  public PXFilter<EmailSendReceiveMaint.OperationFilter> Filter;
  [PXCacheName("Emails")]
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessingJoin<EMailAccount, EmailSendReceiveMaint.OperationFilter, LeftJoin<EMailAccountStatistics, On<EMailAccountStatistics.emailAccountID, Equal<EMailAccount.emailAccountID>>>, Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  EMailAccount.emailAccountType, 
  #nullable disable
  In3<EmailAccountTypesAttribute.standard, EmailAccountTypesAttribute.plugin>>>>>.And<BqlChainableConditionMirror<TypeArrayOf<IBqlBinary>.Append<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<
  #nullable enable
  EMailAccount.isActive, 
  #nullable disable
  Equal<True>>>>, And<BqlOperand<
  #nullable enable
  EMailAccount.address, IBqlString>.IsNotNull>>>.And<
  #nullable disable
  BqlOperand<
  #nullable enable
  Empty, IBqlString>.IsNotEqual<
  #nullable disable
  RTrim<EMailAccount.address>>>>>> FilteredItems;
  public PXCancel<EmailSendReceiveMaint.OperationFilter> Cancel;
  public PXAction<EmailSendReceiveMaint.OperationFilter> ViewDetails;

  public EmailSendReceiveMaint()
  {
    this.CorrectUI();
    this.InitializeProcessing();
  }

  private void CorrectUI()
  {
    ((PXGraph) this).Actions.Move("Process", "Cancel");
    ((PXGraph) this).Actions.Move("Cancel", "Save");
  }

  private void InitializeProcessing()
  {
    ((PXProcessingBase<EMailAccount>) this.FilteredItems).SetSelected<EMailAccount.selected>();
  }

  [PXUIField(DisplayName = "View Details", Visible = false)]
  [PXButton]
  protected IEnumerable viewDetails(PXAdapter adapter)
  {
    PXCache cache = ((PXSelectBase) this.FilteredItems).Cache;
    EMailAccount current = ((PXSelectBase<EMailAccount>) this.FilteredItems).Current;
    if (current != null)
    {
      EMailAccountMaint instance = PXGraph.CreateInstance<EMailAccountMaint>();
      ((PXSelectBase<EMailAccount>) instance.EMailAccounts).Current = current;
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
    }
    return adapter.Get();
  }

  [PXRSACryptString]
  [PXUIField(DisplayName = "Password")]
  protected virtual void EMailAccount_Password_CacheAttached(PXCache sender)
  {
  }

  [PXRSACryptString]
  [PXUIField(DisplayName = "Password")]
  protected virtual void EMailAccount_OutcomingPassword_CacheAttached(PXCache sender)
  {
  }

  public virtual void _(
    Events.RowSelected<EmailSendReceiveMaint.OperationFilter> e)
  {
    ((PXProcessingBase<EMailAccount>) this.FilteredItems).SetProcessWorkflowAction(e.Row?.Operation);
  }

  public virtual void EMailAccount_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is EMailAccount row))
      return;
    int valueOrDefault1 = PXSelectBase<SMEmail, PXSelectGroupBy<SMEmail, Where<SMEmail.mailAccountID, Equal<Required<SMEmail.mailAccountID>>, And<SMEmail.mpstatus, Equal<MailStatusListAttribute.preProcess>, And<SMEmail.isIncome, NotEqual<True>>>>, Aggregate<Count>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.EmailAccountID
    }).RowCount.GetValueOrDefault();
    row.OutboxCount = new int?(valueOrDefault1);
    int valueOrDefault2 = PXSelectBase<SMEmail, PXSelectGroupBy<SMEmail, Where<SMEmail.mailAccountID, Equal<Required<SMEmail.mailAccountID>>, And<SMEmail.mpstatus, Equal<MailStatusListAttribute.preProcess>, And<SMEmail.isIncome, Equal<True>>>>, Aggregate<Count>>.Config>.Select((PXGraph) this, new object[1]
    {
      (object) row.EmailAccountID
    }).RowCount.GetValueOrDefault();
    row.InboxCount = new int?(valueOrDefault2);
  }

  [PXHidden]
  [Serializable]
  public class OperationFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
  {
    [PXWorkflowMassProcessing(DisplayName = "Action", AddUndefinedState = false)]
    public virtual string Operation { get; set; }

    public abstract class operation : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      EmailSendReceiveMaint.OperationFilter.operation>
    {
    }
  }
}
