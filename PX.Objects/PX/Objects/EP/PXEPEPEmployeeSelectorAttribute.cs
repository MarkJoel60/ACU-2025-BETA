// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.PXEPEPEmployeeSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.EP;

public class PXEPEPEmployeeSelectorAttribute : PXDimensionSelectorAttribute
{
  public PXEPEPEmployeeSelectorAttribute()
    : base("EMPLOYEE", typeof (Search<EPEmployee.bAccountID>), typeof (EPEmployee.acctCD), new Type[4]
    {
      typeof (EPEmployee.bAccountID),
      typeof (EPEmployee.acctCD),
      typeof (EPEmployee.acctName),
      typeof (EPEmployee.departmentID)
    })
  {
    this.DescriptionField = typeof (EPEmployee.acctName);
  }
}
