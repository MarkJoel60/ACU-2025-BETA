// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.Use`1
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.BQL;

/// <summary>A wrapper that allows you to use fluent comparisons with the operands that are not inherited from <see cref="T:PX.Data.BQL.BqlOperand`2" />.</summary>
/// <typeparam name="TLegacyOperand">An operand defined in the traditional BQL style, such as a constant derived from the Constant&lt;Type&gt; class.</typeparam>
/// <example>
/// The following code shows the declaration of the <tt>decimal_0</tt> constant in traditional BQL style and its use in a fluent BQL comparison.
/// <code>public class decimal_0 : Constant&lt;Decimal&gt;
/// {
///     public decimal_0()
///         : base(0m)
///     {
///     }
/// }
/// 
/// SelectFrom&lt;Table&gt;.
///     Where&lt;Table.decimalField.AsDecimal.IsEqual&lt;Use&lt;decimal_0&gt;.AsDecimal&gt;&gt;.
///     View records;</code></example>
public static class Use<TLegacyOperand> where TLegacyOperand : IBqlOperand
{
  /// <summary>
  /// Pretends that the <typeparamref name="TLegacyOperand" /> is of custom <typeparamref name="TBqlType" /> type.
  /// Changes the operand's type only in terms of BQL - it does not generate SQL CAST.
  /// </summary>
  public class As<TBqlType> : BqlCast<TLegacyOperand, TBqlType> where TBqlType : class, IBqlDataType
  {
  }

  /// <summary>
  /// Pretends that the <typeparamref name="TLegacyOperand" /> is of <see cref="T:PX.Data.BQL.IBqlBool" /> type.
  /// Changes the operand's type only in terms of BQL - it does not generate SQL CAST.
  /// </summary>
  public sealed class AsBool : Use<TLegacyOperand>.As<IBqlBool>
  {
  }

  /// <summary>
  /// Pretends that the <typeparamref name="TLegacyOperand" /> is of <see cref="T:PX.Data.BQL.IBqlGuid" /> type.
  /// Changes the operand's type only in terms of BQL - it does not generate SQL CAST.
  /// </summary>
  public sealed class AsGuid : Use<TLegacyOperand>.As<IBqlGuid>
  {
  }

  /// <summary>
  /// Pretends that the <typeparamref name="TLegacyOperand" /> is of <see cref="T:PX.Data.BQL.IBqlDateTime" /> type.
  /// Changes the operand's type only in terms of BQL - it does not generate SQL CAST.
  /// </summary>
  public sealed class AsDateTime : Use<TLegacyOperand>.As<IBqlDateTime>
  {
  }

  /// <summary>
  /// Pretends that the <typeparamref name="TLegacyOperand" /> is of <see cref="T:PX.Data.BQL.IBqlString" /> type.
  /// Changes the operand's type only in terms of BQL - it does not generate SQL CAST.
  /// </summary>
  public sealed class AsString : Use<TLegacyOperand>.As<IBqlString>
  {
  }

  /// <summary>
  /// Pretends that the <typeparamref name="TLegacyOperand" /> is of <see cref="T:PX.Data.BQL.IBqlByte" /> type.
  /// Changes the operand's type only in terms of BQL - it does not generate SQL CAST.
  /// </summary>
  public sealed class AsByte : Use<TLegacyOperand>.As<IBqlByte>
  {
  }

  /// <summary>
  /// Pretends that the <typeparamref name="TLegacyOperand" /> is of <see cref="T:PX.Data.BQL.IBqlShort" /> type.
  /// Changes the operand's type only in terms of BQL - it does not generate SQL CAST.
  /// </summary>
  public sealed class AsShort : Use<TLegacyOperand>.As<IBqlShort>
  {
  }

  /// <summary>
  /// Pretends that the <typeparamref name="TLegacyOperand" /> is of <see cref="T:PX.Data.BQL.IBqlInt" /> type.
  /// Changes the operand's type only in terms of BQL - it does not generate SQL CAST.
  /// </summary>
  public sealed class AsInt : Use<TLegacyOperand>.As<IBqlInt>
  {
  }

  /// <summary>
  /// Pretends that the <typeparamref name="TLegacyOperand" /> is of <see cref="T:PX.Data.BQL.IBqlLong" /> type.
  /// Changes the operand's type only in terms of BQL - it does not generate SQL CAST.
  /// </summary>
  public sealed class AsLong : Use<TLegacyOperand>.As<IBqlLong>
  {
  }

  /// <summary>
  /// Pretends that the <typeparamref name="TLegacyOperand" /> is of <see cref="T:PX.Data.BQL.IBqlFloat" /> type.
  /// Changes the operand's type only in terms of BQL - it does not generate SQL CAST.
  /// </summary>
  public sealed class AsFloat : Use<TLegacyOperand>.As<IBqlFloat>
  {
  }

  /// <summary>
  /// Pretends that the <typeparamref name="TLegacyOperand" /> is of <see cref="T:PX.Data.BQL.IBqlDouble" /> type.
  /// Changes the operand's type only in terms of BQL - it does not generate SQL CAST.
  /// </summary>
  public sealed class AsDouble : Use<TLegacyOperand>.As<IBqlDouble>
  {
  }

  /// <summary>
  /// Pretends that the <typeparamref name="TLegacyOperand" /> is of <see cref="T:PX.Data.BQL.IBqlDecimal" /> type.
  /// Changes the operand's type only in terms of BQL - it does not generate SQL CAST.
  /// </summary>
  public sealed class AsDecimal : Use<TLegacyOperand>.As<IBqlDecimal>
  {
  }

  /// <summary>
  /// Pretends that the <typeparamref name="TLegacyOperand" /> is of <see cref="T:PX.Data.BQL.IBqlByteArray" /> type.
  /// Changes the operand's type only in terms of BQL - it does not generate SQL CAST.
  /// </summary>
  public sealed class AsByteArray : Use<TLegacyOperand>.As<IBqlByteArray>
  {
  }
}
