// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDBGroupMaskAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Marks a DAC field of <tt>byte[]</tt> type that holds the
/// group mask value.</summary>
/// <example><para>The code below shows definition of a DAC field tha holds a group mask value.</para>
/// <code title="Example" lang="CS">
/// [PXDBGroupMask()]
/// public virtual Byte[] GroupMask { get; set; }</code>
/// </example>
public class PXDBGroupMaskAttribute : PXDBBinaryAttribute
{
  protected bool hideFromEntityTypesList;

  /// <summary>Hide this type from the list of available entity types of the restriction group</summary>
  public bool HideFromEntityTypesList
  {
    get => this.hideFromEntityTypesList;
    set => this.hideFromEntityTypesList = value;
  }

  /// <summary>Initializes an instance of the attribute with default
  /// parameters.</summary>
  public PXDBGroupMaskAttribute()
  {
  }

  /// <summary>Initializes an instance of the attribute with the specified
  /// maximum length of the value.</summary>
  public PXDBGroupMaskAttribute(int length)
    : base(length)
  {
  }

  /// <exclude />
  public virtual void securedFieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    e.ReturnValue = (object) false;
    if (e.Row != null)
    {
      if (!(sender.GetValue(e.Row, this._FieldOrdinal) is byte[] numArray))
        return;
      for (int index = 0; index < numArray.Length; ++index)
      {
        if (numArray[index] != (byte) 0)
        {
          e.ReturnValue = (object) true;
          break;
        }
      }
    }
    else
      e.ReturnState = (object) PXFieldState.CreateInstance((object) null, typeof (bool), new bool?(false), new bool?(true), fieldName: "Secured", displayName: PXMessages.Localize("Secured"), enabled: new bool?(false), visible: new bool?(true), visibility: PXUIVisibility.Invisible);
  }

  /// <exclude />
  public override void CacheAttached(PXCache sender)
  {
    base.CacheAttached(sender);
    if (sender.Fields.Contains("Secured"))
      return;
    sender.Fields.Add("Secured");
    sender.FieldSelectingEvents["secured"] += new PXFieldSelecting(this.securedFieldSelecting);
  }
}
