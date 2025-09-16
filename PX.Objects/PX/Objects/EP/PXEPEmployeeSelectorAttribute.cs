// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.PXEPEmployeeSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.EP;

/// <summary>Dimension selector for EPEmployee.</summary>
/// <example>[PXEPEmployeeSelector]</example>
public class PXEPEmployeeSelectorAttribute : PXDimensionSelectorAttribute
{
  public PXEPEmployeeSelectorAttribute()
    : base("EMPLOYEE", typeof (Search<PX.Objects.CR.Standalone.EPEmployee.bAccountID>), typeof (PX.Objects.CR.Standalone.EPEmployee.acctCD), new Type[4]
    {
      typeof (PX.Objects.CR.Standalone.EPEmployee.bAccountID),
      typeof (PX.Objects.CR.Standalone.EPEmployee.acctCD),
      typeof (PX.Objects.CR.Standalone.EPEmployee.acctName),
      typeof (PX.Objects.CR.Standalone.EPEmployee.departmentID)
    })
  {
    this.DescriptionField = typeof (PX.Objects.CR.Standalone.EPEmployee.acctName);
  }
}
