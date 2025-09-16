// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxPeriodPlusOpen
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using System;

#nullable enable
namespace PX.Objects.TX;

[PXProjection(typeof (Select5<TaxPeriodMaster, LeftJoin<TaxPeriod, On<TaxPeriod.vendorID, Equal<TaxPeriodMaster.vendorID>, And<TaxPeriod.organizationID, Equal<TaxPeriodMaster.organizationID>, And<TaxPeriod.status, Equal<TaxPeriodStatus.open>>>>>, Aggregate<GroupBy<TaxPeriodMaster.organizationID, GroupBy<TaxPeriodMaster.vendorID, GroupBy<TaxPeriodMaster.taxPeriodID, Max<TaxPeriodMaster.endDate, Min<TaxPeriod.taxPeriodID>>>>>>>))]
[PXHidden]
public class TaxPeriodPlusOpen : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _VendorID;
  protected 
  #nullable disable
  string _TaxPeriodID;
  protected string _Status;
  protected DateTime? _EndDate;
  protected string _OpenPeriodID;

  [Organization(true, IsKey = true, BqlField = typeof (TaxPeriodMaster.organizationID))]
  public virtual int? OrganizationID { get; set; }

  [PXDBInt(IsKey = true, BqlField = typeof (TaxPeriodMaster.vendorID))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlField = typeof (TaxPeriodMaster.taxPeriodID))]
  [PXUIField]
  public virtual string TaxPeriodID
  {
    get => this._TaxPeriodID;
    set => this._TaxPeriodID = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (TaxPeriodMaster.status))]
  [TaxPeriodStatus.List]
  [PXUIField]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBDate(BqlField = typeof (TaxPeriodMaster.endDate))]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXDBString(6, IsFixed = true, BqlField = typeof (TaxPeriod.taxPeriodID))]
  public virtual string OpenPeriodID
  {
    get => this._OpenPeriodID;
    set => this._OpenPeriodID = value;
  }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TaxPeriodPlusOpen.organizationID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxPeriodPlusOpen.vendorID>
  {
  }

  public abstract class taxPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxPeriodPlusOpen.taxPeriodID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxPeriodPlusOpen.status>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxPeriodPlusOpen.endDate>
  {
  }

  public abstract class openPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxPeriodPlusOpen.openPeriodID>
  {
  }
}
