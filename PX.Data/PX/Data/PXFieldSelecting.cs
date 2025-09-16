// Decompiled with JetBrains decompiler
// Type: PX.Data.PXFieldSelecting
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>
/// The delegate for the <tt>FieldSelecting</tt> event.
/// </summary>
/// <param name="sender">Required. The cache object that raised the event.</param>
/// <param name="args">Required. The instance of the <see cref="T:PX.Data.PXFieldSelectingEventArgs">PXFieldSelectingEventArgs</see> type
/// that holds data for the <tt>FieldSelecting</tt> event.</param>
/// <remarks>
///   <para>The <tt>FieldSelecting</tt> event is generated in the following cases:</para>
///   <list type="bullet">
///     <item><description>When the external representation (that is, with the way the value should be displayed in the UI) of
///     a data access class field (DAC) value is requested
///     from the UI or through the Web Service API.</description></item>
///     <item><description>When any of the following methods of the <tt>PXCache</tt> class initiates the assigning the default value to a field:
///         <ul><li><tt>Insert()</tt></li><li><tt>Insert(object)</tt></li><li><tt>Insert(IDictionary)</tt></li></ul></description></item>
///     <item><description>While a field is updated in the <tt>PXCache</tt> object, and this action is initiated
///     by any of the following methods of the <tt>PXCache</tt> class:
///         <ul><li><tt>Update(object)</tt></li><li><tt>Update(IDictionary, IDictionary)</tt></li></ul></description></item>
///     <item><description>While a DAC field value is requested through any of the following methods of the <tt>PXCache</tt> class:
///         <ul><li><tt>GetValueInt(object, string)</tt></li><li><tt>GetValueIntField(object)</tt></li><li><tt>GetValueExt(object, string)</tt></li><li><tt>GetValueExt&lt;Field&gt;(object)</tt></li><li><tt>GetValuePending(object, string)</tt></li><li><tt>ToDictionary(object)</tt></li><li><tt>GetStateExt(object, string)</tt></li><li><tt>GetStateExt&lt;Field&gt;(object)</tt></li></ul></description></item>
///   </list>
///   <para>The <tt>FieldSelecting</tt> event handler is used to perform the following actions:</para>
///   <list type="bullet">
///     <item><description>Conversion of the internal presentation of a DAC field (the data field value of a DAC instance)
///     to the external presentation (the value displayed in the UI)</description></item>
///     <item><description>Conversion of the values of multiple DAC fields to a single external presentation</description></item>
///     <item><description>Provision of additional information to set up a DAC field input control or cell presentation</description></item>
///   </list>
///   <para>The following execution order is used for the <tt>FieldSelecting</tt> event handlers:</para>
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
/// protected virtual void DACName_FieldName_FieldSelecting(
///   PXCache sender,
///   PXFieldSelectingEventArgs e)
/// {
///   ...
/// }</code>
/// </example>
/// <example>
/// <para>
/// The following code converts the DAC field value to its external presentation
/// by using a redefinition of the attribute method.
/// </para>
/// <code>
/// public class PXTimeSpanLongAttribute : PXIntAttribute
/// {
///     ...
/// 
///     public override void FieldSelecting(PXCache sender,
///                                         PXFieldSelectingEventArgs e)
///     {
///         if (_AttributeLevel == PXAttributeLevel.Item || e.IsAltered)
///         {
///             string inputMask = this.inputMask ??
///                                _inputMasks[(int)this._Format];
///             int length = this.inputMask != null ? _maskLenght :
///                                            _lengths[(int)this._Format];
///             inputMask = PXMessages.LocalizeNoPrefix(inputMask);
///             e.ReturnState = PXStringState.CreateInstance(
///                 e.ReturnState,
///                 length,
///                 null,
///                 _FieldName,
///                 _IsKey,
///                 null,
///                 String.IsNullOrEmpty(inputMask) ? null : inputMask,
///                 null, null, null, null);
///         }
///         if (e.ReturnValue != null)
///         {
///             TimeSpan span = new TimeSpan(0, 0, (int)e.ReturnValue, 0);
///             int hours =
///                 (this._Format == TimeSpanFormatType.LongHoursMinutes) ?
///                 span.Days * 24 + span.Hours :  span.Hours;
///             e.ReturnValue = string.Format(_outputFormats[(int)this._Format],
///                                           span.Days, hours, span.Minutes);
///         }
///     }
/// 
///     ...
/// }
/// </code>
/// </example>
/// <example>
/// <para>
/// The following code defines the mask for the input control or
/// the cell presentation of a DAC field by using a redefinition of the attribute method.
/// </para>
/// <code>
/// [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter |
///                 AttributeTargets.Class | AttributeTargets.Method)]
/// public class PXDBStringWithMaskAttribute : PXDBStringAttribute,
///                                            IPXFieldSelectingSubscriber
/// {
///     ...
/// 
///     public override void FieldSelecting(PXCache sender,
///                                         PXFieldSelectingEventArgs e)
///     {
///         if (e.Row == null) return;
/// 
///         string mask = this.FindMask(sender, e.Row);
///         if (!string.IsNullOrEmpty(mask))
///             e.ReturnState = PXStringState.CreateInstance(e.ReturnState,
///                                                          _Length,
///                                                          null,
///                                                          _FieldName,
///                                                          _IsKey,
///                                                          null,
///                                                          mask,
///                                                          null, null, null,
///                                                          null);
///         else
///             base.FieldSelecting(sender, e);
///     }
/// 
///     ...
/// }
/// </code>
/// </example>
/// <example>
/// <para>
/// The following code defines the lists of values and labels for the PXDropDown input control
/// of the DAC field by using a redefinition of the attribute method.
/// </para>
/// <code>
/// [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class |
///                 AttributeTargets.Parameter | AttributeTargets.Method)]
/// [PXAttributeFamily(typeof(PXBaseListAttribute))]
/// public class PXStringListAttribute : PXEventSubscriberAttribute,
///                                      IPXFieldSelectingSubscriber
/// {
///     ...
/// 
///     public virtual void FieldSelecting(PXCache sender,
///                                        PXFieldSelectingEventArgs e)
///     {
///         if (_AttributeLevel == PXAttributeLevel.Item || e.IsAltered)
///         {
///             string[] values = _AllowedValues;
///             e.ReturnState = PXStringState.CreateInstance(
///                 e.ReturnState, null, null, _FieldName,
///                 null, -1, null, values, _AllowedLabels,
///                 _ExclusiveValues, null);
///         }
///     }
/// 
///     ...
/// }
/// </code>
/// </example>
public delegate void PXFieldSelecting(PXCache sender, PXFieldSelectingEventArgs args);
