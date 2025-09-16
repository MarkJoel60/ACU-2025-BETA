// Decompiled with JetBrains decompiler
// Type: PX.Objects.TX.TaxPeriodMaster
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.TX;

[PXHidden]
[Serializable]
public class TaxPeriodMaster : TaxPeriod
{
  protected int? _RevisionId;

  [Vendor(typeof (Search<PX.Objects.AP.Vendor.bAccountID, Where<PX.Objects.AP.Vendor.taxAgency, Equal<True>>>), IsKey = true, DisplayName = "Tax Agency", Required = true)]
  public override int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true, IsKey = true)]
  [PXDefault]
  public override 
  #nullable disable
  string TaxPeriodID
  {
    get => this._TaxPeriodID;
    set => this._TaxPeriodID = value;
  }

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public override DateTime? StartDate
  {
    get => this._StartDate;
    set => this._StartDate = value;
  }

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public override DateTime? EndDate
  {
    get => this._EndDate;
    set => this._EndDate = value;
  }

  [PXDate]
  [PXUIField]
  public virtual DateTime? EndDateInclusive
  {
    [PXDependsOnFields(new Type[] {typeof (TaxPeriodMaster.endDate)})] get
    {
      return this._EndDate.HasValue ? new DateTime?(this._EndDate.Value.AddDays(-1.0)) : new DateTime?();
    }
    set
    {
      this._EndDate = !value.HasValue ? new DateTime?() : new DateTime?(value.Value.AddDays(1.0));
    }
  }

  [PXInt]
  [PXUIField(DisplayName = "Revision ID")]
  [PXSelector(typeof (Search5<TaxHistory.revisionID, InnerJoin<PX.Objects.GL.Branch, On<TaxHistory.branchID, Equal<PX.Objects.GL.Branch.branchID>>>, Where<PX.Objects.GL.Branch.organizationID, Equal<Current<TaxPeriodMaster.organizationID>>, And<TaxHistory.vendorID, Equal<Current<TaxPeriodMaster.vendorID>>, And<TaxHistory.taxPeriodID, Equal<Current<TaxPeriodMaster.taxPeriodID>>>>>, Aggregate<GroupBy<TaxHistory.revisionID>>, OrderBy<Asc<TaxHistory.revisionID>>>))]
  [PXDBScalar(typeof (Search5<TaxHistory.revisionID, InnerJoin<PX.Objects.GL.Branch, On<TaxHistory.branchID, Equal<PX.Objects.GL.Branch.branchID>>>, Where<PX.Objects.GL.Branch.organizationID, Equal<TaxPeriod.organizationID>, And<TaxHistory.vendorID, Equal<TaxPeriod.vendorID>, And<TaxHistory.taxPeriodID, Equal<TaxPeriod.taxPeriodID>>>>, Aggregate<Max<TaxHistory.revisionID>>>))]
  public virtual int? RevisionId
  {
    get => this._RevisionId;
    set => this._RevisionId = value;
  }

  public static implicit operator TaxPeriodFilter(TaxPeriodMaster master)
  {
    return new TaxPeriodFilter()
    {
      VendorID = master.VendorID,
      TaxPeriodID = master.TaxPeriodID,
      RevisionId = master.RevisionId
    };
  }

  public new abstract class organizationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    TaxPeriodMaster.organizationID>
  {
  }

  public new abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxPeriodMaster.vendorID>
  {
  }

  public new abstract class taxPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    TaxPeriodMaster.taxPeriodID>
  {
  }

  public new abstract class startDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxPeriodMaster.startDate>
  {
  }

  public new abstract class endDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  TaxPeriodMaster.endDate>
  {
  }

  public abstract class endDateInclusive : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    TaxPeriodMaster.endDateInclusive>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  TaxPeriodMaster.status>
  {
  }

  public abstract class revisionId : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  TaxPeriodMaster.revisionId>
  {
  }
}
