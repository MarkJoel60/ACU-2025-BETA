// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCommandPreparing
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>The delegate for the <tt>CommandPreparing</tt> event.</summary>
/// <param name="sender">Required. The cache object that raised the event.</param>
/// <param name="e">Required. The instance of the <see cref="T:PX.Data.PXCommandPreparingEventArgs">PXCommandPreparingEventArgs</see> type
/// that contains data for the <tt>CommandPreparing</tt> event.</param>
/// <remarks>
///   <para>The <tt>CommandPreparing</tt> event is generated each time the Acumatica data access layer prepares a database-specific SQL statement for a <tt>SELECT</tt>,
/// <tt>INSERT</tt>, <tt>UPDATE</tt>, or <tt>DELETE</tt> operation. This event is raised for every data access class (DAC) field placed in the <tt>PXCache</tt>
/// object. By using the <tt>CommandPreparing</tt> event subscriber, you can alter the property values of the <tt>PXCommandPreparingEventArgs.FieldDescription</tt>
/// object that is used in the generation of an SQL statement.</para>
///   <para>The <tt>CommandPreparing</tt> event handler is used in the following cases:</para>
///   <list type="bullet">
///     <item><description>To exclude a DAC field from a <tt>SELECT</tt>, <tt>INSERT</tt>, or <tt>UPDATE</tt> operation</description></item>
///     <item><description>To replace a DAC field from a <tt>SELECT</tt> operation with a custom SQL statement</description></item>
///     <item><description>To transform a DAC field value submitted to the server for an <tt>INSERT</tt>, <tt>UPDATE</tt>, or <tt>DELETE</tt> operation</description></item>
///   </list>
///   <para>The following execution order is used for the <tt>CommandPreparing</tt> event handlers:</para>
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
/// protected virtual void DACName_FieldType_CommandPreparing(
///   PXCache sender,
///   PXCommandPreparingEventArgs e)
/// {
///   ...
/// }
/// </code>
/// </example>
/// <example>
/// <para>
/// The following code transforms the DAC field value during INSERT and UPDATE operations
/// by using a redefinition of the attribute method.
/// </para>
/// <code>
/// public class PXDBCryptStringAttribute : PXDBStringAttribute,
///                                         IPXFieldVerifyingSubscriber,
///                                         IPXRowUpdatingSubscriber,
///                                         IPXRowSelectingSubscriber
/// {
///     ...
/// 
///     public override void CommandPreparing(PXCache sender,
///                                           PXCommandPreparingEventArgs e)
///     {
///         if ((e.Operation &amp; PXDBOperation.Command) == PXDBOperation.Insert ||
///             (e.Operation &amp; PXDBOperation.Command) == PXDBOperation.Update)
///         {
///             string value = (string)sender.GetValue(e.Row, _FieldOrdinal);
/// 
///             e.Value = !string.IsNullOrEmpty(value) ?
///                       Convert.ToBase64String(
///                           Encrypt(Encoding.Unicode.GetBytes(value))) :
///                       null;
///         }
///         base.CommandPreparing(sender, e);
///     }
/// 
///     ...
/// }
/// </code>
/// </example>
public delegate void PXCommandPreparing(PXCache sender, PXCommandPreparingEventArgs e);
