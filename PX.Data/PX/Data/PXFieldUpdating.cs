// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFieldUpdating
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// The delegate for the <tt>FieldUpdating</tt> event.
/// </summary>
/// <param name="sender">Required. The cache object that raised the event.</param>
/// <param name="args">Required. The instance of the <see cref="T:PX.Data.PXFieldUpdatingEventArgs" />
/// type that holds data for the <tt>FieldUpdating</tt> event.</param>
/// <remarks>
///   <para>In the following cases, the <tt>FieldUpdating</tt> event is generated for a data access class (DAC) field
///   before the field is updated:</para>
///   <list type="bullet">
///     <item><description>For each DAC field value that is received
///     from the UI or through the Web Service
///     API when a data record is being inserted or updated.</description></item>
///     <item><description>For each DAC key field value in the process
///     of deleting a data record when the deletion is initiated
///     from the UI or through the Web Service API.</description></item>
///     <item><description>When any of the following methods of the <tt>PXCache</tt> class
///     initiates the assigning of the default value to a field:
///         <ul><li><tt>Insert()</tt></li><li><tt>Insert(object)</tt></li><li><tt>Insert(IDictionary)</tt></li><li><tt>SetDefaultExt(object, string)</tt></li><li><tt>SetDefaultExt&lt;Field&gt;(object)</tt></li></ul></description></item>
///     <item><description>When any of the following methods of the <tt>PXCache</tt> class
///     initiates the updating of a field:
///         <ul><li><tt>Update(IDictionary, IDictionary)</tt></li><li><tt>SetValueExt(object, string, object)</tt></li><li><tt>SetValueExt&lt;Field&gt;(object, object)</tt></li><li><tt>SetValuePending(object, string, object)</tt></li><li><tt>SetValuePending&lt;Field&gt;(object, object)</tt></li></ul></description></item>
///     <item><description>When the conversion of the external DAC key field presentation
///     to the internal field value is initiated by the following <tt>PXCache</tt> class methods:
///         <ul><li><tt>Insert()</tt></li><li><tt>Insert(object)</tt></li><li><tt>Insert(IDictionary)</tt></li></ul></description></item>
///   </list>
///   <para>The <tt>FieldUpdating</tt> event handler is used when either or both of the following occur:</para>
///   <list type="bullet">
///     <item><description>The external presentation of a DAC field (the value displayed in the UI) differs from the value stored in the DAC.</description></item>
///     <item><description>The storage of values is spread among multiple DAC fields (database columns).</description></item>
///   </list>
///   <para>In both cases, the application should implement both the <tt>FieldUpdating</tt> and
///   <tt>FieldSelecting</tt> events.</para>
///   <para>The following execution order is used for the <tt>FieldUpdating</tt> event handlers:</para>
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
/// <code lang="CS">
/// protected virtual void DACName_FieldName_FieldUpdating(
///     PXCache sender,
///     PXFieldUpdatingEventArgs e)
/// {
///     ...
/// }
/// </code>
/// </example>
public delegate void PXFieldUpdating(PXCache sender, PXFieldUpdatingEventArgs args);
