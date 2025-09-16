// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.FeatureDependencyAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.CS;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class FeatureDependencyAttribute : PXEventSubscriberAttribute, IPXFieldVerifyingSubscriber
{
  protected Type[] Depencency;
  protected bool AllDependenciesRequired;

  public FeatureDependencyAttribute(params Type[] dependency)
  {
    this.Depencency = dependency;
    this.AllDependenciesRequired = false;
  }

  public FeatureDependencyAttribute(bool allDependenciesRequired, params Type[] dependency)
  {
    this.Depencency = dependency;
    this.AllDependenciesRequired = allDependenciesRequired;
  }

  public virtual void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    foreach (Type type in this.Depencency)
    {
      PXGraph.FieldUpdatedEvents fieldUpdated = sender.Graph.FieldUpdated;
      Type declaringType = type.DeclaringType;
      string name = type.Name;
      FeatureDependencyAttribute dependencyAttribute = this;
      // ISSUE: virtual method pointer
      PXFieldUpdated pxFieldUpdated = new PXFieldUpdated((object) dependencyAttribute, __vmethodptr(dependencyAttribute, FieldUpdated));
      fieldUpdated.AddHandler(declaringType, name, pxFieldUpdated);
    }
  }

  protected virtual void FieldUpdated(PXCache sender, PXFieldUpdatedEventArgs e)
  {
    bool allUnchecked = true;
    bool allChecked = true;
    foreach (Type type in this.Depencency)
    {
      object obj = sender.GetValue(e.Row, type.Name);
      bool? nullable = (bool?) obj;
      if (nullable.GetValueOrDefault() && allUnchecked)
      {
        allUnchecked = false;
      }
      else
      {
        nullable = (bool?) obj;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue && allChecked)
          allChecked = false;
      }
    }
    this.SetFieldValue(sender, e, allUnchecked, allChecked);
  }

  protected virtual void SetFieldValue(
    PXCache sender,
    PXFieldUpdatedEventArgs e,
    bool allUnchecked,
    bool allChecked)
  {
    if (!allUnchecked && (!this.AllDependenciesRequired || allChecked))
      return;
    sender.SetValueExt(e.Row, this._FieldName, (object) false);
  }

  public void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (!((bool?) e.NewValue).GetValueOrDefault() || this.Depencency == null || e.Row == null)
      return;
    bool allUnchecked = true;
    bool allChecked = true;
    string dependencyNames = (string) null;
    foreach (Type field in this.Depencency)
    {
      PXFieldState fieldState = this.GetFieldState(sender, e, field);
      if (fieldState != null)
      {
        bool? stateValue = (bool?) fieldState.Value;
        if (!stateValue.HasValue)
          return;
        if (stateValue.GetValueOrDefault())
          allUnchecked = false;
        else
          allChecked = false;
        dependencyNames = this.GetDependencyNames(dependencyNames, fieldState, stateValue);
      }
    }
    this.ShowDependencyError(allUnchecked, allChecked, dependencyNames);
  }

  protected virtual PXFieldState GetFieldState(
    PXCache sender,
    PXFieldVerifyingEventArgs e,
    Type field)
  {
    return sender.GetStateExt(e.Row, field.Name) as PXFieldState;
  }

  protected virtual string GetDependencyNames(
    string dependencyNames,
    PXFieldState state,
    bool? stateValue)
  {
    if (this.AllDependenciesRequired)
    {
      bool? nullable = stateValue;
      bool flag = false;
      if (!(nullable.GetValueOrDefault() == flag & nullable.HasValue))
        goto label_3;
    }
    dependencyNames = dependencyNames == null ? state.DisplayName : $"{dependencyNames}, {state.DisplayName}";
label_3:
    return dependencyNames;
  }

  protected virtual void ShowDependencyError(
    bool allUnchecked,
    bool allChecked,
    string dependencyNames)
  {
    if (!this.AllDependenciesRequired & allUnchecked)
      throw new PXSetPropertyException(this.Depencency.Length > 1 ? "To enable this feature, enable one of the following features first: {0}." : "To enable this feature, enable {0} first.", new object[1]
      {
        (object) dependencyNames
      });
    if (this.AllDependenciesRequired && !allChecked)
      throw new PXSetPropertyException("To enable this feature, enable the following features first: {0}.", new object[1]
      {
        (object) dependencyNames
      });
  }
}
