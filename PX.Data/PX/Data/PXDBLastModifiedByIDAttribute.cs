// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBLastModifiedByIDAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data.BQL;
using PX.SM;
using System;

#nullable enable
namespace PX.Data;

/// <summary>Maps a DAC field to the database column and automatically
/// sets the field value to the ID of the user who was the last to modify
/// the data record.</summary>
/// <remarks>The attribute is added to the value declaration of a DAC field.
/// The field data type should be <tt>Guid?</tt>.</remarks>
/// <example>
/// <code>
/// [PXDBLastModifiedByID()]
/// [PXUIField(DisplayName = "Last Modified By")]
/// public virtual Guid? LastModifiedByID { get; set; }
/// </code>
/// </example>
[Serializable]
public class PXDBLastModifiedByIDAttribute : 
  PXDBCreatedByIDAttribute,
  IPXRowUpdatingSubscriber,
  IPXRowPersistingSubscriber
{
  protected override bool ExcludeFromUpdate => false;

  /// <summary>Initializes a new instance with default parameters.</summary>
  public PXDBLastModifiedByIDAttribute()
    : base(typeof (PXDBLastModifiedByIDAttribute.Modifier.pKID), typeof (PXDBLastModifiedByIDAttribute.Modifier.username), typeof (PXDBLastModifiedByIDAttribute.Modifier.displayName), typeof (PXDBLastModifiedByIDAttribute.Modifier.pKID), typeof (PXDBLastModifiedByIDAttribute.Modifier.username))
  {
    this.AddUIFieldAttributeIfNeeded("Last Modified By");
  }

  void IPXRowUpdatingSubscriber.RowUpdating(
  #nullable disable
  PXCache sender, PXRowUpdatingEventArgs e)
  {
    sender.SetValue(e.NewRow, this._FieldOrdinal, (object) this.GetUserID(sender));
  }

  void IPXRowPersistingSubscriber.RowPersisting(PXCache sender, PXRowPersistingEventArgs e)
  {
    if ((e.Operation & PXDBOperation.Delete) == PXDBOperation.Delete)
      return;
    sender.SetValue(e.Row, this._FieldOrdinal, (object) this.GetUserID(sender));
  }

  /// <summary>Is used internally to represent the user who modified the data record.</summary>
  [PXBreakInheritance]
  [Serializable]
  public sealed class Modifier : Users
  {
    [PXDBString(256 /*0x0100*/, IsKey = true, IsUnicode = true, InputMask = "")]
    [PXUIField(DisplayName = "Last Modified By", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
    public override string Username { get; set; }

    [PXString]
    [PXUIField(DisplayName = "Last Modified By", Enabled = false, Visibility = PXUIVisibility.SelectorVisible)]
    [PXFormula(typeof (IsNull<IsNull<SmartJoin<Space, Users.firstName, Users.lastName>, PXDBLastModifiedByIDAttribute.Modifier.username>, Empty>))]
    public override string DisplayName { get; set; }

    [PXString(IsUnicode = true)]
    [PXUIField(DisplayName = "Mail Signature")]
    [PXDBScalar(typeof (Search<UserPreferences.mailSignature, Where<BqlOperand<UserPreferences.userID, IBqlGuid>.IsEqual<PXDBLastModifiedByIDAttribute.Modifier.pKID>>>))]
    public string MailSignature { get; set; }

    /// <exclude />
    public new abstract class pKID : 
      BqlType<
      #nullable enable
      IBqlGuid, Guid>.Field<
      #nullable disable
      PXDBLastModifiedByIDAttribute.Modifier.pKID>
    {
    }

    /// <exclude />
    public new abstract class username : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXDBLastModifiedByIDAttribute.Modifier.username>
    {
    }

    /// <exclude />
    public new abstract class displayName : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXDBLastModifiedByIDAttribute.Modifier.displayName>
    {
    }

    /// <exclude />
    public abstract class mailSignature : 
      BqlType<
      #nullable enable
      IBqlString, string>.Field<
      #nullable disable
      PXDBLastModifiedByIDAttribute.Modifier.mailSignature>
    {
    }
  }

  /// <exclude />
  [PXLocalizable]
  public static class DisplayFieldNames
  {
    public const string CreatedByID = "Created By";
    public const string CreatedDateTime = "Created On";
    public const string CreatedByScreenID = "Created by Screen ID";
    public const string LastModifiedByID = "Last Modified By";
    public const string LastModifiedDateTime = "Last Modified On";
    public const string LastModifiedByScreenID = "Last Modified by Screen ID";
    public const string StateChangedByID = "Workflow State Changed By";
    public const string StateChangedDateTime = "Workflow State Changed On";
  }
}
