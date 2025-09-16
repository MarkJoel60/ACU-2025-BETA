// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSLicenseFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class FSLicenseFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(1, IsFixed = true, IsUnicode = true)]
  [ListField_OwnerType.ListAtrribute]
  [PXDefault("B")]
  [PXUIField(DisplayName = "Owner Type")]
  public virtual 
  #nullable disable
  string OwnerType { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Staff Member Name")]
  [FSSelector_StaffMember_All(null)]
  public virtual int? EmployeeID { get; set; }

  public abstract class ownerType : ListField_OwnerType
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSLicenseFilter.employeeID>
  {
  }
}
