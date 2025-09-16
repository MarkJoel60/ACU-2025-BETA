// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSSelector_Driver_AllAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.EP;
using System;

#nullable disable
namespace PX.Objects.FS;

public class FSSelector_Driver_AllAttribute : PXDimensionSelectorAttribute
{
  public FSSelector_Driver_AllAttribute()
    : base("BIZACCT", typeof (Search<EPEmployee.bAccountID, Where<FSxEPEmployee.sDEnabled, Equal<True>, And<EPEmployee.vStatus, NotEqual<VendorStatus.inactive>, And<FSxEPEmployee.isDriver, Equal<True>>>>, OrderBy<Asc<EPEmployee.acctCD>>>), typeof (EPEmployee.acctCD), new Type[4]
    {
      typeof (EPEmployee.acctCD),
      typeof (EPEmployee.acctName),
      typeof (EPEmployee.vStatus),
      typeof (EPEmployee.departmentID)
    })
  {
    this.DescriptionField = typeof (EPEmployee.acctName);
  }
}
