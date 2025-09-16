// Decompiled with JetBrains decompiler
// Type: PX.Data.Licensing.BAccount
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Data.Licensing;

[PXHidden]
[Serializable]
public class BAccount : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBInt]
  public virtual int? BAccountID { get; set; }

  [PXDBInt]
  public virtual int? ParentBAccountID { get; set; }

  [PXDBInt]
  public virtual int? DefContactID { get; set; }

  [PXDBInt]
  public virtual int? DefAddressID { get; set; }

  public abstract class bAccountID : BqlType<IBqlInt, int>.Field<
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

  public abstract class defAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  BAccount.defAddressID>
  {
  }
}
