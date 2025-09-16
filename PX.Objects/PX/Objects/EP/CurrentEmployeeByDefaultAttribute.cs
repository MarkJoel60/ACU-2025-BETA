// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.CurrentEmployeeByDefaultAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.EP;

/// <summary>
/// Allow determine current logined employee on 'field default' event.
/// </summary>
/// <example>[CurrentEmployeeByDefault]</example>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public sealed class CurrentEmployeeByDefaultAttribute : PXDefaultAttribute
{
  public virtual void FieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs e)
  {
    EPEmployee currentEmployee = EmployeeMaint.GetCurrentEmployee(sender.Graph);
    if (currentEmployee == null || !currentEmployee.TimeCardRequired.GetValueOrDefault())
      return;
    e.NewValue = (object) currentEmployee.BAccountID;
  }
}
