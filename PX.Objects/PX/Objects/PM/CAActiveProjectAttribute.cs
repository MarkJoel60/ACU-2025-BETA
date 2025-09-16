// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.CAActiveProjectAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CA;
using System;

#nullable disable
namespace PX.Objects.PM;

[PXRestrictor(typeof (Where<PMProject.status, NotEqual<ProjectStatus.closed>>), "The {0} project is closed.", new Type[] {typeof (PMProject.contractCD)})]
[PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new Type[] {typeof (PMProject.contractCD)})]
[PXRestrictor(typeof (Where<PMProject.visibleInCA, Equal<True>, Or<PMProject.nonProject, Equal<True>>>), "The '{0}' project is invisible in the module.", new Type[] {typeof (PMProject.contractCD)})]
public class CAActiveProjectAttribute : ProjectBaseAttribute
{
  public Type AccountFieldType { get; set; }

  public CAActiveProjectAttribute()
    : base((Type) null)
  {
    this.AccountFieldType = typeof (CASplit.accountID);
    this.Filterable = true;
  }

  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    PMProject pmProject = PXResultset<PMProject>.op_Implicit(PXSelectBase<PMProject, PXSelect<PMProject>.Config>.Search<PMProject.contractID>(sender.Graph, e.NewValue, Array.Empty<object>()));
    if (pmProject == null || pmProject.NonProject.GetValueOrDefault() || !(pmProject.BaseType == "P") || !(this.AccountFieldType != (Type) null))
      return;
    PX.Objects.GL.Account account = PXResultset<PX.Objects.GL.Account>.op_Implicit(PXSelectBase<PX.Objects.GL.Account, PXSelect<PX.Objects.GL.Account, Where<PX.Objects.GL.Account.accountID, Equal<Required<PX.Objects.GL.Account.accountID>>>>.Config>.Select(sender.Graph, new object[1]
    {
      sender.GetValue(e.Row, this.AccountFieldType.Name)
    }));
    if (account != null && !account.AccountGroupID.HasValue)
      throw new PXSetPropertyException("Record is associated with Project whereas Account '{0}' is not associated with any Account Group", (PXErrorLevel) 4, new object[1]
      {
        (object) account.AccountCD
      })
      {
        ErrorValue = (object) pmProject.ContractCD
      };
  }
}
