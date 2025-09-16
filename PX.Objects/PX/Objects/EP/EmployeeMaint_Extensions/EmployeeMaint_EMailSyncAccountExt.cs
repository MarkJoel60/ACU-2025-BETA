// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EmployeeMaint_Extensions.EmployeeMaint_EMailSyncAccountExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL.Fluent;

#nullable disable
namespace PX.Objects.EP.EmployeeMaint_Extensions;

public class EmployeeMaint_EMailSyncAccountExt : PXGraphExtension<EmployeeMaint>
{
  [PXHidden]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<PX.SM.EMailSyncAccount, TypeArrayOf<IFbqlJoin>.Empty>, PX.SM.EMailSyncAccount>.View EMailSyncAccount;
  [PXHidden]
  [PXCopyPasteHiddenView]
  public FbqlSelect<SelectFromBase<PX.SM.EMailSyncAccountPreferences, TypeArrayOf<IFbqlJoin>.Empty>, PX.SM.EMailSyncAccountPreferences>.View EMailSyncAccountPreferences;

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXParent(typeof (Select<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Current<PX.SM.EMailSyncAccount.employeeID>>>>))]
  public virtual void _(
    PX.Data.Events.CacheAttached<PX.SM.EMailSyncAccount.employeeID> e)
  {
  }

  [PXMergeAttributes(Method = MergeMethod.Append)]
  [PXParent(typeof (Select<PX.Objects.EP.EPEmployee, Where<PX.Objects.EP.EPEmployee.bAccountID, Equal<Current<PX.SM.EMailSyncAccountPreferences.employeeID>>>>))]
  public virtual void _(
    PX.Data.Events.CacheAttached<PX.SM.EMailSyncAccountPreferences.employeeID> e)
  {
  }
}
