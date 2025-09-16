// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.INPIHeader
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.IN;

[PXCacheName("Physical Inventory Review")]
[PXPrimaryGraph(typeof (INPIReview))]
[PXGroupMask(typeof (InnerJoin<INSite, On<BqlChainableConditionBase<TypeArrayOf<IBqlBinary>.FilledWith<And<Compare<INSite.siteID, Equal<INPIHeader.siteID>>>>>.And<MatchUserFor<INSite>>>>))]
public class INPIHeader : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDefault]
  [PXDBString(15, IsUnicode = true, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search<INPIHeader.pIID, Where<True, Equal<True>>, OrderBy<Desc<INPIHeader.pIID>>>), Filterable = true)]
  [AutoNumber(typeof (INSetup.pINumberingID), typeof (AccessInfo.businessDate))]
  [PXFieldDescription]
  public virtual 
  #nullable disable
  string PIID { get; set; }

  [PXDBString(30, IsUnicode = true)]
  public virtual string PIClassID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXUIField]
  public virtual string Descr { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField]
  public virtual int? LineCntr { get; set; }

  [PXDBBool]
  [PXUIField]
  [PXDefault(typeof (Search<INSetup.pIUseTags>))]
  public virtual bool? TagNumbered { get; set; }

  [PXDBInt]
  public virtual int? FirstTagNbr { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  public virtual string FinPeriodID { get; set; }

  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  public virtual string TranPeriodID { get; set; }

  [Account(Enabled = false)]
  [PXDefault(typeof (Search2<PX.Objects.CS.ReasonCode.accountID, InnerJoin<INSetup, On<BqlOperand<INSetup.pIReasonCode, IBqlString>.IsEqual<PX.Objects.CS.ReasonCode.reasonCodeID>>>>))]
  public virtual int? PIAdjAcctID { get; set; }

  [SubAccount(Enabled = false)]
  [PXDefault(typeof (Search2<PX.Objects.CS.ReasonCode.subID, InnerJoin<INSetup, On<BqlOperand<INSetup.pIReasonCode, IBqlString>.IsEqual<PX.Objects.CS.ReasonCode.reasonCodeID>>>>))]
  public virtual int? PIAdjSubID { get; set; }

  [Site(Enabled = false)]
  [PXDefault]
  [PXForeignReference(typeof (INPIHeader.FK.Site))]
  public virtual int? SiteID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("N")]
  [PXUIField]
  [INPIHdrStatus.List]
  public virtual string Status { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? CountDate { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (INRegister.refNbr))]
  public virtual string PIAdjRefNbr { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (INRegister.refNbr))]
  public virtual string PIRcptRefNbr { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TotalPhysicalQty { get; set; }

  [PXDBQuantity]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TotalVarQty { get; set; }

  [PXDBBaseCury(null, null)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? TotalVarCost { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXUIField]
  public virtual int? TotalNbrOfTags { get; set; }

  [PXString(5, IsUnicode = true)]
  [PXFormula(typeof (Selector<INPIHeader.siteID, INSite.baseCuryID>))]
  [PXUIField(DisplayName = "Currency", Enabled = false, FieldClass = "MultipleBaseCurrencies")]
  public virtual string BaseCuryID { get; set; }

  [PXNote(DescriptionField = typeof (INPIHeader.pIID), Selector = typeof (INPIHeader.pIID))]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : PrimaryKeyOf<INPIHeader>.By<INPIHeader.pIID>
  {
    public static INPIHeader Find(PXGraph graph, string pIID, PKFindOptions options = 0)
    {
      return (INPIHeader) PrimaryKeyOf<INPIHeader>.By<INPIHeader.pIID>.FindBy(graph, (object) pIID, options);
    }
  }

  public static class FK
  {
    public class Site : 
      PrimaryKeyOf<INSite>.By<INSite.siteID>.ForeignKeyOf<INPIHeader>.By<INPIHeader.siteID>
    {
    }

    public class PIClass : 
      PrimaryKeyOf<INPIClass>.By<INPIClass.pIClassID>.ForeignKeyOf<INPIHeader>.By<INPIHeader.pIClassID>
    {
    }

    public class PIAdjustmentAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<INPIHeader>.By<INPIHeader.pIAdjAcctID>
    {
    }

    public class PIAdjustmentSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<INPIHeader>.By<INPIHeader.pIAdjSubID>
    {
    }
  }

  public abstract class pIID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIHeader.pIID>
  {
  }

  public abstract class pIClassID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIHeader.pIClassID>
  {
  }

  public abstract class descr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIHeader.descr>
  {
  }

  public abstract class lineCntr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIHeader.lineCntr>
  {
  }

  public abstract class tagNumbered : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  INPIHeader.tagNumbered>
  {
  }

  public abstract class firstTagNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIHeader.firstTagNbr>
  {
  }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIHeader.finPeriodID>
  {
  }

  public abstract class tranPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIHeader.tranPeriodID>
  {
  }

  public abstract class pIAdjAcctID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIHeader.pIAdjAcctID>
  {
  }

  public abstract class pIAdjSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIHeader.pIAdjSubID>
  {
  }

  public abstract class siteID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIHeader.siteID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIHeader.status>
  {
  }

  public abstract class countDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  INPIHeader.countDate>
  {
  }

  public abstract class pIAdjRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIHeader.pIAdjRefNbr>
  {
  }

  public abstract class pIRcptRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIHeader.pIRcptRefNbr>
  {
  }

  public abstract class totalPhysicalQty : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    INPIHeader.totalPhysicalQty>
  {
  }

  public abstract class totalVarQty : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INPIHeader.totalVarQty>
  {
  }

  public abstract class totalVarCost : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  INPIHeader.totalVarCost>
  {
  }

  public abstract class totalNbrOfTags : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  INPIHeader.totalNbrOfTags>
  {
  }

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  INPIHeader.baseCuryID>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INPIHeader.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INPIHeader.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPIHeader.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INPIHeader.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  INPIHeader.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    INPIHeader.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    INPIHeader.lastModifiedDateTime>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  INPIHeader.Tstamp>
  {
  }
}
