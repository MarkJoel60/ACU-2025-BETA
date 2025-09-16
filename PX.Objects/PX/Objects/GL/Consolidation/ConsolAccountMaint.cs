// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.Consolidation.ConsolAccountMaint
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.GL.Consolidation;

public class ConsolAccountMaint : PXGraph<ConsolAccountMaint>
{
  public PXSavePerRow<GLConsolAccount, GLConsolAccount.accountCD> Save;
  public PXCancel<GLConsolAccount> Cancel;
  [PXImport(typeof (GLConsolAccount))]
  [PXFilterable(new Type[] {})]
  public PXSelect<GLConsolAccount, Where<True, Equal<True>>, OrderBy<Asc<GLConsolAccount.accountCD>>> AccountRecords;
  public PXSelect<Account, Where<Account.gLConsolAccountCD, Equal<Required<Account.gLConsolAccountCD>>>> Accounts;

  protected virtual void GLConsolAccount_RowDeleted(PXCache sender, PXRowDeletedEventArgs e)
  {
    GLConsolAccount row = (GLConsolAccount) e.Row;
    foreach (PXResult<Account> pxResult in ((PXSelectBase<Account>) this.Accounts).Select(new object[1]
    {
      (object) row.AccountCD
    }))
    {
      Account account = PXResult<Account>.op_Implicit(pxResult);
      account.GLConsolAccountCD = (string) null;
      ((PXSelectBase<Account>) this.Accounts).Update(account);
    }
  }
}
