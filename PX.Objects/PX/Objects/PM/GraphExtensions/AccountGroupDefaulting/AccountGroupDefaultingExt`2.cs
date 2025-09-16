// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.GraphExtensions.AccountGroupDefaulting.AccountGroupDefaultingExt`2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.PM.GraphExtensions.AccountGroupDefaulting;

public abstract class AccountGroupDefaultingExt<TGraph, TLine> : PXGraphExtension<TGraph>
  where TGraph : PXGraph, new()
  where TLine : class, IBqlTable, new()
{
  protected abstract string InventoryFieldName { get; }

  protected abstract string ProjectFieldName { get; }

  protected virtual string? TaskFieldName { get; }

  protected abstract string AccountGroupFieldName { get; }

  protected virtual string? AccountTypeFieldName { get; }

  public virtual void Initialize()
  {
    ((PXGraphExtension) this).Initialize();
    PXGraph.FieldDefaultingEvents fieldDefaulting = this.Base.FieldDefaulting;
    Type type = typeof (TLine);
    string accountGroupFieldName = this.AccountGroupFieldName;
    AccountGroupDefaultingExt<TGraph, TLine> groupDefaultingExt = this;
    // ISSUE: virtual method pointer
    PXFieldDefaulting pxFieldDefaulting = new PXFieldDefaulting((object) groupDefaultingExt, __vmethodptr(groupDefaultingExt, AccountGroupFieldDefaulting));
    fieldDefaulting.AddHandler(type, accountGroupFieldName, pxFieldDefaulting);
  }

  protected virtual void AccountGroupFieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    if (e.Row != null && !sender.Graph.IsCopyPasteContext && !sender.Graph.IsImportFromExcel && (!sender.Graph.IsImport || sender.Graph.IsImport && sender.Graph.IsMobile) && !sender.Graph.IsContractBasedAPI)
    {
      int? defaultAccountGroup = this.CalculateDefaultAccountGroup(sender, (TLine) e.Row);
      if (defaultAccountGroup.HasValue)
      {
        e.NewValue = (object) defaultAccountGroup;
        return;
      }
    }
    e.NewValue = e.NewValue ?? sender.GetValue(e.Row, this.AccountGroupFieldName);
  }

  protected virtual int? CalculateDefaultAccountGroup(PXCache sender, TLine row)
  {
    int? projectID = sender.GetValue((object) row, this.ProjectFieldName) as int?;
    int? inventoryID = sender.GetValue((object) row, this.InventoryFieldName) as int?;
    if (!projectID.HasValue)
      return new int?();
    string str1 = this.GetDefaultAccountType();
    if (!string.IsNullOrEmpty(this.AccountTypeFieldName))
    {
      string str2 = sender.GetValue((object) row, this.AccountTypeFieldName) as string;
      if (!string.IsNullOrEmpty(str2))
        str1 = str2;
    }
    if (str1 == "I")
    {
      PXResultset<PMAccountGroup> pxResultset = PXSelectBase<PMAccountGroup, PXSelect<PMAccountGroup, Where<PMAccountGroup.type, Equal<AccountType.income>, And<PMAccountGroup.isActive, Equal<True>>>>.Config>.Select((PXGraph) this.Base, Array.Empty<object>());
      if (pxResultset.Count == 1)
        return PXResult<PMAccountGroup>.op_Implicit(pxResultset[0]).GroupID;
    }
    int? defaultAccountGroup;
    if (inventoryID.HasValue)
    {
      defaultAccountGroup = inventoryID;
      int emptyInventoryId = PMInventorySelectorAttribute.EmptyInventoryID;
      if (!(defaultAccountGroup.GetValueOrDefault() == emptyInventoryId & defaultAccountGroup.HasValue))
      {
        PX.Objects.IN.InventoryItem inventoryItem = PX.Objects.IN.InventoryItem.PK.Find((PXGraph) this.Base, inventoryID);
        if (inventoryItem != null)
        {
          int? accountID = str1 == "I" ? inventoryItem.SalesAcctID : inventoryItem.COGSAcctID;
          if (accountID.HasValue)
          {
            PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this.Base, accountID);
            if (account != null)
            {
              defaultAccountGroup = account.AccountGroupID;
              if (defaultAccountGroup.HasValue && account.Type == str1)
                return account.AccountGroupID;
            }
          }
        }
      }
    }
    int? taskID = new int?();
    if (!string.IsNullOrEmpty(this.TaskFieldName))
    {
      taskID = sender.GetValue((object) row, this.TaskFieldName) as int?;
      if (!taskID.HasValue)
      {
        defaultAccountGroup = new int?();
        return defaultAccountGroup;
      }
    }
    PMTask pmTask = PMTask.PK.Find((PXGraph) this.Base, projectID, taskID);
    if (pmTask != null)
    {
      int? accountID = str1 == "I" ? pmTask.DefaultSalesAccountID : pmTask.DefaultExpenseAccountID;
      if (accountID.HasValue)
      {
        PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find((PXGraph) this.Base, accountID);
        if (account != null)
        {
          defaultAccountGroup = account.AccountGroupID;
          if (defaultAccountGroup.HasValue && account.Type == str1)
            return account.AccountGroupID;
        }
      }
    }
    defaultAccountGroup = new int?();
    return defaultAccountGroup;
  }

  protected virtual string GetDefaultAccountType() => "E";
}
