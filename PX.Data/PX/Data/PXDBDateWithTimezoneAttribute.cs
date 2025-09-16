// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBDateWithTimezoneAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;

#nullable disable
namespace PX.Data;

/// <exclude />
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXDBDateWithTimezoneAttribute : PXDBDateAttribute
{
  private object _timeZone;
  private System.Type _timeZoneType;

  public PXDBDateWithTimezoneAttribute(System.Type timeZoneType)
  {
    this._timeZoneType = timeZoneType;
    this.UseTimeZone = true;
  }

  public override void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    this._timeZone = sender.GetValue(e.Row, this._timeZoneType.Name);
    base.CommandPreparing(sender, e);
  }

  public override void RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    this._timeZone = sender.GetValue(e.Row, this._timeZoneType.Name);
    base.RowSelecting(sender, e);
  }

  protected override PXTimeZoneInfo GetTimeZone() => this.GetTimeZoneInt();

  internal PXTimeZoneInfo GetTimeZoneInt()
  {
    return PXTimeZoneInfo.FindSystemTimeZoneById((string) this._timeZone);
  }
}
