// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.MigrationMode.MigratedRecordAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections;
using System.Reflection;

#nullable disable
namespace PX.Objects.Common.MigrationMode;

/// <summary>
/// Attribute that sets <c>true</c> on the underlying field defaulting
/// if migration mode is activated in the specified setup field, and
/// <c>false</c> otherwise.
/// </summary>
[PXDBBool]
[PXDefault]
public class MigratedRecordAttribute : PXAggregateAttribute, IPXFieldDefaultingSubscriber
{
  public virtual Type MigrationModeSetupField { get; private set; }

  public MigratedRecordAttribute(Type migrationModeSetupField)
  {
    this.MigrationModeSetupField = migrationModeSetupField;
  }

  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    IList list = (IList) BqlCommand.Compose(new Type[2]
    {
      typeof (PXSetup<>),
      this.MigrationModeSetupField.DeclaringType
    }).GetMethod("Select", BindingFlags.Static | BindingFlags.Public).Invoke((object) null, new object[2]
    {
      (object) sender.Graph,
      (object) new object[0]
    });
    bool? nullable = new bool?(list != null && list.Count > 0 && (sender.Graph.Caches[this.MigrationModeSetupField.DeclaringType].GetValue((object) PXResult.Unwrap(list[0], this.MigrationModeSetupField.DeclaringType), this.MigrationModeSetupField.Name) as bool?).GetValueOrDefault());
    e.NewValue = (object) nullable;
  }
}
