// Decompiled with JetBrains decompiler
// Type: PX.TM.OwnerEmployeeAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.TM;

/// <summary>
/// Base class for attributes showing employees related to the current logged in employee.
/// </summary>
public abstract class OwnerEmployeeAttribute(
  Type workgroupType,
  Type search,
  bool validateValue = true,
  bool inquiryMode = false,
  Type[] fieldList = null,
  string[] headerList = null,
  PXSelectorMode selectorMode = 0) : OwnerAttribute(workgroupType, search, validateValue, inquiryMode, fieldList, headerList, selectorMode: selectorMode)
{
  protected override PXSelectorAttribute CreateSelector(
    Type search,
    Type[] fieldList,
    string[] headerList,
    bool validateValue,
    PXSelectorMode selectorMode)
  {
    Type type = search;
    Type[] typeArray = fieldList;
    if (typeArray == null)
      typeArray = new Type[3]
      {
        typeof (PX.Objects.EP.EPEmployee.acctName),
        typeof (PX.Objects.EP.EPEmployee.acctCD),
        typeof (PX.Objects.EP.EPEmployee.departmentID)
      };
    PXSelectorAttribute selector = new PXSelectorAttribute(type, typeArray);
    selector.DescriptionField = typeof (PX.Objects.EP.EPEmployee.acctName);
    selector.SubstituteKey = typeof (PX.Objects.EP.EPEmployee.acctCD);
    selector.ValidateValue = validateValue;
    if (selectorMode != null)
      selector.SelectorMode = selectorMode;
    if (headerList != null)
      selector.Headers = headerList;
    return selector;
  }
}
