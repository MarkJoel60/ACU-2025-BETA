// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.Light.BAccount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.AP;
using PX.Objects.AR;
using System;

#nullable enable
namespace PX.Objects.CA.Light;

[Serializable]
public class BAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  public virtual int? BAccountID { get; set; }

  [PXDBString(60, IsUnicode = true)]
  public virtual 
  #nullable disable
  string AcctName { get; set; }

  [PXDBString(5, IsUnicode = true)]
  public virtual string CuryID { get; set; }

  [PXDBInt]
  public virtual int? ConsolidatingBAccountID { get; set; }

  [PXDimensionSelector("BIZACCT", typeof (BAccount.acctCD), typeof (BAccount.acctCD), DescriptionField = typeof (BAccount.acctName))]
  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  public virtual string AcctCD { get; set; }

  [PXDBString(1, IsFixed = true)]
  [CustomerStatus.BusinessAccountNonCustomerList]
  public virtual string Status { get; set; }

  [PXDBString(1, IsFixed = true)]
  [VendorStatus.List]
  public virtual string VStatus { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? NoteID { get; set; }

  /// <summary>Vendor Restriction Group.</summary>
  [PXDBInt]
  public virtual int? VOrgBAccountID { get; set; }

  /// <summary>Customer Restriction Group.</summary>
  [PXDBInt]
  public virtual int? COrgBAccountID { get; set; }

  public class PK : PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>
  {
    public static BAccount Find(PXGraph graph, int bAccountID, PKFindOptions options = 0)
    {
      return (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.bAccountID>.FindBy(graph, (object) bAccountID, options);
    }
  }

  public class UK : PrimaryKeyOf<BAccount>.By<BAccount.acctCD>
  {
    public static BAccount Find(PXGraph graph, string acctCD, PKFindOptions options = 0)
    {
      return (BAccount) PrimaryKeyOf<BAccount>.By<BAccount.acctCD>.FindBy(graph, (object) acctCD, options);
    }
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount.bAccountID>
  {
  }

  public abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.acctName>
  {
  }

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.curyID>
  {
  }

  public abstract class consolidatingBAccountID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    BAccount.consolidatingBAccountID>
  {
  }

  public abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.acctCD>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.status>
  {
  }

  public abstract class vStatus : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.vStatus>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  BAccount.noteID>
  {
  }

  public abstract class vOrgBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount.vOrgBAccountID>
  {
  }

  public abstract class cOrgBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount.cOrgBAccountID>
  {
  }
}
