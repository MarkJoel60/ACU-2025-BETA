// Decompiled with JetBrains decompiler
// Type: PX.SM.EmailProcessingMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CR;
using PX.SM.Email;
using System;
using System.Collections;

#nullable enable
namespace PX.SM;

[Serializable]
public class EmailProcessingMaint : PXGraph<
#nullable disable
EmailProcessingMaint>
{
  [PXCacheName("Filter")]
  public PXFilter<EmailProcessingMaint.EmailProcessingFilter> Filter;
  [PXCacheName("Emails")]
  [PXFilterable(new System.Type[] {})]
  public PXFilteredProcessingJoin<SMEmail, EmailProcessingMaint.EmailProcessingFilter, LeftJoin<EMailAccount, On<EMailAccount.emailAccountID, Equal<SMEmail.mailAccountID>>, LeftJoin<CRActivity, On<CRActivity.noteID, Equal<SMEmail.refNoteID>>>>, Where2<Where<SMEmail.isIncome, Equal<True>, Or<SMEmail.mpstatus, Equal<MailStatusListAttribute.failed>, Or<EMailAccount.emailAccountType, NotEqual<EmailAccountTypesAttribute.exchange>>>>, And2<Where<Current<EmailProcessingMaint.EmailProcessingFilter.includeFailed>, Equal<False>, Or<SMEmail.mpstatus, Equal<MailStatusListAttribute.preProcess>, Or<SMEmail.mpstatus, Equal<MailStatusListAttribute.failed>>>>, And2<Where<Current<EmailProcessingMaint.EmailProcessingFilter.includeFailed>, Equal<True>, Or<SMEmail.mpstatus, Equal<MailStatusListAttribute.preProcess>>>, And2<Where<Current<EmailProcessingMaint.EmailProcessingFilter.account>, IsNull, Or<SMEmail.mailAccountID, Equal<Current<EmailProcessingMaint.EmailProcessingFilter.account>>>>, And2<Where<Current<EmailProcessingMaint.EmailProcessingFilter.type>, Equal<EmailProcessingMaint.AllEmailes>, Or2<Where<Current<EmailProcessingMaint.EmailProcessingFilter.type>, Equal<EmailProcessingMaint.IncomingEmails>, And<SMEmail.isIncome, Equal<True>>>, Or<Where<Current<EmailProcessingMaint.EmailProcessingFilter.type>, Equal<EmailProcessingMaint.OutgoingEmails>, And<SMEmail.isIncome, NotEqual<True>>>>>>, And<Where<Current<EmailProcessingMaint.EmailProcessingFilter.ownerID>, IsNull, Or<CRActivity.ownerID, Equal<Current<EmailProcessingMaint.EmailProcessingFilter.ownerID>>>>>>>>>>> FilteredItems;
  public PXCancel<EmailProcessingMaint.EmailProcessingFilter> Cancel;
  public PXAction<EmailProcessingMaint.EmailProcessingFilter> ViewDetails;

  public EmailProcessingMaint()
  {
    this.InitializeUI();
    this.InitializeProcessing();
  }

  private void InitializeUI()
  {
    PXUIFieldAttribute.SetDisplayName<EMailAccount.address>(((PXGraph) this).Caches[typeof (EMailAccount)], "Account");
    ((PXGraph) this).Actions.Move("Process", "Cancel");
    ((PXGraph) this).Actions.Move("Cancel", "Save");
  }

  private void InitializeProcessing()
  {
    ((PXProcessingBase<SMEmail>) this.FilteredItems).SetSelected<SMEmail.selected>();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    ((PXProcessingBase<SMEmail>) this.FilteredItems).SetProcessDelegate(EmailProcessingMaint.\u003C\u003Ec.\u003C\u003E9__8_0 ?? (EmailProcessingMaint.\u003C\u003Ec.\u003C\u003E9__8_0 = new PXProcessingBase<SMEmail>.ProcessListDelegate((object) EmailProcessingMaint.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CInitializeProcessing\u003Eb__8_0))));
  }

  [PXUIField(DisplayName = "View Details", Visible = false)]
  [PXButton]
  protected IEnumerable viewDetails(PXAdapter adapter)
  {
    SMEmail current = ((PXSelectBase<SMEmail>) this.FilteredItems).Current;
    if (current != null)
    {
      if (current.RefNoteID.HasValue)
      {
        CREmailActivityMaint instance = PXGraph.CreateInstance<CREmailActivityMaint>();
        ((PXSelectBase<CRSMEmail>) instance.Message).Current = PXResultset<CRSMEmail>.op_Implicit(((PXSelectBase<CRSMEmail>) instance.Message).Search<CRSMEmail.noteID>((object) current.RefNoteID, Array.Empty<object>()));
        PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
      }
      else
      {
        CRSMEmailMaint instance = PXGraph.CreateInstance<CRSMEmailMaint>();
        ((PXSelectBase<SMEmail>) instance.Email).Current = PXResultset<SMEmail>.op_Implicit(((PXSelectBase<SMEmail>) instance.Email).Search<SMEmail.noteID>((object) current.NoteID, Array.Empty<object>()));
        PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 3);
      }
    }
    return adapter.Get();
  }

  protected virtual void EmailProcessingFilter_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (!(e.Row is EmailProcessingMaint.EmailProcessingFilter))
      return;
    bool flag1 = true.Equals(sender.GetValue(e.Row, typeof (EmailProcessingMaint.EmailProcessingFilter.myOwner).Name));
    bool flag2 = true.Equals(sender.GetValue(e.Row, typeof (EmailProcessingMaint.EmailProcessingFilter.myWorkGroup).Name));
    PXUIFieldAttribute.SetEnabled(sender, e.Row, typeof (EmailProcessingMaint.EmailProcessingFilter.ownerID).Name, !flag1);
    PXUIFieldAttribute.SetEnabled(sender, e.Row, typeof (EmailProcessingMaint.EmailProcessingFilter.workGroupID).Name, !flag2);
  }

  [PXUIField]
  [PXMergeAttributes]
  protected virtual void SMEmail_Subject_CacheAttached(PXCache sender)
  {
  }

  [Serializable]
  public class EmailProcessingFilter : OwnedFilter
  {
    [PXUIField(DisplayName = "Account")]
    [EmailAccountRaw]
    public virtual int? Account { get; set; }

    [PXInt]
    [PXDefault(0)]
    [PXUIField(DisplayName = "Type")]
    [PXIntList(new int[] {0, 1, 2}, new string[] {"All", "Incoming", "Outgoing"})]
    public virtual int? Type { get; set; }

    [PXDBBool]
    [PXDefault(false)]
    [PXUIField(DisplayName = "Include Failed")]
    public virtual bool? IncludeFailed { get; set; }

    public abstract class account : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmailProcessingMaint.EmailProcessingFilter.account>
    {
    }

    public abstract class type : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmailProcessingMaint.EmailProcessingFilter.type>
    {
    }

    public abstract class includeFailed : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      EmailProcessingMaint.EmailProcessingFilter.includeFailed>
    {
    }

    public new abstract class ownerID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmailProcessingMaint.EmailProcessingFilter.ownerID>
    {
    }

    public new abstract class myOwner : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      EmailProcessingMaint.EmailProcessingFilter.myOwner>
    {
    }

    public new abstract class myWorkGroup : 
      BqlType<
      #nullable enable
      IBqlBool, bool>.Field<
      #nullable disable
      EmailProcessingMaint.EmailProcessingFilter.myWorkGroup>
    {
    }

    public new abstract class workGroupID : 
      BqlType<
      #nullable enable
      IBqlInt, int>.Field<
      #nullable disable
      EmailProcessingMaint.EmailProcessingFilter.workGroupID>
    {
    }
  }

  public sealed class AllEmailes : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  EmailProcessingMaint.AllEmailes>
  {
    public AllEmailes()
      : base(0)
    {
    }
  }

  public sealed class IncomingEmails : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    EmailProcessingMaint.IncomingEmails>
  {
    public IncomingEmails()
      : base(1)
    {
    }
  }

  public sealed class OutgoingEmails : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Constant<
    #nullable disable
    EmailProcessingMaint.OutgoingEmails>
  {
    public OutgoingEmails()
      : base(2)
    {
    }
  }
}
