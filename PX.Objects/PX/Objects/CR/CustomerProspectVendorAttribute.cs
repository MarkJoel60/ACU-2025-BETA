// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CustomerProspectVendorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CR;

[PXAttributeFamily(typeof (PXEntityAttribute))]
public class CustomerProspectVendorAttribute(
  System.Type customSearchQuery = null,
  System.Type[] fieldList = null,
  string[] headerList = null) : BAccountAttribute(new System.Type[4]
{
  typeof (BAccountType.prospectType),
  typeof (BAccountType.customerType),
  typeof (BAccountType.combinedType),
  typeof (BAccountType.vendorType)
}, customSearchQuery, fieldList, headerList)
{
}
