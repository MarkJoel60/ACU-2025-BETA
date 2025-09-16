// Decompiled with JetBrains decompiler
// Type: PX.Data.PXImageAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXImageAttribute : PXStringAttribute
{
  /// <summary>Get, set.</summary>
  public string HeaderImage { get; set; }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AttributeLevel != PXAttributeLevel.Item && !e.IsAltered)
      return;
    e.ReturnState = (object) PXImageState.CreateInstance(e.ReturnState, new int?(this._Length), new bool?(this._IsUnicode), this._FieldName, new bool?(this._IsKey), new int?(), this._InputMask, (string[]) null, (string[]) null, new bool?(), (string) null, this.HeaderImage);
  }
}
