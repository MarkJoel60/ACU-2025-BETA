// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDefaultValidateAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXDefaultValidateAttribute : PXDefaultAttribute
{
  private readonly BqlCommand validateExists;

  public PXDefaultValidateAttribute(System.Type sourceType, System.Type validateExists)
    : base(sourceType)
  {
    this.validateExists = BqlCommand.CreateInstance(validateExists);
  }

  public override void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    base.FieldDefaulting(sender, e);
    if (e.NewValue == null)
      return;
    PXView view = sender.Graph.TypedViews.GetView(this.validateExists, false);
    int num1 = -1;
    int num2 = 0;
    object[] currents = new object[1]{ e.Row };
    object[] parameters = new object[1]{ e.NewValue };
    ref int local1 = ref num1;
    ref int local2 = ref num2;
    List<object> objectList = view.Select(currents, parameters, (object[]) null, (string[]) null, (bool[]) null, (PXFilterRow[]) null, ref local1, 1, ref local2);
    if (objectList == null || objectList.Count <= 0)
      return;
    e.NewValue = (object) null;
    e.Cancel = true;
  }
}
