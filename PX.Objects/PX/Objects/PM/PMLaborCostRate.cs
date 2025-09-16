// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.PMLaborCostRate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM.Extensions;
using PX.Objects.CT;
using PX.Objects.EP;
using PX.Objects.IN;
using System;
using System.Diagnostics.CodeAnalysis;

#nullable enable
namespace PX.Objects.PM;

/// <summary>
/// Represents a labor rate.
/// Labor rates are used to determine the cost of employee time spent on a particular project
/// and bill the customers based on this cost.
/// The records of this type are created and edited through the Labor Rates (PM209900) form
/// (which corresponds to the <see cref="T:PX.Objects.PM.LaborCostRateMaint" /> graph).
/// </summary>
[PXCacheName("Labor Cost Rates")]
[ExcludeFromCodeCoverage]
[Serializable]
public class PMLaborCostRate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _InventoryID;
  protected 
  #nullable disable
  string _Description;
  protected Decimal? _Rate;
  protected string _CuryID;
  protected Guid? _NoteID;

  [PXDBIdentity(IsKey = true)]
  public virtual int? RecordID { get; set; }

  [PXDBString(1)]
  [PXDefault]
  [PMLaborCostRateType.List]
  [PXUIField(DisplayName = "Labor Rate Type")]
  public virtual string Type { get; set; }

  [PXForeignReference(typeof (Field<PMLaborCostRate.unionID>.IsRelatedTo<PMUnion.unionID>))]
  [PXRestrictor(typeof (Where<PMUnion.isActive, Equal<True>>), "The {0} union local is inactive.", new System.Type[] {typeof (PMUnion.unionID)})]
  [PXSelector(typeof (Search<PMUnion.unionID>))]
  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "Union Local")]
  public virtual string UnionID { get; set; }

  [Project(typeof (Where<PMProject.baseType, Equal<CTPRType.project>, And<PMProject.nonProject, NotEqual<True>>>), WarnIfCompleted = false)]
  [PXForeignReference(typeof (Field<PMLaborCostRate.projectID>.IsRelatedTo<PMProject.contractID>))]
  public virtual int? ProjectID { get; set; }

  [ProjectTask(typeof (PMLaborCostRate.projectID), AllowNull = true)]
  [PXForeignReference(typeof (CompositeKey<Field<PMLaborCostRate.projectID>.IsRelatedTo<PMTask.projectID>, Field<PMLaborCostRate.taskID>.IsRelatedTo<PMTask.taskID>>))]
  public virtual int? TaskID { get; set; }

  [PXEPEmployeeSelector]
  [PXDBInt]
  [PXUIField(DisplayName = "Employee")]
  [PXForeignReference(typeof (Field<PMLaborCostRate.employeeID>.IsRelatedTo<PX.Objects.CR.BAccount.bAccountID>))]
  public virtual int? EmployeeID { get; set; }

  [PMActiveLaborItem(null, null, null)]
  [PXForeignReference(typeof (Field<PMLaborCostRate.inventoryID>.IsRelatedTo<PX.Objects.IN.InventoryItem.inventoryID>))]
  public virtual int? InventoryID
  {
    get => this._InventoryID;
    set => this._InventoryID = value;
  }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("H")]
  [PXUIField(DisplayName = "Type of Employment")]
  [RateTypes]
  public virtual string EmploymentType { get; set; }

  [PXDBDecimal(1)]
  [PXUIField(DisplayName = "Regular Hours per week")]
  public virtual Decimal? RegularHours { get; set; }

  [PXDBBaseCury]
  [PXUIField(DisplayName = "Annual Rate")]
  public virtual Decimal? AnnualSalary { get; set; }

  [PXDefault]
  [PXDBDate]
  [PXUIField(DisplayName = "Effective Date")]
  public virtual DateTime? EffectiveDate { get; set; }

  [PXString(6, IsUnicode = true)]
  [PXUIField(Visible = false, IsReadOnly = true)]
  public virtual string UOM { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBPriceCost]
  [PXUIField(DisplayName = "Wage Rate", FieldClass = "PayrollModule")]
  public virtual Decimal? WageRate { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBPriceCost]
  [PXUIField(DisplayName = "Burden Rate", Enabled = false, FieldClass = "PayrollModule")]
  [PXFormula(typeof (Sub<PMLaborCostRate.rate, PMLaborCostRate.wageRate>))]
  public virtual Decimal? BurdenRate { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBPriceCost]
  [LaborRate]
  [LaborRateName]
  public virtual Decimal? Rate
  {
    get => this._Rate;
    set => this._Rate = value;
  }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXUIField(DisplayName = "Currency", Enabled = false)]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID))]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField(DisplayName = "External Ref. Nbr")]
  public virtual string ExtRefNbr { get; set; }

  [PXNote]
  public virtual Guid? NoteID
  {
    get => this._NoteID;
    set => this._NoteID = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public abstract class recordID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMLaborCostRate.recordID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMLaborCostRate.type>
  {
  }

  public abstract class unionID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMLaborCostRate.unionID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMLaborCostRate.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMLaborCostRate.taskID>
  {
  }

  public abstract class employeeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMLaborCostRate.employeeID>
  {
  }

  public abstract class inventoryID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMLaborCostRate.inventoryID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMLaborCostRate.description>
  {
  }

  public abstract class employmentType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMLaborCostRate.employmentType>
  {
  }

  public abstract class regularHours : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMLaborCostRate.regularHours>
  {
  }

  public abstract class annualSalary : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    PMLaborCostRate.annualSalary>
  {
  }

  public abstract class effectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMLaborCostRate.effectiveDate>
  {
  }

  public abstract class uOM : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMLaborCostRate.uOM>
  {
  }

  public abstract class wageRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMLaborCostRate.wageRate>
  {
  }

  public abstract class burdenRate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMLaborCostRate.burdenRate>
  {
  }

  public abstract class rate : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  PMLaborCostRate.rate>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMLaborCostRate.curyID>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMLaborCostRate.extRefNbr>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMLaborCostRate.noteID>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  PMLaborCostRate.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  PMLaborCostRate.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMLaborCostRate.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMLaborCostRate.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    PMLaborCostRate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PMLaborCostRate.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PMLaborCostRate.lastModifiedDateTime>
  {
  }
}
