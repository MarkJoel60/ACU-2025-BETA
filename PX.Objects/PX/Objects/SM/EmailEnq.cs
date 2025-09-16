// Decompiled with JetBrains decompiler
// Type: PX.Objects.SM.EmailEnq
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.EP;
using PX.SM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.SM;

public class EmailEnq : PXGraph<EmailEnq>
{
  [PXHidden]
  public PXSelect<PX.Objects.CR.BAccount> BAccounts;
  [PXHidden]
  public PXSelect<PX.Objects.AR.Customer> Customers;
  [PXHidden]
  public PXSelect<PX.Objects.AP.Vendor> Vendors;
  [PXHidden]
  public PXSelect<EPEmployee> Employees;
  [PXHidden]
  public PXSelect<CRSMEmail> crEmail;
  [PXFilterable(new System.Type[] {})]
  [PXViewDetailsButton(typeof (SMEmail))]
  public PXSelectJoinOrderBy<SMEmail, LeftJoin<EMailAccount, On<EMailAccount.emailAccountID, Equal<SMEmail.mailAccountID>>, LeftJoin<CRActivity, On<CRActivity.noteID, Equal<SMEmail.refNoteID>>>>, OrderBy<Desc<SMEmail.createdDateTime>>> Emails;
  public PXAction<SMEmail> AddNew;
  public PXAction<SMEmail> DoubleClick;
  public PXAction<SMEmail> EditRecord;
  public PXAction<SMEmail> Delete;
  public PXAction<SMEmail> ViewEMail;
  public PXAction<SMEmail> ViewEntity;

  [PXMergeAttributes]
  [PXCustomizeBaseAttribute(typeof (PXUIFieldAttribute), "DisplayName", "Type")]
  protected virtual void _(PX.Data.Events.CacheAttached<CRActivity.classIcon> e)
  {
  }

  [PXUIField(DisplayName = "")]
  [PXInsertButton(Tooltip = "Add Email", CommitChanges = true)]
  public virtual void addNew()
  {
    CREmailActivityMaint instance = PXGraph.CreateInstance<CREmailActivityMaint>();
    ((PXSelectBase<CRSMEmail>) instance.Message).Current = ((PXSelectBase<CRSMEmail>) instance.Message).Insert();
    ((PXSelectBase) instance.Message).Cache.IsDirty = false;
    PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 1);
  }

  [PXUIField]
  public virtual IEnumerable doubleClick(PXAdapter adapter) => this.viewEMail(adapter);

  [PXUIField(DisplayName = "")]
  [PXEditDetailButton]
  public virtual void editRecord()
  {
    SMEmail current = ((PXSelectBase<SMEmail>) this.Emails).Current;
    if (current == null)
      return;
    Guid? refNoteId = current.RefNoteID;
    Guid? noteId = current.NoteID;
    if ((refNoteId.HasValue == noteId.HasValue ? (refNoteId.HasValue ? (refNoteId.GetValueOrDefault() != noteId.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
    {
      CREmailActivityMaint instance = PXGraph.CreateInstance<CREmailActivityMaint>();
      ((PXSelectBase<CRSMEmail>) instance.Message).Current = PXResultset<CRSMEmail>.op_Implicit(((PXSelectBase<CRSMEmail>) instance.Message).Search<CRSMEmail.noteID>((object) current.RefNoteID, Array.Empty<object>()));
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 1);
    }
    else
    {
      CRSMEmailMaint instance = PXGraph.CreateInstance<CRSMEmailMaint>();
      ((PXSelectBase<SMEmail>) instance.Email).Current = PXResultset<SMEmail>.op_Implicit(((PXSelectBase<SMEmail>) instance.Email).Search<SMEmail.noteID>((object) current.NoteID, Array.Empty<object>()));
      PXRedirectHelper.TryRedirect((PXGraph) instance, (PXRedirectHelper.WindowMode) 1);
    }
  }

  [PXUIField]
  [PXButton(ImageKey = "Remove")]
  public virtual IEnumerable delete(PXAdapter adapter)
  {
    // ISSUE: method pointer
    PXLongOperation.StartOperation((PXGraph) this, new PXToggleAsyncDelegate((object) this, __methodptr(\u003Cdelete\u003Eb__14_0)));
    return adapter.Get();
  }

  [PXUIField(DisplayName = "", Visible = false)]
  protected virtual IEnumerable viewEMail(PXAdapter adapter)
  {
    this.editRecord();
    return adapter.Get();
  }

  [PXUIField(DisplayName = "", Visible = false)]
  protected virtual IEnumerable viewEntity(PXAdapter adapter)
  {
    SMEmail current = ((PXSelectBase<SMEmail>) this.Emails).Current;
    if (current != null)
    {
      CRActivity crActivity = PXResultset<CRActivity>.op_Implicit(PXSelectBase<CRActivity, PXSelect<CRActivity, Where<CRActivity.noteID, Equal<Required<CRActivity.noteID>>>>.Config>.SelectSingleBound((PXGraph) this, (object[]) null, new object[1]
      {
        (object) current.RefNoteID
      }));
      if (crActivity != null)
        new EntityHelper((PXGraph) this).NavigateToRow(crActivity.RefNoteID, (PXRedirectHelper.WindowMode) 1);
    }
    return adapter.Get();
  }

  private static void DeleteMessage(IEnumerable<SMEmail> messages)
  {
    foreach (SMEmail message in messages)
    {
      Guid? refNoteId = message.RefNoteID;
      Guid? noteId = message.NoteID;
      if ((refNoteId.HasValue == noteId.HasValue ? (refNoteId.HasValue ? (refNoteId.GetValueOrDefault() != noteId.GetValueOrDefault() ? 1 : 0) : 0) : 1) != 0)
      {
        CREmailActivityMaint instance = PXGraph.CreateInstance<CREmailActivityMaint>();
        ((PXSelectBase<CRSMEmail>) instance.Message).Current = PXResultset<CRSMEmail>.op_Implicit(((PXSelectBase<CRSMEmail>) instance.Message).Search<CRSMEmail.noteID>((object) message.RefNoteID, Array.Empty<object>()));
        ((PXAction) instance.Delete).Press();
      }
      else
      {
        CRSMEmailMaint instance = PXGraph.CreateInstance<CRSMEmailMaint>();
        ((PXSelectBase<SMEmail>) instance.Email).Current = PXResultset<SMEmail>.op_Implicit(((PXSelectBase<SMEmail>) instance.Email).Search<SMEmail.noteID>((object) message.NoteID, Array.Empty<object>()));
        ((PXAction) instance.Delete).Press();
      }
    }
  }

  protected virtual IEnumerable<SMEmail> SelectedList()
  {
    return (IEnumerable<SMEmail>) ((PXSelectBase) this.Emails).Cache.Updated.Cast<SMEmail>().Where<SMEmail>((Func<SMEmail, bool>) (a => a.Selected.GetValueOrDefault())).ToList<SMEmail>();
  }
}
