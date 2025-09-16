// Decompiled with JetBrains decompiler
// Type: PX.Data.InjectDependencyAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>The attribute that is used in the <see cref="T:PX.Data.PXGraph" />, <see cref="T:PX.Data.PXAction" />, or <see cref="T:PX.Data.PXEventSubscriberAttribute" />-derived classes to create the properties that need to be injected via
/// dependency injection.</summary>
/// <example><para>Suppose that dependency injection is defined as follows.</para>
///   <code title="Example" lang="CS">
/// using System;
/// using Autofac;
/// using PX.Data;
/// namespace MyNamespace
/// {
///     //An interface that defines the service for dependency injection
///     public interface IMyService
///     {
///         void ProvideServiceFunctions();
///     }
///     //A class that implements IMyService
///     public class MyService : IMyService
///     {
///         public void ProvideServiceFunctions()
///         {
///             //An implementation
///         }
///     }
///     //A class that registers the implementation class with Autofac
///     public class MyServiceRegistrarion : Module
///     {
///         protected override void Load(ContainerBuilder builder)
///         {
///             builder.RegisterType&lt;MyService&gt;().As&lt;IMyService&gt;();
///         }
///     }
/// }</code>
///   <code title="Example2" description="The following example shows dependency injection in a graph." lang="CS">
/// namespace MyNameSpace
/// {
///     public class MyGraph : PXGraph&lt;MyGraph&gt;
///     {
///         [InjectDependency]
///         private IMyService MyService { get; set; }
/// 
///         public PXAction&lt;MyDAC&gt; MyButton;
///         [PXButton]
///         protected void myButton()
///         {
///             MyService.ProvideServiceFunctions();
///         }
/// 
///         // Other code of the graph
///     }
/// }</code>
/// </example>
[AttributeUsage(AttributeTargets.Property)]
public sealed class InjectDependencyAttribute : Attribute
{
}
