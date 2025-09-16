// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Contact2
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

[CRContactCacheName("Contact")]
[Serializable]
public class Contact2 : Contact
{
  [PXDBInt]
  [PXUIField(DisplayName = "Business Account")]
  [PXSelector(typeof (BAccount.bAccountID), SubstituteKey = typeof (BAccount.acctCD))]
  public override int? BAccountID { get; set; }

  public new abstract class contactID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  Contact2.contactID>
  {
  }

  public new abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Contact2.bAccountID>
  {
  }

  public new abstract class duplicateStatus : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    Contact2.duplicateStatus>
  {
  }

  public new abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact2.status>
  {
  }

  public new abstract class contactType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Contact2.contactType>
  {
  }

  public new abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Contact2.isActive>
  {
  }

  public new abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  Contact2.userID>
  {
  }
}
