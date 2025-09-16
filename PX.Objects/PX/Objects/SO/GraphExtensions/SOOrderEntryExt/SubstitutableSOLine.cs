// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.GraphExtensions.SOOrderEntryExt.SubstitutableSOLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.IN.RelatedItems;

#nullable enable
namespace PX.Objects.SO.GraphExtensions.SOOrderEntryExt;

public sealed class SubstitutableSOLine : PXCacheExtension<
#nullable disable
SOLine>, ISubstitutableLineExt
{
  public static bool IsActive() => AddRelatedItemsToOrder.IsActive();

  [PXBool]
  public bool? SuggestRelatedItems { get; set; }

  [PXString]
  public string RelatedItems { get; set; }

  [PXInt]
  public int? RelatedItemsRelation { get; set; }

  [PXInt]
  public int? RelatedItemsRequired { get; set; }

  [PXInt]
  [PXDBDefault(typeof (RelatedItemHistory.lineID))]
  public int? HistoryLineID { get; set; }

  public abstract class suggestRelatedItems : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SubstitutableSOLine.suggestRelatedItems>
  {
  }

  public abstract class relatedItems : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SubstitutableSOLine.relatedItems>
  {
  }

  public abstract class relatedItemsRelation : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SubstitutableSOLine.relatedItemsRelation>
  {
  }

  public abstract class relatedItemsRequired : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SubstitutableSOLine.relatedItemsRequired>
  {
  }

  public abstract class historyLineID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    SubstitutableSOLine.historyLineID>
  {
  }
}
