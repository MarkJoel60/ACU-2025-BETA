// Decompiled with JetBrains decompiler
// Type: PX.Data.PXUIRequiredAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.Linq;

#nullable disable
namespace PX.Data;

/// <summary>This attribute conditionally puts a required field marker
/// and checks for a null value when you save the object.</summary>
/// <remarks>This field attribute combines the functionality of
/// <tt>PXUIFieldAttribute.SetRequired</tt> and <tt>PXDefaultAttribute.SetPersistingCheck</tt>
/// functions. Since <tt>PXUIRequiredAttribute</tt> implies a dynamic change
/// of the mandatory, it should be used in conjunction with the ASPX property
/// <tt>MarkRequired = "Dynamic"</tt>.
/// <para>This attribute subscribes to the <tt>RowSelected</tt> event handler
/// of the attribute level. It is applicable when the field mandatory
/// depends on the state of the object only.</para>
/// <para>You must use this attribute together with the <tt>PXDefaultAttribute</tt>
/// attribute. You do not need to specify any value to the
/// <tt>PXDefaultAttribute.PersistingCheck</tt> property, because its value
/// is set automatically.</para>
/// <para>Make sure that you do not use this attribute and the
/// <tt>PXUIFieldAttribute.SetRequired</tt> method at the same time.</para>
/// </remarks>
/// <seealso cref="T:PX.Data.PXUIEnabledAttribute" />
/// <example>
/// <code lang="CS">
/// [PXDBString(5, IsUnicode = false, IsFixed = true)]
/// [PXDefault("W", PersistingCheck = PXPersistingCheck.Nothing)]
/// [PXUIField(DisplayName = "Default Time Activity Type", Visibility = PXUIVisibility.SelectorVisible)]
/// // ...
/// [PXUIRequired(typeof(FeatureInstalled&lt;FeaturesSet.timeReportingModule&gt;))] // IBqlCreator used
/// public virtual string DefaultActivityType { get; set; }
/// [ContactDisplayName(typeof(Contact.lastName), typeof(Contact.firstName), typeof(Contact.midName),
///     typeof(Contact.title), true)]
/// [PXDependsOnFields(typeof(Contact.lastName), typeof(Contact.firstName), typeof(Contact.midName),
///     typeof(Contact.title))]
/// [PXUIField(DisplayName = "Display Name", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
/// [PXDefault]
/// // ...
/// [PXUIRequired(typeof(Where&lt;Contact.contactType, Equal&lt;ContactTypesAttribute.lead&gt;,
///     Or&lt;Contact.contactType, Equal&lt;ContactTypesAttribute.person&gt;&gt;&gt;))] // IBqlWhere used
/// public virtual String DisplayName { get; set; }</code>
/// </example>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
public class PXUIRequiredAttribute(System.Type conditionType) : 
  PXBaseConditionAttribute(conditionType),
  IPXFieldSelectingSubscriber,
  IPXRowPersistingSubscriber
{
  public override void CacheAttached(PXCache sender)
  {
    PXDefaultAttribute defaultAttribute = this.GetDefaultAttribute(sender);
    if (defaultAttribute != null)
    {
      defaultAttribute.PersistingCheck = PXPersistingCheck.Nothing;
      base.CacheAttached(sender);
      sender.SetAltered(this._FieldName, true);
    }
    else
      throw new PXException("The attribute {0} is being used without the prerequisite {1} on the field {2}.{3}.", new object[4]
      {
        (object) this.GetType().Name,
        (object) typeof (PXDefaultAttribute).Name,
        (object) sender.GetItemType().FullName,
        (object) this.FieldName
      });
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    object row = e.Row;
    System.Type formula = this.Formula;
    if (row == null || formula == (System.Type) null)
      return;
    PXPersistingCheck pxPersistingCheck = PXBaseConditionAttribute.GetConditionResult(sender, row, formula) ? PXPersistingCheck.NullOrBlank : PXPersistingCheck.Nothing;
    if (this.AttributeLevel != PXAttributeLevel.Item && !e.IsAltered && pxPersistingCheck == PXPersistingCheck.Nothing)
      return;
    e.ReturnState = (object) PXFieldState.CreateInstance(e.ReturnState, (System.Type) null, required: pxPersistingCheck == PXPersistingCheck.Nothing ? new int?() : new int?(1), fieldName: this._FieldName);
  }

  public virtual void RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    object row = e.Row;
    System.Type formula = this.Formula;
    if (row == null || formula == (System.Type) null)
      return;
    PXDefaultAttribute defaultAttribute = this.GetDefaultAttribute(sender);
    if (defaultAttribute == null)
      return;
    PXPersistingCheck pxPersistingCheck = PXBaseConditionAttribute.GetConditionResult(sender, row, formula) ? PXPersistingCheck.NullOrBlank : PXPersistingCheck.Nothing;
    defaultAttribute.PersistingCheck = pxPersistingCheck;
    defaultAttribute.RowPersisting(sender, e);
    defaultAttribute.PersistingCheck = PXPersistingCheck.Nothing;
  }

  private PXDefaultAttribute GetDefaultAttribute(PXCache sender)
  {
    return sender.GetAttributesReadonly(this._FieldName).OfType<PXDefaultAttribute>().FirstOrDefault<PXDefaultAttribute>();
  }
}
