// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.DAC.Unbound.InventoryFullTextSearchFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.IN.DAC.Unbound;

/// <exclude />
[PXHidden]
public class InventoryFullTextSearchFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString]
  public virtual 
  #nullable disable
  string ContainsSearchCondition { get; set; }

  [PXInt]
  public virtual int? Top { get; set; }

  [PXString]
  public virtual string Word1 { get; set; }

  [PXString]
  public virtual string Word2 { get; set; }

  [PXString]
  public virtual string Word3 { get; set; }

  [PXString]
  public virtual string Word4 { get; set; }

  [PXString]
  public virtual string Word5 { get; set; }

  [PXString]
  public virtual string Word6 { get; set; }

  public abstract class containsSearchCondition : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryFullTextSearchFilter.containsSearchCondition>
  {
  }

  public abstract class top : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  InventoryFullTextSearchFilter.top>
  {
  }

  public abstract class word1 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryFullTextSearchFilter.word1>
  {
  }

  public abstract class word2 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryFullTextSearchFilter.word2>
  {
  }

  public abstract class word3 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryFullTextSearchFilter.word3>
  {
  }

  public abstract class word4 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryFullTextSearchFilter.word4>
  {
  }

  public abstract class word5 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryFullTextSearchFilter.word5>
  {
  }

  public abstract class word6 : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    InventoryFullTextSearchFilter.word6>
  {
  }
}
