// Decompiled with JetBrains decompiler
// Type: PX.Objects.PR.Standalone.PREmployee
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.EP;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.PR.Standalone;

/// <summary>
/// Standalone DAC related to PR.Objects.PR.PREmployee /&gt;
/// </summary>
[PXCacheName("Payroll Employee")]
[Serializable]
public class PREmployee : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>Key field used to retrieve an Employee</summary>
  [PXDBInt(IsKey = true)]
  [PXDefault(typeof (EPEmployee.bAccountID))]
  [PXParent(typeof (Select<EPEmployee, Where<EPEmployee.bAccountID, Equal<Current<PREmployee.bAccountID>>>>))]
  public int? BAccountID { get; set; }

  /// <summary>
  /// Indicates whether the employee is active in the payroll module
  /// </summary>
  [PXDBBool]
  public bool? ActiveInPayroll { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXSelector(typeof (PREmployeeClass.employeeClassID))]
  public 
  #nullable disable
  string EmployeeClassID { get; set; }

  [PXDBBool]
  public bool? WorkCodeUseDflt { get; set; }

  [PXDBString(15)]
  [PXUnboundDefault(typeof (Switch<Case<Where<BqlOperand<PREmployee.workCodeUseDflt, IBqlBool>.IsEqual<True>>, Selector<PREmployee.employeeClassID, PREmployeeClass.workCodeID>>, PREmployee.workCodeID>))]
  public string WorkCodeID { get; set; }

  public class PK : PrimaryKeyOf<PREmployee>.By<PREmployee.bAccountID>
  {
    public static PREmployee Find(PXGraph graph, int? bAccountID, PKFindOptions options = 0)
    {
      return (PREmployee) PrimaryKeyOf<PREmployee>.By<PREmployee.bAccountID>.FindBy(graph, (object) bAccountID, options);
    }
  }

  public static class FK
  {
    public class EmployeeClass : 
      PrimaryKeyOf<PREmployeeClass>.By<PREmployeeClass.employeeClassID>.ForeignKeyOf<PREmployee>.By<PREmployee.employeeClassID>
    {
    }

    public class WorkCode : 
      PrimaryKeyOf<PMWorkCode>.By<PMWorkCode.workCodeID>.ForeignKeyOf<PREmployee>.By<PREmployee.workCodeID>
    {
    }
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PREmployee.bAccountID>
  {
  }

  public abstract class activeInPayroll : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PREmployee.activeInPayroll>
  {
  }

  public abstract class employeeClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PREmployee.employeeClassID>
  {
  }

  public abstract class workCodeUseDflt : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PREmployee.workCodeUseDflt>
  {
  }

  public abstract class workCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PREmployee.workCodeID>
  {
  }
}
