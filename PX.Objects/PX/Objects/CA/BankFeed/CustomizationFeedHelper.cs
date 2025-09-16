// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.CustomizationFeedHelper
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace PX.Objects.CA.BankFeed;

internal static class CustomizationFeedHelper
{
  internal static void DisconnectFeed(CABankFeed bankFeed)
  {
    bool flag = false;
    if (bankFeed == null || bankFeed.AccessToken == null || bankFeed.Type == null)
    {
      CustomizationFeedHelper.WriteDiconnectionWarnInTrace(bankFeed);
    }
    else
    {
      string type = bankFeed.Type;
      string accessToken = bankFeed.AccessToken;
      try
      {
        Assembly asm = ((IEnumerable<Assembly>) AppDomain.CurrentDomain.GetAssemblies()).Where<Assembly>((Func<Assembly, bool>) (i => i.FullName.Contains("EIS.BankFeed"))).FirstOrDefault<Assembly>();
        if (asm == (Assembly) null || !PXSubstManager.IsSuitableTypeExportAssembly(asm, false))
        {
          CustomizationFeedHelper.WriteDiconnectionWarnInTrace(bankFeed);
        }
        else
        {
          if (EnumerableExtensions.IsIn<string>(type, "P", "T"))
            flag = CustomizationFeedHelper.DicsonnectPlaidFeed(asm, accessToken);
          if (type == "M")
          {
            string[] source = bankFeed.AccessToken.Split(';');
            if (source.Length == 2)
            {
              string userGuid = ((IEnumerable<string>) source).Where<string>((Func<string, bool>) (i => i.StartsWith("USR"))).FirstOrDefault<string>();
              string memberGuid = ((IEnumerable<string>) source).Where<string>((Func<string, bool>) (i => i.StartsWith("MBR"))).FirstOrDefault<string>();
              flag = CustomizationFeedHelper.DicsonnectMXFeed(asm, userGuid, memberGuid);
            }
          }
          if (flag)
            return;
          CustomizationFeedHelper.WriteDiconnectionWarnInTrace(bankFeed);
        }
      }
      catch (Exception ex)
      {
        string message = ex.Message;
        if (ex.InnerException != null)
          message = ex.InnerException.Message;
        PXTrace.WriteError("Unable to disconnect the {bankFeed} bank feed: {error}.", new object[2]
        {
          (object) bankFeed.BankFeedID,
          (object) message
        });
      }
    }
  }

  private static bool DicsonnectPlaidFeed(Assembly asm, string accessToken)
  {
    bool flag = false;
    if (accessToken == null || !accessToken.StartsWith("access-production"))
      return flag;
    Type[] exportedTypes = asm.GetExportedTypes();
    Type type1 = ((IEnumerable<Type>) exportedTypes).Where<Type>((Func<Type, bool>) (i => i.Name.Contains("BankFeedsProxyAPI"))).FirstOrDefault<Type>();
    if (type1 != (Type) null)
    {
      object instance1 = Activator.CreateInstance(type1);
      instance1.GetType().GetMethod("GetToken").Invoke(instance1, (object[]) null);
      Type enumType = ((IEnumerable<Type>) exportedTypes).Where<Type>((Func<Type, bool>) (i => i.Name.Contains("Environment") && i.BaseType == typeof (Enum))).FirstOrDefault<Type>();
      Type type2 = ((IEnumerable<Type>) exportedTypes).Where<Type>((Func<Type, bool>) (i => i.Name.Contains("DeleteItemRequest"))).FirstOrDefault<Type>();
      if (enumType != (Type) null && type2 != (Type) null)
      {
        object obj = Enum.Parse(enumType, "Production");
        object instance2 = Activator.CreateInstance(type2);
        type2.GetProperty("AccessToken").SetValue(instance2, (object) accessToken, (object[]) null);
        type1.GetMethod("DeleteItem").Invoke(instance1, new object[2]
        {
          obj,
          instance2
        });
        flag = true;
      }
    }
    return flag;
  }

  private static bool DicsonnectMXFeed(Assembly asm, string userGuid, string memberGuid)
  {
    bool flag = false;
    if (userGuid == null || memberGuid == null)
      return flag;
    Type type = ((IEnumerable<Type>) asm.GetExportedTypes()).Where<Type>((Func<Type, bool>) (i => i.Name.Contains("BankFeedsProxyAPI"))).FirstOrDefault<Type>();
    if (type != (Type) null)
    {
      object instance = Activator.CreateInstance(type);
      instance.GetType().GetMethod("GetToken").Invoke(instance, (object[]) null);
      type.GetMethod("DeleteMember").Invoke(instance, new object[2]
      {
        (object) memberGuid,
        (object) userGuid
      });
      flag = true;
    }
    return flag;
  }

  private static void WriteDiconnectionWarnInTrace(CABankFeed bankFeed)
  {
    PXTrace.WriteWarning("Disconnection of the {bankfeed} bank feed was skipped.", new object[1]
    {
      (object) bankFeed.BankFeedID
    });
  }
}
