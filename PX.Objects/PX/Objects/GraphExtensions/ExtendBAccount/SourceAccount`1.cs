// Decompiled with JetBrains decompiler
// Type: PX.Objects.GraphExtensions.ExtendBAccount.SourceAccount`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GraphExtensions.ExtendBAccount;

public class SourceAccount<Extension> : PXMappedCacheExtension where Extension : 
#nullable disable
PXGraphExtension
{
  public virtual string AcctCD { get; set; }

  public virtual string Type { get; set; }

  public virtual string LocaleName { get; set; }

  public virtual Guid? NoteID { get; set; }

  public abstract class acctCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SourceAccount<Extension>.acctCD>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SourceAccount<Extension>.type>
  {
  }

  public abstract class localeName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SourceAccount<Extension>.localeName>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SourceAccount<Extension>.noteID>
  {
  }
}
