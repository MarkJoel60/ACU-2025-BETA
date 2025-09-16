// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.EPShiftCodeRate
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;
using System.Diagnostics;

#nullable enable
namespace PX.Objects.EP;

/// <summary>
/// Stores information on the rate associated with different shifts. The information will be displayed on the Shift Codes (EP103000) form.
/// </summary>
[PXCacheName("Shift Code Rate")]
[DebuggerDisplay("{GetType().Name,nq}: ShiftID = {ShiftID,nq}, EffectiveDate = {EffectiveDate,nq}, CuryID = {CuryID,nq}")]
[Serializable]
public class EPShiftCodeRate : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  [PXDBDefault(typeof (EPShiftCode.shiftID))]
  [PXParent(typeof (EPShiftCodeRate.FK.ShiftCode))]
  public virtual int? ShiftID { get; set; }

  [PXDBDate(IsKey = true)]
  [PXUIField(DisplayName = "Effective Date")]
  [PXDefault(typeof (AccessInfo.businessDate))]
  public virtual DateTime? EffectiveDate { get; set; }

  [PXDBString(10, IsKey = true)]
  [PXUIField]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  public virtual 
  #nullable disable
  string CuryID { get; set; }

  [PXDBString]
  [PXUIField(DisplayName = "Type")]
  [EPShiftCodeType.List]
  [PXDefault("AMT")]
  public virtual string Type { get; set; }

  [PXDBDecimal(2, MinValue = 0.0)]
  [PXUIField(DisplayName = "Percent")]
  [ShowValueWhen(typeof (Where<BqlOperand<EPShiftCodeRate.type, IBqlString>.IsEqual<EPShiftCodeType.percent>>), false)]
  public virtual Decimal? Percent { get; set; }

  [PXDBDecimal(2, MinValue = 0.0)]
  [PXUIField(DisplayName = "Wage Amount", FieldClass = "PayrollModule")]
  [ShowValueWhen(typeof (Where<BqlOperand<EPShiftCodeRate.type, IBqlString>.IsEqual<EPShiftCodeType.amount>>), false)]
  public virtual Decimal? WageAmount { get; set; }

  [PXDBDecimal(2, MinValue = 0.0)]
  [ShiftAmountName]
  [ShowValueWhen(typeof (Where<BqlOperand<EPShiftCodeRate.type, IBqlString>.IsEqual<EPShiftCodeType.amount>>), false)]
  public virtual Decimal? CostingAmount { get; set; }

  [PXDecimal(2)]
  [PXUIField(DisplayName = "Burden Amount", Enabled = false, FieldClass = "PayrollModule")]
  [ShowValueWhen(typeof (Where<BqlOperand<EPShiftCodeRate.type, IBqlString>.IsEqual<EPShiftCodeType.amount>>), false)]
  [PXDependsOnFields(new System.Type[] {typeof (EPShiftCodeRate.costingAmount), typeof (EPShiftCodeRate.wageAmount)})]
  public virtual Decimal? BurdenAmount
  {
    get
    {
      Decimal? costingAmount = this.CostingAmount;
      Decimal? wageAmount = this.WageAmount;
      return !(costingAmount.HasValue & wageAmount.HasValue) ? new Decimal?() : new Decimal?(costingAmount.GetValueOrDefault() - wageAmount.GetValueOrDefault());
    }
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  public class PK : 
    PrimaryKeyOf<EPShiftCodeRate>.By<EPShiftCodeRate.shiftID, EPShiftCodeRate.effectiveDate, EPShiftCodeRate.curyID>
  {
    public static EPShiftCodeRate Find(
      PXGraph graph,
      int? shiftID,
      DateTime? effectiveDate,
      string curyID,
      PKFindOptions options = 0)
    {
      return (EPShiftCodeRate) PrimaryKeyOf<EPShiftCodeRate>.By<EPShiftCodeRate.shiftID, EPShiftCodeRate.effectiveDate, EPShiftCodeRate.curyID>.FindBy(graph, (object) shiftID, (object) effectiveDate, (object) curyID, options);
    }
  }

  public static class FK
  {
    public class ShiftCode : 
      PrimaryKeyOf<EPShiftCode>.By<EPShiftCode.shiftID>.ForeignKeyOf<EPShiftCodeRate>.By<EPShiftCodeRate.shiftID>
    {
    }
  }

  public abstract class shiftID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  EPShiftCodeRate.shiftID>
  {
  }

  public abstract class effectiveDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPShiftCodeRate.effectiveDate>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPShiftCodeRate.curyID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  EPShiftCodeRate.type>
  {
  }

  public abstract class percent : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPShiftCodeRate.percent>
  {
  }

  public abstract class wageAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  EPShiftCodeRate.wageAmount>
  {
  }

  public abstract class costingAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPShiftCodeRate.costingAmount>
  {
  }

  public abstract class burdenAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    EPShiftCodeRate.burdenAmount>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  EPShiftCodeRate.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPShiftCodeRate.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPShiftCodeRate.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    EPShiftCodeRate.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    EPShiftCodeRate.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    EPShiftCodeRate.lastModifiedDateTime>
  {
  }
}
