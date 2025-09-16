// Decompiled with JetBrains decompiler
// Type: PX.Data.BQL.SetOfConstants`2
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.BQL;

/// <summary>The base class for BQL set of constants.</summary>
/// <typeparam name="TConstantType">The data type of the constant.</typeparam>
/// <typeparam name="TSetProvider">Type that is used as the set of constants provider</typeparam>
/// <example>
/// The following code shows how to define a set of constants.
/// <code>
/// public class PayableDocSet : SetOfConstants&lt;String, PayableDocSet.SetProvider&gt;
/// {
/// 	public class SetProvider : ISetProvider
/// 	{
/// 		public Constant&lt;String&gt;[] Constants { get; } =
/// 		{
/// 			new ARDocType.Invoice(),
/// 			new ARDocType.DebitMemo(),
/// 			new ARDocType.FinCharge(),
/// 			new ARDocType.SmallCreditWO()
/// 		}
/// 	}
/// }
/// ...
/// // Using the set of constants in BQL.
/// public PXSelect&lt;ARInvoice,
///     Where&lt;PayableDocSet.Contains&lt;ARInvoice.docType&gt;&gt;&gt; records;
/// 
/// // Using the set of constants in code.
/// if (PayableDocSet.ContainsValue(arinvoice.DocType))
/// {
/// 	...
/// }
/// </code>
/// </example>
public abstract class SetOfConstants<TConstantType, TSetProvider> : 
  SetOfConstants<TConstantType, Equal<IBqlOperand>, TSetProvider>
  where TSetProvider : SetOfConstants<TConstantType, Equal<IBqlOperand>, TSetProvider>.ISetProvider, new()
{
}
