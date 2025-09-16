// Decompiled with JetBrains decompiler
// Type: PX.Data.HotKeyInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Export;
using System;
using System.Text;

#nullable disable
namespace PX.Data;

/// <exclude />
public struct HotKeyInfo
{
  private readonly bool _ctrlKey;
  private readonly bool _shiftKey;
  private readonly bool _altKey;
  private readonly int[] _charCodes;
  private readonly int _keyCode;
  private string _toString;
  public static readonly HotKeyInfo Empty = new HotKeyInfo();

  public HotKeyInfo(bool ctrl, bool shift, bool alt, int keyCode, int[] charCodes)
  {
    if (!ctrl && !shift && !alt)
      throw new ArgumentException("At least one of special keys (Ctrl, Shift, Alt) must be set.");
    if (keyCode == 0 && (charCodes == null || charCodes.Length == 0))
      throw new ArgumentException("A shortcut must contain a functional key or at least one char.");
    this._ctrlKey = ctrl;
    this._shiftKey = shift;
    this._altKey = alt;
    this._charCodes = charCodes ?? new int[0];
    this._keyCode = keyCode;
    this._toString = (string) null;
  }

  public bool CtrlKey => this._ctrlKey;

  public bool ShiftKey => this._shiftKey;

  public bool AltKey => this._altKey;

  public int[] CharCodes => this._charCodes;

  public int KeyCode => this._keyCode;

  public override string ToString()
  {
    if (this._toString == null && this._charCodes != null)
    {
      StringBuilder stringBuilder1 = new StringBuilder(this._charCodes.Length * 4 + 5);
      if (this.CtrlKey)
        stringBuilder1.Append("Ctrl");
      if (this.AltKey)
      {
        if (stringBuilder1.Length > 0)
          stringBuilder1.Append(" + ");
        stringBuilder1.Append("Alt");
      }
      if (this.ShiftKey)
      {
        if (stringBuilder1.Length > 0)
          stringBuilder1.Append(" + ");
        stringBuilder1.Append("Shift");
      }
      int index;
      if (this.KeyCode > 0)
      {
        if (stringBuilder1.Length > 0)
          stringBuilder1.Append(" + ");
        string name = Enum.GetName(typeof (KeyCodes), (object) this.KeyCode);
        StringBuilder stringBuilder2 = stringBuilder1;
        string str;
        if (!string.IsNullOrEmpty(name))
        {
          str = name;
        }
        else
        {
          index = this.KeyCode;
          str = index.ToString();
        }
        stringBuilder2.Append(str);
      }
      int[] charCodes = this._charCodes;
      for (index = 0; index < charCodes.Length; ++index)
      {
        char ch = (char) charCodes[index];
        stringBuilder1.Append(" + ");
        stringBuilder1.Append(ch);
      }
      this._toString = stringBuilder1.ToString();
    }
    return this._toString;
  }

  public static string ConvertCharCodes(string str)
  {
    StringBuilder stringBuilder = new StringBuilder(str.Length * 4);
    foreach (char ch in str)
    {
      if (stringBuilder.Length > 0)
        stringBuilder.Append(" + ");
      stringBuilder.Append(ch);
    }
    return stringBuilder.ToString();
  }

  public bool Equals(HotKeyInfo other)
  {
    return other._ctrlKey.Equals(this._ctrlKey) && other._shiftKey.Equals(this._shiftKey) && other._altKey.Equals(this._altKey) && object.Equals((object) other._charCodes, (object) this._charCodes) && other._keyCode == this._keyCode && object.Equals((object) other._toString, (object) this._toString);
  }

  public override bool Equals(object obj)
  {
    return obj != null && !(obj.GetType() != typeof (HotKeyInfo)) && this.Equals((HotKeyInfo) obj);
  }

  public override int GetHashCode()
  {
    bool flag = this._ctrlKey;
    int num1 = flag.GetHashCode() * 397;
    flag = this._shiftKey;
    int hashCode1 = flag.GetHashCode();
    int num2 = (num1 ^ hashCode1) * 397;
    flag = this._altKey;
    int hashCode2 = flag.GetHashCode();
    return (((num2 ^ hashCode2) * 397 ^ (this._charCodes != null ? this._charCodes.GetHashCode() : 0)) * 397 ^ this._keyCode) * 397 ^ (this._toString != null ? this._toString.GetHashCode() : 0);
  }

  public static bool operator ==(HotKeyInfo left, HotKeyInfo right) => left.Equals(right);

  public static bool operator !=(HotKeyInfo left, HotKeyInfo right) => !left.Equals(right);

  public static int[] ConvertChars(char[] data)
  {
    int[] numArray = new int[data.Length];
    for (int index = 0; index < data.Length; ++index)
      numArray[index] = (int) data[index];
    return numArray;
  }
}
