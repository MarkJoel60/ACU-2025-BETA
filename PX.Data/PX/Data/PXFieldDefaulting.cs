// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFieldDefaulting
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// The delegate for the <tt>FieldDefaulting</tt> event.
/// </summary>
/// <param name="sender">Required. The cache object that raised the event.</param>
/// <param name="args">Required. The instance of the <see cref="T:PX.Data.PXFieldDefaultingEventArgs" />
/// type that holds data for a <tt>FieldDefaulting</tt> event.</param>
/// <remarks>
///   <para>The <tt>FieldDefaulting</tt> event is generated in either of the following cases:</para>
///   <list type="bullet">
///     <item><description>A new record is inserted into the <tt>PXCache</tt> object by user action in the user interface or via the Web API.</description></item>
///     <item><description>Any of the following methods of the <tt>PXCache</tt> class initiates the assigning the default value to a field:
///         <ul><li><tt>Insert()</tt></li><li><tt>Insert(object)</tt></li><li><tt>Insert(IDictionary)</tt></li><li><tt>SetDefaultExt(object, string)</tt></li><li><tt>SetDefaultExt(object)</tt></li></ul></description></item>
///   </list>
///   <para>The <tt>FieldDefaulting</tt> event handler is used to generate and assign the default value to a data access class (DAC) field.</para>
///   <para>The following execution order is used for the <tt>FieldDefaulting</tt> event handlers:</para>
///   <list type="number">
///     <item><description>Graph event handlers are executed.</description></item>
///     <item><description>If <tt>e.Cancel</tt> is <tt>false</tt>, attribute event handlers are executed.</description></item>
///   </list>
/// </remarks>
/// <example>
/// <para>
/// According to the naming convention for graph event handlers in Acumatica Framework,
/// the classic event handler has the following signature.
/// </para>
/// <code>
/// protected virtual void DACName_FieldName_FieldDefaulting(
///     PXCache sender,
///     PXFieldDefaultingEventArgs e)
/// {
///     ...
/// }
/// </code>
/// </example>
public delegate void PXFieldDefaulting(PXCache sender, PXFieldDefaultingEventArgs args);
