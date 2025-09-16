// Decompiled with JetBrains decompiler
// Type: PX.Objects.CC.DefaultTerminal
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using PX.Objects.CA;
using PX.SM;
using System;

#nullable enable
namespace PX.Objects.CC;

/// <summary>
/// Represents default POS Terminal of the User in the Branch
/// </summary>
[PXCacheName("Default POS Terminal")]
[Serializable]
public class DefaultTerminal : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  /// <summary>
  /// The identifier of the <see cref="T:PX.SM.Users">Users</see> to be used for the employee to sign into the system.
  /// </summary>
  /// <value>
  /// Corresponds to the value of the <see cref="P:PX.SM.Users.PKID">Users.PKID</see> field.
  /// </value>
  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (Search<Users.pKID, Where<Users.pKID, Equal<Current<AccessInfo.userID>>>>))]
  public virtual Guid? UserID { get; set; }

  /// <summary>Branch ID</summary>
  [PXDBInt(IsKey = true)]
  public virtual int? BranchID { get; set; }

  /// <summary>Processing Center ID</summary>
  [PXDBString(10, IsUnicode = true, IsKey = true)]
  [PXDBDefault(typeof (CCProcessingCenter.processingCenterID))]
  [PXUIField]
  public virtual string? ProcessingCenterID { get; set; }

  /// <summary>Terminal ID</summary>
  [PXDBString(36, IsUnicode = true)]
  [PXDefault]
  [PXUIField]
  public virtual string? TerminalID { get; set; }

  public class PK : 
    PrimaryKeyOf<DefaultTerminal>.By<DefaultTerminal.userID, DefaultTerminal.branchID, DefaultTerminal.processingCenterID>
  {
    public static DefaultTerminal Find(
      PXGraph graph,
      Guid? userID,
      int? branchID,
      string processingCenterID)
    {
      return (DefaultTerminal) PrimaryKeyOf<DefaultTerminal>.By<DefaultTerminal.userID, DefaultTerminal.branchID, DefaultTerminal.processingCenterID>.FindBy(graph, (object) userID, (object) branchID, (object) processingCenterID, (PKFindOptions) 0);
    }
  }

  public abstract class userID : BqlType<IBqlGuid, Guid>.Field<DefaultTerminal.userID>
  {
  }

  public abstract class branchID : BqlType<IBqlInt, int>.Field<DefaultTerminal.branchID>
  {
  }

  public abstract class processingCenterID : 
    BqlType<IBqlString, string>.Field<DefaultTerminal.processingCenterID>
  {
  }

  public abstract class terminalID : BqlType<IBqlString, string>.Field<DefaultTerminal.terminalID>
  {
  }
}
