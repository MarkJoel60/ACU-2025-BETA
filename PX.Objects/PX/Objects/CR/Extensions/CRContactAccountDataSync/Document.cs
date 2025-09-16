// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRContactAccountDataSync.Document
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR.Extensions.CRContactAccountDataSync;

/// <exclude />
[PXHidden]
[Obsolete("Not used anymore.")]
public class Document : PXMappedCacheExtension
{
  public virtual bool? OverrideRefContact { get; set; }

  public virtual int? RefContactID { get; set; }

  public virtual int? BAccountID { get; set; }

  public abstract class overrideRefContact : 
    BqlType<IBqlBool, bool>.Field<
    #nullable disable
    Document.overrideRefContact>
  {
  }

  public abstract class refContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.refContactID>
  {
  }

  public abstract class bAccountID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.bAccountID>
  {
  }
}
