// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CACorpCard
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CS;
using PX.Objects.GL;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXCacheName("Corporate Card")]
[PXPrimaryGraph(typeof (CACorpCardsMaint))]
[Serializable]
public class CACorpCard : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  [PXDefault]
  [PXUIField(DisplayName = "Corporate Card ID")]
  [PXReferentialIntegrityCheck]
  public virtual int? CorpCardID { get; set; }

  [PXDBString(30, IsKey = true, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Corporate Card ID", Required = true)]
  [PXSelector(typeof (Search<CACorpCard.corpCardCD>), new Type[] {typeof (CACorpCard.corpCardCD), typeof (CACorpCard.name), typeof (CACorpCard.cardNumber), typeof (CACorpCard.cashAccountID)})]
  [AutoNumber(typeof (Search<CASetup.corpCardNumberingID>), typeof (AccessInfo.businessDate))]
  public virtual 
  #nullable disable
  string CorpCardCD { get; set; }

  [PXDBString(100, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Name")]
  public virtual string Name { get; set; }

  [PXDBString(20, IsUnicode = true, InputMask = "")]
  [PXDefault]
  [PXUIField(DisplayName = "Card Number")]
  public virtual string CardNumber { get; set; }

  [CashAccount(null, typeof (Search<CashAccount.cashAccountID, Where<CashAccount.useForCorpCard, Equal<True>, And<CashAccount.branchID, Equal<Current<CACorpCard.branchID>>>>>))]
  [PXDefault]
  public virtual int? CashAccountID { get; set; }

  [Branch(null, null, true, true, true)]
  public virtual int? BranchID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXDBTimestamp]
  public virtual byte[] Tstamp { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On")]
  public virtual DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  [PXUIField(DisplayName = "Last Modified On")]
  public virtual DateTime? LastModifiedDateTime { get; set; }

  [PXNote]
  public virtual Guid? Noteid { get; set; }

  public class PK : PrimaryKeyOf<CACorpCard>.By<CACorpCard.corpCardID>
  {
    public static CACorpCard Find(PXGraph graph, int? corpCardID, PKFindOptions options = 0)
    {
      return (CACorpCard) PrimaryKeyOf<CACorpCard>.By<CACorpCard.corpCardID>.FindBy(graph, (object) corpCardID, options);
    }
  }

  public class UK : PrimaryKeyOf<CACorpCard>.By<CACorpCard.corpCardCD>
  {
    public static CACorpCard Find(PXGraph graph, string corpCardCD, PKFindOptions options = 0)
    {
      return (CACorpCard) PrimaryKeyOf<CACorpCard>.By<CACorpCard.corpCardCD>.FindBy(graph, (object) corpCardCD, options);
    }
  }

  public static class FK
  {
    public class CashAccount : 
      PrimaryKeyOf<CashAccount>.By<CashAccount.cashAccountID>.ForeignKeyOf<CACorpCard>.By<CACorpCard.cashAccountID>
    {
    }
  }

  [Obsolete("This foreign key is obsolete and is going to be removed in 2021R1. Use PK instead.")]
  public class PKID : CACorpCard.PK
  {
  }

  [Obsolete("This foreign key is obsolete and is going to be removed in 2021R1. Use UK instead.")]
  public class PKCD : CACorpCard.UK
  {
  }

  public abstract class corpCardID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CACorpCard.corpCardID>
  {
  }

  public abstract class corpCardCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CACorpCard.corpCardCD>
  {
  }

  public abstract class name : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CACorpCard.name>
  {
  }

  public abstract class cardNumber : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  CACorpCard.cardNumber>
  {
  }

  public abstract class cashAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CACorpCard.cashAccountID>
  {
  }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CACorpCard.branchID>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  CACorpCard.isActive>
  {
  }

  public abstract class tstamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  CACorpCard.tstamp>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CACorpCard.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CACorpCard.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CACorpCard.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CACorpCard.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CACorpCard.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CACorpCard.lastModifiedDateTime>
  {
  }

  public abstract class noteid : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CACorpCard.noteid>
  {
  }
}
