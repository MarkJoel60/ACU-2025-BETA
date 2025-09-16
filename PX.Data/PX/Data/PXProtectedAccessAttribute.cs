// Decompiled with JetBrains decompiler
// Type: PX.Data.PXProtectedAccessAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>
///   <para>Allows to make a protected member of a graph available to be called in a graph extension.</para>
///   <para>To be able to call a protected member in a graph extension, annotate the member in a graph extension with the PXProtectedAccess attribute. The member has to
/// have the same signature as the corresponding member in the graph or lower-level graph extension. The framework will replace the body of the member annotated
/// with PXProtectedAccess with the body of the corresponding member in the graph or lower-level graph extension.</para>
/// </summary>
/// <remarks>The member you want to access and the graph extension must be declared abstract.</remarks>
/// <example><para>Suppose that the code of Acumatica ERP includes the following graph.</para>
///   <code title="Graph Example" lang="CS">
/// public class MyGraph : PXGraph&lt;MyGraph&gt;
/// {
///     protected void Foo(int param1, string param2) { ... }
///     protected static void Foo2() { }
///     protected int Bar(MyDac dac) =&gt; dac.IntValue;
///     protected decimal Prop { get; set; }
///     protected double Field;
/// }</code>
///   <code title="Graph Extension Example" description="You can use the members in an extension of the graph, as shown in the following example." lang="CS">
/// [PXProtectedAccess]
/// public abstract class MyExt : PXGraphExtension&lt;MyGraph&gt;
/// {
///       [PXProtectedAccess] protected abstract void Foo(int param1, string param2)
///       [PXProtectedAccess] protected abstract void Foo2();
///       [PXProtectedAccess] protected abstract int Bar(MyDac dac);
///       [PXProtectedAccess] protected abstract decimal Prop { get; set; }
///       [PXProtectedAccess] protected abstract double Field { get; set; }
///       private void Test()
///       {
///              Foo(42, "23");
///              int bar = Bar(new MyDac());
///              decimal prop = Prop;
///              Prop = prop + 12;
///              double field = Field;
///              Field = field + 15;
///       }
/// }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false)]
public class PXProtectedAccessAttribute : Attribute
{
  public System.Type TargetType { get; private set; }

  public PXProtectedAccessAttribute(System.Type targetType = null) => this.TargetType = targetType;
}
