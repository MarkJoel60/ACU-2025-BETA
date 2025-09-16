// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.EPEmployeePrimaryGraphAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.AP;
using PX.Objects.EP;
using System;

#nullable disable
namespace PX.Objects.CR;

public sealed class EPEmployeePrimaryGraphAttribute : CRCacheIndependentPrimaryGraphListAttribute
{
  public EPEmployeePrimaryGraphAttribute()
    : base(new System.Type[2]
    {
      typeof (EmployeeMaint),
      typeof (EmployeeMaint)
    }, new System.Type[2]
    {
      typeof (Select<EPEmployee, Where<Vendor.type, Equal<BAccountType.employeeType>, And<EPEmployee.bAccountID, Equal<Current<EPEmployee.bAccountID>>>>>),
      typeof (Select<EPEmployee>)
    })
  {
  }

  protected override void OnAccessDenied(System.Type graphType)
  {
    throw new AccessViolationException(Messages.FormNoAccessRightsMessage(graphType));
  }
}
