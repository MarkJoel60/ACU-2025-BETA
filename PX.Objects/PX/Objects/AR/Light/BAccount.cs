// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Light.BAccount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR.Light;

[PXHidden]
public class BAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBIdentity]
  public virtual int? BAccountID { get; set; }

  [PXUIField]
  [PXDBString(60, IsUnicode = true)]
  public virtual 
  #nullable disable
  string AcctName { get; set; }

  [PXDBInt]
  public virtual int? ConsolidatingBAccountID { get; set; }

  [PXDBString(30, IsUnicode = true, IsKey = true, InputMask = "")]
  [PXUIField]
  public virtual string AcctCD { get; set; }

  [PXDBString(5, IsUnicode = true)]
  public virtual string CuryID { get; set; }

  [PXDBString(1, IsFixed = true)]
  [CustomerStatus.List]
  public virtual string Status { get; set; }

  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Locale")]
  public virtual string LocaleName { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? NoteID { get; set; }

  [PXDBInt]
  [PXUIField]
  public virtual int? ParentBAccountID { get; set; }

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

  public abstract class curyID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.curyID>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.status>
  {
  }

  public abstract class localeName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.localeName>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  BAccount.noteID>
  {
  }

  public abstract class parentBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount.parentBAccountID>
  {
  }
}
