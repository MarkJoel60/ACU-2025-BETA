// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.Relational.Document`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR.Extensions.Relational;

[PXHidden]
public abstract class Document<T> : PXMappedCacheExtension where T : 
#nullable disable
PXGraphExtension
{
  public virtual int? RelatedID { get; set; }

  public virtual int? ChildID { get; set; }

  public virtual bool? IsOverrideRelated { get; set; }

  public abstract class relatedID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document<T>.relatedID>
  {
  }

  public abstract class childID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Document<T>.childID>
  {
  }

  public abstract class isOverrideRelated : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    Document<T>.isOverrideRelated>
  {
  }
}
