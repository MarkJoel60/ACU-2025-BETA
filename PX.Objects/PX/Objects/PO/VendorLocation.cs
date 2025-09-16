// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.VendorLocation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.EP;
using PX.Objects.AP;
using PX.Objects.CR;
using PX.Objects.IN;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXProjection(typeof (SelectFromBase<PX.Objects.CR.Standalone.Location, TypeArrayOf<IFbqlJoin>.Append<TypeArrayOf<IFbqlJoin>.Empty, FbqlJoins.Inner<PX.Objects.AP.Vendor>.On<BqlOperand<PX.Objects.AP.Vendor.bAccountID, IBqlInt>.IsEqual<PX.Objects.CR.Standalone.Location.bAccountID>>>>.Where<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.Vendor.type, NotEqual<BAccountType.employeeType>>>>>.And<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<PX.Objects.AP.Vendor.vStatus, Equal<VendorStatus.active>>>>>.Or<BqlOperand<PX.Objects.AP.Vendor.vStatus, IBqlString>.IsEqual<VendorStatus.oneTime>>>>))]
[PXCacheName("Vendor Location")]
public class VendorLocation : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BAccountID;
  protected 
  #nullable disable
  string _AcctCD;
  protected int? _LocationID;
  protected string _CuryID;
  protected int? _VSiteID;
  protected short? _VLeadTime;

  [VendorNonEmployeeActive(IsKey = true, BqlField = typeof (PX.Objects.CR.Standalone.Location.bAccountID))]
  public virtual int? BAccountID
  {
    get => this._BAccountID;
    set => this._BAccountID = value;
  }

  [VendorRaw(BqlField = typeof (PX.Objects.AP.Vendor.acctCD))]
  [PXDefault]
  [PXFieldDescription]
  public virtual string AcctCD
  {
    get => this._AcctCD;
    set => this._AcctCD = value;
  }

  [PX.Objects.CS.LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<VendorLocation.bAccountID>>>))]
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.defLocationID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<VendorLocation.bAccountID>>>>))]
  [PXFormula(typeof (Default<VendorLocation.bAccountID>))]
  [PXFieldDescription]
  public virtual int? LocationID
  {
    get => this._LocationID;
    set => this._LocationID = value;
  }

  [PXDBString(5, IsUnicode = true, BqlField = typeof (PX.Objects.AP.Vendor.curyID))]
  [PXSelector(typeof (PX.Objects.CM.Extensions.Currency.curyID), CacheGlobal = true)]
  [PXDefault(typeof (Search<VendorClass.curyID, Where<VendorClass.vendorClassID, Equal<Current<PX.Objects.AP.Vendor.vendorClassID>>>>))]
  [PXUIField(DisplayName = "Currency", Enabled = false)]
  public virtual string CuryID
  {
    get => this._CuryID;
    set => this._CuryID = value;
  }

  [PXDBString(5, IsUnicode = true, BqlField = typeof (PX.Objects.AP.Vendor.baseCuryID))]
  public virtual string BaseCuryID { get; set; }

  [PXDBInt(BqlField = typeof (PX.Objects.CR.Standalone.Location.vSiteID))]
  [PXUIField]
  [PXDimensionSelector("INSITE", typeof (INSite.siteID), typeof (INSite.siteCD), DescriptionField = typeof (INSite.descr))]
  public virtual int? VSiteID
  {
    get => this._VSiteID;
    set => this._VSiteID = value;
  }

  [PXDBShort(MinValue = 0, MaxValue = 100000, BqlField = typeof (PX.Objects.CR.Standalone.Location.vLeadTime))]
  [PXUIField(DisplayName = "Lead Time (Days)", Enabled = false)]
  public virtual short? VLeadTime
  {
    get => this._VLeadTime;
    set => this._VLeadTime = value;
  }

  [PXNote(DescriptionField = typeof (VendorLocation.acctCD), Selector = typeof (VendorLocation.acctCD), BqlField = typeof (PX.Objects.AP.Vendor.noteID))]
  public virtual Guid? NoteID { get; set; }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorLocation.bAccountID>
  {
  }

  public abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  VendorLocation.acctCD>
  {
  }

  public abstract class locationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorLocation.locationID>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  VendorLocation.curyID>
  {
  }

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  VendorLocation.baseCuryID>
  {
  }

  public abstract class vSiteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  VendorLocation.vSiteID>
  {
  }

  public abstract class vLeadTime : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  VendorLocation.vLeadTime>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  VendorLocation.noteID>
  {
  }
}
