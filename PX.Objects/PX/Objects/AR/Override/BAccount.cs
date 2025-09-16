// Decompiled with JetBrains decompiler
// Type: PX.Objects.AR.Override.BAccount
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.AR.Override;

[Serializable]
public class BAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt(IsKey = true)]
  public virtual int? BAccountID { get; set; }

  [PXDBString(60, IsUnicode = true)]
  public virtual 
  #nullable disable
  string AcctName { get; set; }

  [PXDBBool]
  public virtual bool? ConsolidateToParent { get; set; }

  [PXDBInt]
  public virtual int? ParentBAccountID { get; set; }

  [PXDBInt]
  public virtual int? ConsolidatingBAccountID { get; set; }

  [PXDBString(5, IsUnicode = true)]
  public virtual string BaseCuryID { get; set; }

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

  public abstract class consolidateToParent : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    BAccount.consolidateToParent>
  {
  }

  public abstract class parentBAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount.parentBAccountID>
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

  public abstract class baseCuryID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BAccount.baseCuryID>
  {
  }
}
