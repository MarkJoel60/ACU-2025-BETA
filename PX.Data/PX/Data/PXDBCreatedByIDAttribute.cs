// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBCreatedByIDAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.BQL;
using PX.SM;
using System;

#nullable enable
namespace PX.Data;

/// <summary>Maps a DAC field to the database column and automatically
/// sets the field value to the ID of the user who created the data
/// record.</summary>
/// <remarks>
/// <para>The attribute is added to the value declaration of a DAC field.
/// The field data type should be <tt>Guid?</tt>.</para>
/// <para>The attribute aggregates the <see cref="T:PX.Data.PXDBGuidAttribute">PXDBGuid</see> and
/// <tt>PXDisplaySelector</tt> (derives from
/// <see cref="T:PX.Data.PXSelectorAttribute">PXSelector</see>).</para>
/// </remarks>
/// <example>
/// <code>
/// [PXDBCreatedByID()]
/// public virtual Guid? CreatedByID { get; set; }
/// </code>
/// </example>
[PXDBGuid(false)]
[PXUIField(DisplayName = "Created By", Enabled = false, Visible = true, IsReadOnly = true)]
[Serializable]
public class PXDBCreatedByIDAttribute : 
  PXDBBaseIDAttribute,
  IPXRowInsertingSubscriber,
  IPXCommandPreparingSubscriber
{
  protected virtual bool ExcludeFromUpdate => true;

  internal PXDBCreatedByIDAttribute(
    #nullable disable
    System.Type search,
    System.Type substituteKey,
    System.Type descriptionField,
    params System.Type[] fields)
    : base(search, substituteKey, descriptionField, fields)
  {
  }

  /// <summary>
  /// Initializes a new unparameterized instance of the
  /// <tt>PXDBCreatedByID</tt> attribute.
  /// </summary>
  public PXDBCreatedByIDAttribute()
    : base(typeof (PXDBCreatedByIDAttribute.Creator.pKID), typeof (PXDBCreatedByIDAttribute.Creator.username), typeof (PXDBCreatedByIDAttribute.Creator.displayName), typeof (PXDBCreatedByIDAttribute.Creator.pKID), typeof (PXDBCreatedByIDAttribute.Creator.username))
  {
  }

  void IPXRowInsertingSubscriber.RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    if (this.DontOverrideValue && sender.GetValue(e.Row, this._FieldOrdinal) != null)
      return;
    sender.SetValue(e.Row, this._FieldOrdinal, (object) this.GetUserID(sender));
  }

  void IPXCommandPreparingSubscriber.CommandPreparing(PXCache sender, PXCommandPreparingEventArgs e)
  {
    if (!this.ExcludeFromUpdate || (e.Operation & PXDBOperation.Delete) != PXDBOperation.Update)
      return;
    e.ExcludeFromInsertUpdate();
  }

  /// <exclude />
  [PXBreakInheritance]
  [Serializable]
  public sealed class Creator : Users
  {
    [PXDBString(256 /*0x0100*/, IsKey = true, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Created By", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
    public override string Username { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Created By", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
    [PXFormula(typeof (IsNull<IsNull<SmartJoin<Space, Users.firstName, Users.lastName>, PXDBCreatedByIDAttribute.Creator.username>, Empty>))]
    public override string DisplayName { get; set; }

    [PXString(IsUnicode = true)]
    [PXUIField(DisplayName = "Email Signature")]
    [PXDBScalar(typeof (Search<UserPreferences.mailSignature, Where<BqlOperand<UserPreferences.userID, IBqlGuid>.IsEqual<PXDBCreatedByIDAttribute.Creator.pKID>>>))]
    public string MailSignature { get; set; }

    /// <exclude />
    public new abstract class pKID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      PXDBCreatedByIDAttribute.Creator.pKID>
    {
    }

    /// <exclude />
    public new abstract class username : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXDBCreatedByIDAttribute.Creator.username>
    {
    }

    /// <exclude />
    public new abstract class displayName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXDBCreatedByIDAttribute.Creator.displayName>
    {
    }

    /// <exclude />
    public abstract class mailSignature : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXDBCreatedByIDAttribute.Creator.mailSignature>
    {
    }
  }
}
