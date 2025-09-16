// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BillingTypeListAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;

#nullable enable
namespace PX.Objects.CR;

public class BillingTypeListAttribute : PXIntListAttribute
{
  public const int PerCase = 0;
  public const int PerActivity = 1;

  public virtual void CacheAttached(
  #nullable disable
  PXCache sender)
  {
    int[] numArray;
    if (!PXAccess.FeatureInstalled<FeaturesSet.timeReportingModule>())
      numArray = new int[1];
    else
      numArray = new int[2]{ 0, 1 };
    this._AllowedValues = numArray;
    string[] strArray;
    if (!PXAccess.FeatureInstalled<FeaturesSet.timeReportingModule>())
      strArray = new string[1]{ "Per Case" };
    else
      strArray = new string[2]{ "Per Case", "Per Activity" };
    this._AllowedLabels = strArray;
    this._NeutralAllowedLabels = this._AllowedLabels;
    base.CacheAttached(sender);
  }

  public class perCase : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  BillingTypeListAttribute.perCase>
  {
    public perCase()
      : base(0)
    {
    }
  }

  public class perActivity : BqlType<
  #nullable enable
  IBqlInt, int>.Constant<
  #nullable disable
  BillingTypeListAttribute.perActivity>
  {
    public perActivity()
      : base(1)
    {
    }
  }
}
