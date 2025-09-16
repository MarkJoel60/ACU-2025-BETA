// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ActiveProjectAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;
using PX.Objects.CT;
using PX.Objects.PM;
using System;
using System.ComponentModel;

#nullable disable
namespace PX.Objects.GL;

[PXDBInt]
[PXInt]
[PXUIField]
[PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The {0} project cannot be selected because it has the {1} status.", new Type[] {typeof (PMProject.contractCD), typeof (PMProject.status)})]
[PXRestrictor(typeof (Where<PMProject.baseType, NotEqual<CTPRType.projectTemplate>, And<PMProject.baseType, NotEqual<CTPRType.contractTemplate>>>), "{0} is reserved for a project template or contract template. Specify the project or contract instead.", new Type[] {typeof (PMProject.contractCD)})]
[PXRestrictor(typeof (Where<PMProject.visibleInGL, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new Type[] {typeof (PMProject.contractCD)})]
[PXUIEnabled(typeof (Where<FeatureInstalled<FeaturesSet.projectAccounting>>))]
[PXUIVisible(typeof (Where<FeatureInstalled<FeaturesSet.projectAccounting>>))]
public class ActiveProjectAttribute : ProjectBaseAttribute
{
  public Type AccountFieldType { get; set; }

  public ActiveProjectAttribute()
    : base((Type) null)
  {
    this.Filterable = true;
  }

  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject>.Config>.Search<PMProject.contractID>(sender.Graph, e.NewValue, Array.Empty<object>()));
    if (pmProject == null || pmProject.NonProject.GetValueOrDefault() || !(pmProject.BaseType == "P") || !(this.AccountFieldType != (Type) null))
      return;
    Account account = PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account, Where<Account.accountID, Equal<Required<Account.accountID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      sender.GetValue(e.Row, this.AccountFieldType.Name)
    }));
    if (account != null && !account.AccountGroupID.HasValue)
    {
      object copy = sender.CreateCopy(e.Row);
      sender.SetValue(copy, ((PXEventSubscriberAttribute) this)._FieldName, e.NewValue);
      e.NewValue = sender.GetStateExt(copy, ((PXEventSubscriberAttribute) this)._FieldName);
      throw new PXSetPropertyException("Record is associated with Project whereas Account '{0}' is not associated with any Account Group", (PXErrorLevel) 4, new object[1]
      {
        (object) account.AccountCD
      });
    }
  }

  public static void VerifyAccountInInAccountGroup(Account account)
  {
    if (account != null && !account.AccountGroupID.HasValue)
      throw new PXSetPropertyException("Record is associated with Project whereas Account '{0}' is not associated with any Account Group", (PXErrorLevel) 4, new object[1]
      {
        (object) account.AccountCD
      });
  }

  public static void VerifyAccountIsInAccountGroup<T>(
    PXCache cache,
    EventArgs e,
    Account account = null,
    bool throwOnVerifying = false)
    where T : IBqlField
  {
    ActiveProjectAttribute.VerifyAccountIsInAccountGroup(cache, typeof (T).Name, e, account, throwOnVerifying);
  }

  public static void VerifyAccountIsInAccountGroup(
    PXCache cache,
    string fieldName,
    EventArgs e,
    Account account = null,
    bool throwOnVerifying = false)
  {
    if (cache == null || e == null)
      return;
    PXFieldVerifyingEventArgs verifyingEventArgs = e as PXFieldVerifyingEventArgs;
    PXFieldUpdatedEventArgs updatedEventArgs = e as PXFieldUpdatedEventArgs;
    PXRowPersistingEventArgs persistingEventArgs = e as PXRowPersistingEventArgs;
    PXRowSelectedEventArgs selectedEventArgs = e as PXRowSelectedEventArgs;
    object obj1 = verifyingEventArgs?.Row ?? updatedEventArgs?.Row ?? persistingEventArgs?.Row ?? selectedEventArgs?.Row;
    if (account == null)
    {
      object obj2 = verifyingEventArgs != null ? verifyingEventArgs.NewValue : cache.GetValue(obj1, fieldName);
      if (obj2 == null)
        return;
      account = (Account) PXSelectorAttribute.Select(cache, obj1, fieldName, obj2) ?? PXResultset<Account>.op_Implicit(PXSelectBase<Account, PXSelect<Account>.Config>.Search<Account.accountCD>(cache.Graph, obj2, Array.Empty<object>()));
    }
    if (account == null)
      return;
    try
    {
      ActiveProjectAttribute.VerifyAccountInInAccountGroup(account);
    }
    catch (PXSetPropertyException ex)
    {
      cache.RaiseExceptionHandling(fieldName, obj1, (object) account.AccountCD, (Exception) ex);
      if (ex.ErrorLevel >= 4 && verifyingEventArgs != null)
      {
        ((CancelEventArgs) verifyingEventArgs).Cancel = true;
        if (throwOnVerifying)
        {
          if (cache.GetStateExt(obj1, fieldName) is PXFieldState stateExt)
            verifyingEventArgs.NewValue = stateExt.Value;
          throw ex;
        }
      }
      else if (ex.ErrorLevel >= 4 && persistingEventArgs != null)
      {
        ((CancelEventArgs) persistingEventArgs).Cancel = true;
        throw ex;
      }
    }
  }
}
