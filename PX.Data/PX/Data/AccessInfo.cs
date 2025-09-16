// Decompiled with JetBrains decompiler
// Type: PX.Data.AccessInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.SM;
using System;

#nullable enable
namespace PX.Data;

/// <exclude />
[Serializable]
public class AccessInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool _CuryViewState;

  [PXDBGuid(false)]
  public virtual Guid UserID { get; set; }

  [PXDBInt]
  public virtual int? ContactID { get; set; }

  [PXDBString(256 /*0x0100*/, IsUnicode = true)]
  [PXSelector(typeof (Search<Users.username, Where<Users.isHidden, Equal<False>>>))]
  public virtual 
  #nullable disable
  string UserName { get; set; }

  [PXString(IsUnicode = true)]
  public virtual string DisplayName { get; set; }

  [PXString(IsUnicode = true)]
  public virtual string CompanyName { get; set; }

  [PXDBDate]
  public virtual System.DateTime? BusinessDate { get; set; }

  [PXDBString(5, IsUnicode = true)]
  public virtual string BaseCuryID { get; set; }

  [PXDBString(8, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  public virtual string ScreenID { get; set; }

  [PXDBBool]
  public virtual bool CuryViewState { get; set; }

  [PXDBInt]
  public virtual int? CuryRateID { get; set; }

  [PXDBInt]
  [PXSelector(typeof (Search<PX.SM.Branch.branchID>), SubstituteKey = typeof (PX.SM.Branch.branchCD))]
  public virtual int? BranchID { get; set; }

  [PXDBInt]
  public virtual int? PortalID { get; set; }

  [PXDBInt]
  public virtual int? BAccountID { get; set; }

  /// <exclude />
  public abstract class userID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  AccessInfo.userID>
  {
  }

  /// <exclude />
  public abstract class contactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AccessInfo.contactID>
  {
  }

  /// <exclude />
  public abstract class userName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AccessInfo.userName>
  {
  }

  /// <exclude />
  public abstract class displayName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AccessInfo.displayName>
  {
  }

  /// <exclude />
  public abstract class companyName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AccessInfo.companyName>
  {
  }

  /// <exclude />
  public abstract class businessDate : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  AccessInfo.businessDate>
  {
  }

  /// <exclude />
  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AccessInfo.baseCuryID>
  {
  }

  /// <exclude />
  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AccessInfo.screenID>
  {
  }

  /// <exclude />
  public abstract class curyViewState : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AccessInfo.curyViewState>
  {
  }

  /// <exclude />
  public abstract class curyRateID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AccessInfo.curyRateID>
  {
  }

  /// <exclude />
  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AccessInfo.branchID>
  {
  }

  /// <exclude />
  public abstract class portalID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AccessInfo.portalID>
  {
  }

  /// <exclude />
  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AccessInfo.bAccountID>
  {
  }
}
