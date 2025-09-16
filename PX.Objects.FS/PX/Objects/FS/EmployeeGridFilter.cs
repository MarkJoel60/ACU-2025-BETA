// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.EmployeeGridFilter
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
public class EmployeeGridFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXInt(IsKey = true)]
  [FSSelector_StaffMember_All(null)]
  [PXUIField(DisplayName = "Staff Member ID", Enabled = false)]
  public virtual int? EmployeeID { get; set; }

  [PXBool]
  [PXUIField(DisplayName = "Selected")]
  public virtual bool? Mem_Selected { get; set; }

  public virtual int GetHashCode() => this.EmployeeID.HasValue ? this.EmployeeID.Value : -1;

  public virtual bool Equals(
  #nullable disable
  object obj) => this.Equals(obj as EmployeeGridFilter);

  public bool Equals(EmployeeGridFilter p)
  {
    int? employeeId1 = this.EmployeeID;
    int? employeeId2 = p.EmployeeID;
    return employeeId1.GetValueOrDefault() == employeeId2.GetValueOrDefault() & employeeId1.HasValue == employeeId2.HasValue;
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EmployeeGridFilter.employeeID>
  {
  }

  public abstract class mem_Selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  EmployeeGridFilter.mem_Selected>
  {
  }
}
