// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BAccountParent
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.EP;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <summary>
/// The class derived from <see cref="T:PX.Objects.CR.BAccount" /> that is used in BQL queries
/// when it is needed to use the join operation for the <see cref="T:PX.Objects.CR.BAccount" /> table twice.
/// <see cref="T:PX.Objects.CR.BAccountParent" /> is used for the join clause that has the condition involving the parent ID
/// (for example, <see cref="P:PX.Objects.CR.Contact.ParentBAccountID">Contact.ParentBAccountID</see>,
/// <see cref="P:PX.Objects.CR.BAccount.ParentBAccountID">Contact.ParentBAccountID</see>).
/// </summary>
[PXCacheName("Parent Business Account")]
[CRCacheIndependentPrimaryGraphList(new System.Type[] {typeof (BusinessAccountMaint)}, new System.Type[] {typeof (Select<BAccountCRM, Where<BAccountCRM.bAccountID, Equal<Current<BAccountParent.bAccountID>>, Or<Current<BAccountParent.bAccountID>, Less<Zero>>>>)})]
[Serializable]
public sealed class BAccountParent : BAccount
{
  [PXDimensionSelector("BIZACCT", typeof (BAccount.acctCD), typeof (BAccount.acctCD))]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public override 
  #nullable disable
  string AcctCD
  {
    get => this._AcctCD;
    set => this._AcctCD = value;
  }

  [PXDBString(60, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  [PXFieldDescription]
  public override string AcctName
  {
    get => this._AcctName;
    set => this._AcctName = value;
  }

  public new class PK : PrimaryKeyOf<BAccountParent>.By<BAccountParent.bAccountID>
  {
    public static BAccountParent Find(PXGraph graph, int bAccountID, PKFindOptions options = 0)
    {
      return (BAccountParent) PrimaryKeyOf<BAccountParent>.By<BAccountParent.bAccountID>.FindBy(graph, (object) bAccountID, options);
    }
  }

  public new class UK : PrimaryKeyOf<BAccountParent>.By<BAccountParent.acctCD>
  {
    public static BAccountParent Find(PXGraph graph, string acctCD, PKFindOptions options = 0)
    {
      return (BAccountParent) PrimaryKeyOf<BAccountParent>.By<BAccountParent.acctCD>.FindBy(graph, (object) acctCD, options);
    }
  }

  public new static class FK
  {
    public class DefaultContact : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<BAccountParent>.By<BAccountParent.defContactID>
    {
    }

    public class DefaultLocation : 
      PrimaryKeyOf<Location>.By<Location.bAccountID, Location.locationID>.ForeignKeyOf<BAccountParent>.By<BAccountParent.bAccountID, BAccountParent.defLocationID>
    {
    }
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountParent.bAccountID>
  {
  }

  public new abstract class acctReferenceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BAccountParent.acctReferenceNbr>
  {
  }

  public new abstract class parentBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BAccountParent.parentBAccountID>
  {
  }

  public new abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountParent.ownerID>
  {
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountParent.type>
  {
  }

  public new abstract class defContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountParent.defContactID>
  {
  }

  public new abstract class defLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccountParent.defLocationID>
  {
  }

  public new abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountParent.acctCD>
  {
  }

  public new abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccountParent.acctName>
  {
  }
}
