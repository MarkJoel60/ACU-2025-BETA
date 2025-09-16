// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.BqlPlaceholder
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.BQL;

/// <summary>
/// A BQL placeholder. You can use these placeholders to compose queries that have a constant structure,
/// but also have gaps that cannot be filled at design time because particular types will be known only at run time.
/// </summary>
/// <remarks>
///   <para>You can use 26 predefined placeholders, which has names from A to Z, in your code.</para>
///   <para>The <see cref="T:PX.Data.BQL.IBqlPlaceholder" /> class implements all BQL-specific interfaces.
///   Therefore its descendants can be placed in any part of a BQL query.
///   However only placeholders that inherit from <see cref="T:PX.Data.BQL.BqlPlaceholder.Named`1" />
///   are fully compatible with fluent BQL.</para>
/// </remarks>
/// <example>
///   <code>//The following example shows the use of placeholders with custom names.
/// private sealed class PhInventory : BqlPlaceholder.Named&lt;PhInventory&gt; {}
/// private sealed class PhSubItem : BqlPlaceholder.Named&lt;PhSubItem&gt; {}
/// private sealed class PhLocation : BqlPlaceholder.Named&lt;PhLocation&gt; {}
/// 
/// public Type BuildLotSerialNbrSearch(Type InventoryType, Type SubItemType, Type LocationType)
/// {
///     Type searchType = BqlTemplate.OfCommand&lt;
///         Search2&lt;INLotSerialStatus.lotSerialNbr,
///         InnerJoin&lt;INSiteLotSerial, On&lt;INLotSerialStatus.inventoryID, Equal&lt;INSiteLotSerial.inventoryID&gt;,
///             And&lt;INLotSerialStatus.siteID, Equal&lt;INSiteLotSerial.siteID&gt;,
///             And&lt;INLotSerialStatus.lotSerialNbr, Equal&lt;INSiteLotSerial.lotSerialNbr&gt;&gt;&gt;&gt;&gt;,
///         Where&lt;INLotSerialStatus.inventoryID, Equal&lt;Optional&lt;PhInventory&gt;&gt;,
///             And&lt;INLotSerialStatus.subItemID, Equal&lt;Optional&lt;PhSubItem&gt;&gt;,
///             And2&lt;Where&lt;INLotSerialStatus.locationID, Equal&lt;Optional&lt;PhLocation&gt;&gt;,
///                 Or&lt;Optional&lt;PhLocation&gt;, IsNull&gt;&gt;,
///             And&lt;INLotSerialStatus.qtyOnHand, Greater&lt;decimal0&gt;&gt;&gt;&gt;&gt;&gt;
///         &gt;
///         .Replace&lt;PhInventory&gt;(InventoryType)
///         .Replace&lt;PhSubItem&gt;(SubItemType)
///         .Replace&lt;PhLocation&gt;(LocationType)
///         .ToType();
///     return searchType;
/// }</code>
///   <code>//The following example shows the use of predefined placeholders.
/// public Type BuildLotSerialNbrSearch(Type InventoryType, Type SubItemType, Type LocationType)
/// {
///     Type searchType = BqlTemplate.OfCommand&lt;
///         Search2&lt;INLotSerialStatus.lotSerialNbr,
///         InnerJoin&lt;INSiteLotSerial, On&lt;INLotSerialStatus.inventoryID, Equal&lt;INSiteLotSerial.inventoryID&gt;,
///             And&lt;INLotSerialStatus.siteID, Equal&lt;INSiteLotSerial.siteID&gt;,
///             And&lt;INLotSerialStatus.lotSerialNbr, Equal&lt;INSiteLotSerial.lotSerialNbr&gt;&gt;&gt;&gt;&gt;,
///         Where&lt;INLotSerialStatus.inventoryID, Equal&lt;Optional&lt;BqlPlaceholder.A&gt;&gt;,
///             And&lt;INLotSerialStatus.subItemID, Equal&lt;Optional&lt;BqlPlaceholder.B&gt;&gt;,
///             And2&lt;Where&lt;INLotSerialStatus.locationID, Equal&lt;Optional&lt;BqlPlaceholder.C&gt;&gt;,
///                 Or&lt;Optional&lt;BqlPlaceholder.C&gt;, IsNull&gt;&gt;,
///             And&lt;INLotSerialStatus.qtyOnHand, Greater&lt;decimal0&gt;&gt;&gt;&gt;&gt;&gt;
///         &gt;
///         .Replace&lt;BqlPlaceholder.A&gt;(InventoryType)
///         .Replace&lt;BqlPlaceholder.B&gt;(SubItemType)
///         .Replace&lt;BqlPlaceholder.C&gt;(LocationType)
///         .ToType();
/// 
///     return searchType;
/// }</code>
///   <code>//The following example shows the use of placeholders in a fluent BQL query.
/// var command = BqlTemplate.OfCommand&lt;
///         SelectFrom&lt;BqlPlaceholder.A&gt;
///         .InnerJoin&lt;BqlPlaceholder.B&gt;.On&lt;BqlPlaceholder.C&gt;
///         .LeftJoin&lt;BqlPlaceholder.D&gt;.On&lt;BqlPlaceholder.E&gt;
///         .PlaceholderJoin&lt;BqlPlaceholder.F&gt;
///         .Where&lt;
///             Item.description.IsNull
///             .And&lt;BqlPlaceholder.G&gt;
///             .And&lt;
///                 BqlPlaceholder.H.AsCondition
///                 .Or&lt;Detail.cost.IsEqual&lt;BqlPlaceholder.I.AsOperand&gt;&gt;&gt;
///             .And&lt;BqlPlaceholder.J.AsField.FromCurrent.IsEqual&lt;BqlPlaceholder.K.AsOperand.Add&lt;Zero&gt;&gt;&gt;&gt;
///         .AggregateTo&lt;GroupBy&lt;BqlPlaceholder.L&gt;, Sum&lt;BqlPlaceholder.M&gt;&gt;
///         .Having&lt;BqlPlaceholder.M.AsField.Summarized.IsGreater&lt;Zero&gt;&gt;
///         .OrderBy&lt;BqlPlaceholder.L.AsField.Asc, BqlPlaceholder.M.AsField.Desc&gt;
///     &gt;
///     // Suppose that instances of the following types were obtained not here.
///     .Replace&lt;BqlPlaceholder.A&gt;(typeof(Document))
///     .Replace&lt;BqlPlaceholder.B&gt;(typeof(Detail))
///     .Replace&lt;BqlPlaceholder.C&gt;(typeof(Document.docType.IsEqual&lt;Detail.docType&gt;.And&lt;Document.documentID.IsEqual&lt;Detail.documentID&gt;&gt;))
///     .Replace&lt;BqlPlaceholder.D&gt;(typeof(Item))
///     .Replace&lt;BqlPlaceholder.E&gt;(typeof(Detail.itemID.IsEqual&lt;Item.itemID&gt;))
///     .Replace&lt;BqlPlaceholder.F&gt;(typeof(InnerJoin&lt;DocumentType, On&lt;DocumentType.docType.IsEqual&lt;Document.docType&gt;&gt;&gt;))
///     .Replace&lt;BqlPlaceholder.G&gt;(typeof(Item.itemID.IsNotNull))
///     .Replace&lt;BqlPlaceholder.H&gt;(typeof(Detail.qty.IsEqual&lt;Zero&gt;))
///     .Replace&lt;BqlPlaceholder.I&gt;(typeof(Zero))
///     .Replace&lt;BqlPlaceholder.J&gt;(typeof(DocumentType.intPreference))
///     .Replace&lt;BqlPlaceholder.K&gt;(typeof(Detail.lineNbr))
///     .Replace&lt;BqlPlaceholder.L&gt;(typeof(Document.documentID))
///     .Replace&lt;BqlPlaceholder.M&gt;(typeof(Detail.amount))
///     .ToType();</code>
/// </example>
public static class BqlPlaceholder
{
  public interface IBqlAny : 
    IBqlDataType,
    IBqlEquitable,
    IBqlComparable,
    IBqlInteger,
    IBqlNumeric,
    IBqlCastableTo<IBqlBool>,
    IBqlCastableTo<IBqlByte>,
    IBqlCastableTo<IBqlShort>,
    IBqlCastableTo<IBqlInt>,
    IBqlCastableTo<IBqlLong>,
    IBqlCastableTo<IBqlFloat>,
    IBqlCastableTo<IBqlDouble>,
    IBqlCastableTo<IBqlDecimal>,
    IBqlCastableTo<IBqlString>,
    IBqlCastableTo<IBqlDateTime>,
    IBqlCastableTo<IBqlGuid>,
    IBqlCastableTo<BqlPlaceholder.IBqlAny>
  {
  }

  [PXHidden]
  public class Named<TSelf> : BqlPlaceholderBase where TSelf : BqlPlaceholderBase, new()
  {
    public sealed class AsCondition : BqlChainableCondition<TSelf>
    {
    }

    public sealed class AsOperand : BqlOperand<TSelf, BqlPlaceholder.IBqlAny>
    {
    }

    public sealed class AsField : BqlField<TSelf, BqlPlaceholder.IBqlAny>
    {
    }
  }

  [PXHidden]
  public sealed class A : BqlPlaceholder.Named<BqlPlaceholder.A>
  {
  }

  [PXHidden]
  public sealed class B : BqlPlaceholder.Named<BqlPlaceholder.B>
  {
  }

  [PXHidden]
  public sealed class C : BqlPlaceholder.Named<BqlPlaceholder.C>
  {
  }

  [PXHidden]
  public sealed class D : BqlPlaceholder.Named<BqlPlaceholder.D>
  {
  }

  [PXHidden]
  public sealed class E : BqlPlaceholder.Named<BqlPlaceholder.E>
  {
  }

  [PXHidden]
  public sealed class F : BqlPlaceholder.Named<BqlPlaceholder.F>
  {
  }

  [PXHidden]
  public sealed class G : BqlPlaceholder.Named<BqlPlaceholder.G>
  {
  }

  [PXHidden]
  public sealed class H : BqlPlaceholder.Named<BqlPlaceholder.H>
  {
  }

  [PXHidden]
  public sealed class I : BqlPlaceholder.Named<BqlPlaceholder.I>
  {
  }

  [PXHidden]
  public sealed class J : BqlPlaceholder.Named<BqlPlaceholder.J>
  {
  }

  [PXHidden]
  public sealed class K : BqlPlaceholder.Named<BqlPlaceholder.K>
  {
  }

  [PXHidden]
  public sealed class L : BqlPlaceholder.Named<BqlPlaceholder.L>
  {
  }

  [PXHidden]
  public sealed class M : BqlPlaceholder.Named<BqlPlaceholder.M>
  {
  }

  [PXHidden]
  public sealed class N : BqlPlaceholder.Named<BqlPlaceholder.N>
  {
  }

  [PXHidden]
  public sealed class O : BqlPlaceholder.Named<BqlPlaceholder.O>
  {
  }

  [PXHidden]
  public sealed class P : BqlPlaceholder.Named<BqlPlaceholder.P>
  {
  }

  [PXHidden]
  public sealed class Q : BqlPlaceholder.Named<BqlPlaceholder.Q>
  {
  }

  [PXHidden]
  public sealed class R : BqlPlaceholder.Named<BqlPlaceholder.R>
  {
  }

  [PXHidden]
  public sealed class S : BqlPlaceholder.Named<BqlPlaceholder.S>
  {
  }

  [PXHidden]
  public sealed class T : BqlPlaceholder.Named<BqlPlaceholder.T>
  {
  }

  [PXHidden]
  public sealed class U : BqlPlaceholder.Named<BqlPlaceholder.U>
  {
  }

  [PXHidden]
  public sealed class V : BqlPlaceholder.Named<BqlPlaceholder.V>
  {
  }

  [PXHidden]
  public sealed class W : BqlPlaceholder.Named<BqlPlaceholder.W>
  {
  }

  [PXHidden]
  public sealed class X : BqlPlaceholder.Named<BqlPlaceholder.X>
  {
  }

  [PXHidden]
  public sealed class Y : BqlPlaceholder.Named<BqlPlaceholder.Y>
  {
  }

  [PXHidden]
  public sealed class Z : BqlPlaceholder.Named<BqlPlaceholder.Z>
  {
  }
}
