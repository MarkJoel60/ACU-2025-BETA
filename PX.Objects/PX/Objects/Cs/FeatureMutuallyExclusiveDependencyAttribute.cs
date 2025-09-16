// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.FeatureMutuallyExclusiveDependencyAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class FeatureMutuallyExclusiveDependencyAttribute : FeatureDependencyAttribute
{
  public FeatureMutuallyExclusiveDependencyAttribute(params Type[] dependency)
    : base()
  {
    this.Depencency = dependency;
    this.AllDependenciesRequired = false;
  }

  public FeatureMutuallyExclusiveDependencyAttribute(
    bool allDependenciesRequired,
    params Type[] dependency)
    : base()
  {
    this.Depencency = dependency;
    this.AllDependenciesRequired = allDependenciesRequired;
  }

  protected override PXFieldState GetFieldState(
    PXCache sender,
    PXFieldVerifyingEventArgs e,
    Type field)
  {
    PXFieldState fieldState = base.GetFieldState(sender, e, field);
    if (sender.GetValuePending(e.Row, field.Name) is bool valuePending)
      fieldState.Value = (object) valuePending;
    return fieldState;
  }

  protected override void SetFieldValue(
    PXCache sender,
    PXFieldUpdatedEventArgs e,
    bool allUnchecked,
    bool allChecked)
  {
    if (!allChecked && (!this.AllDependenciesRequired || allUnchecked))
      return;
    sender.SetValueExt(e.Row, this._FieldName, (object) false);
  }

  protected override string GetDependencyNames(
    string dependencyNames,
    PXFieldState state,
    bool? stateValue)
  {
    if (!this.AllDependenciesRequired || stateValue.GetValueOrDefault())
      dependencyNames = dependencyNames == null ? state.DisplayName : $"{dependencyNames}, {state.DisplayName}";
    return dependencyNames;
  }

  protected override void ShowDependencyError(
    bool allUnchecked,
    bool allChecked,
    string dependencyNames)
  {
    if (!this.AllDependenciesRequired & allChecked)
      throw new PXSetPropertyException(this.Depencency.Length > 1 ? "To enable this feature, disable one of the following features first: {0}." : "To enable this feature, disable {0} first.", new object[1]
      {
        (object) dependencyNames
      });
    if (this.AllDependenciesRequired && !allUnchecked)
      throw new PXSetPropertyException("To enable this feature, disable the following features first: {0}.", new object[1]
      {
        (object) dependencyNames
      });
  }
}
