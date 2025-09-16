// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.Email.BAccount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AP;
using System;

#nullable enable
namespace PX.Objects.CS.Email;

[PXCacheName("Customer")]
[PXHidden]
public class BAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt]
  [PXUIField]
  public int? BAccountID { get; set; }

  [PXDBInt]
  [PXUIField]
  public int? ParentBAccountID { get; set; }

  [PXDBInt]
  public int? DefContactID { get; set; }

  [PXDBString(60, IsUnicode = true)]
  [PXUIField]
  public virtual 
  #nullable disable
  string AcctName { get; set; }

  [PXDBString(30, IsUnicode = true, InputMask = "")]
  [PXUIField]
  public virtual string AcctCD { get; set; }

  /// <summary>
  /// <inheritdoc cref="P:PX.Objects.EP.EPEmployee.VStatus" />
  /// </summary>
  [PXDBString(1, IsFixed = true)]
  [VendorStatus.List]
  [PXUIField]
  public virtual string VStatus { get; set; }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount.bAccountID>
  {
  }

  public abstract class parentBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount.parentBAccountID>
  {
  }

  public abstract class defContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount.defContactID>
  {
  }

  public abstract class acctName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.acctName>
  {
  }

  public abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.acctCD>
  {
  }

  public abstract class vStatus : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  BAccount.vStatus>
  {
  }
}
