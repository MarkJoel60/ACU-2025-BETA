// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARFinCharge
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CM;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>
/// Represents an overdue charge code, which is used to calculate overdue charges
/// used for late payments in the collection process. The record encapsulates
/// various information about the overdue charge, such as the calculation method,
/// overdue fee rates, and overdue GL accounts. The entities of this type are
/// created and edited on the Overdue Charges (AR204500) form, which corresponds
/// to the <see cref="T:PX.Objects.AR.ARFinChargesMaint" /> graph. The overdue charge codes are
/// then used in the Calculate Overdue Charges (AR507000) processing, which
/// corresponds to the <see cref="T:PX.Objects.AR.ARFinChargesApplyMaint" /> graph.
/// </summary>
[PXPrimaryGraph(typeof (ARFinChargesMaint))]
[PXCacheName("AR Financial Charge")]
[Serializable]
public class ARFinCharge : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (ARFinCharge.finChargeID))]
  public virtual 
  #nullable disable
  string FinChargeID { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Description")]
  public virtual string FinChargeDesc { get; set; }

  [PXDBString(10, IsUnicode = true, InputMask = ">aaaaaaaaaa")]
  [PXDefault]
  [PXUIField]
  [PXSelector(typeof (Search<PX.Objects.CS.Terms.termsID, Where<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.all>, Or<PX.Objects.CS.Terms.visibleTo, Equal<TermsVisibleTo.customer>>>>), DescriptionField = typeof (PX.Objects.CS.Terms.descr), Filterable = true)]
  public virtual string TermsID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Base Currency")]
  public virtual bool? BaseCurFlag { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Line Minimum Amount")]
  public virtual bool? MinFinChargeFlag { get; set; }

  [PXDBBaseCury(null, null, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Min. Amount")]
  public virtual Decimal? MinFinChargeAmount { get; set; }

  [PXBaseCury(MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Threshold")]
  public virtual Decimal? LineThreshold
  {
    get => this.MinFinChargeAmount;
    set => this.MinFinChargeAmount = value;
  }

  [PXBaseCury(MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Amount")]
  public virtual Decimal? FixedAmount
  {
    get => this.MinFinChargeAmount;
    set => this.MinFinChargeAmount = value;
  }

  [PXDBBaseCury(null, null, MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Total Threshold")]
  public virtual Decimal? MinChargeDocumentAmt { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Use Percent Rate")]
  public virtual bool? PercentFlag { get; set; }

  [PXDefault]
  [PXNonCashAccount]
  public virtual int? FinChargeAccountID { get; set; }

  [PXDefault]
  [SubAccount(typeof (ARFinCharge.finChargeAccountID))]
  public virtual int? FinChargeSubID { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXUIField]
  [PXSelector(typeof (PX.Objects.TX.TaxCategory.taxCategoryID), DescriptionField = typeof (PX.Objects.TX.TaxCategory.descr))]
  [PXRestrictor(typeof (Where<PX.Objects.TX.TaxCategory.active, Equal<True>>), "Tax Category '{0}' is inactive", new Type[] {typeof (PX.Objects.TX.TaxCategory.taxCategoryID)})]
  [PXDefault]
  [PXForeignReference(typeof (Field<ARFinCharge.taxCategoryID>.IsRelatedTo<PX.Objects.TX.TaxCategory.taxCategoryID>))]
  public virtual string TaxCategoryID { get; set; }

  [PXDefault]
  [PXNonCashAccount]
  public virtual int? FeeAccountID { get; set; }

  [PXDefault]
  [SubAccount(typeof (ARFinCharge.finChargeAccountID))]
  public virtual int? FeeSubID { get; set; }

  [PXDBDecimal(MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField]
  public virtual Decimal? FeeAmount { get; set; }

  [PXDBLocalizableString(60, IsUnicode = true)]
  [PXUIField(DisplayName = "Fee Description")]
  public virtual string FeeDesc { get; set; }

  [PXDBInt]
  [PXDefault(0)]
  [PXIntList(new int[] {0, 1, 2}, new string[] {"Interest on Balance", "Interest on Prorated Balance", "Interest on Arrears"})]
  [PXUIField]
  public virtual int? CalculationMethod { get; set; }

  [PXInt]
  [PXDefault(1)]
  [PXIntList(new int[] {1, 2, 3}, new string[] {"Fixed Amount", "Percent with Threshold", "Percent with Min. Amount"})]
  [PXUIField(DisplayName = "Charging Method")]
  public virtual int? ChargingMethod
  {
    get
    {
      bool? nullable;
      if (this.MinFinChargeFlag.GetValueOrDefault())
      {
        nullable = this.PercentFlag;
        bool flag = false;
        if (nullable.GetValueOrDefault() == flag & nullable.HasValue)
          return new int?(1);
      }
      nullable = this.MinFinChargeFlag;
      bool flag1 = false;
      if (nullable.GetValueOrDefault() == flag1 & nullable.HasValue)
      {
        nullable = this.PercentFlag;
        if (nullable.GetValueOrDefault())
          return new int?(2);
      }
      nullable = this.MinFinChargeFlag;
      if (nullable.GetValueOrDefault())
      {
        nullable = this.PercentFlag;
        if (nullable.GetValueOrDefault())
          return new int?(3);
      }
      return new int?();
    }
    set
    {
      if (!value.HasValue)
        return;
      switch (value.GetValueOrDefault())
      {
        case 1:
          this.MinFinChargeFlag = new bool?(true);
          this.PercentFlag = new bool?(false);
          break;
        case 2:
          this.MinFinChargeFlag = new bool?(false);
          this.PercentFlag = new bool?(true);
          break;
        case 3:
          this.MinFinChargeFlag = new bool?(true);
          this.PercentFlag = new bool?(true);
          break;
      }
    }
  }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  [PXNote]
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

  public class PK : PrimaryKeyOf<ARFinCharge>.By<ARFinCharge.finChargeID>
  {
    public static ARFinCharge Find(PXGraph graph, string finChargeID, PKFindOptions options = 0)
    {
      return (ARFinCharge) PrimaryKeyOf<ARFinCharge>.By<ARFinCharge.finChargeID>.FindBy(graph, (object) finChargeID, options);
    }
  }

  public static class FK
  {
    public class Terms : 
      PrimaryKeyOf<PX.Objects.CS.Terms>.By<PX.Objects.CS.Terms.termsID>.ForeignKeyOf<ARFinCharge>.By<ARFinCharge.termsID>
    {
    }

    public class TaxCategory : 
      PrimaryKeyOf<PX.Objects.TX.TaxCategory>.By<PX.Objects.TX.TaxCategory.taxCategoryID>.ForeignKeyOf<ARFinCharge>.By<ARFinCharge.taxCategoryID>
    {
    }

    public class OverdueChargeAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<ARFinCharge>.By<ARFinCharge.finChargeAccountID>
    {
    }

    public class OverdueChargeSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<ARFinCharge>.By<ARFinCharge.finChargeSubID>
    {
    }

    public class FeeAccount : 
      PrimaryKeyOf<PX.Objects.GL.Account>.By<PX.Objects.GL.Account.accountID>.ForeignKeyOf<ARFinCharge>.By<ARFinCharge.feeAccountID>
    {
    }

    public class FeeSubaccount : 
      PrimaryKeyOf<PX.Objects.GL.Sub>.By<PX.Objects.GL.Sub.subID>.ForeignKeyOf<ARFinCharge>.By<ARFinCharge.feeSubID>
    {
    }
  }

  public abstract class finChargeID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARFinCharge.finChargeID>
  {
  }

  public abstract class finChargeDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARFinCharge.finChargeDesc>
  {
  }

  public abstract class termsID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARFinCharge.termsID>
  {
  }

  public abstract class baseCurFlag : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARFinCharge.baseCurFlag>
  {
  }

  public abstract class minFinChargeFlag : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    ARFinCharge.minFinChargeFlag>
  {
  }

  public abstract class minFinChargeAmount : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARFinCharge.minFinChargeAmount>
  {
  }

  public abstract class lineThreshold : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARFinCharge.lineThreshold>
  {
  }

  public abstract class fixedAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARFinCharge.fixedAmount>
  {
  }

  public abstract class minChargeDocumentAmt : 
    BqlType<
    #nullable enable
    IBqlDecimal, Decimal>.Field<
    #nullable disable
    ARFinCharge.minChargeDocumentAmt>
  {
  }

  public abstract class percentFlag : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARFinCharge.percentFlag>
  {
  }

  public abstract class finChargeAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARFinCharge.finChargeAccountID>
  {
  }

  public abstract class finChargeSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARFinCharge.finChargeSubID>
  {
  }

  public abstract class taxCategoryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARFinCharge.taxCategoryID>
  {
  }

  public abstract class feeAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARFinCharge.feeAccountID>
  {
  }

  public abstract class feeSubID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARFinCharge.feeSubID>
  {
  }

  public abstract class feeAmount : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARFinCharge.feeAmount>
  {
  }

  public abstract class feeDesc : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARFinCharge.feeDesc>
  {
  }

  public abstract class calculationMethod : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARFinCharge.calculationMethod>
  {
  }

  public abstract class chargingMethod : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARFinCharge.chargingMethod>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARFinCharge.Tstamp>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARFinCharge.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARFinCharge.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARFinCharge.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARFinCharge.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    ARFinCharge.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARFinCharge.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARFinCharge.lastModifiedDateTime>
  {
  }
}
