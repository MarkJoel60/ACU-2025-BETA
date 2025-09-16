// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.APVendorRefNbr
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.AP;

[PXHidden]
[PXCacheName("APVendorRefNbr")]
[Serializable]
public class APVendorRefNbr : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _VendorID;
  protected 
  #nullable disable
  string _VendorDocumentID;
  protected Guid? _MasterID;
  protected int? _DetailId;
  protected bool? _IsProcessed;
  protected Guid? _SiblingID;
  protected bool? _IsChecked;
  protected bool? _IsIgnored;
  protected byte[] _tstamp;

  [PXDBInt]
  [PXDefault]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [PXDBString(64 /*0x40*/, IsUnicode = true)]
  [PXDefault]
  public virtual string VendorDocumentID
  {
    get => this._VendorDocumentID;
    set => this._VendorDocumentID = value;
  }

  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? MasterID
  {
    get => this._MasterID;
    set => this._MasterID = value;
  }

  [PXDBInt(IsKey = true)]
  public virtual int? DetailID
  {
    get => this._DetailId;
    set => this._DetailId = value;
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2020 R2.")]
  [PXBool]
  public virtual bool? IsProcessed
  {
    get => this._IsProcessed;
    set => this._IsProcessed = value;
  }

  [PXDBGuid(false)]
  [PXDefault]
  public virtual Guid? SiblingID
  {
    get => this._SiblingID;
    set => this._SiblingID = value;
  }

  [PXBool]
  public virtual bool? IsChecked
  {
    get => this._IsChecked;
    set => this._IsChecked = value;
  }

  [PXBool]
  [PXDBCalced(typeof (False), typeof (bool))]
  public virtual bool? IsIgnored
  {
    get => this._IsIgnored;
    set => this._IsIgnored = value;
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp
  {
    get => this._tstamp;
    set => this._tstamp = value;
  }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  public class PK : PrimaryKeyOf<APVendorRefNbr>.By<APVendorRefNbr.masterID, APVendorRefNbr.detailID>
  {
    public static APVendorRefNbr Find(
      PXGraph graph,
      Guid? masterID,
      int? detailID,
      PKFindOptions options = PKFindOptions.None)
    {
      return PrimaryKeyOf<APVendorRefNbr>.By<APVendorRefNbr.masterID, APVendorRefNbr.detailID>.FindBy(graph, (object) masterID, (object) detailID, options);
    }
  }

  public static class FK
  {
    public class Vendor : 
      PrimaryKeyOf<Vendor>.By<Vendor.bAccountID>.ForeignKeyOf<APVendorRefNbr>.By<APVendorRefNbr.vendorID>
    {
    }
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APVendorRefNbr.vendorID>
  {
  }

  public abstract class vendorDocumentID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APVendorRefNbr.vendorDocumentID>
  {
  }

  public abstract class masterID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APVendorRefNbr.masterID>
  {
  }

  public abstract class detailID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  APVendorRefNbr.detailID>
  {
  }

  [Obsolete("This item has been deprecated and will be removed in Acumatica ERP 2020 R2.")]
  public abstract class isProcessed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APVendorRefNbr.isProcessed>
  {
  }

  public abstract class siblingID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APVendorRefNbr.siblingID>
  {
  }

  public abstract class isChecked : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APVendorRefNbr.isChecked>
  {
  }

  public abstract class isIgnored : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  APVendorRefNbr.isIgnored>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  APVendorRefNbr.Tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  APVendorRefNbr.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    APVendorRefNbr.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    APVendorRefNbr.createdDateTime>
  {
  }
}
