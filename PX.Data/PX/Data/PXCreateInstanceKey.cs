// Decompiled with JetBrains decompiler
// Type: PX.Data.PXCreateInstanceKey
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
internal sealed class PXCreateInstanceKey
{
  private System.Type[] _Tables;
  private int? _HashCode;

  public PXCreateInstanceKey(System.Type[] tables) => this._Tables = tables;

  public override bool Equals(object obj)
  {
    if (!(obj is PXCreateInstanceKey createInstanceKey))
      return false;
    if (this._Tables != null && createInstanceKey._Tables != null && this._Tables.Length == createInstanceKey._Tables.Length)
    {
      for (int index = 0; index < this._Tables.Length; ++index)
      {
        if ((!(this._Tables[index] == (System.Type) null) || !(createInstanceKey._Tables[index] == (System.Type) null)) && !object.Equals((object) this._Tables[index], (object) createInstanceKey._Tables[index]))
          return false;
      }
    }
    else if (this._Tables != null || createInstanceKey._Tables != null)
      return false;
    return true;
  }

  public override int GetHashCode()
  {
    if (!this._HashCode.HasValue)
    {
      int num = 13;
      if (this._Tables != null)
      {
        for (int index = 0; index < this._Tables.Length; ++index)
        {
          num = 37 * num;
          if (this._Tables[index] != (System.Type) null)
            num += this._Tables[index].GetHashCode();
        }
      }
      this._HashCode = new int?(num);
    }
    return this._HashCode.Value;
  }
}
