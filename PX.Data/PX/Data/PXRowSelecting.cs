// Decompiled with JetBrains decompiler
// Type: PX.Data.PXRowSelecting
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>The delegate for the <tt>RowSelecting</tt> event.</summary>
/// <param name="sender">Required. The cache object that raised the event.</param>
/// <param name="e">Required. The instance of the <see cref="T:PX.Data.PXRowSelectingEventArgs" /> type
/// that holds data for the RowSelecting event.</param>
/// <remarks>
/// <para>We recommend that you not use RowSelecting event handlers in the application code.</para>
/// <para>The RowSelecting event is generated for each retrieved data record
///   when the result of a BQL statement is processed. For a BQL statement that
/// contains a JOIN clause, the RowSelecting event is raised for every joined data access class (DAC).</para>
/// <para>The RowSelecting event handler can be used for the following purposes:</para>
/// <list type="bullet">
/// <item><description>To calculate DAC field values that are not bound to specific database columns</description></item>
/// <item><description>To modify the logic that converts the database record to the DAC</description></item>
/// </list>
/// <para>The following execution order is used for the RowSelecting event handlers:</para>
/// <list type="number">
/// <item><description>If e.Cancel is false, attribute event handlers are executed.</description></item>
/// <item><description>After the iteration through the rows of the initial connection scope is completed,
/// graph event handlers are executed. However if optimized export is performed or the PXStreamingQueryScope is used,
/// the RowSelecting graph event handlers are executed during iteration throw records.
/// In this case, a new database connection is opened automatically for execution of additional data queries.</description></item>
/// </list>
/// </remarks>
/// <example>
/// <para>
/// According to the naming convention for graph event handlers in Acumatica Framework,
/// the classic event handler has the following signature.
/// </para>
/// <code>
/// protected virtual void DACName_RowSelecting(PXCache sender,
///                                             PXRowSelectingEventArgs e)
/// {
///     ...
/// }
/// </code>
/// </example>
/// <example>
/// <para>
/// The code below converts the database table value of a DAC field
/// to the internal presentation.
/// </para>
/// <code>
/// public class PXDBCryptStringAttribute : PXDBStringAttribute,
///                                         IPXFieldVerifyingSubscriber,
///                                         IPXRowUpdatingSubscriber,
///                                         IPXRowSelectingSubscriber
/// {
///     ...
/// 
///     public override void RowSelecting(PXCache sender,
///                                       PXRowSelectingEventArgs e)
///     {
///         base.RowSelecting(sender, e);
///         if (e.Row == null || sender.GetStatus(e.Row)
///                           != PXEntryStatus.Notchanged) return;
///         string value = (string)sender.GetValue(e.Row, _FieldOrdinal);
///         string result = string.Empty;
///         if (!string.IsNullOrEmpty(value))
///         {
///             try
///             {
///                 result = Encoding.
///                     Unicode.
///                     GetString(Decrypt(Convert.FromBase64String(value)));
///             }
///             catch (Exception)
///             {
///                 try
///                 {
///                     result = Encoding.Unicode.
///                         GetString(Convert.FromBase64String(value));
///                 }
///                 catch (Exception)
///                 {
///                     result = value;
///                 }
///             }
///         }
///         sender.SetValue(e.Row, _FieldOrdinal,
///                         result.Replace("\0", string.Empty));
///     }
/// 
///     ...
/// }
/// </code>
/// </example>
public delegate void PXRowSelecting(PXCache sender, PXRowSelectingEventArgs e);
