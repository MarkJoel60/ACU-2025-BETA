// Decompiled with JetBrains decompiler
// Type: PX.Data.PXViewNameAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Defines the user-friendly name of a data view.</summary>
/// <remarks>The attribute is added to the view declaration.</remarks>
/// <example><para>The code below shows the usage of the PXViewName attribute on the data view definition in a graph. (Messages.Orders is a constant defined by the application.)</para>
/// <code title="Example" lang="CS">
/// [PXViewName(Messages.Orders)]
/// public PXSelectReadonly&lt;SOOrder,
///     Where&lt;SOOrder.customerID, Equal&lt;Current&lt;BAccount.bAccountID&gt;&gt;&gt;&gt;
///     Orders;</code>
/// </example>
/// <summary>
/// Initializes a new instance that sets the provided string as
/// the user-friendly name of the data view.
/// </summary>
/// <param name="name">The string used as the name of the data view.</param>
public class PXViewNameAttribute(string name) : PXNameAttribute(name)
{
}
