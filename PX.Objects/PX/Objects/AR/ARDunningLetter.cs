// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.ARDunningLetter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.AR;

/// <summary>Header of Dunning Letter</summary>
[PXPrimaryGraph(typeof (ARDunningLetterUpdate))]
[PXCacheName("Dunning Letter")]
[Serializable]
public class ARDunningLetter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity(IsKey = true)]
  [PXUIField]
  [PXSelector(typeof (ARDunningLetter.dunningLetterID), new Type[] {typeof (ARDunningLetter.dunningLetterID), typeof (ARDunningLetter.branchID_Branch_branchCD), typeof (ARDunningLetter.bAccountID_Customer_acctCD), typeof (ARDunningLetter.dunningLetterDate), typeof (ARDunningLetter.dunningLetterLevel), typeof (ARDunningLetter.deadline)})]
  public virtual int? DunningLetterID { get; set; }

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Customer", IsReadOnly = true)]
  [PXSelector(typeof (Search<Customer.bAccountID, Where<Customer.bAccountID, Equal<Current<ARDunningLetter.bAccountID>>>>), DescriptionField = typeof (Customer.acctCD), ValidateValue = false)]
  public virtual int? BAccountID { get; set; }

  [PXDBDate]
  [PXDefault(TypeCode.DateTime, "01/01/1900")]
  [PXUIField(DisplayName = "Dunning Letter Date", IsReadOnly = true)]
  public virtual DateTime? DunningLetterDate { get; set; }

  [PXDBDate]
  [PXDefault(TypeCode.DateTime, "01/01/1900")]
  [PXUIField(DisplayName = "Deadline")]
  public virtual DateTime? Deadline { get; set; }

  [PXDBInt]
  [PXDefault]
  [PXUIField(DisplayName = "Dunning Letter Level", IsReadOnly = true)]
  public virtual int? DunningLetterLevel { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Printed { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Don't Print")]
  public virtual bool? DontPrint { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Released { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Voided { get; set; }

  [PXString(1, IsFixed = true)]
  [PXDefault("D")]
  [PXUIField]
  [PXStringList(new string[] {"D", "R", "V"}, new string[] {"Draft", "Released", "Voided"})]
  public virtual 
  #nullable disable
  string Status
  {
    get
    {
      if (this.Voided.GetValueOrDefault())
        return "V";
      return this.Released.GetValueOrDefault() ? "R" : "D";
    }
    set
    {
    }
  }

  [PXInt]
  [PXUIField(DisplayName = "Number of Documents")]
  public virtual int? DetailsCount { get; set; }

  [PXDBString(3, IsFixed = true)]
  [ARDocType.List]
  [PXUIField(DisplayName = "Fee Type")]
  public virtual string FeeDocType { get; set; }

  [PXDBString(15, IsUnicode = true)]
  [PXSelector(typeof (Search<ARInvoice.refNbr, Where<ARInvoice.docType, Equal<Current<ARDunningLetter.feeDocType>>, And<ARInvoice.refNbr, Equal<Current<ARDunningLetter.feeRefNbr>>>>>), ValidateValue = false)]
  [PXUIField(DisplayName = "Fee Reference Nbr.", Enabled = false)]
  public virtual string FeeRefNbr { get; set; }

  [PXDBDecimal(MinValue = 0.0)]
  [PXDefault(TypeCode.Decimal, "0.0")]
  [PXUIField(DisplayName = "Dunning Fee")]
  public virtual Decimal? DunningFee { get; set; }

  [PXString(5, IsUnicode = true)]
  [PXUIField]
  [PXFormula(typeof (Selector<ARDunningLetter.branchID, PX.Objects.GL.Branch.baseCuryID>))]
  public virtual string CuryID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? Emailed { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Don't Email")]
  public virtual bool? DontEmail { get; set; }

  [PXDBString(1, IsFixed = true)]
  [PXDefault("C")]
  public virtual string ConsolidationSettings { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? LastLevel { get; set; }

  [PXDBTimestamp]
  public virtual byte[] tstamp { get; set; }

  public class PK : 
    PrimaryKeyOf<ARDunningLetter>.By<ARDunningLetter.dunningLetterID, ARDunningLetter.branchID>
  {
    public static ARDunningLetter Find(
      PXGraph graph,
      int? dunningLetterID,
      int? branchID,
      PKFindOptions options = 0)
    {
      return (ARDunningLetter) PrimaryKeyOf<ARDunningLetter>.By<ARDunningLetter.dunningLetterID, ARDunningLetter.branchID>.FindBy(graph, (object) dunningLetterID, (object) branchID, options);
    }
  }

  public static class FK
  {
    public class Branch : 
      PrimaryKeyOf<PX.Objects.GL.Branch>.By<PX.Objects.GL.Branch.branchID>.ForeignKeyOf<ARDunningLetter>.By<ARDunningLetter.branchID>
    {
    }

    public class Customer : 
      PrimaryKeyOf<Customer>.By<Customer.bAccountID>.ForeignKeyOf<ARDunningLetter>.By<ARDunningLetter.bAccountID>
    {
    }
  }

  public abstract class dunningLetterID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARDunningLetter.dunningLetterID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARDunningLetter.branchID>
  {
  }

  public abstract class branchID_Branch_branchCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARDunningLetter.branchID_Branch_branchCD>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARDunningLetter.bAccountID>
  {
  }

  public abstract class bAccountID_Customer_acctCD : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    ARDunningLetter.bAccountID_Customer_acctCD>
  {
  }

  public abstract class dunningLetterDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    ARDunningLetter.dunningLetterDate>
  {
  }

  public abstract class deadline : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  ARDunningLetter.deadline>
  {
  }

  public abstract class dunningLetterLevel : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    ARDunningLetter.dunningLetterLevel>
  {
  }

  public abstract class printed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARDunningLetter.printed>
  {
  }

  public abstract class dontPrint : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARDunningLetter.dontPrint>
  {
  }

  public abstract class released : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARDunningLetter.released>
  {
  }

  public abstract class voided : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARDunningLetter.voided>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARDunningLetter.status>
  {
  }

  public abstract class detailsCount : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  ARDunningLetter.detailsCount>
  {
  }

  public abstract class feeDocType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARDunningLetter.feeDocType>
  {
  }

  public abstract class feeRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARDunningLetter.feeRefNbr>
  {
  }

  public abstract class dunningFee : BqlType<
  #nullable enable
  IBqlDecimal, Decimal>.Field<
  #nullable disable
  ARDunningLetter.dunningFee>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ARDunningLetter.curyID>
  {
  }

  public abstract class emailed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARDunningLetter.emailed>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  ARDunningLetter.noteID>
  {
  }

  public abstract class dontEmail : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARDunningLetter.dontEmail>
  {
  }

  public abstract class consolidationSettings : 
    BqlType<
    #nullable enable
    IBqlShort, short>.Field<
    #nullable disable
    ARDunningLetter.consolidationSettings>
  {
  }

  public abstract class lastLevel : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  ARDunningLetter.lastLevel>
  {
  }

  public abstract class Tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  ARDunningLetter.Tstamp>
  {
  }
}
