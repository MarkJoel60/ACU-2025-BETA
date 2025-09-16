// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CREmailAccountUpdatePasswordExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;
using System.Collections;

#nullable disable
namespace PX.Objects.CR.Extensions;

/// <exclude />
public class CREmailAccountUpdatePasswordExt : PXGraphExtension<SMAccessPersonalMaint>
{
  [PXViewName("Update Password")]
  public CRValidationFilter<UpdatePasswordFilter> UpdatePasswordFilterView;
  public PXAction<Users> UpdatePassword;

  [PXUIField]
  [PXButton(Tooltip = "Update Password")]
  protected virtual IEnumerable updatePassword(PXAdapter adapter)
  {
    if (this.UpdatePasswordFilterView.AskExtFullyValid((DialogAnswerType) 1, true))
    {
      EMailAccount current = ((PXSelectBase<EMailAccount>) this.Base.EMailAccounts).Current;
      current.Password = ((PXSelectBase<UpdatePasswordFilter>) this.UpdatePasswordFilterView).Current.EmailAccountPassword;
      ((PXSelectBase<EMailAccount>) this.Base.EMailAccounts).Update(current);
    }
    ((PXGraph) this.Base).Actions.PressSave();
    return adapter.Get();
  }
}
