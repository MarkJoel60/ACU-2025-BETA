// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.Relational.Child`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR.Extensions.Relational;

[PXHidden]
public abstract class Child<T> : PXMappedCacheExtension where T : 
#nullable disable
PXGraphExtension
{
  public virtual int? ChildID { get; set; }

  public virtual int? RelatedID { get; set; }

  public abstract class childID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Child<T>.childID>
  {
  }

  public abstract class relatedID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  Child<T>.relatedID>
  {
  }
}
