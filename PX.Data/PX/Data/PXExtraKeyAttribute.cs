// Decompiled with JetBrains decompiler
// Type: PX.Data.PXExtraKeyAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;

#nullable disable
namespace PX.Data;

/// <summary>Indicates that the field implements a relationship between
/// two tables in a projection. The use of this attribute enables update
/// of the referenced table on update of the projection.</summary>
/// <remarks>You can place the attribute on the field declaration in the DAC
/// that represents a <see cref="T:PX.Data.PXProjectionAttribute">projection</see>.
/// The attribute is required when the projection combines data from
/// joined tables and more than one table needs to be updated on update of
/// the projection. In this case the attribute should be placed on all
/// fields that implement the relationship between the main and the joined
/// tables.</remarks>
/// <example>
/// The following example shows the declaration of a projection that can
/// update data in two tables.
/// Note that the <tt>Select</tt> commands retrieves data from two tables,
/// <tt>CRCampaignMembers</tt> and <tt>Contact</tt>. To make the
/// projection updatable, you set the <tt>Persistent</tt> property to
/// <see langword="true" />. The projection field that implements relationship between the
/// tables is marked with the <tt>PXExtraKey</tt> attribute.
/// <code>
/// // Projection declaration
/// [PXProjection(
/// typeof(
///     Select2&lt;CRCampaignMembers,
///         RightJoin&lt;Contact,
///             On&lt;Contact.contactID, Equal&lt;CRCampaignMembers.contactID&gt;&gt;&gt;&gt;
/// ),
/// Persistent = true)]
/// [Serializable]
/// public partial class SelCampaignMembers : CRCampaignMembers, IPXSelectable
/// {
///     ...
///     // The field connecting the current DAC with the Contact DAC
///     [PXDBInt(BqlField = typeof(Contact.contactID))]
///     [PXExtraKey]
///     public virtual int? ContactContactID { get; set; }
///     ...
/// }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXExtraKeyAttribute : PXEventSubscriberAttribute, IPXCommandPreparingSubscriber
{
  public virtual void CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    e.IsRestriction = true;
  }
}
