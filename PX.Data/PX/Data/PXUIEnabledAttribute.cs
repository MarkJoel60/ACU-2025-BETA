// Decompiled with JetBrains decompiler
// Type: PX.Data.PXUIEnabledAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>This attribute manages whether the field is editable at runtime.</summary>
/// <remarks>This attribute subscribes to the <tt>RowSelected</tt> event handler of the attribute level. It is applicable when the field editability depends on the state of
/// the object only. The constructor of the attribute takes a type that represents the condition for enabling or disabling the field as the first parameter. This
/// type should be the same as for <tt>PXFormulaAttribute</tt> (<tt>IBqlCreator</tt>, <tt>IBqlField</tt> or <tt>Constant</tt>). As a syntactic sugar, types that
/// implement <tt>IBqlWhere</tt> interface may be used as well.
/// <para>You must use this attribute together with the <tt>PXUIFieldAttribute</tt> attribute.</para><para>Make sure that you do not use this attribute and the <tt>PXUIFieldAttribute.SetEnabled</tt> method at the same time.</para></remarks>
/// <example>
/// In the example below, the <tt>AveragingConvention</tt> field is enabled
/// if <tt>Depreciate</tt> is <tt>true</tt> for the same record. The <tt>Depreciable</tt> field
/// is enabled if the record does not represent a particular asset (<tt>RecordType != AssetType</tt>),
/// the asset is not acquired (<tt>IsAcquired != true</tt>), or the record was just inserted
/// (<tt>EntryStatus = Inserted</tt>).
/// <code title="" description="" lang="CS">
/// [PXDBString(2, IsFixed = true)]
/// [PXUIField(DisplayName = "Averaging Convention")]
/// [FAAveragingConvention.List]
/// // ...
/// [PXUIEnabled(typeof(FABookSettings.depreciate))] // IBqlField used.
/// public virtual string AveragingConvention { get; set; }
/// [PXDBBool]
/// [PXUIField(DisplayName = "Depreciate", Visibility = PXUIVisibility.SelectorVisible)]
/// // ...
/// [PXUIEnabled(typeof(Where&lt;FixedAsset.recordType, NotEqual&lt;FARecordType.assetType&gt;, // IBqlWhere used
///     Or&lt;FixedAsset.isAcquired, NotEqual&lt;True&gt;,
///     Or&lt;EntryStatus, Equal&lt;EntryStatus.inserted&gt;&gt;&gt;&gt;))]
/// public virtual bool? Depreciable { get; set; }</code></example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXUIEnabledAttribute(System.Type conditionType) : 
  PXBaseConditionAttribute(conditionType),
  IPXFieldSelectingSubscriber
{
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    sender.SetAltered(this._FieldName, true);
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (e.Row == null || this._Formula == null)
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (System.Type) null, enabled: new bool?(PXBaseConditionAttribute.GetConditionResult(sender, e.Row, this.Formula)));
  }
}
