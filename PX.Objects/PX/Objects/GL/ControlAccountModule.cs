// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.ControlAccountModule
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System.Collections.Generic;

#nullable enable
namespace PX.Objects.GL;

public class ControlAccountModule
{
  public const 
  #nullable disable
  string ANY = "ANY";
  public const string AP = "AP";
  public const string AR = "AR";
  public const string TX = "TX";
  public const string IN = "IN";
  public const string SO = "SO";
  public const string PO = "PO";
  public const string FA = "FA";
  public const string DR = "DR";

  public sealed class any : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ControlAccountModule.any>
  {
    public any()
      : base("ANY")
    {
    }
  }

  public sealed class aP : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ControlAccountModule.aP>
  {
    public aP()
      : base("AP")
    {
    }
  }

  public sealed class aR : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ControlAccountModule.aR>
  {
    public aR()
      : base("AR")
    {
    }
  }

  public sealed class tX : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ControlAccountModule.tX>
  {
    public tX()
      : base("TX")
    {
    }
  }

  public sealed class iN : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ControlAccountModule.iN>
  {
    public iN()
      : base("IN")
    {
    }
  }

  public sealed class sO : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ControlAccountModule.sO>
  {
    public sO()
      : base("SO")
    {
    }
  }

  public sealed class pO : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ControlAccountModule.pO>
  {
    public pO()
      : base("PO")
    {
    }
  }

  public sealed class fA : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ControlAccountModule.fA>
  {
    public fA()
      : base("FA")
    {
    }
  }

  public sealed class dR : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ControlAccountModule.dR>
  {
    public dR()
      : base("DR")
    {
    }
  }

  public class ListAttribute : 
    PXStringListAttribute,
    IPXFieldSelectingSubscriber,
    IPXFieldVerifyingSubscriber
  {
    protected static string[] values = new string[9]
    {
      string.Empty,
      "AP",
      "AR",
      "TX",
      "IN",
      "SO",
      "PO",
      "FA",
      "DR"
    };
    protected static string[] labels = new string[9]
    {
      string.Empty,
      "AP",
      "AR",
      "TX",
      "IN",
      "SO",
      "PO",
      "FA",
      "DR"
    };

    public ListAttribute()
      : base(ControlAccountModule.ListAttribute.values, ControlAccountModule.ListAttribute.labels)
    {
    }

    public virtual void FieldSelecting(PXCache cache, PXFieldSelectingEventArgs e)
    {
      base.FieldSelecting(cache, e);
      List<string> stringList = new List<string>()
      {
        string.Empty,
        "AP",
        "AR",
        "TX"
      };
      if (PXAccess.FeatureInstalled<FeaturesSet.fixedAsset>())
        stringList.Add("FA");
      if (PXAccess.FeatureInstalled<FeaturesSet.defferedRevenue>())
        stringList.Add("DR");
      if (PXAccess.FeatureInstalled<FeaturesSet.distributionModule>())
      {
        stringList.Add("IN");
        stringList.Add("PO");
        stringList.Add("SO");
      }
      string[] strArray1 = new string[stringList.Count];
      for (int index = 0; index < this._AllowedValues.Length; ++index)
      {
        if (stringList.Contains(this._AllowedValues[index]))
          strArray1[stringList.IndexOf(this._AllowedValues[index])] = this._AllowedLabels[index];
      }
      string[] array = stringList.ToArray();
      string[] strArray2 = strArray1;
      e.ReturnState = (object) (PXStringState) PXStringState.CreateInstance(e.ReturnState, new int?(), new bool?(), ((PXEventSubscriberAttribute) this)._FieldName, new bool?(), new int?(-1), (string) null, array, strArray2, new bool?(true), (string) null, this._NeutralAllowedLabels);
    }

    public virtual void FieldVerifying(PXCache cache, PXFieldVerifyingEventArgs e)
    {
      string newValue = (string) e.NewValue;
      if (newValue == "FA" && !PXAccess.FeatureInstalled<FeaturesSet.fixedAsset>() || newValue == "DR" && !PXAccess.FeatureInstalled<FeaturesSet.defferedRevenue>() || EnumerableExtensions.IsIn<string>(newValue, "IN", "PO", "SO") && !PXAccess.FeatureInstalled<FeaturesSet.distributionModule>())
      {
        e.NewValue = (object) null;
      }
      else
      {
        if (EnumerableExtensions.IsIn<string>(newValue, (IEnumerable<string>) ControlAccountModule.ListAttribute.values))
          return;
        e.NewValue = (object) null;
      }
    }
  }
}
