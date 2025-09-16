// Decompiled with JetBrains decompiler
// Type: PX.Data.PXOverrideAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Indicates that the method defined in a graph extension
/// overrides a virtual method defined in the graph.</summary>
/// <remarks>The attribute is placed on the declaration of a method in a graph extension. (The graph extension is a class that derives from the <tt>PXGraphExtension</tt>
/// generic class, where the type parameter is set to the graph to extend.) As a result, the method overrides the graph method with the same signature. That is,
/// the method is executed instead of the graph method whenever the graph method is invoked.</remarks>
/// <example>
/// The example below shows the declaration of a graph extension and the
/// method that overrides the graph method.
/// <code title="Example" lang="CS">
/// // The  definition of the JournalWithSubEntry graph extension
/// public class JournalWithSubEntryExtension :
/// PXGraphExtension&lt;JournalWithSubEntry&gt;
/// {
///     [PXOverride]
///     public void PrepareItems(string viewName, IEnumerable items)
///     {
///         ...
///     }
/// }</code></example>
public class PXOverrideAttribute : Attribute
{
}
