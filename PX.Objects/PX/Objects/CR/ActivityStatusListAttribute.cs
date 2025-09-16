// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.ActivityStatusListAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace PX.Objects.CR;

public class ActivityStatusListAttribute : PXStringListAttribute
{
  public const 
  #nullable disable
  string Draft = "DR";
  public const string Open = "OP";
  public const string Completed = "CD";
  public const string Approved = "AP";
  public const string Rejected = "RJ";
  public const string Canceled = "CL";
  public const string InProcess = "IP";
  public const string PendingApproval = "PA";
  public const string Released = "RL";

  public ActivityStatusListAttribute()
    : base(new string[9]
    {
      "DR",
      "OP",
      "IP",
      "CD",
      "AP",
      "RJ",
      "CL",
      "PA",
      "RL"
    }, new string[9]
    {
      nameof (Draft),
      nameof (Open),
      "Processing",
      nameof (Completed),
      nameof (Approved),
      nameof (Rejected),
      nameof (Canceled),
      "Pending Approval",
      nameof (Released)
    })
  {
    this.RestictedMode = false;
  }

  public bool RestictedMode { get; set; }

  /// <summary>Sets the restricted mode of the specified field.</summary>
  /// <param name="cache">The cache object to search for the attributes of
  /// <tt>ActivityStatusList</tt> type.</param>
  /// <param name="restictedMode">The new restricted mode value.</param>
  public static void SetRestictedMode<Field>(PXCache cache, bool restictedMode) where Field : IBqlField
  {
    cache.SetAltered<Field>(true);
    foreach (PXEventSubscriberAttribute attribute in cache.GetAttributes<Field>())
    {
      if (attribute is ActivityStatusListAttribute)
      {
        ((ActivityStatusListAttribute) attribute).RestictedMode = restictedMode;
        break;
      }
    }
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    if (!this.RestictedMode || !(e.ReturnState is PXStringState returnState))
      return;
    string[] strArray1 = this.AllowedState(sender, e.Row);
    if (strArray1 == null)
      return;
    string[] strArray2 = new string[strArray1.Length];
    for (int index1 = 0; index1 < strArray1.Length; ++index1)
    {
      int index2 = Array.IndexOf<string>(returnState.AllowedValues, strArray1[index1]);
      strArray2[index1] = returnState.AllowedLabels[index2];
    }
    returnState.AllowedValues = strArray1;
    returnState.AllowedLabels = strArray2;
  }

  protected virtual string[] AllowedState(PXCache sender, object row) => (string[]) null;

  public void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
    if (this.RestictedMode && !((IEnumerable<object>) this.AllowedState(sender, e.Row)).Contains<object>(e.NewValue))
      throw new PXSetPropertyException("Status is not valid.");
  }

  public class draft : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ActivityStatusListAttribute.draft>
  {
    public draft()
      : base("DR")
    {
    }
  }

  public class open : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ActivityStatusListAttribute.open>
  {
    public open()
      : base("OP")
    {
    }
  }

  public class completed : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ActivityStatusListAttribute.completed>
  {
    public completed()
      : base("CD")
    {
    }
  }

  public class approved : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ActivityStatusListAttribute.approved>
  {
    public approved()
      : base("AP")
    {
    }
  }

  public class rejected : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ActivityStatusListAttribute.rejected>
  {
    public rejected()
      : base("RJ")
    {
    }
  }

  public class canceled : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ActivityStatusListAttribute.canceled>
  {
    public canceled()
      : base("CL")
    {
    }
  }

  public class pendingApproval : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ActivityStatusListAttribute.pendingApproval>
  {
    public pendingApproval()
      : base("PA")
    {
    }
  }

  public class released : BqlType<
  #nullable enable
  IBqlString, string>.Constant<
  #nullable disable
  ActivityStatusListAttribute.released>
  {
    public released()
      : base("RL")
    {
    }
  }

  public class inprocess : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    ActivityStatusListAttribute.inprocess>
  {
    public inprocess()
      : base("IP")
    {
    }
  }
}
