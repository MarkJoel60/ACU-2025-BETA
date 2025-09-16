// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxPeriodEffective
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.GL;
using PX.Objects.GL.Attributes;
using System;

#nullable enable
namespace PX.Objects.TX;

[PXProjection(typeof (Select<TaxPeriodPlusOpen, Where<TaxPeriodPlusOpen.taxPeriodID, Equal<TaxPeriodPlusOpen.openPeriodID>, Or<TaxPeriodPlusOpen.status, NotEqual<TaxPeriodStatus.open>>>>))]
public class TaxPeriodEffective : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _VendorID;
  protected 
  #nullable disable
  string _TaxPeriodID;
  protected string _Status;
  protected DateTime? _EndDate;
  protected int? _RevisionID;

  [Organization(true, IsKey = true, BqlField = typeof (TaxPeriodPlusOpen.organizationID))]
  public virtual int? OrganizationID { get; set; }

  [Vendor(typeof (Search<PX.Objects.AP.Vendor.bAccountID, Where<PX.Objects.AP.Vendor.taxAgency, Equal<True>>>), IsKey = true, BqlField = typeof (TaxPeriodPlusOpen.vendorID), DisplayName = "Tax Agency")]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true, BqlField = typeof (TaxPeriodPlusOpen.taxPeriodID))]
  [PXSelector(typeof (Search<TaxPeriodPlusOpen.taxPeriodID, Where<TaxPeriodPlusOpen.organizationID, Equal<Optional<TaxPeriodEffective.organizationID>>, And<TaxPeriodPlusOpen.vendorID, Equal<Optional<TaxPeriodEffective.vendorID>>, And<Where<TaxPeriodPlusOpen.taxPeriodID, Equal<TaxPeriodPlusOpen.openPeriodID>, Or<TaxPeriodPlusOpen.status, NotEqual<TaxPeriodStatus.open>>>>>>>))]
  [PXUIField]
  public virtual string TaxPeriodID
  {
    get => this._TaxPeriodID;
    set => this._TaxPeriodID = value;
  }

  [PXDBString(1, IsFixed = true, BqlField = typeof (TaxPeriodPlusOpen.status))]
  [PXDefault("N")]
  [TaxPeriodStatus.List]
  [PXUIField]
  public virtual string Status
  {
    get => this._Status;
    set => this._Status = value;
  }

  [PXDBDate(BqlField = typeof (TaxPeriodPlusOpen.endDate))]
  public virtual DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXSelector(typeof (Search5<TaxHistory.revisionID, InnerJoin<PX.Objects.GL.Branch, On<TaxHistory.branchID, Equal<PX.Objects.GL.Branch.branchID>>>, Where<PX.Objects.GL.Branch.organizationID, Equal<Optional<TaxPeriodEffective.organizationID>>, And<TaxHistory.vendorID, Equal<Optional<TaxPeriodEffective.vendorID>>, And<Where<TaxHistory.taxPeriodID, Equal<Optional<TaxPeriodEffective.taxPeriodID>>>>>>, Aggregate<GroupBy<TaxHistory.revisionID>>>))]
  [PXUIField]
  [PXInt]
  public virtual int? RevisionID
  {
    get => this._RevisionID;
    set => this._RevisionID = value;
  }

  public abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TaxPeriodEffective.organizationID>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxPeriodEffective.vendorID>
  {
  }

  public abstract class taxPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxPeriodEffective.taxPeriodID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxPeriodEffective.status>
  {
  }

  public abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxPeriodEffective.endDate>
  {
  }

  public abstract class revisionID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxPeriodEffective.revisionID>
  {
  }
}
