// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPActivityProjectDefaultAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.PM;
using System;

#nullable disable
namespace PX.Objects.EP;

public class EPActivityProjectDefaultAttribute : ProjectDefaultAttribute
{
  private readonly Type isBillableField;

  public EPActivityProjectDefaultAttribute(Type _isBillableField = null)
    : base("TA", typeof (Search<PMProject.contractID, Where<PMProject.nonProject, Equal<True>>>))
  {
    if (!(_isBillableField != (Type) null))
      return;
    this.isBillableField = _isBillableField;
  }

  public override void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    bool? nullable = new bool?();
    if (this.isBillableField != (Type) null)
      nullable = (bool?) sender.GetValue(e.Row, this.isBillableField.Name);
    if (e.Row != null && nullable.GetValueOrDefault() && ProjectAttribute.IsPMVisible("TA"))
      return;
    base.FieldDefaulting(sender, e);
  }
}
