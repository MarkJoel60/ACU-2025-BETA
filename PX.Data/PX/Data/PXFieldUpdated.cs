// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFieldUpdated
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// The delegate for the <tt>FieldUpdated</tt> event.
/// </summary>
/// <param name="sender">Required. The cache object that raised the event.</param>
/// <param name="args">Required. The instance of the <see cref="T:PX.Data.PXFieldUpdatedEventArgs">PXFieldUpdatedEventArgs</see>
/// type that holds data for the <tt>FieldUpdated</tt> event.</param>
/// <remarks>
///   <para>In the following cases, the <tt>FieldUpdated</tt> event is generated after a data access class (DAC) field is actually updated:</para>
///   <list type="bullet">
///     <item><description>For each DAC field value that is received
///     from the UI or through the Web Service API when a data record is
///     inserted or updated in the <tt>PXCache</tt> object.</description></item>
///     <item><description>For each DAC key field value in the process of deleting a data record
///     from the <tt>PXCache</tt> object when the deletion is initiated from the UI or
///     through the Web Service API.</description></item>
///     <item><description>When any of the following methods of the <tt>PXCache</tt> class
///     initiates the assigning of a default value to a field:
///         <ul><li><tt>Insert()</tt></li><li><tt>Insert(object)</tt></li><li><tt>Insert(IDictionary)</tt></li><li><tt>SetDefaultExt(object, string)</tt></li><li><tt>SetDefaultExt&lt;Event&gt;(object)</tt></li></ul></description></item>
///     <item><description>When the field update is initiated
///     by any of the following methods of the <tt>PXCache</tt> class:
///         <ul><li><tt>Update(object)</tt></li><li><tt>SetValueExt(object, string, object)</tt></li><li><tt>SetValueExt&lt;Field&gt;(object, object)</tt></li></ul></description></item>
///     <item><description>During the validation of the DAC key field value
///     that is initiated by any of the following <tt>PXCache</tt> class methods:
///         <ul><li><tt>Locate(IDictionary)</tt></li><li><tt>Update(IDictionary, IDictionary)</tt></li><li><tt>Delete(IDictionary, IDictionary)</tt></li></ul></description></item>
///   </list>
///   <para>The <tt>FieldUpdated</tt> event handler is used to implement the business logic
///   related to the changes to the value of the DAC field in the following cases:</para>
///   <list type="bullet">
///     <item><description>To update the related fields of a data record containing a modified field or assigning default values to these fields</description></item>
///     <item><description>To update any of the following:
///         <ul><li>The detail data records in a one-to-many relationship</li>
///         <li>The related data records in a one-to-one relationship</li>
///         <li>The master data records in a many-to-one relationship</li></ul></description></item>
///   </list>
///   <para>The following execution order is used for the <tt>FieldUpdated</tt> event handlers:</para>
///   <list type="number">
///     <item><description>Attribute event handlers are executed.</description></item>
///     <item><description>Graph event handlers are executed.</description></item>
///   </list>
/// </remarks>
/// <example>
/// <para>
/// According to the naming convention for graph event handlers in Acumatica Framework,
/// the classic event handler has the following signature.
/// </para>
/// <code>
/// protected virtual void DACName_FieldName_FieldUpdated(
///     PXCache sender,
///     PXFieldUpdatedEventArgs e)
/// {
///     ...
/// }
/// </code>
/// </example>
public delegate void PXFieldUpdated(PXCache sender, PXFieldUpdatedEventArgs args);
