// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLConsolSetupMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Objects.GL.Consolidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace PX.Objects.GL;

public class GLConsolSetupMaint : PXGraph<GLConsolSetupMaint>
{
  public PXSelect<GLSetup> GLSetupRecord;
  public PXSave<GLSetup> Save;
  public PXCancel<GLSetup> Cancel;
  public PXFilteredProcessing<GLConsolSetup, GLSetup> ConsolSetupRecords;
  public PXSelect<Account, Where<Account.gLConsolAccountCD, Equal<Required<Account.gLConsolAccountCD>>>> Accounts;
  public PXSelect<GLConsolLedger, Where<GLConsolLedger.setupID, Equal<Required<GLConsolSetup.setupID>>>> ConsolLedgers;
  public PXSelect<GLConsolBranch, Where<GLConsolBranch.setupID, Equal<Required<GLConsolBranch.setupID>>>> ConsolBranches;
  public PXSetup<GLSetup> glsetup;

  public GLConsolSetupMaint()
  {
    GLSetup current = ((PXSelectBase<GLSetup>) this.glsetup).Current;
    ((PXProcessing<GLConsolSetup>) this.ConsolSetupRecords).SetProcessCaption("Synchronize");
    ((PXProcessing<GLConsolSetup>) this.ConsolSetupRecords).SetProcessAllCaption("Synchronize All");
    // ISSUE: method pointer
    ((PXProcessingBase<GLConsolSetup>) this.ConsolSetupRecords).SetProcessDelegate<GLConsolSetupMaint>(new PXProcessingBase<GLConsolSetup>.ProcessItemDelegate<GLConsolSetupMaint>((object) null, __methodptr(Synchronize)));
    PXUIFieldAttribute.SetEnabled(((PXSelectBase) this.ConsolSetupRecords).Cache, (string) null, true);
    PXUIFieldAttribute.SetEnabled<GLConsolSetup.lastPostPeriod>(((PXSelectBase) this.ConsolSetupRecords).Cache, (object) null, false);
    PXUIFieldAttribute.SetEnabled<GLConsolSetup.lastConsDate>(((PXSelectBase) this.ConsolSetupRecords).Cache, (object) null, false);
    ((PXProcessingBase<GLConsolSetup>) this.ConsolSetupRecords).SetAutoPersist(true);
    ((PXSelectBase) this.ConsolSetupRecords).Cache.AllowDelete = true;
    ((PXSelectBase) this.ConsolSetupRecords).Cache.AllowInsert = true;
    PXUIFieldAttribute.SetRequired<GLConsolSetup.segmentValue>(((PXSelectBase) this.ConsolSetupRecords).Cache, true);
    PXUIFieldAttribute.SetRequired<GLConsolSetup.sourceLedgerCD>(((PXSelectBase) this.ConsolSetupRecords).Cache, true);
    // ISSUE: method pointer
    ((PXAction) this.Save).StateSelectingEvents += new PXFieldSelecting((object) this, __methodptr(\u003C\u002Ector\u003Eb__8_0));
  }

  private static void AwaitOrRethrow(params System.Threading.Tasks.Task[] apitasks)
  {
    try
    {
      System.Threading.Tasks.Task.WaitAll(apitasks);
    }
    catch (Exception ex)
    {
      throw new PXException(ConsolidationClient.GetServerError(ex), ex);
    }
  }

  protected static void Synchronize(GLConsolSetupMaint graph, GLConsolSetup consolSetup)
  {
    using (ConsolidationClient consolidationClient = new ConsolidationClient(consolSetup.Url, consolSetup.Login, consolSetup.Password, consolSetup.HttpClientTimeout))
    {
      Task<IEnumerable<ConsolAccountAPI>> consolAccounts = consolidationClient.GetConsolAccounts();
      Task<IEnumerable<OrganizationAPI>> organizations = consolidationClient.GetOrganizations();
      Task<IEnumerable<BranchAPI>> branches = consolidationClient.GetBranches();
      Task<IEnumerable<LedgerAPI>> ledgers = consolidationClient.GetLedgers();
      PXSelect<Account> pxSelect = new PXSelect<Account>((PXGraph) graph);
      List<Account> list1 = GraphHelper.RowCast<Account>((IEnumerable) ((PXSelectBase<Account>) pxSelect).Select(Array.Empty<object>())).ToList<Account>();
      List<GLConsolLedger> list2 = GraphHelper.RowCast<GLConsolLedger>((IEnumerable) ((PXSelectBase<GLConsolLedger>) graph.ConsolLedgers).Select(new object[1]
      {
        (object) consolSetup.SetupID
      })).ToList<GLConsolLedger>();
      List<GLConsolBranch> list3 = GraphHelper.RowCast<GLConsolBranch>((IEnumerable) ((PXSelectBase<GLConsolBranch>) graph.ConsolBranches).Select(new object[1]
      {
        (object) consolSetup.SetupID
      })).ToList<GLConsolBranch>();
      GLConsolSetupMaint.AwaitOrRethrow((System.Threading.Tasks.Task) consolAccounts, (System.Threading.Tasks.Task) organizations, (System.Threading.Tasks.Task) branches, (System.Threading.Tasks.Task) ledgers);
      consolidationClient.Relogin();
      foreach (ConsolAccountAPI account1 in consolAccounts.Result)
      {
        Account account2 = new Account();
        account2.AccountCD = account1.AccountCD;
        object accountCd = (object) account2.AccountCD;
        ((PXSelectBase) pxSelect).Cache.RaiseFieldUpdating<Account.accountCD>((object) account2, ref accountCd);
        account2.AccountCD = (string) accountCd;
        Account account3 = ((PXSelectBase<Account>) pxSelect).Locate(account2);
        if (account3 == null)
        {
          GLConsolSetupMaint.AwaitOrRethrow(consolidationClient.DeleteConsolAccount(account1));
        }
        else
        {
          list1.Remove(account3);
          if (account3.Description != account1.Description)
          {
            account1.Description = account3.Description;
            GLConsolSetupMaint.AwaitOrRethrow(consolidationClient.UpdateConsolAccount(account1));
          }
        }
      }
      foreach (LedgerAPI ledgerApi in ledgers.Result)
      {
        GLConsolLedger glConsolLedger = ((PXSelectBase<GLConsolLedger>) graph.ConsolLedgers).Locate(new GLConsolLedger()
        {
          SetupID = consolSetup.SetupID,
          LedgerCD = ledgerApi.LedgerCD
        });
        if (glConsolLedger != null)
        {
          list2.Remove(glConsolLedger);
          if (glConsolLedger.Description != ledgerApi.Descr || glConsolLedger.BalanceType != ledgerApi.BalanceType)
          {
            glConsolLedger.Description = ledgerApi.Descr;
            glConsolLedger.BalanceType = ledgerApi.BalanceType;
            ((PXSelectBase) graph.ConsolLedgers).Cache.SetStatus((object) glConsolLedger, (PXEntryStatus) 1);
          }
        }
        else
          ((PXSelectBase<GLConsolLedger>) graph.ConsolLedgers).Insert(new GLConsolLedger()
          {
            SetupID = consolSetup.SetupID,
            LedgerCD = ledgerApi.LedgerCD,
            Description = ledgerApi.Descr,
            BalanceType = ledgerApi.BalanceType
          });
      }
      foreach (GLConsolLedger glConsolLedger in list2)
      {
        ((PXSelectBase<GLConsolLedger>) graph.ConsolLedgers).Delete(glConsolLedger);
        if (consolSetup.SourceLedgerCD == glConsolLedger.LedgerCD)
        {
          consolSetup.SourceLedgerCD = (string) null;
          ((PXSelectBase<GLConsolSetup>) graph.ConsolSetupRecords).Update(consolSetup);
        }
      }
      foreach (OrganizationAPI organizationApi in organizations.Result)
      {
        GLConsolBranch glConsolBranch = ((PXSelectBase<GLConsolBranch>) graph.ConsolBranches).Locate(new GLConsolBranch()
        {
          SetupID = consolSetup.SetupID,
          BranchCD = organizationApi.OrganizationCD
        });
        if (glConsolBranch != null)
        {
          list3.Remove(glConsolBranch);
          if (glConsolBranch.Description != organizationApi.OrganizationName || glConsolBranch.LedgerCD != organizationApi.LedgerCD || glConsolBranch.OrganizationCD != organizationApi.OrganizationCD || !glConsolBranch.IsOrganization.GetValueOrDefault())
          {
            glConsolBranch.OrganizationCD = organizationApi.OrganizationCD;
            glConsolBranch.LedgerCD = organizationApi.LedgerCD;
            glConsolBranch.Description = organizationApi.OrganizationName;
            glConsolBranch.IsOrganization = new bool?(true);
            ((PXSelectBase) graph.ConsolBranches).Cache.SetStatus((object) glConsolBranch, (PXEntryStatus) 1);
          }
        }
        else
          ((PXSelectBase<GLConsolBranch>) graph.ConsolBranches).Insert(new GLConsolBranch()
          {
            SetupID = consolSetup.SetupID,
            BranchCD = organizationApi.OrganizationCD,
            OrganizationCD = organizationApi.OrganizationCD,
            LedgerCD = organizationApi.LedgerCD,
            Description = organizationApi.OrganizationName,
            IsOrganization = new bool?(true)
          });
      }
      foreach (BranchAPI branchApi in branches.Result)
      {
        GLConsolBranch glConsolBranch = ((PXSelectBase<GLConsolBranch>) graph.ConsolBranches).Locate(new GLConsolBranch()
        {
          SetupID = consolSetup.SetupID,
          BranchCD = branchApi.BranchCD
        });
        if (glConsolBranch != null)
        {
          list3.Remove(glConsolBranch);
          if (glConsolBranch.Description != branchApi.AcctName || glConsolBranch.LedgerCD != branchApi.LedgerCD || glConsolBranch.OrganizationCD != branchApi.OrganizationCD)
          {
            glConsolBranch.OrganizationCD = branchApi.OrganizationCD;
            glConsolBranch.LedgerCD = branchApi.LedgerCD;
            glConsolBranch.Description = branchApi.AcctName;
            ((PXSelectBase) graph.ConsolBranches).Cache.SetStatus((object) glConsolBranch, (PXEntryStatus) 1);
          }
        }
        else
          ((PXSelectBase<GLConsolBranch>) graph.ConsolBranches).Insert(new GLConsolBranch()
          {
            SetupID = consolSetup.SetupID,
            BranchCD = branchApi.BranchCD,
            OrganizationCD = branchApi.OrganizationCD,
            LedgerCD = branchApi.LedgerCD,
            Description = branchApi.AcctName
          });
      }
      foreach (GLConsolBranch glConsolBranch in list3)
      {
        ((PXSelectBase<GLConsolBranch>) graph.ConsolBranches).Delete(glConsolBranch);
        if (consolSetup.SourceBranchCD == glConsolBranch.BranchCD)
        {
          consolSetup.SourceBranchCD = (string) null;
          ((PXSelectBase<GLConsolSetup>) graph.ConsolSetupRecords).Update(consolSetup);
        }
      }
      consolidationClient.Relogin();
      foreach (Account account in list1)
        GLConsolSetupMaint.AwaitOrRethrow(consolidationClient.InsertConsolAccount(new ConsolAccountAPI()
        {
          AccountCD = account.AccountCD,
          Description = account.Description
        }));
    }
    ((PXAction) graph.Save).Press();
  }

  protected virtual void GLSetup_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    if (e.Row == null)
      return;
    bool hasValue = ((GLSetup) e.Row).ConsolSegmentId.HasValue;
    PXUIFieldAttribute.SetEnabled<GLConsolSetup.segmentValue>(((PXSelectBase) this.ConsolSetupRecords).Cache, (object) null, hasValue);
    PXUIFieldAttribute.SetEnabled<GLConsolSetup.pasteFlag>(((PXSelectBase) this.ConsolSetupRecords).Cache, (object) null, hasValue);
    PXUIFieldAttribute.SetVisible<GLConsolSetup.segmentValue>(((PXSelectBase) this.ConsolSetupRecords).Cache, (object) null, hasValue);
    PXUIFieldAttribute.SetVisible<GLConsolSetup.pasteFlag>(((PXSelectBase) this.ConsolSetupRecords).Cache, (object) null, hasValue);
    PXDefaultAttribute.SetPersistingCheck<GLConsolSetup.segmentValue>(((PXSelectBase) this.ConsolSetupRecords).Cache, (object) null, hasValue ? (PXPersistingCheck) 1 : (PXPersistingCheck) 2);
  }

  protected virtual void GLSetup_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    foreach (PXResult<GLConsolSetup> pxResult in ((PXSelectBase<GLConsolSetup>) this.ConsolSetupRecords).Select(Array.Empty<object>()))
      GraphHelper.MarkUpdated(((PXSelectBase) this.ConsolSetupRecords).Cache, (object) PXResult<GLConsolSetup>.op_Implicit(pxResult));
  }

  protected virtual void GLSetup_ConsolSegmentId_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    if (((GLSetup) e.Row).ConsolSegmentId.HasValue)
      return;
    EnumerableExtensions.ForEach<GLConsolSetup>(GraphHelper.RowCast<GLConsolSetup>((IEnumerable) ((PXSelectBase<GLConsolSetup>) this.ConsolSetupRecords).Select(Array.Empty<object>())), (Action<GLConsolSetup>) (row => row.PasteFlag = new bool?(false)));
  }

  protected virtual void GLConsolSetup_BranchID_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    sender.SetDefaultExt<GLConsolSetup.ledgerId>(e.Row);
  }

  protected virtual void GLConsolSetup_RowSelected(PXCache sender, PXRowSelectedEventArgs e)
  {
    GLConsolSetup row = (GLConsolSetup) e.Row;
    if (row != null)
      PXUIFieldAttribute.SetEnabled<GLConsolSetup.selected>(sender, (object) row, row.IsActive.GetValueOrDefault());
    GLConsolLedger glConsolLedger = PXResultset<GLConsolLedger>.op_Implicit(PXSelectBase<GLConsolLedger, PXSelect<GLConsolLedger, Where<GLConsolLedger.setupID, Equal<Current<GLConsolSetup.setupID>>, And<GLConsolLedger.ledgerCD, Equal<Current<GLConsolSetup.sourceLedgerCD>>>>>.Config>.Select((PXGraph) this, Array.Empty<object>()));
    PXUIFieldAttribute.SetEnabled<GLConsolSetup.sourceBranchCD>(sender, e.Row, glConsolLedger != null);
  }

  protected virtual void GLConsolSetup_RowUpdated(PXCache sender, PXRowUpdatedEventArgs e)
  {
    GLConsolSetup row = (GLConsolSetup) e.Row;
    if (sender.ObjectsEqual<GLConsolSetup.sourceLedgerCD>(e.Row, e.OldRow))
      return;
    string str = (string) sender.GetValue<GLConsolSetup.sourceBranchCD>(e.Row);
    sender.SetValue<GLConsolSetup.sourceBranchCD>(e.Row, (object) null);
    sender.SetValueExt<GLConsolSetup.sourceBranchCD>(e.Row, (object) str);
  }

  protected virtual void GLConsolSetup_RowUpdating(PXCache sender, PXRowUpdatingEventArgs e)
  {
    GLConsolSetup newRow = (GLConsolSetup) e.NewRow;
    if (!newRow.PasteFlag.GetValueOrDefault() || !string.IsNullOrEmpty(newRow.SegmentValue))
      return;
    sender.RaiseExceptionHandling<GLConsolSetup.segmentValue>(e.NewRow, (object) newRow.SegmentValue, (Exception) new PXSetPropertyException((IBqlTable) newRow, "Consolidation Segment Value may not be empty if Paste Segment Value is selected."));
    ((CancelEventArgs) e).Cancel = true;
  }

  protected virtual void GLConsolSetup_IsActive_FieldUpdated(
    PXCache sender,
    PXFieldUpdatedEventArgs e)
  {
    GLConsolSetup row = (GLConsolSetup) e.Row;
    if (row == null)
      return;
    bool? isActive = row.IsActive;
    bool flag = false;
    if (!(isActive.GetValueOrDefault() == flag & isActive.HasValue))
      return;
    row.Selected = new bool?(false);
  }

  protected virtual void GLConsolSetup_Url_FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    string newValue = e.NewValue as string;
    if (string.IsNullOrEmpty(newValue))
      return;
    string str1 = PXUrl.IngoreAllQueryParameters(newValue);
    string str2 = "~/Main".TrimStart('~');
    if (!str1.EndsWith(str2, StringComparison.OrdinalIgnoreCase))
      return;
    e.NewValue = (object) str1.Substring(0, str1.Length - str2.Length);
  }
}
