// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.SOFreightDetailTask
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PM;

/// <summary>
/// Task Selector that displays all active Tasks for the given Project. Task Field is Disabled if a Non-Project is selected; otherwise mandatory.
/// Task Selector always work in pair with Project Selector. When the Project Selector displays a valid Project Task field becomes mandatory.
/// If Completed Task is selected an error will be displayed - Completed Task cannot be used in DataEntry.
/// 
/// Task is mandatory only if the Freight amount is greater then zero.
/// </summary>
public class SOFreightDetailTask : ActiveProjectTaskAttribute
{
  protected Type curyTotalFreightAmtField;

  public SOFreightDetailTask(Type projectID, Type curyTotalFreightAmt)
    : base(projectID, "SO")
  {
    this.curyTotalFreightAmtField = curyTotalFreightAmt;
  }

  public override void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if (e.Operation == 3)
      return;
    Decimal? nullable = (Decimal?) sender.GetValue(e.Row, this.curyTotalFreightAmtField.Name);
    Decimal num = 0M;
    if (!(nullable.GetValueOrDefault() > num & nullable.HasValue))
      return;
    base.RowPersisting(sender, e);
  }
}
