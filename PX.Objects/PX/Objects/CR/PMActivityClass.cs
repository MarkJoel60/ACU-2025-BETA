// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PMActivityClass
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CR;

public class PMActivityClass : CRActivityClass
{
  public const int TimeActivity = 8;

  public PMActivityClass()
  {
    int[] numArray = new int[this._AllowedValues.Length + 1];
    this._AllowedValues.CopyTo((Array) numArray, 0);
    numArray[this._AllowedValues.Length] = 8;
    this._AllowedValues = numArray;
    string[] strArray = new string[this._AllowedLabels.Length + 1];
    this._AllowedLabels.CopyTo((Array) strArray, 0);
    strArray[this._AllowedLabels.Length] = "Time Activity";
    this._AllowedLabels = strArray;
  }

  public class UI
  {
    public class timeActivity : BqlType<IBqlString, string>.Constant<
    #nullable disable
    PMActivityClass.UI.timeActivity>
    {
      public timeActivity()
        : base("Time Activity")
      {
      }
    }
  }

  public class timeActivity : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  PMActivityClass.timeActivity>
  {
    public timeActivity()
      : base(8)
    {
    }
  }
}
