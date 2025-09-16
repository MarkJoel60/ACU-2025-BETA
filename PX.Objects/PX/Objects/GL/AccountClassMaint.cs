// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.AccountClassMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS;
using PX.Data;
using PX.Objects.CS;
using System.Collections;

#nullable disable
namespace PX.Objects.GL;

public class AccountClassMaint : PXGraph<AccountClassMaint>, PXImportAttribute.IPXPrepareItems
{
  public PXSavePerRow<AccountClass> Save;
  public PXCancel<AccountClass> Cancel;
  [PXImport(typeof (AccountClass))]
  public PXSelect<AccountClass> AccountClassRecords;
  public PXSetup<Branch> Company;

  public AccountClassMaint()
  {
    if (!((PXSelectBase<Branch>) this.Company).Current.BAccountID.HasValue)
      throw new PXSetupNotEnteredException("The required configuration data is not entered on the {0} form.", typeof (Branch), new object[1]
      {
        (object) PXMessages.LocalizeNoPrefix("Company Branches")
      });
  }

  protected virtual void AccountClass_RowDeleting(PXCache sender, PXRowDeletingEventArgs e)
  {
    AccountClass row = (AccountClass) e.Row;
    string usedIn;
    if (row != null && AccountClassMaint.AccountClassInUse((PXGraph) this, row.AccountClassID, out usedIn))
      throw new PXSetPropertyException("Account Class {0} is used in {1}.", new object[2]
      {
        (object) row.AccountClassID,
        (object) usedIn
      });
  }

  protected virtual void AccountClass_AccountClassID_FieldVerifying(
    PXCache sender,
    PXFieldVerifyingEventArgs e)
  {
    AccountClass row = (AccountClass) e.Row;
    string usedIn;
    if (row != null && AccountClassMaint.AccountClassInUse((PXGraph) this, row.AccountClassID, out usedIn))
      throw new PXSetPropertyException("Account Class {0} is used in {1}.", new object[2]
      {
        (object) row.AccountClassID,
        (object) usedIn
      });
  }

  public virtual void _(
    Events.FieldVerifying<AccountClass, AccountClass.type> e)
  {
    if (((Events.FieldVerifyingBase<Events.FieldVerifying<AccountClass, AccountClass.type>, AccountClass, object>) e).NewValue is string newValue && !new AccountType.ListAttribute().ValueLabelDic.ContainsKey(newValue))
      throw new PXException("Error: '{0}' cannot be found in the system.", new object[1]
      {
        (object) newValue
      });
  }

  public static bool AccountClassInUse(PXGraph graph, string accountClassID, out string usedIn)
  {
    usedIn = (string) null;
    Account account = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountClassID, Equal<Required<AccountClass.accountClassID>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
    {
      (object) accountClassID
    }));
    if (account != null)
      usedIn = "GL Account" + account.AccountCD;
    else if (PXResultset<RMDataSource>.op_Implicit(PXSelectBase<RMDataSource, PXSelect<RMDataSource, Where<RMDataSourceGL.accountClassID, Equal<Required<AccountClass.accountClassID>>>>.Config>.SelectSingleBound(graph, (object[]) null, new object[1]
    {
      (object) accountClassID
    })) != null)
      usedIn = "Analitycal Report Manager";
    return usedIn != null;
  }

  public virtual bool PrepareImportRow(string viewName, IDictionary keys, IDictionary values)
  {
    return !string.IsNullOrWhiteSpace(keys[(object) "AccountClassID"]?.ToString());
  }

  public bool RowImporting(string viewName, object row) => row == null;

  public bool RowImported(string viewName, object row, object oldRow) => oldRow == null;

  public virtual void PrepareItems(string viewName, IEnumerable items)
  {
  }
}
