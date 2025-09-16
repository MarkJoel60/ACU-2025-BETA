// Decompiled with JetBrains decompiler
// Type: ReconciliationTools.DiscrepancyEnqFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.Common.Attributes;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using System;

#nullable enable
namespace ReconciliationTools;

[Serializable]
public class DiscrepancyEnqFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Organization(false, null, Required = false)]
  public int? OrganizationID { get; set; }

  [BranchOfOrganization(typeof (DiscrepancyEnqFilter.organizationID), false, null, null)]
  public virtual int? BranchID { get; set; }

  [PXBool]
  public bool? UseMasterCalendar { get; set; }

  [AnyPeriodFilterable(null, null, typeof (DiscrepancyEnqFilter.branchID), null, typeof (DiscrepancyEnqFilter.organizationID), typeof (DiscrepancyEnqFilter.useMasterCalendar), null, false, null, null)]
  [PXDefault]
  [PXUIField(DisplayName = "From Period")]
  public virtual 
  #nullable disable
  string PeriodFrom { get; set; }

  [AnyPeriodFilterable(null, null, typeof (DiscrepancyEnqFilter.branchID), null, typeof (DiscrepancyEnqFilter.organizationID), typeof (DiscrepancyEnqFilter.useMasterCalendar), null, false, null, null)]
  [PXDefault]
  [PXUIField(DisplayName = "To Period")]
  public virtual string PeriodTo { get; set; }

  [Account(null, DisplayName = "Account", DescriptionField = typeof (PX.Objects.GL.Account.description))]
  public virtual int? AccountID { get; set; }

  [PXDBString(30, IsUnicode = true)]
  [PXUIField(DisplayName = "Subaccount", Visibility = PXUIVisibility.Invisible, FieldClass = "SUBACCOUNT")]
  [PXDimension("SUBACCOUNT", ValidComboRequired = false)]
  public virtual string SubCD { get; set; }

  [PXDBString(30, IsUnicode = true)]
  public virtual string SubCDWildcard => SubCDUtils.CreateSubCDWildcard(this.SubCD, "SUBACCOUNT");

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Show Only Documents with Discrepancies")]
  public virtual bool? ShowOnlyWithDiscrepancy { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total GL Amount", Enabled = false)]
  public virtual Decimal? TotalGLAmount { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Module Amount", Enabled = false)]
  public virtual Decimal? TotalXXAmount { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Discrepancy", Enabled = false)]
  public virtual Decimal? TotalDiscrepancy { get; set; }

  [PXResultStorage]
  public byte[][] FilterDetails { get; set; }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    DiscrepancyEnqFilter.organizationID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DiscrepancyEnqFilter.branchID>
  {
  }

  public abstract class useMasterCalendar : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DiscrepancyEnqFilter.useMasterCalendar>
  {
  }

  public abstract class periodFrom : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscrepancyEnqFilter.periodFrom>
  {
  }

  public abstract class periodTo : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DiscrepancyEnqFilter.periodTo>
  {
  }

  public abstract class accountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  DiscrepancyEnqFilter.accountID>
  {
  }

  public abstract class subCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  DiscrepancyEnqFilter.subCD>
  {
  }

  public abstract class subCDWildcard : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    DiscrepancyEnqFilter.subCDWildcard>
  {
  }

  public abstract class showOnlyWithDiscrepancy : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DiscrepancyEnqFilter.showOnlyWithDiscrepancy>
  {
  }

  public abstract class totalGLAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscrepancyEnqFilter.totalGLAmount>
  {
  }

  public abstract class totalXXAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscrepancyEnqFilter.totalXXAmount>
  {
  }

  public abstract class totalDiscrepancy : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    DiscrepancyEnqFilter.totalDiscrepancy>
  {
  }

  public abstract class filterDetails : IBqlField, IBqlOperand
  {
  }
}
