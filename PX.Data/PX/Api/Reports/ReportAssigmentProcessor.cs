// Decompiled with JetBrains decompiler
// Type: PX.Api.Reports.ReportAssigmentProcessor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;
using PX.Reports.Controls;

#nullable disable
namespace PX.Api.Reports;

internal class ReportAssigmentProcessor : PropertyAssigmentProcessor
{
  private readonly Report _target;
  private readonly string _commonSettings = "CommonSettings";
  private readonly string _mailSettings = "MailSettings";

  public ReportAssigmentProcessor(Report target)
    : base(typeof (Report))
  {
    this._target = target;
  }

  protected override object GetPropertyOwner(Command cmd)
  {
    return this.TargetType.GetProperty(cmd.ObjectName).GetValue((object) this._target, (object[]) null);
  }

  public override bool CanExecute(Command cmd)
  {
    return (string.Equals(cmd.ObjectName, this._commonSettings) || string.Equals(cmd.ObjectName, this._mailSettings)) && base.CanExecute(cmd);
  }
}
