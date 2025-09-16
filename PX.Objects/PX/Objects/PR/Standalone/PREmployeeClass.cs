// Decompiled with JetBrains decompiler
// Type: PX.Objects.PR.Standalone.PREmployeeClass
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.PM;
using System;

#nullable enable
namespace PX.Objects.PR.Standalone;

/// <summary>
/// Standalone DAC related to PR.Objects.PR.PREmployeeClass /&gt;
/// </summary>
[PXCacheName("Payroll Employee Class")]
[Serializable]
public class PREmployeeClass : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  public 
  #nullable disable
  string EmployeeClassID { get; set; }

  [PXDBString(15)]
  public string WorkCodeID { get; set; }

  public class PK : PrimaryKeyOf<PREmployeeClass>.By<PREmployeeClass.employeeClassID>
  {
    public static PREmployeeClass Find(
      PXGraph graph,
      string employeeClassID,
      PKFindOptions options = 0)
    {
      return (PREmployeeClass) PrimaryKeyOf<PREmployeeClass>.By<PREmployeeClass.employeeClassID>.FindBy(graph, (object) employeeClassID, options);
    }
  }

  public static class FK
  {
    public class WorkCode : 
      PrimaryKeyOf<PMWorkCode>.By<PMWorkCode.workCodeID>.ForeignKeyOf<PREmployeeClass>.By<PREmployeeClass.workCodeID>
    {
    }
  }

  public abstract class employeeClassID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PREmployeeClass.employeeClassID>
  {
  }

  public abstract class workCodeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PREmployeeClass.workCodeID>
  {
  }
}
