// Decompiled with JetBrains decompiler
// Type: PX.Data.PXExceptionHandling
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// The delegate for the <tt>ExceptionHandling</tt> event.
/// </summary>
/// <param name="sender">Required. The cache object that raised the event.</param>
/// <param name="args">Required. The instance of the <see cref="T:PX.Data.PXExceptionHandlingEventArgs" />
/// type that holds data for the <tt>ExceptionHandling</tt> event.</param>
/// <remarks>
///   <para>The <tt>ExceptionHandling</tt> event is generated in the following cases:</para>
///   <list type="bullet">
///     <item><description>The <tt>PXSetPropertyException</tt> exception is thrown while the system is processing
///     a data access class (DAC) field value received from the UI or
///     through the Web Service API when a data record
///     is being inserted or updated in the <tt>PXCache</tt> object.</description></item>
///     <item><description>The <tt>PXSetPropertyException</tt> exception is thrown while the system is processing
///     DAC key field values when the deletion of a data record from the <tt>PXCache</tt> object
///     is initiated in the UI or through the Web Service API.</description></item>
///     <item><description>The <tt>PXSetPropertyException</tt> exception is thrown when
///     the system is assigning the default value to a field
///     or updating the value when the assignment or update is initiated by any
///     of the following methods of the <tt>PXCache</tt> class:
///     <list type="bullet">
///     <item><description><tt>Insert(IDictionary)</tt></description></item>
///     <item><description><tt>SetDefaultExt(object, string)</tt></description></item>
///     <item><description><tt>SetDefaultExt&lt;Field&gt;(object)</tt></description></item>
///     <item><description><tt>Update(IDictionary, IDictionary)</tt></description></item>
///     <item><description><tt>SetValueExt(object, string, object)</tt></description></item>
///     <item><description><tt>SetValueExt&lt;Field&gt;(object, object</tt></description></item>
///     </list>
///     </description></item>
///     <item><description>The <tt>PXSetPropertyException</tt> exception is thrown while the system is converting
///     the external DAC key field presentation to the internal field value initiated by any of the following methods
///     of the <tt>PXCache</tt> class:
///     <list type="bullet">
///     <item><description><tt>Locate(IDictionary)</tt></description></item>
///     <item><description><tt>Update(IDictionary, IDictionary)</tt></description></item>
///     <item><description><tt>Delete(IDictionary, IDictionary)</tt></description></item>
///     </list>
///     </description></item>
///     <item><description>The <tt>PXCommandPreparingException</tt>, <tt>PXRowPersistingException</tt>,
///     or <tt>PXRowPersistedException</tt> exception is thrown when an inserted, updated,
///     or deleted data record is saved in the database.</description></item>
///   </list>
///   <para>The <tt>ExceptionHandling</tt> event handler is used to do the following:</para>
///   <list type="bullet">
///     <item><description>Catch and handle the exceptions mentioned above
///     (the platform rethrows all unhandled exceptions)</description></item>
///     <item><description>Implement non-standard handling of the exceptions mentioned above</description></item>
///   </list>
///   <para>The following execution order is used for the <tt>ExceptionHandling</tt> event handlers:</para>
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
///   <code lang="CS">
/// protected virtual void DACName_FieldName_ExceptionHandling(
///     PXCache sender,
///     PXExceptionHandlingEventArgs e)
/// {
///     ...
/// }
/// </code>
/// </example>
public delegate void PXExceptionHandling(PXCache sender, PXExceptionHandlingEventArgs args);
