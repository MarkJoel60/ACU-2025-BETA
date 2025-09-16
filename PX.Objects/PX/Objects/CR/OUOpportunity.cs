// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.OUOpportunity
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CM;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <exclude />
[PXHidden]
[Serializable]
public class OUOpportunity : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected int? _BranchID;

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXUIField(DisplayName = "Opportunity Class")]
  [PXSelector(typeof (CROpportunityClass.cROpportunityClassID), DescriptionField = typeof (CROpportunityClass.description), CacheGlobal = true)]
  public virtual 
  #nullable disable
  string ClassID { get; set; }

  [PXDBString(255 /*0xFF*/, IsUnicode = true)]
  [PXDefault(typeof (OUMessage.subject))]
  [PXUIField]
  public virtual string Subject { get; set; }

  [PXDBString(2)]
  [PXUIField(DisplayName = "Stage")]
  [CROpportunityStages(typeof (OUOpportunity.classID), null, OnlyActiveStages = true)]
  [PXDefault]
  public virtual string StageID { get; set; }

  [PXDBDate]
  [PXDefault(typeof (AccessInfo.businessDate))]
  [PXUIField]
  public virtual DateTime? CloseDate { get; set; }

  [PXDBString(5, IsUnicode = true, InputMask = ">LLLLL")]
  [PXDefault(typeof (Current<AccessInfo.baseCuryID>))]
  [PXSelector(typeof (PX.Objects.CM.Currency.curyID))]
  [PXUIField]
  public virtual string CurrencyID { get; set; }

  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXDBBaseCury(null, null)]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? ManualAmount { get; set; }

  [Branch(null, null, true, true, true)]
  [PXUIField]
  public virtual int? BranchID
  {
    get => this._BranchID;
    set => this._BranchID = value;
  }

  public abstract class classID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUOpportunity.classID>
  {
  }

  public abstract class subject : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUOpportunity.subject>
  {
  }

  public abstract class stageID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUOpportunity.stageID>
  {
  }

  public abstract class closeDate : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  OUOpportunity.closeDate>
  {
  }

  public abstract class currencyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  OUOpportunity.currencyID>
  {
  }

  public abstract class manualAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    OUOpportunity.manualAmount>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  OUOpportunity.branchID>
  {
  }
}
