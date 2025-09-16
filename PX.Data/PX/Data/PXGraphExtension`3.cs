// Decompiled with JetBrains decompiler
// Type: PX.Data.PXGraphExtension`3
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;

#nullable disable
namespace PX.Data;

/// <summary>A third-level graph extension.</summary>
/// <typeparam name="Extension2">The second-level graph extension.</typeparam>
/// <typeparam name="Extension1">The first-level graph extension.</typeparam>
/// <typeparam name="Graph">The base graph.</typeparam>
/// <remarks>A definition of a higher-level graph extension has two possible variants.
/// In the first variant, you derive the extension class from <see cref="T:PX.Data.PXGraph`2" />,
/// where the first type parameter is set to an extension of the previous level.
/// In the second variant, you derive the extension class from the PXGraphExtension generic class
/// with the same number of type parameters as the level of the extension of the new class.
/// In this case, you set type parameters to extension classes from all lower extension levels,
/// from the previous level down to the base class.</remarks>
/// <example>
/// The example below shows a declaration of a third--level BLC extension that is derived from the PXGraphExtension generic class with three type parameters.
/// <code>class BaseGraphAdvMultiExtensionOnExtension :
///     PXGraphExtension&lt;BaseGraphExtensionOnExtension, BaseGraphExtension, BaseGraph&gt;
/// {
///     public void SomeMethod()
///     {
///         BaseGraph graph = Base;
///         BaseGraphExtension ext = Base1;
///         BaseGraphExtensionOnExtension extOnExt = Base2;
///     }
/// }</code></example>
public abstract class PXGraphExtension<Extension2, Extension1, Graph> : 
  PXGraphExtension<Extension1, Graph>,
  IExtends<Extension2>
  where Extension2 : PXGraphExtension<Graph>
  where Extension1 : PXGraphExtension<Graph>
  where Graph : PXGraph
{
  internal Extension2 _Base2;

  protected Extension2 Base2 => this._Base2;
}
