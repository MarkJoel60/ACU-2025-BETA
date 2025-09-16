// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CABankFeedUser
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using System;

#nullable enable
namespace PX.Objects.CA;

[PXHidden]
public class CABankFeedUser : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(50, IsUnicode = true, IsKey = true)]
  public virtual 
  #nullable disable
  string ExternalUserID { get; set; }

  [PXDBInt]
  public int? OrganizationID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual DateTime? CreatedDateTime { get; set; }

  public class PK : PrimaryKeyOf<CABankFeedUser>.By<CABankFeedUser.externalUserID>
  {
    public static CABankFeedUser Find(PXGraph graph, string extUser, PKFindOptions options = 0)
    {
      return (CABankFeedUser) PrimaryKeyOf<CABankFeedUser>.By<CABankFeedUser.externalUserID>.FindBy(graph, (object) extUser, options);
    }
  }

  public abstract class externalUserID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankFeedUser.externalUserID>
  {
  }

  public abstract class organizationID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  CABankFeedUser.organizationID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  CABankFeedUser.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CABankFeedUser.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CABankFeedUser.createdDateTime>
  {
  }
}
