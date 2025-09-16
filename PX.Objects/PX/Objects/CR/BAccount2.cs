// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BAccount2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CR;

/// <exclude />
[Serializable]
public sealed class BAccount2 : BAccount
{
  public new class PK : PrimaryKeyOf<
  #nullable disable
  BAccount2>.By<BAccount2.bAccountID>
  {
    public static BAccount2 Find(PXGraph graph, int bAccountID, PKFindOptions options = 0)
    {
      return (BAccount2) PrimaryKeyOf<BAccount2>.By<BAccount2.bAccountID>.FindBy(graph, (object) bAccountID, options);
    }
  }

  public new class UK : PrimaryKeyOf<BAccount2>.By<BAccount2.acctCD>
  {
    public static BAccount2 Find(PXGraph graph, string acctCD, PKFindOptions options = 0)
    {
      return (BAccount2) PrimaryKeyOf<BAccount2>.By<BAccount2.acctCD>.FindBy(graph, (object) acctCD, options);
    }
  }

  public new static class FK
  {
    public class ParentBAccount : 
      PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.ForeignKeyOf<BAccount2>.By<BAccount2.parentBAccountID>
    {
    }

    public class DefaultContact : 
      PrimaryKeyOf<Contact>.By<Contact.contactID>.ForeignKeyOf<BAccount2>.By<BAccount2.defContactID>
    {
    }

    public class DefaultLocation : 
      PrimaryKeyOf<Location>.By<Location.bAccountID, Location.locationID>.ForeignKeyOf<BAccount2>.By<BAccount2.bAccountID, BAccount2.defLocationID>
    {
    }
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount2.bAccountID>
  {
  }

  public new abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount2.acctCD>
  {
  }

  public new abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount2.acctName>
  {
  }

  public new abstract class acctReferenceNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    BAccount2.acctReferenceNbr>
  {
  }

  public new abstract class parentBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BAccount2.parentBAccountID>
  {
  }

  public new abstract class ownerID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount2.ownerID>
  {
  }

  public new abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount2.type>
  {
  }

  public new abstract class defContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount2.defContactID>
  {
  }

  public new abstract class defLocationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount2.defLocationID>
  {
  }

  public new abstract class isBranch : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  BAccount2.isBranch>
  {
  }

  public new abstract class cOrgBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount2.cOrgBAccountID>
  {
  }

  public new abstract class vOrgBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount2.vOrgBAccountID>
  {
  }

  public new abstract class groupMask : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount2.groupMask>
  {
  }
}
