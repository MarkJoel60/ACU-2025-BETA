// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFieldVerifying
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// The delegate for the <tt>FieldVerifying</tt> event.
/// </summary>
/// <param name="sender">Required. The cache object that raised the event.</param>
/// <param name="args">Required. The instance of the <see cref="T:PX.Data.PXFieldVerifyingEventArgs">PXFieldVerifyingEventArgs</see>
/// type that holds data for the <tt>PXFieldVerifying</tt> event.</param>
/// <remarks>
///   <para>The system generates the <tt>FieldVerifying</tt> event for each data access class (DAC) field
///   of a data record that is inserted or updated in the
/// <tt>PXCache</tt> object in the following processes:</para>
///   <list type="bullet">
///     <item><description>Insertion or update that is initiated in the UI
///     or through the Web Service API.</description></item>
///     <item><description>Assignment of the default value to the DAC field that is initiated by any of the following methods of the <tt>PXCache</tt> class:
///         <ul><li><tt>Insert()</tt></li><li><tt>Insert(object)</tt></li><li><tt>Insert(IDictionary)</tt></li><li><tt>SetDefaultExt(object, string)</tt></li><li><tt>SetDefaultExt&lt;Event&gt;(object)</tt></li></ul></description></item>
///     <item><description>A DAC field update that is initiated by any of the following methods of the <tt>PXCache</tt> class:
///         <ul><li><tt>Update(object)</tt></li><li><tt>Update(IDictionary, IDictionary)</tt></li><li><tt>SetValueExt(object, string, object)</tt></li><li><tt>SetValueExt&lt;Field&gt;(object, object)</tt></li></ul></description></item>
///     <item><description>Validation of a DAC key field value when the validation is initiated by any of the following methods of the <tt>PXCache</tt> class:
///         <ul><li><tt>Locate(IDictionary)</tt></li><li><tt>Update(IDictionary, IDictionary)</tt></li></ul></description></item>
///   </list>
///   <para>The <tt>FieldVerifying</tt> event handler is used to perform the following actions:</para>
///   <list type="bullet">
///     <item><description>Implementation of the business logic associated with the validation of the DAC field value before the value is assigned to the DAC field.</description></item>
///     <item><description>Cancellation of the assigning of a value by throwing an exception of the <tt>PXSetPropertyException</tt>
///     type if the value does not meet the requirements.</description></item>
///     <item><description>Conversion of the external presentation of a DAC field value to the internal presentation
///     and implementation of the associated business logic. The internal presentation is the value stored in a DAC instance.</description></item>
///   </list>
///   <para>The following execution order is used for the <tt>FieldVerifying</tt> event handlers:</para>
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
/// protected virtual void DACName_FieldName_FieldVerifying(
///     PXCache sender,
///     PXFieldVerifyingEventArgs e)
/// {
///     ...
/// }
/// </code>
/// </example>
public delegate void PXFieldVerifying(PXCache sender, PXFieldVerifyingEventArgs args);
