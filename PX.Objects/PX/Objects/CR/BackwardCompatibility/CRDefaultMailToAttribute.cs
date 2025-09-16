// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BackwardCompatibility.CRDefaultMailToAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CR.BackwardCompatibility;

/// <exclude />
[Obsolete]
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class CRDefaultMailToAttribute : CRActivityListBaseAttribute
{
  private readonly bool _takeCurrent;

  public CRDefaultMailToAttribute() => this._takeCurrent = true;

  public CRDefaultMailToAttribute(System.Type select)
    : base(select)
  {
  }

  protected override void AttachHandlers(PXGraph graph)
  {
    // ISSUE: method pointer
    graph.RowInserting.AddHandler<CRSMEmail>(new PXRowInserting((object) this, __methodptr(RowInserting)));
  }

  private void RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (!(e.Row is CRSMEmail row))
      return;
    string str1 = (string) null;
    IEmailMessageTarget emailMessageTarget = !this._takeCurrent ? this.SelectRecord() as IEmailMessageTarget : GraphHelper.GetPrimaryCache(sender.Graph).Current as IEmailMessageTarget;
    if (emailMessageTarget != null)
    {
      string str2 = emailMessageTarget.DisplayName.With<string, string>((Func<string, string>) (_ => _.Trim()));
      string str3 = emailMessageTarget.Address.With<string, string>((Func<string, string>) (_ => _.Trim()));
      if (!string.IsNullOrEmpty(str3))
        str1 = PXDBEmailAttribute.FormatAddressesWithSingleDisplayName(str3, str2);
    }
    row.MailTo = str1;
  }
}
