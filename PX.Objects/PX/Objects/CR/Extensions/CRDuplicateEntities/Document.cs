// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRDuplicateEntities.Document
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR.Extensions.CRDuplicateEntities;

/// <exclude />
[PXHidden]
public class Document : PXMappedCacheExtension
{
  public virtual int? Key { get; set; }

  public virtual int? DefContactID { get; set; }

  public virtual int? DefAddressID { get; set; }

  public abstract class key : BqlType<IBqlInt, int>.Field<
  #nullable disable
  Document.key>
  {
  }

  public abstract class defContactID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.defContactID>
  {
  }

  public abstract class defAddressID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document.defAddressID>
  {
  }
}
