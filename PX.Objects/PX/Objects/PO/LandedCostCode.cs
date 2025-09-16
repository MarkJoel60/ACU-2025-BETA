// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.LandedCostCode
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.PO;

[PXCacheName]
[Serializable]
public class LandedCostCode : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _LandedCostCodeID;
  protected string _Descr;
  protected string _LCType;
  protected string _AllocationMethod;
  protected int? _VendorID;
  protected int? _VendorLocationID;
  protected string _TermsID;
  protected string _ReasonCode;
  protected int? _LCAccrualAcct;
  protected int? _LCAccrualSub;
  protected int? _LCVarianceAcct;
  protected int? _LCVarianceSub;
  protected string _TaxCategoryID;
  protected Guid? _CreatedByID;
  protected string _CreatedByScreenID;
  protected DateTime? _CreatedDateTime;
  protected Guid? _LastModifiedByID;
  protected string _LastModifiedByScreenID;
  protected DateTime? _LastModifiedDateTime;
  protected byte[] _TStamp;

  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaaaaaaa")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<LandedCostCode.landedCostCodeID>))]
  [PXFieldDescription]
  public virtual string LandedCostCodeID
  {
    get => this._LandedCostCodeID;
    set => this._LandedCostCodeID = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr
  {
    get => this._Descr;
    set => this._Descr = value;
  }

  [PXDBString(2, IsFixed = true)]
  [PXDefault("FO")]
  [LandedCostType.List]
  [PXUIField]
  public virtual string LCType
  {
    get => this._LCType;
    set => this._LCType = value;
  }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("Q")]
  [LandedCostAllocationMethod.List]
  [PXUIField]
  public virtual string AllocationMethod
  {
    get => this._AllocationMethod;
    set => this._AllocationMethod = value;
  }

  [Vendor(typeof (Search<PX.Objects.AP.Vendor.bAccountID, Where<PX.Objects.AP.Vendor.landedCostVendor, Equal<boolTrue>>>))]
  public virtual int? VendorID
  {
    get => this._VendorID;
    set => this._VendorID = value;
  }

  [LocationID(typeof (Where<PX.Objects.CR.Location.bAccountID, Equal<Current<LandedCostCode.vendorID>>>))]
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.defLocationID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<LandedCostCode.vendorID>>>>))]
  public virtual int? VendorLocationID
  {
    get => this._VendorLocationID;
    set => this._VendorLocationID = value;
  }

  [PXDBString(10, IsUnicode = true)]
  [PXDefault(typeof (Search<PX.Objects.AP.Vendor.termsID, Where<PX.Objects.AP.Vendor.bAccountID, Equal<Current<LandedCostCode.vendorID>>>>))]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.vendor>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), Filterable = true)]
  public virtual string TermsID
  {
    get => this._TermsID;
    set => this._TermsID = value;
  }

  [PXDBString(20, IsUnicode = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Reason Code")]
  [PXSelector(typeof (Search<PX.Objects.CS.ReasonCode.reasonCodeID, Where<PX.Objects.CS.ReasonCode.usage, Equal<ReasonCodeUsages.adjustment>>>), DescriptionField = typeof (PX.Objects.CS.ReasonCode.descr))]
  [PXForeignReference(typeof (LandedCostCode.FK.ReasonCode))]
  public virtual string ReasonCode
  {
    get => this._ReasonCode;
    set => this._ReasonCode = value;
  }

  [Account]
  [PXDefault]
  [PXForeignReference(typeof (LandedCostCode.FK.LandedCostAccrualAccount))]
  public virtual int? LCAccrualAcct
  {
    get => this._LCAccrualAcct;
    set => this._LCAccrualAcct = value;
  }

  [SubAccount(typeof (LandedCostCode.lCAccrualAcct))]
  [PXDefault]
  [PXForeignReference(typeof (LandedCostCode.FK.LandedCostAccrualSubaccount))]
  public virtual int? LCAccrualSub
  {
    get => this._LCAccrualSub;
    set => this._LCAccrualSub = value;
  }

  [Account]
  [PXDefault]
  [PXForeignReference(typeof (LandedCostCode.FK.LandedCostVarianceAccount))]
  public virtual int? LCVarianceAcct
  {
    get => this._LCVarianceAcct;
    set => this._LCVarianceAcct = value;
  }

  [SubAccount(typeof (LandedCostCode.lCVarianceAcct))]
  [PXDefault]
  [PXForeignReference(typeof (LandedCostCode.FK.LandedCostVarianceSubaccount))]
  public virtual int? LCVarianceSub
  {
    get => this._LCVarianceSub;
    set => this._LCVarianceSub = value;
  }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new System.Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  [PXForeignReference(typeof (LandedCostCode.FK.TaxCategory))]
  public virtual string TaxCategoryID
  {
    get => this._TaxCategoryID;
    set => this._TaxCategoryID = value;
  }

  [PXNote(DescriptionField = typeof (LandedCostCode.landedCostCodeID), Selector = typeof (Search<LandedCostCode.landedCostCodeID>), FieldList = new System.Type[] {typeof (LandedCostCode.landedCostCodeID), typeof (LandedCostCode.descr), typeof (LandedCostCode.lCType), typeof (LandedCostCode.allocationMethod), typeof (LandedCostCode.vendorID)})]
  public virtual Guid? NoteID { get; set; }

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
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
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
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime
  {
    get => this._LastModifiedDateTime;
    set => this._LastModifiedDateTime = value;
  }

  [PXDBTimestamp]
  public virtual byte[] TStamp
  {
    get => this._TStamp;
    set => this._TStamp = value;
  }

  public class PK : PrimaryKeyOf<LandedCostCode>.By<LandedCostCode.landedCostCodeID>
  {
    public static LandedCostCode Find(
      PXGraph graph,
      string landedCostCodeID,
      PKFindOptions options = 0)
    {
      return (LandedCostCode) PrimaryKeyOf<LandedCostCode>.By<LandedCostCode.landedCostCodeID>.FindBy(graph, (object) landedCostCodeID, options);
    }
  }

  public static class FK
  {
    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<LandedCostCode>.By<LandedCostCode.taxCategoryID>
    {
    }

    public class ReasonCode : 
      PrimaryKeyOf<PX.Objects.CS.ReasonCode>.By<PX.Objects.CS.ReasonCode.reasonCodeID>.ForeignKeyOf<LandedCostCode>.By<LandedCostCode.reasonCode>
    {
    }

    public class Vendor : 
      PrimaryKeyOf<PX.Objects.AP.Vendor>.By<PX.Objects.AP.Vendor.bAccountID>.ForeignKeyOf<LandedCostCode>.By<LandedCostCode.vendorID>
    {
    }

    public class VendorLocation : 
      PrimaryKeyOf<PX.Objects.CR.Location>.By<PX.Objects.CR.Location.bAccountID, PX.Objects.CR.Location.locationID>.ForeignKeyOf<LandedCostCode>.By<LandedCostCode.vendorID, LandedCostCode.vendorLocationID>
    {
    }

    public class Terms : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<LandedCostCode>.By<LandedCostCode.termsID>
    {
    }

    public class LandedCostAccrualAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<LandedCostCode>.By<LandedCostCode.lCAccrualAcct>
    {
    }

    public class LandedCostAccrualSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<LandedCostCode>.By<LandedCostCode.lCAccrualSub>
    {
    }

    public class LandedCostVarianceAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<LandedCostCode>.By<LandedCostCode.lCVarianceAcct>
    {
    }

    public class LandedCostVarianceSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<LandedCostCode>.By<LandedCostCode.lCVarianceSub>
    {
    }
  }

  public abstract class landedCostCodeID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LandedCostCode.landedCostCodeID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LandedCostCode.descr>
  {
  }

  public abstract class lCType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LandedCostCode.lCType>
  {
  }

  public abstract class allocationMethod : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LandedCostCode.allocationMethod>
  {
  }

  public abstract class vendorID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LandedCostCode.vendorID>
  {
  }

  public abstract class vendorLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    LandedCostCode.vendorLocationID>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LandedCostCode.termsID>
  {
  }

  public abstract class reasonCode : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  LandedCostCode.reasonCode>
  {
  }

  public abstract class lCAccrualAcct : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LandedCostCode.lCAccrualAcct>
  {
  }

  public abstract class lCAccrualSub : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LandedCostCode.lCAccrualSub>
  {
  }

  public abstract class lCVarianceAcct : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LandedCostCode.lCVarianceAcct>
  {
  }

  public abstract class lCVarianceSub : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  LandedCostCode.lCVarianceSub>
  {
  }

  public abstract class taxCategoryID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LandedCostCode.taxCategoryID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  LandedCostCode.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  LandedCostCode.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LandedCostCode.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    LandedCostCode.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    LandedCostCode.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    LandedCostCode.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    LandedCostCode.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  LandedCostCode.tStamp>
  {
  }
}
