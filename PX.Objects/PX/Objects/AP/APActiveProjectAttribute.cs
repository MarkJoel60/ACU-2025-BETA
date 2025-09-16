// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APActiveProjectAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.AP;

[PXRestrictor(typeof (Where<PMProject.status, NotEqual<ProjectStatus.closed>>), "The {0} project is closed.", new System.Type[] {typeof (PMProject.contractCD)})]
[PXRestrictor(typeof (Where<PMProject.isActive, Equal<True>>), "The {0} project or contract is inactive.", new System.Type[] {typeof (PMProject.contractCD)})]
[PXRestrictor(typeof (Where<PMProject.visibleInAP, Equal<True>>), "The '{0}' project is invisible in the module.", new System.Type[] {typeof (PMProject.contractCD)})]
public class APActiveProjectAttribute : ProjectBaseAttribute
{
  public System.Type AccountFieldType { get; set; }

  public APActiveProjectAttribute()
    : base((System.Type) null)
  {
    this.AccountFieldType = typeof (APTran.accountID);
    this.Filterable = true;
  }

  public override void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (e.Row is APTran row && (!string.IsNullOrEmpty(row.PONbr) || !string.IsNullOrEmpty(row.ReceiptNbr)))
      return;
    PMProject pmProject = (PMProject) PXSelectBase<PMProject, PXSelect<PMProject>.Config>.Search<PMProject.contractID>(sender.Graph, e.NewValue);
    APInvoice current = (APInvoice) sender.Graph.Caches[typeof (APInvoice)]?.Current;
    if (pmProject != null)
    {
      bool? nullable = pmProject.NonProject;
      if (!nullable.GetValueOrDefault() && pmProject.BaseType == "P" && this.AccountFieldType != (System.Type) null)
      {
        int num;
        if (current == null)
        {
          num = 1;
        }
        else
        {
          nullable = current.IsRetainageDocument;
          num = !nullable.GetValueOrDefault() ? 1 : 0;
        }
        if (num != 0)
        {
          PX.Objects.GL.Account account = PX.Objects.GL.Account.PK.Find(sender.Graph, sender.GetValue(e.Row, this.AccountFieldType.Name) as int?);
          if (account != null && !account.AccountGroupID.HasValue)
          {
            object copy = sender.CreateCopy(e.Row);
            sender.SetValue(copy, this._FieldName, e.NewValue);
            e.NewValue = sender.GetStateExt(copy, this._FieldName);
            throw new PXSetPropertyException("The line is associated with the {0} project but the {1} account specified in the document line is not mapped to any account group. Select the non-project code or change the account in the line.", PXErrorLevel.Error, new object[2]
            {
              (object) pmProject.ContractCD,
              (object) account.AccountCD
            });
          }
        }
      }
    }
    base.FieldVerifying(sender, e);
  }
}
