// Decompiled with JetBrains decompiler
// Type: PX.Objects.FA.FAService
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AP.Standalone;
using PX.Objects.CM;
using PX.Objects.EP;
using System;

#nullable enable
namespace PX.Objects.FA;

[PXCacheName("FA Service")]
[Serializable]
public class FAService : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _AssetID;
  protected 
  #nullable disable
  string _ServiceNumber;
  protected DateTime? _ServiceDate;
  protected DateTime? _ScheduledDate;
  protected Guid? _PerfomedBy;
  protected Guid? _InspectedBy;
  protected string _Description;
  protected Decimal? _ServiceAmount;
  protected int? _VendorID;
  protected string _BillNumber;
  protected bool? _Completed;
  protected byte[] _tstamp;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;

  [PXDBInt(IsKey = true)]
  [PXUIField]
  [PXParent(typeof (Select<FixedAsset, Where<FixedAsset.assetID, Equal<Current<FAService.assetID>>>>))]
  [PXDBDefault(typeof (FixedAsset.assetID))]
  public virtual int? AssetID
  {
    get => this._AssetID;
    set => this._AssetID = value;
  }

  [PXDBString(15, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField]
  public virtual string ServiceNumber
  {
    get => this._ServiceNumber;
    set => this._ServiceNumber = value;
  }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? ServiceDate
  {
    get => this._ServiceDate;
    set => this._ServiceDate = value;
  }

  [PXDBDate]
  [PXDefault]
  [PXUIField]
  public virtual DateTime? ScheduledDate
  {
    get => this._ScheduledDate;
    set => this._ScheduledDate = value;
  }

  [PXDBGuid(false)]
  [PXDefault]
  [PXSelector(typeof (EPEmployee.userID), SubstituteKey = typeof (EPEmployee.acctCD), DescriptionField = typeof (EPEmployee.acctName))]
  [PXUIField(DisplayName = "Perfomed By", TabOrder = 4)]
  public virtual Guid? PerfomedBy
  {
    get => this._PerfomedBy;
    set => this._PerfomedBy = value;
  }

  [PXDBGuid(false)]
  [PXSelector(typeof (EPEmployee.userID), SubstituteKey = typeof (EPEmployee.acctCD), DescriptionField = typeof (EPEmployee.acctName))]
  [PXUIField(DisplayName = "Inspected By", TabOrder = 5)]
  public virtual Guid? InspectedBy
  {
    get => this._InspectedBy;
    set => this._InspectedBy = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description", TabOrder = 6)]
  public virtual string Description
  {
    get => this._Description;
    set => this._Description = value;
  }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Service Amount")]
  public virtual Decimal? ServiceAmount
  {
    get => this._ServiceAmount;
    set => this._ServiceAmount = value;
  }

  [Vendor]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [APInvoiceType.RefNbr(typeof (Search2<APRegisterAlias.refNbr, InnerJoinSingleTable<PX.Objects.AP.APInvoice, On<PX.Objects.AP.APInvoice.docType, Equal<APRegisterAlias.docType>, And<PX.Objects.AP.APInvoice.refNbr, Equal<APRegisterAlias.refNbr>>>, InnerJoinSingleTable<PX.Objects.AP.Vendor, On<APRegisterAlias.vendorID, Equal<PX.Objects.AP.Vendor.bAccountID>>>>, Where<APRegisterAlias.docType, Equal<APDocType.invoice>, And<APRegisterAlias.vendorID, Equal<Current<FAService.vendorID>>, And<Match<PX.Objects.AP.Vendor, Current<AccessInfo.userName>>>>>, OrderBy<Desc<APRegisterAlias.refNbr>>>), Filterable = true)]
  public virtual string BillNumber
  {
    get => this._BillNumber;
    set => this._BillNumber = value;
  }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Completed")]
  public virtual bool? Completed
  {
    get => this._Completed;
    set => this._Completed = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID
  {
    get => this._CreatedByID;
    set => this._CreatedByID = value;
  }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID
  {
    get => this._CreatedByScreenID;
    set => this._CreatedByScreenID = value;
  }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime
  {
    get => this._CreatedDateTime;
    set => this._CreatedDateTime = value;
  }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID
  {
    get => this._LastModifiedByID;
    set => this._LastModifiedByID = value;
  }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID
  {
    get => this._LastModifiedByScreenID;
    set => this._LastModifiedByScreenID = value;
  }

  [PXDBLastModifiedDateTime]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  public class PK : PrimaryKeyOf<FAService>.By<FAService.assetID, FAService.serviceNumber>
  {
    public static FAService Find(
      PXGraph graph,
      int? assetID,
      string serviceNumber,
      PKFindOptions options = 0)
    {
      return (FAService) PrimaryKeyOf<FAService>.By<FAService.assetID, FAService.serviceNumber>.FindBy(graph, (object) assetID, (object) serviceNumber, options);
    }
  }

  public static class FK
  {
    public class FixedAsset : 
      PrimaryKeyOf<FixedAsset>.By<FixedAsset.assetID>.ForeignKeyOf<FAService>.By<FAService.assetID>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<FAService>.By<FAService.vendorID>
    {
    }
  }

  public abstract class assetID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAService.assetID>
  {
  }

  public abstract class serviceNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAService.serviceNumber>
  {
  }

  public abstract class serviceDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FAService.serviceDate>
  {
  }

  public abstract class scheduledDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FAService.scheduledDate>
  {
  }

  public abstract class perfomedBy : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FAService.perfomedBy>
  {
  }

  public abstract class inspectedBy : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FAService.inspectedBy>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAService.description>
  {
  }

  public abstract class serviceAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  FAService.serviceAmount>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FAService.vendorID>
  {
  }

  public abstract class billNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FAService.billNumber>
  {
  }

  public abstract class completed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FAService.completed>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  FAService.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FAService.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAService.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FAService.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  FAService.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FAService.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FAService.lastModifiedDateTime>
  {
  }
}
